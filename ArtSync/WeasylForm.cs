﻿using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.OAuth;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using InkbunnyLib;
using Tweetinvi.Models;
using Tweetinvi;
using System.Text.RegularExpressions;
using Tweetinvi.Parameters;
using ArtSourceWrapper;
using System.Security.Cryptography;

namespace ArtSync {
	public partial class WeasylForm : Form {
		private static Settings GlobalSettings;

		public ISiteWrapper SourceWrapper { get; private set; }
        public int WrapperPosition { get; private set; }

		private TumblrClient Tumblr;
		public string TumblrUsername { get; private set; }

		private ITwitterCredentials TwitterCredentials;
		private int shortURLLength;
		private int shortURLLengthHttps;
		private List<ITweet> tweetCache;

		private InkbunnyClient Inkbunny;

        // Stores a DeviantArtWrapper instance if the user is logged into dA; null if they are not.
        // This can be used as a source SiteWrapper or to look up whether a given submission has been uploaded to dA already.
        private DeviantArtWrapper _deviantArtWrapper;

		// Stores references to the four WeasylThumbnail controls along the side. Each of them is responsible for fetching the submission information and image.
		private WeasylThumbnail[] thumbnails;

		// The current submission's details and image, which are fetched by the WeasylThumbnail and passed to SetCurrentImage.
		private ISubmissionWrapper currentSubmission;
		private BinaryFile currentImage;

        private string GeneratedUniqueTag {
            get {
                string tag = this.currentSubmission?.GeneratedUniqueTag;
                if (string.IsNullOrEmpty(tag)) return null;
                while (tag.StartsWith("#")) tag = tag.Substring(1);
                return tag;
            }
        }

		// The image displayed in the main panel. This is used again if WeasylSync needs to add padding to the image to force a square aspect ratio.
		private Bitmap currentImageBitmap;

		// The existing Tumblr post for the selected Weasyl submission, if any - looked up by using the #weasylXXXXXX tag.
		private BasePost ExistingTumblrPost;

		// Allows WeasylThumbnail access to the progress bar.
		public LProgressBar LProgressBar {
			get {
				return lProgressBar1;
			}
		}

		public WeasylForm() {
			InitializeComponent();
			tweetCache = new List<ITweet>();

			GlobalSettings = Settings.Load();

			thumbnails = new WeasylThumbnail[] { thumbnail1, thumbnail2, thumbnail3 };

            txtSaveDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            this.Shown += (o, e) => LoadFromSettings();
		}

        private void ShowException(Exception e, string source) {
            MessageBox.Show(this, e.Message, $"Error in {source}: {e.GetType()}");
        }

		#region GUI updates
        private async Task DeviantArtLogin() {
            if (!string.IsNullOrEmpty(GlobalSettings.DeviantArt.RefreshToken)) {
                try {
                    DeviantArtWrapper.ClientId = OAuthConsumer.DeviantArt.CLIENT_ID;
                    DeviantArtWrapper.ClientSecret = OAuthConsumer.DeviantArt.CLIENT_SECRET;
                    string oldToken = GlobalSettings.DeviantArt.RefreshToken;
                    string newToken = await DeviantArtWrapper.UpdateTokens(oldToken);
                    if (oldToken != newToken) {
                        GlobalSettings.DeviantArt.RefreshToken = newToken;
                        GlobalSettings.Save();
                    }
                    _deviantArtWrapper = new DeviantArtWrapper();
                    lblDeviantArtStatus2.Text = await _deviantArtWrapper.WhoamiAsync();
                    lblDeviantArtStatus2.ForeColor = Color.DarkGreen;
                    return;
                } catch (DeviantArtException e) when (e.Message == "User canceled") {
                    GlobalSettings.DeviantArt.RefreshToken = null;
                } catch (DeviantArtException e) {
                    ShowException(e, nameof(DeviantArtLogin));
                }
            }

            lblDeviantArtStatus2.Text = "not logged in";
            lblDeviantArtStatus2.ForeColor = SystemColors.WindowText;
            _deviantArtWrapper = null;
        }

        private async Task GetNewWrapper() {
            List<ISiteWrapper> wrappers = new List<ISiteWrapper>();

            if (_deviantArtWrapper != null) {
                try {
                    await _deviantArtWrapper.WhoamiAsync();
                    wrappers.Add(_deviantArtWrapper);
                } catch (Exception e) {
                    ShowException(e, nameof(GetNewWrapper));
                }
            }

            if (!string.IsNullOrEmpty(GlobalSettings.FurAffinity.a) && !string.IsNullOrEmpty(GlobalSettings.FurAffinity.b)) {
                wrappers.Add(new FurAffinityWrapper(new FurAffinityIdWrapper(GlobalSettings.FurAffinity.a, GlobalSettings.FurAffinity.b, scraps: true)));
                wrappers.Add(new FurAffinityWrapper(new FurAffinityIdWrapper(GlobalSettings.FurAffinity.a, GlobalSettings.FurAffinity.b, scraps: false)));
            }

            if (!string.IsNullOrEmpty(GlobalSettings.Weasyl.APIKey)) {
                wrappers.Add(new WeasylWrapper(new WeasylGalleryIdWrapper(GlobalSettings.Weasyl.APIKey)));
                wrappers.Add(new WeasylWrapper(new WeasylCharacterWrapper(GlobalSettings.Weasyl.APIKey)));
            }

            if (Inkbunny != null) {
                wrappers.Add(new InkbunnyWrapper(Inkbunny));
            }

            if (TwitterCredentials != null) {
                wrappers.Add(new TwitterWrapper(TwitterCredentials));
            }

            if (Tumblr != null) {
                wrappers.Add(new TumblrWrapper(Tumblr, GlobalSettings.Tumblr.BlogName));
            }

            if (wrappers.Count == 0) {
                wrappers.Add(new EmptyWrapper());
            }

            if (wrappers.Count == 1) {
                SourceWrapper = wrappers.Single();
            } else {
                var form = new SourceChoiceForm(wrappers) {
                    SelectedWrapper = SourceWrapper ?? wrappers.First()
                };
                var dialogResult = form.ShowDialog(this);
                SourceWrapper = dialogResult == DialogResult.OK
                    ? form.SelectedWrapper
                    : new EmptyWrapper();
            }
            WrapperPosition = 0;

            lblWeasylStatus1.Text = SourceWrapper.SiteName + ":";

            string user = null;
            try {
                user = await SourceWrapper.WhoamiAsync();
                lblWeasylStatus2.Text = user ?? "not logged in";
                lblWeasylStatus2.ForeColor = string.IsNullOrEmpty(lblWeasylStatus2.Text)
                    ? SystemColors.WindowText
                    : Color.DarkGreen;
            } catch (Exception e) {
                lblWeasylStatus2.Text = ((e as WebException)?.Response as HttpWebResponse)?.StatusDescription ?? e.Message;
                lblWeasylStatus2.ForeColor = Color.DarkRed;
            }
        }

        private async Task GetNewTumblrClient() {
            Token token = GlobalSettings.TumblrToken;
            if (token != null && token.IsValid) {
                if (Tumblr != null) Tumblr.Dispose();
                Tumblr = new TumblrClientFactory().Create<TumblrClient>(
                    OAuthConsumer.Tumblr.CONSUMER_KEY,
                    OAuthConsumer.Tumblr.CONSUMER_SECRET,
                    token);
            }

            if (Tumblr == null) {
                lblTumblrStatus2.Text = "not logged in";
                lblTumblrStatus2.ForeColor = SystemColors.WindowText;
            } else {
                try {
                    TumblrUsername = (await Tumblr.GetUserInfoAsync()).Name;
                    lblTumblrStatus2.Text = TumblrUsername ?? "not logged in";
                    lblTumblrStatus2.ForeColor = TumblrUsername == null
                        ? SystemColors.WindowText
                        : Color.DarkGreen;
                } catch (Exception e) {
                    TumblrUsername = null;
                    ShowException(e, nameof(GetNewTumblrClient));
                    lblTumblrStatus2.Text = "Error";
                    lblTumblrStatus2.ForeColor = Color.DarkRed;
                }
            }
        }

        private async Task GetNewInkbunnyClient() {
            if (GlobalSettings.Inkbunny.Sid != null && GlobalSettings.Inkbunny.UserId != null) {
                Inkbunny = new InkbunnyClient(GlobalSettings.Inkbunny.Sid, GlobalSettings.Inkbunny.UserId.Value);
            }

            if (Inkbunny == null) {
                lblInkbunnyStatus2.Text = "not logged in";
                lblInkbunnyStatus2.ForeColor = SystemColors.WindowText;
            } else {
                try {
                    lblInkbunnyStatus2.Text = await Inkbunny.GetUsernameAsync();
                    lblInkbunnyStatus2.ForeColor = Color.DarkGreen;
                } catch (Exception e) {
                    Inkbunny = null;
                    lblInkbunnyStatus2.Text = e.Message;
                    lblInkbunnyStatus2.ForeColor = Color.DarkRed;
                }
            }
        }

        private async Task GetNewTwitterClient() {
            TwitterCredentials = GlobalSettings.TwitterCredentials;
            try {
                string screenName = TwitterCredentials?.GetScreenName();
                lblTwitterStatus2.Text = screenName ?? "not logged in";
                lblTwitterStatus2.ForeColor = screenName == null
                    ? SystemColors.WindowText
                    : Color.DarkGreen;

                if (screenName != null) {
                    Auth.ExecuteOperationWithCredentials(TwitterCredentials, () => {
                        if (shortURLLength == 0 || shortURLLengthHttps == 0) {
                            var conf = Tweetinvi.Help.GetTwitterConfiguration();
                            shortURLLength = conf.ShortURLLength;
                            shortURLLengthHttps = conf.ShortURLLengthHttps;
                        }
                    });

                    if (!tweetCache.Any()) {
                        tweetCache.AddRange(await GetMoreOldTweets());
                    }
                }
            } catch (Exception e) {
                TwitterCredentials = null;
                lblTwitterStatus2.Text = e.Message;
                lblTwitterStatus2.ForeColor = Color.DarkRed;
            }
        }

		private async void LoadFromSettings() {
			try {
                LProgressBar.Report(0);
				LProgressBar.Visible = true;

                var tasks = new Task[] {
                    GetNewTumblrClient(),
                    GetNewInkbunnyClient(),
                    GetNewTwitterClient(),
                    DeviantArtLogin()
                };

                int progress = 0;
                foreach (var t in tasks) {
                    var _ = t.ContinueWith(x => LProgressBar.Report(++progress / (tasks.Length + 1.0)));
                }

                await Task.WhenAll(tasks);

                await GetNewWrapper();

				LProgressBar.Visible = false;
                
				txtFooter.Text = GlobalSettings.Defaults.FooterHTML ?? "";

				// Global tags that you can include in each Tumblr submission if you want.
				txtTags2.Text = GlobalSettings.Defaults.Tags ?? "";

                UpdateGalleryAsync();
			} catch (Exception e) {
				Console.Error.WriteLine(e.Message);
				Console.Error.WriteLine(e.StackTrace);
                ShowException(e, nameof(LoadFromSettings));
            }
		}

		// This function is called after clicking on a WeasylThumbnail.
		public async Task SetCurrentImage(ISubmissionWrapper submission, BinaryFile file) {
			this.currentSubmission = submission;
			tabControl1.Enabled = submission?.OwnWork == true;

            var tags = new List<string>();

			if (submission != null) {
                txtHeader.Text = string.IsNullOrEmpty(submission.Title)
                    ? ""
                    : GlobalSettings.Defaults.HeaderHTML?.Replace("{TITLE}", submission.Title) ?? "";
                txtInkbunnyTitle.Text = submission.Title;
				txtDescription.Text = submission.HTMLDescription;
				string bbCode = HtmlToBBCode.ConvertHtml(txtDescription.Text);
				txtInkbunnyDescription.Text = bbCode;
				txtURL.Text = submission.ViewURL;

				ResetTweetText();

				lnkTwitterLinkToInclude.Text = submission.ViewURL;
                chkTweetPotentiallySensitive.Checked = submission.PotentiallySensitive;

                tags.AddRange(submission.Tags);
                if (GlobalSettings.IncludeGeneratedUniqueTag) {
                    tags.Add(submission.GeneratedUniqueTag.Replace("#", ""));
                }
				txtTags1.Text = txtInkbunnyTags.Text = string.Join(" ", tags.Select(s => "#" + s));

                pickDate.Value = pickTime.Value = submission.Timestamp;
			}
			this.currentImage = file;
            this.lnkOriginalUrl.Text = submission?.ViewURL ?? "";
			if (file == null) {
				mainPictureBox.Image = null;
                txtSaveFilename.Text = "";
            } else {
                char[] invalid = Path.GetInvalidFileNameChars();
                string basename = submission?.Title;
                if (string.IsNullOrEmpty(basename)) {
                    basename = submission?.GeneratedUniqueTag?.Replace("#", "");
                }
                if (string.IsNullOrEmpty(basename)) {
                    basename = "image";
                }
                basename = new string(basename.Select(c => invalid.Contains(c) ? '_' : c).ToArray());
                string ext = file.MimeType.StartsWith("image/")
                    ? $".{file.MimeType.Replace("image/", "")}"
                    : "";
                txtSaveFilename.Text = basename + ext;

                try {
					this.currentImageBitmap = (Bitmap)Image.FromStream(new MemoryStream(file.Data));
					mainPictureBox.Image = this.currentImageBitmap;
                } catch (ArgumentException) {
					MessageBox.Show("This submission is not an image file.");
					mainPictureBox.Image = null;
				}
            }
            
            deviantArtUploadControl1.SetSubmission(
                data: file?.Data,
                title: submission?.Title,
                htmlDescription: submission?.HTMLDescription,
                tags: tags,
                mature: submission?.PotentiallySensitive == true,
                originalUrl: submission?.ViewURL);

            try {
                UpdateExistingTweetLink();
                await Task.WhenAll(
                    UpdateExistingDeviantArtLink(),
                    UpdateExistingTumblrPostLink(),
                    UpdateExistingInkbunnyPostLink()
                );
            } catch (Exception ex) {
                MessageBox.Show(this, "Could not check for existing post on one or more sites.", ex.GetType().Name);
            }
        }

		private void ResetTweetText() {
			List<string> plainTextList = new List<string>(2);
			if (chkIncludeTitle.Checked) {
				plainTextList.Add(currentSubmission.Title);
			}
			if (chkIncludeDescription.Checked) {
				string bbCode = HtmlToBBCode.ConvertHtml(txtDescription.Text);
				plainTextList.Add(Regex.Replace(bbCode, @"\[\/?(b|i|u|q|url=?[^\]]*)\]", ""));
			}
			string plainText = string.Join("﹘", plainTextList.Where(s => s != ""));

			int maxLength = 140;
			if (chkIncludeLink.Checked) {
				maxLength -= (shortURLLengthHttps + 1);
			}

			txtTweetText.Text = plainText.Length > maxLength
				? $"{plainText.Substring(0, maxLength - 1)}…"
				: plainText;
		}

		private Task<IEnumerable<ITweet>> GetMoreOldTweets() {
			if (TwitterCredentials == null)
				return Task.FromResult(Enumerable.Empty<ITweet>());

			long sinceId = tweetCache.Select(t => t.Id).DefaultIfEmpty(20).Max();
			return Task.Run(() => Auth.ExecuteOperationWithCredentials(TwitterCredentials, () => {
				var user = User.GetAuthenticatedUser();
				var parameters = new UserTimelineParameters {
					MaximumNumberOfTweetsToRetrieve = 200,
					TrimUser = true,
					SinceId = sinceId
				};

				TweetinviConfig.CurrentThreadSettings.TweetMode = TweetMode.Extended;
				var tweets = Timeline.GetUserTimeline(user, parameters);
				if (tweets == null) {
					var x = ExceptionHandler.GetLastException();
					throw new Exception(x.TwitterDescription, x.WebException);
				}
				return tweets
					.Where(t => t.CreatedBy.Id == user.Id);
			}));
		}
        
		private async void UpdateGalleryAsync(bool back = false, bool next = false) {
			try {
                if (SourceWrapper == null) return;

                LProgressBar.Report(0);
                LProgressBar.Visible = true;

                int addedCount = this.thumbnails.Length;

                if (back) WrapperPosition -= addedCount;
                if (next) WrapperPosition += addedCount;
                if (WrapperPosition < 0) WrapperPosition = 0;

                int totalCount = WrapperPosition + addedCount;

                btnUp.Enabled = WrapperPosition > 0;
                btnDown.Enabled = true;
                
                while (true) {
                    int got = SourceWrapper.Cache.Count() - WrapperPosition;
                    int outOf = totalCount - WrapperPosition;
                    if (got >= outOf) break;

                    LProgressBar.Report((double)got / outOf);

                    int read = await SourceWrapper.FetchAsync();
                    if (read == -1) {
                        btnDown.Enabled = false;
                        break;
                    }
                }

                var slice = SourceWrapper.Cache.Skip(WrapperPosition).Take(addedCount).ToList();

                for (int i = 0; i < this.thumbnails.Length; i++) {
                    LProgressBar.Report((double)i / this.thumbnails.Length);
                    await this.thumbnails[i].SetSubmission(i < slice.Count
						? slice[i]
						: null);
				}
			} catch (Exception ex) {
                ShowException(ex, nameof(UpdateGalleryAsync));
            } finally {
                LProgressBar.Visible = false;
            }
        }
		#endregion

		#region Lookup
		private async Task UpdateExistingTumblrPostLink() {
            string tag = GeneratedUniqueTag;
            if (tag == null) return;

            if (Tumblr != null) {
                this.lnkTumblrFound.Enabled = false;
                this.lnkTumblrFound.Text = $"checking your Tumblr for tag {tag}...";
                this.ExistingTumblrPost = await this.GetTaggedPostForSubmissionAsync();
				if (this.ExistingTumblrPost == null) {
					this.lnkTumblrFound.Text = $"tag not found ({tag})";
				} else {
					this.lnkTumblrFound.Text = this.ExistingTumblrPost.Url;
                    this.lnkTumblrFound.Enabled = true;
                }
			}
        }

        private async Task UpdateExistingInkbunnyPostLink() {
            string tag = GeneratedUniqueTag;
            if (tag == null) return;

            if (Inkbunny != null) {
                this.lnkInkbunnyFound.Text = $"checking Inkbunny for keyword {tag}...";
                var existing = await Inkbunny.SearchFirstOrDefaultAsync(new InkbunnySearchParameters {
					UserId = Inkbunny.UserId,
					Text = tag
				});
                if (existing == null && this.currentImage != null) {
                    using (var m = MD5.Create()) {
                        byte[] hash = m.ComputeHash(this.currentImage.Data);
                        string hashStr = string.Join("", hash.Select(b => ((int)b).ToString("X2")));
                        this.lnkInkbunnyFound.Enabled = false;
                        this.lnkInkbunnyFound.Text = $"checking Inkbunny for MD5 hash {hashStr}...";
						existing = await Inkbunny.SearchFirstOrDefaultAsync(new InkbunnySearchParameters {
							Text = hashStr,
							Keywords = false,
							MD5 = true
						});
                    }
                }
                if (existing == null) {
                    this.lnkInkbunnyFound.Text = $"keyword not found ({tag})";
                } else {
                    this.lnkInkbunnyFound.Text = $"https://inkbunny.net/submissionview.php?id={existing.submission_id}";
                    this.lnkInkbunnyFound.Enabled = true;
                }
            }
        }

        private void UpdateExistingTweetLink() {
            string url = this.currentSubmission.ViewURL;
            foreach (var tweet in tweetCache) {
                if (tweet.Entities.Urls.Any(u => u.ExpandedURL == url)) {
                    this.lnkTwitterFound.Enabled = true;
                    this.lnkTwitterFound.Text = "https://mobile.twitter.com/twitter/status/" + tweet.IdStr;
                    return;
                }
            }
            this.lnkTwitterFound.Enabled = false;
            this.lnkTwitterFound.Text = $"Link to original not found in {tweetCache.Count} most recent tweets";
        }

        private async Task UpdateExistingDeviantArtLink() {
            this.lnkDeviantArtFound.Text = "";
            this.lnkDeviantArtFound.Enabled = false;
            this.lnkDeviantArtFindMore.Visible = false;

            string tag = GeneratedUniqueTag;
            if (tag == null) return;

            if (_deviantArtWrapper != null) {
                this.lnkDeviantArtFound.Text = $"checking DeviantArt for tag #{tag}...";

                if (!_deviantArtWrapper.Cache.Any()) {
                    await _deviantArtWrapper.FetchAsync();
                }

                string url = _deviantArtWrapper.Cache
                    .Where(d => d.Tags.Contains(tag))
                    .Select(d => d.ViewURL)
                    .FirstOrDefault();
                if (url == null) {
                    this.lnkDeviantArtFound.Text = $"tag not found in {_deviantArtWrapper.Cache.Count()} most recent submissions (#{tag})";
                    this.lnkDeviantArtFindMore.Visible = !_deviantArtWrapper.IsEnded;
                } else {
                    this.lnkDeviantArtFound.Text = url;
                    this.lnkDeviantArtFound.Enabled = true;
                }
            }
        }
        #endregion

        #region HTML compilation
        public string CompileHTML() {
			StringBuilder html = new StringBuilder();

			if (chkHeader.Checked) {
				html.Append(txtHeader.Text);
			}

			if (chkDescription.Checked) {
				html.Append(txtDescription.Text);
			}

			if (chkFooter.Checked) {
				html.Append(txtFooter.Text);
			}

			html.Replace("{URL}", txtURL.Text)
                .Replace("{SITENAME}", SourceWrapper.SiteName);

			return html.ToString();
		}

		private static string HTML_PREVIEW = @"
<html>
	<head>
	<style type='text/css'>
		body {
			font-family: ""Helvetica Neue"",""HelveticaNeue"",Helvetica,Arial,sans-serif;
			font-weight: 400;
			line-height: 1.4;
			font-size: 14px;
			font-style: normal;
			color: #444;
		}
		p {
			margin: 0 0 10px;
			padding: 0px;
			border: 0px none;
			font: inherit;
			vertical-align: baseline;
		}
		a img {
			border: 0;
		}
	</style>
</head>
	<body>{HTML}</body>
</html>";
		#endregion

		#region Tumblr
		private void CreateTumblrClient_GetNewToken() {
			Token token = TumblrKey.Obtain(OAuthConsumer.Tumblr.CONSUMER_KEY, OAuthConsumer.Tumblr.CONSUMER_SECRET);
			if (token == null) {
				return;
			} else {
				GlobalSettings.TumblrToken = token;
				GlobalSettings.Save();
				Tumblr = new TumblrClientFactory().Create<TumblrClient>(
					OAuthConsumer.Tumblr.CONSUMER_KEY,
					OAuthConsumer.Tumblr.CONSUMER_SECRET,
					token);
			}
		}

		private async Task<BasePost> GetTaggedPostForSubmissionAsync() {
            string tag = GeneratedUniqueTag;
            if (tag == null) return null;

            var r = await Tumblr.GetPostsAsync(GlobalSettings.Tumblr.BlogName, 0, 1, PostType.All, false, false, PostFilter.Html, tag);
            return r.Result.FirstOrDefault();
		}

		private async void PostToTumblr() {
			try {
				if (this.currentImage == null) {
					MessageBox.Show("No image is selected.");
					return;
				}

				if (Tumblr == null) CreateTumblrClient_GetNewToken();
				if (Tumblr == null) {
					MessageBox.Show("Posting cancelled.");
					return;
				}

                LProgressBar.Report(0);
				LProgressBar.Visible = true;

				long? updateid = null;
				if (this.ExistingTumblrPost != null) {
					DialogResult result = new PostAlreadyExistsDialog(GeneratedUniqueTag, this.ExistingTumblrPost.Url).ShowDialog();
					if (result == DialogResult.Cancel) {
						LProgressBar.Visible = false;
						return;
					} else if (result == PostAlreadyExistsDialog.Result.Replace) {
						updateid = this.ExistingTumblrPost.Id;
					}
				}

                LProgressBar.Report(0.5);

				var tags = new List<string>();
				if (chkTags1.Checked) tags.AddRange(txtTags1.Text.Replace("#", "").Split(' ').Where(s => s != ""));
				if (chkTags2.Checked) tags.AddRange(txtTags2.Text.Replace("#", "").Split(' ').Where(s => s != ""));

				BinaryFile imageToPost = GlobalSettings.Tumblr.AutoSidePadding && this.currentImageBitmap.Height > this.currentImageBitmap.Width
					? MakeSquare(this.currentImageBitmap)
					: currentImage;

				PostData post = PostData.CreatePhoto(new BinaryFile[] { imageToPost }, CompileHTML(), txtURL.Text, tags);
				post.Date = chkNow.Checked
					? (DateTimeOffset?)null
					: (pickDate.Value.Date + pickTime.Value.TimeOfDay);

				Task<PostCreationInfo> task = updateid == null
					? Tumblr.CreatePostAsync(GlobalSettings.Tumblr.BlogName, post)
					: Tumblr.EditPostAsync(GlobalSettings.Tumblr.BlogName, updateid.Value, post);
				PostCreationInfo info = await task;
				await UpdateExistingTumblrPostLink();
			} catch (Exception e) {
				Console.Error.WriteLine(e.Message);
				Console.Error.WriteLine(e.StackTrace);
				var messages = (e as AggregateException)?.InnerExceptions?.Select(x => x.Message) ?? new string[] { e.Message };
				MessageBox.Show("An error occured: \"" + string.Join(", ", messages) + "\"\r\nCheck to see if the blog name is correct.");
			} finally {
				LProgressBar.Visible = false;
			}
		}
		#endregion

		#region Inkbunny
		public async void PostToInkbunny() {
			try {
				if (this.currentImage == null) {
					MessageBox.Show("No image is selected.");
					return;
				}

				if (Inkbunny == null) {
					MessageBox.Show("You must log into Inkbunny before posting.");
					return;
				}

				var rating = new List<InkbunnyRatingTag>();
				if (chkInbunnyTag2.Checked) rating.Add(InkbunnyRatingTag.Nudity);
				if (chkInbunnyTag3.Checked) rating.Add(InkbunnyRatingTag.Violence);
				if (chkInbunnyTag4.Checked) rating.Add(InkbunnyRatingTag.SexualThemes);
				if (chkInbunnyTag5.Checked) rating.Add(InkbunnyRatingTag.StrongViolence);
				if (currentSubmission.PotentiallySensitive && !rating.Any()) {
					DialogResult r = MessageBox.Show(this, $"This image has a non-general rating on the source site. Are you sure you want to post it on Inkbunny without any ratings?", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
					if (r != DialogResult.OK) return;
				}

                LProgressBar.Report(0);
                LProgressBar.Visible = true;

				long submission_id = await Inkbunny.UploadAsync(files: new byte[][] {
					currentImage.Data
				});

                LProgressBar.Report(0.5);

                var keywords = txtInkbunnyTags.Text.Replace("#", "").Split(' ').Where(s => s != "").ToList();

                if (txtInkbunnyTitle.Text == "") {
                    MessageBox.Show("Please enter a title for this submission to use on Inkbunny.");
                } else {
                    var o = await Inkbunny.EditSubmissionAsync(
                        submission_id: submission_id,
                        title: txtInkbunnyTitle.Text,
                        desc: txtInkbunnyDescription.Text,
                        convert_html_entities: true,
                        type: InkbunnySubmissionType.Picture,
                        scraps: chkInkbunnyScraps.Checked,
                        isPublic: chkInkbunnyPublic.Checked,
                        notifyWatchersWhenPublic: chkInkbunnyNotifyWatchers.Checked,
                        keywords: keywords,
                        tag: rating
                    );
                    await UpdateExistingInkbunnyPostLink();
                }
            } catch (Exception ex) {
				Console.Error.WriteLine(ex.Message);
				Console.Error.WriteLine(ex.StackTrace);
                ShowException(ex, nameof(PostToInkbunny));
            } finally {
				LProgressBar.Visible = false;
			}
		}
		#endregion

		#region Event handlers
		private void btnUp_Click(object sender, EventArgs e) {
            try {
                UpdateGalleryAsync(back: true);
            } catch (Exception ex) {
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine(ex.StackTrace);
                ShowException(ex, nameof(btnUp_Click));
            }
		}

		private void btnDown_Click(object sender, EventArgs e) {
			UpdateGalleryAsync(next: true);
		}

		private void chkNow_CheckedChanged(object sender, EventArgs e) {
			pickDate.Visible = pickTime.Visible = !chkNow.Checked;
		}

		private void btnPost_Click(object sender, EventArgs args) {
			PostToTumblr();
		}

		private void chkTitle_CheckedChanged(object sender, EventArgs e) {
			txtHeader.Enabled = chkHeader.Checked;
		}

		private void chkDescription_CheckedChanged(object sender, EventArgs e) {
			txtDescription.Enabled = chkDescription.Checked;
		}

		private void chkFooter_CheckedChanged(object sender, EventArgs e) {
			txtFooter.Enabled = chkFooter.Checked;
			txtURL.Enabled = chkFooter.Checked;
		}

		private void chkTags1_CheckedChanged(object sender, EventArgs e) {
			txtTags1.Enabled = chkTags1.Checked;
		}

		private void chkTags2_CheckedChanged(object sender, EventArgs e) {
			txtTags2.Enabled = chkTags2.Checked;
        }

        private async void changeSourceToolStripMenuItem_Click(object sender, EventArgs e) {
            try {
                await GetNewWrapper();
            } catch (Exception ex) {
                ShowException(ex, nameof(changeSourceToolStripMenuItem_Click));
            }
            UpdateGalleryAsync();
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e) {
            WrapperPosition = 0;
            UpdateGalleryAsync();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs args) {
			using (SettingsDialog dialog = new SettingsDialog(GlobalSettings)) {
				if (dialog.ShowDialog() != DialogResult.Cancel) {
					GlobalSettings = dialog.Settings;
					GlobalSettings.Save();
					LoadFromSettings();
				}
            }
		}

		private void chkHTMLPreview_CheckedChanged(object sender, EventArgs e) {
            using (var form = new Form()) {
                form.Width = 400;
                form.Height = 300;
                var browser = new WebBrowser {
                    Dock = DockStyle.Fill
                };
                form.Controls.Add(browser);
                form.Load += (x, y) => {
                    browser.Navigate("about:blank");
                    browser.Document.Write(HTML_PREVIEW.Replace("{HTML}", CompileHTML()));
                };
                form.ShowDialog(this);
            }
        }

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e) {
			using (var d = new AboutDialog()) d.ShowDialog(this);
        }

        private void lnkOriginalUrl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (lnkOriginalUrl.Text.StartsWith("http"))
                Process.Start(lnkOriginalUrl.Text);
        }

        private void lnkTumblrFound_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (lnkTumblrFound.Text.StartsWith("http"))
                Process.Start(lnkTumblrFound.Text);
        }

        private void lnkInkbunnyFound_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (lnkInkbunnyFound.Text.StartsWith("http"))
                Process.Start(lnkInkbunnyFound.Text);
        }

        private void lnkTwitterFound_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (lnkTwitterFound.Text.StartsWith("http"))
                Process.Start(lnkTwitterFound.Text);
        }

        private void lnkDeviantArtFound_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            if (lnkDeviantArtFound.Text.StartsWith("http"))
                Process.Start(lnkDeviantArtFound.Text);
        }

        private async void lnkDeviantArtFindMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            try {
                if (_deviantArtWrapper != null && !_deviantArtWrapper.IsEnded) {
                    await _deviantArtWrapper.FetchAsync();
                    await UpdateExistingDeviantArtLink();
                }
            } catch (Exception ex) {
                ShowException(ex, nameof(lnkDeviantArtFindMore_LinkClicked));
            }
        }

        private void lnkTwitterLinkToInclude_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			Process.Start(lnkTwitterLinkToInclude.Text);
		}

		private void btnInkbunnyPost_Click(object sender, EventArgs e) {
			PostToInkbunny();
		}

		private void chkInkbunnyPublic_CheckedChanged(object sender, EventArgs e) {
			chkInkbunnyNotifyWatchers.Enabled = chkInkbunnyPublic.Checked;
		}

		private void txtTweetText_TextChanged(object sender, EventArgs e) {
			int length = txtTweetText.Text.Where(c => !char.IsLowSurrogate(c)).Count();
			if (chkIncludeLink.Checked) {
				length += (shortURLLengthHttps + 1);
			}
			lblTweetLength.Text = $"{length}/140";
		}

		private void chkIncludeLink_CheckedChanged(object sender, EventArgs e) {
			ResetTweetText();
		}

		private void btnTweet_Click(object sender, EventArgs e) {
			if (TwitterCredentials == null) {
				MessageBox.Show("You must log into Twitter from the Options screen to send a tweet.");
				return;
			}

			string text = txtTweetText.Text;

			int length = text.Where(c => !char.IsLowSurrogate(c)).Count();
			if (chkIncludeLink.Checked) {
				text += $" {lnkTwitterLinkToInclude.Text}";
				length += (shortURLLengthHttps + 1);
			}
			if (length > 140) {
				MessageBox.Show("This tweet is over 140 characters. Please shorten it or remove the Weasyl link (if present.)");
				return;
			}

            LProgressBar.Report(0);
            LProgressBar.Visible = true;
			Task.Run(() => Auth.ExecuteOperationWithCredentials(TwitterCredentials, () => {
                try {
                    var options = new PublishTweetOptionalParameters();

                    if (chkIncludeImage.Checked) {
                        IMedia media = Upload.UploadImage(currentImage.Data);
                        options.Medias = new List<IMedia> { media };
                    }
                    LProgressBar.Report(0.5);

                    if (chkTweetPotentiallySensitive.Checked) {
                        options.PossiblySensitive = true;
                    }

                    ITweet tweet = Tweet.PublishTweet(text, options);

                    if (tweet == null) {
                        string desc = ExceptionHandler.GetLastException().TwitterDescription;
                        MessageBox.Show(this, desc, "Could not send tweet");
                    } else {
                        this.tweetCache.Add(tweet);
                        UpdateExistingTweetLink();
                    }
                } catch (Exception ex) {
                    ShowException(ex, nameof(btnTweet_Click));
                }
				LProgressBar.Visible = false;
			}));
		}

		private void chkIncludeTitle_CheckedChanged(object sender, EventArgs e) {
			ResetTweetText();
		}

		private void chkIncludeDescription_CheckedChanged(object sender, EventArgs e) {
			ResetTweetText();
		}

		private void chkIncludeTag_CheckedChanged(object sender, EventArgs e) {
			ResetTweetText();
		}
        
        private void btnSaveDirBrowse_Click(object sender, EventArgs e) {
            using (var dialog = new FolderBrowserDialog()) {
                dialog.SelectedPath = txtSaveDir.Text;
                if (dialog.ShowDialog(this) == DialogResult.OK) {
                    txtSaveDir.Text = dialog.SelectedPath;
                }
            }
		}

		private void btnSaveLocal_Click(object sender, EventArgs e) {
            string path = Path.Combine(txtSaveDir.Text, txtSaveFilename.Text);
            if (File.Exists(path)) {
                var result = MessageBox.Show(this, $"The file {txtSaveFilename.Text} already exists. Would you like to overwrite it?", "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;
            }

            File.WriteAllBytes(path, this.currentImage.Data);
        }

        private void deviantArtUploadControl1_UploadProgressChanged(double percentage) {
            LProgressBar.Visible = true;
            LProgressBar.Report(percentage);
        }

        private void deviantArtUploadControl1_Uploaded(string url) {
            lnkDeviantArtFound.Text = url;
            lnkDeviantArtFound.Enabled = true;
            lnkDeviantArtFindMore.Visible = false;
            _deviantArtWrapper.Clear();
            LProgressBar.Visible = false;
        }

        private void deviantArtUploadControl1_UploadError(Exception ex) {
            LProgressBar.Visible = false;
        }
        #endregion

        private static BinaryFile MakeSquare(Bitmap oldBitmap) {
			int newSize = Math.Max(oldBitmap.Width, oldBitmap.Height);
			Bitmap newBitmap = new Bitmap(newSize, newSize);

			int offsetX = (newSize - oldBitmap.Width) / 2;
			int offsetY = (newSize - oldBitmap.Height) / 2;

			using (Graphics g = Graphics.FromImage(newBitmap)) {
				g.DrawImage(oldBitmap, offsetX, offsetY, oldBitmap.Width, oldBitmap.Height);
			}

			using (MemoryStream stream = new MemoryStream()) {
				newBitmap.Save(stream, oldBitmap.RawFormat);
				return new BinaryFile(stream.ToArray());
			}
		}
    }
}
