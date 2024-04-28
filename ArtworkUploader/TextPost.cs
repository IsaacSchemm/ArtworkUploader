using Microsoft.FSharp.Collections;

namespace ArtworkUploader {
	public record struct TextPost(string Title, string HTMLDescription, bool Mature, bool Adult, FSharpList<string> Tags);
}
