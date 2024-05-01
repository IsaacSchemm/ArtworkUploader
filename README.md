Artwork Uploader 6.0
====================

Source: https://github.com/IsaacSchemm/ArtworkUploader

------------------

Artwork Uploader is a Windows desktop application that lets you post artwork
and journal entries to DeviantArt, Fur Affinity, and Weasyl.

Artwork Uploader does not support:

* Viewing posts
* Editing posts
* Deleting posts
* Other submission types (literary, character, status update, etc.)

To run Artwork Uploader, you'll need the .NET 8.0 and Visual C++ 2022 runtimes
on 64-bit Windows. (A small amount of C++ code is used to manage browser
cookies in the embedded web browser.)

Supported Sites
---------------

Use the Tools menu to add and remove accounts.

* DeviantArt (uses [DeviantArtFs](https://github.com/IsaacSchemm/DeviantArtFs))
* Fur Affinity (uses [FurAffinityFs](https://github.com/IsaacSchemm/FurAffinityFs))
* Weasyl

The Weasyl uploader also supports setting alt text for [Crowmask](https://github.com/IsaacSchemm/Crowmask).

Credentials
-----------

Credentials are stored in the file ArtworkUploader.json. This includes tokens
that give Artwork Uploader access to your accounts. Make sure you keep
this file safe!

Compiling from Source
---------------------

This project can be built with Visual Studio 2022.

To include DeviantArt support, edit the file DeviantArtAppCredentials.cs:

	using DeviantArtFs;

	namespace ArtworkUploader {
		public static class DeviantArtAppCredentials {
			public static DeviantArtApp AppCredentials =>
				new DeviantArtApp("client_id", "client_secret");
		}
	}
