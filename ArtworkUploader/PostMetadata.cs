using Microsoft.FSharp.Collections;

namespace ArtworkUploader {
	public record struct PostMetadata(string Title, string HTMLDescription, bool Mature, bool Adult, FSharpList<string> Tags);
}
