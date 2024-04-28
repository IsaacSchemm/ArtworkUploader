﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArtworkSourceSpecification {
	public interface IAuthor {
		string Name { get; }
		string IconUrl { get; }
	}

	public interface IPostBase {
		string Title { get; }
		string HTMLDescription { get; }
		bool Mature { get; }
		bool Adult { get; }
		IEnumerable<string> Tags { get; }
		DateTime Timestamp { get; }
		string ViewURL { get; }
	}

	public interface IRemotePhotoPost : IPostBase {
		string ThumbnailURL { get; }
		string ImageURL { get; }
	}

	public interface IDownloadedData : DeviantArtFs.Api.Stash.IFormFile { }
}
