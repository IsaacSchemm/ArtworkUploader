CrosspostSharp 3.3
==================

Source: https://github.com/libertyernie/CrosspostSharp

--------------------

CrosspostSharp is a Windows desktop application that loads individual
submissions from art sites and lets you post them to your other
accounts, maintaining the title, description, and tags.

The application is written primarily in C#. The SourceWrappers library (which
wraps the libraries used to get existing submissions from sites) is mostly
written in F# (except the Twitter wrapper, which is in C#.)

Only visual submissions (artwork) are supported at this time.

Requirements:

* Internet Explorer 11
* .NET Framework 4.6.1 or higher

Supported Sites
---------------

Use the Tools menu to add and remove accounts. Multiple accounts per site are
supported on all sites except DeviantArt and Sta.sh.

On Tumblr and Furry Network, CrosspostSharp lets you use read from and post to
multiple blogs/characters using a single account.

* DeviantArt / Sta.sh
* Flickr
* FurAffinity (read-only)
* Furry Network
* Inkbunny
* Pixiv (read-only)
* Tumblr
* Twitter
* Weasyl

You can also use a Media RSS feed as a source or open or save local files.

File Formats
------------

CrosspostSharp can read PNG, JPEG, and GIF image formats, either from the web
or from a file (with File > Open.)

CrosspostSharp can also open and save .cps files. These are JSON files that
contain encoded image data and metadata. The .cps file format looks like this:

	{
		"data": "[base 64 encoded image data]",
		"title": "text string",
		"description": "html string",
		"url": "text string",
		"tags": ["tag1", "tag2"],
		"mature": false,
		"adult": false
	}

Credentials
-----------

Credentials are stored in the file CrosspostSharp.json. This includes tokens
are used to give CrosspostSharp access to your accounts. Make sure you keep
this file safe! (Note that for Pixiv, your actual password will be stored in
plaintext.)

Compiling from Source
---------------------

This project can be built with Visual Studio 2015 or 2017. When you clone the
Git repository, make sure you do it recursively (or you can manually clone all
the submodules.)

The file OAuthConsumer.cs is missing from the CrosspostSharp3 project. Get your own
OAuth keys, then put the following into OAuthConsumer.cs:

    namespace CrosspostSharp {
        public static class OAuthConsumer {
            public static class Tumblr {
                public static string CONSUMER_KEY = "consumer key goes here";
                public static string CONSUMER_SECRET = "secret key goes here";
            }
            public static class Twitter {
                public static string CONSUMER_KEY = "consumer key goes here";
                public static string CONSUMER_SECRET = "secret key goes here";
            }
            public static class DeviantArt {
                public static string CLIENT_ID = "client_id goes here";
                public static string CLIENT_SECRET = "client_secret goes here";
            }
            public static class Flickr {
                public static string KEY = "consumer key goes here";
                public static string SECRET = "secret key goes here";
            }
        }
    }
