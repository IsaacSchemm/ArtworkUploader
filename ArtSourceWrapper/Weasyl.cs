﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WeasylLib;

namespace ArtSourceWrapper {
    public abstract class WeasylIdWrapper : AsynchronousCachedEnumerable<int, int> {
        private WeasylClient _client;
        protected WeasylClient Client => _client;

        public abstract string SiteName { get; }

        public WeasylIdWrapper(string apiKey) {
            _client = new WeasylClient(apiKey);
        }

        public async Task<string> WhoamiAsync() {
            try {
                return (await Client.WhoamiAsync()).login;
            } catch (WebException e) when ((e.Response as HttpWebResponse)?.StatusCode == HttpStatusCode.Unauthorized) {
                throw new Exception("No username returned from Weasyl. The API key might be invalid or deleted.");
            }
        }

        public abstract Task<WeasylSubmissionBaseDetail> GetSubmissionDetails(int id);
    }

    public class WeasylGalleryIdWrapper : WeasylIdWrapper {
        private string _username;

        public override int BatchSize { get; set; } = 100;
        public override int MinBatchSize => 1;
        public override int MaxBatchSize => 100;

        public override string SiteName => "Weasyl (gallery)";

        public WeasylGalleryIdWrapper(string apiKey) : base(apiKey) { }

        public override async Task<WeasylSubmissionBaseDetail> GetSubmissionDetails(int submitid) {
            return await Client.GetSubmissionAsync(submitid);
        }

        /// <summary>
        /// Fetch submissions from Weasyl.
        /// </summary>
        /// <param name="startPosition">The nextid from the previous Weasyl search.</param>
        /// <param name="maxCount">The number of results to return.</param>
        protected override async Task<InternalFetchResult> InternalFetchAsync(int? startPosition, int maxCount) {
            if (_username == null) {
                _username = await WhoamiAsync();
            }

            var result = await Client.GetUserGalleryAsync(_username, nextid: startPosition, count: maxCount);

            return new InternalFetchResult(
                result.submissions.Select(s => s.submitid),
                result.nextid ?? 0,
                isEnded: result.nextid == null
            );
        }
    }

    public class WeasylCharacterWrapper : WeasylIdWrapper {
        private string _username;

        public override int BatchSize { get; set; } = 0;
        public override int MinBatchSize => 0;
        public override int MaxBatchSize => 0;

        public override string SiteName => "Weasyl (characters)";

        public WeasylCharacterWrapper(string apiKey) : base(apiKey) { }

        public override async Task<WeasylSubmissionBaseDetail> GetSubmissionDetails(int charid) {
            return await Client.GetCharacterAsync(charid);
        }

        /// <summary>
        /// Scrape the Weasyl site to load character IDs, and use the API to get information for each.
        /// </summary>
        /// <param name="startPosition">The ID of the lowest (oldest) character already downloaded.</param>
        /// <param name="maxCount">Ignored.</param>
        protected override async Task<InternalFetchResult> InternalFetchAsync(int? startPosition, int maxCount) {
            if (_username == null) {
                _username = await WhoamiAsync();
            }
            List<int> all_ids = await Client.ScrapeCharacterIdsAsync(_username);
            
            return new InternalFetchResult(
                all_ids,
                all_ids.DefaultIfEmpty(0).Min(),
                isEnded: true
            );
        }
    }

    public class WeasylWrapper : SiteWrapper<WeasylSubmissionWrapper, int> {
        private WeasylIdWrapper _idWrapper;

        public override int BatchSize { get; set; } = 1;
        public override int MinBatchSize => 1;
        public override int MaxBatchSize => 1;

        public override string SiteName => _idWrapper.SiteName;

        public WeasylWrapper(WeasylIdWrapper idWrapper) {
            _idWrapper = idWrapper;
        }

        public override Task<string> WhoamiAsync() {
            return _idWrapper.WhoamiAsync();
        }

        protected async override Task<InternalFetchResult> InternalFetchAsync(int? startPosition, int count) {
            int skip = startPosition ?? 0;

            while (_idWrapper.Cache.Count() < skip + 1 && !_idWrapper.IsEnded) {
                await _idWrapper.FetchAsync();
            }

            var task = _idWrapper.Cache
                .Skip(skip)
                .Select(id => _idWrapper.GetSubmissionDetails(id))
                .FirstOrDefault();

            var wrappers = task == null
                ? Enumerable.Empty<WeasylSubmissionWrapper>()
                : new[] { new WeasylSubmissionWrapper(await task) };

            return new InternalFetchResult(wrappers, skip + 1, _idWrapper.Cache.Count() <= skip + 1);
        }
    }

    public class WeasylSubmissionWrapper : ISubmissionWrapper {
        public string Rating => Submission.rating;
        public bool PotentiallySensitive => Rating != "general";

        public string GeneratedUniqueTag =>
            Submission is WeasylCharacterDetail ? $"#weasylcharacter{((WeasylCharacterDetail)Submission).charid}"
            : Submission is WeasylSubmissionDetail ? $"#weasyl{((WeasylSubmissionDetail)Submission).submitid}"
            : null;
        public string HTMLDescription => HtmlLinkUtils.MakeLinksAbsolute(Submission.HTMLDescription, "http://www.weasyl.com");
        public IEnumerable<string> Tags => Submission.tags;
        public DateTime Timestamp => Submission.posted_at;
        public string Title => Submission.title;

        public string ViewURL => Submission.link;
        public string ImageURL => Submission.media.submission.First().url;
        public string ThumbnailURL => Submission.media.thumbnail.First().url;

        public Color? BorderColor {
            get {
                switch (Submission.rating) {
                    case "mature":
                        return Color.FromArgb(170, 187, 34);
                    case "explicit":
                        return Color.FromArgb(185, 30, 35);
                    default:
                        return null;
                }
            }
		}

		public bool OwnWork => true;

		public WeasylSubmissionBaseDetail Submission { get; private set; }

        public WeasylSubmissionWrapper(WeasylSubmissionBaseDetail submission) {
            Submission = submission;
        }
    }
}
