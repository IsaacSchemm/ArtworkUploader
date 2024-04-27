using DeviantArtFs.Api;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ArtworkUploader {
	public class PostImage : Stash.IFormFile {
		public string Filename { get; }
		public ReadOnlyMemory<byte> Data { get; }
		public Image Image { get; }

		public PostImage(string path) {
			Filename = Path.GetFileName(path);

			byte[] data = File.ReadAllBytes(path);

			Data = data;

			using var ms = new MemoryStream(data, false);
			Image = Image.FromStream(ms);
		}

		public string ContentType {
			get {
				return Image.RawFormat.Guid == ImageFormat.Png.Guid ? "image/png"
					: Image.RawFormat.Guid == ImageFormat.Jpeg.Guid ? "image/jpeg"
					: Image.RawFormat.Guid == ImageFormat.Gif.Guid ? "image/gif"
					: "application/octet-stream";
			}
		}

		public byte[] Read() {
			return Data.ToArray();
		}

		byte[] Stash.IFormFile.Data => Read();
	}
}
