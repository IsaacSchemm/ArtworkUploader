using DeviantArtFs.Api;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ArtworkUploader {
	public record LocalFile(string Filename) : Stash.IFormFile {
		string Stash.IFormFile.ContentType {
			get {
				using var image = Image.FromFile(Filename);
				return image.RawFormat.Guid == ImageFormat.Png.Guid ? "image/png"
					: image.RawFormat.Guid == ImageFormat.Jpeg.Guid ? "image/jpeg"
					: image.RawFormat.Guid == ImageFormat.Gif.Guid ? "image/gif"
					: "application/octet-stream";
			}
		}

		byte[] Stash.IFormFile.Data => File.ReadAllBytes(Filename);
	}
}
