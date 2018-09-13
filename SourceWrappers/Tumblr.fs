﻿namespace SourceWrappers

open DontPanic.TumblrSharp.Client
open DontPanic.TumblrSharp

[<AbstractClass>]
type TumblrPostWrapper<'T when 'T :> BasePost>(client: TumblrClient, post: BasePost) =
    abstract member HTMLDescription: string with get

    interface IPostBase with
        member this.Title = ""
        member this.HTMLDescription = this.HTMLDescription
        member this.Mature = false
        member this.Adult = false
        member this.Tags = post.Tags :> seq<string>
        member this.Timestamp = post.Timestamp
        member this.ViewURL = post.Url

    interface IDeletable with
        member this.SiteName = "Tumblr"
        member this.DeleteAsync () = client.DeletePostAsync(post.BlogName, post.Id)

type TumblrPhotoPostWrapper(client: TumblrClient, post: PhotoPost) =
    inherit TumblrPostWrapper<PhotoPost>(client, post)
    override __.HTMLDescription = post.Caption

    interface IRemotePhotoPost with
        member __.ImageURL = post.Photo.OriginalSize.ImageUrl
        member __.ThumbnailURL =
            post.Photo.AlternateSizes
            |> Seq.sortBy (fun s -> s.Width)
            |> Seq.filter (fun s -> s.Width >= 120 && s.Height >= 120)
            |> Seq.map (fun s -> s.ImageUrl)
            |> Seq.append (Seq.singleton post.Photo.OriginalSize.ImageUrl)
            |> Seq.head

type TumblrTextPostWrapper(client: TumblrClient, post: TextPost) =
    inherit TumblrPostWrapper<TextPost>(client, post)
    override __.HTMLDescription = post.Body

type TumblrSourceWrapper(client: TumblrClient, blogName: string, photosOnly: bool) =
    inherit SourceWrapper<int64>()

    let mutable blogNames: seq<string> = null

    override this.Name =
        if photosOnly then
            "Tumblr (photos)"
        else
            "Tumblr (text + photos)"
    
    override this.SuggestedBatchSize = 20

    override this.Fetch cursor take = async {
        if isNull blogNames then
            let! user = client.GetUserInfoAsync() |> Async.AwaitTask
            blogNames <- user.Blogs |> Seq.map (fun b -> b.Name)

        let t = if photosOnly then PostType.Photo else PostType.All

        let skip = cursor |> Option.defaultValue (int64 0)

        let! posts =
            client.GetPostsAsync(
                blogName,
                skip,
                take,
                t,
                true) |> Async.AwaitTask
                
        let wrapped = seq {
            for post in posts.Result do
                let postBlogName =
                    if not (isNull post.RebloggedRootName)
                        then post.RebloggedRootName
                        else post.BlogName
                if blogNames |> Seq.contains postBlogName then
                    match post with
                    | :? PhotoPost as photo -> yield TumblrPhotoPostWrapper(client, photo) :> IPostBase
                    | :? TextPost as text -> yield TumblrTextPostWrapper(client, text) :> IPostBase
                    | _ -> ()
        }
        return {
            Posts = wrapped
            Next = skip + (int64 take)
            HasMore = posts.Result.Length > 0
        }
    }

    override this.Whoami = async {
        return blogName
    }

    override this.GetUserIcon size = async {
        let blogHostname =
            if blogName.Contains(".") then
                sprintf "%s.tumblr.com" blogName
            else
                blogName
        return sprintf "https://api.tumblr.com/v2/blog/%s/avatar/%d" blogHostname size
    }