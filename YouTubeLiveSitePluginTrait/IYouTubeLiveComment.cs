using ryu_s.YouTubeLive.Message;
using System.Collections.Generic;

namespace Mcv.YouTubeLive
{
    public interface IYouTubeLiveComment : IYouTubeLiveMessage
    {
        IReadOnlyList<IMessagePart> MessageItems { get; }
        string AuthorName { get; }
        IReadOnlyList<IBadge> AuthorBadges { get; }
        string AuthorChannelId { get; }
        string Id { get; }
        long TimeStampUsec { get; }
    }
    //public interface IMessagePart { }
    //public interface ITextPart : IMessagePart
    //{
    //    string Raw { get; }
    //}
    //public class TextPart : ITextPart
    //{
    //    public TextPart(string raw)
    //    {
    //        Raw = raw;
    //    }

    //    public string Raw { get; }
    //    public override bool Equals(object? obj)
    //    {
    //        if (obj is not TextPart b)
    //        {
    //            return false;
    //        }
    //        return Raw == b.Raw;
    //    }
    //    public override int GetHashCode()
    //    {
    //        return Raw.GetHashCode();
    //    }
    //}

    //public class CustomEmojiPart : IMessagePart
    //{
    //    public CustomEmojiPart(string url, int width, int height, string tooltip)
    //    {
    //        Url = url;
    //        Width = width;
    //        Height = height;
    //        Tooltip = tooltip;
    //    }

    //    public string Url { get; }
    //    public int Width { get; }
    //    public int Height { get; }
    //    public string Tooltip { get; }
    //}
    //public class EmojiPart : IMessagePart
    //{
    //    public string EmojiId { get; }
    //    public string Url { get; }
    //    public EmojiPart(string emojiId, string url)
    //    {
    //        EmojiId = emojiId;
    //        Url = url;
    //    }
    //}

    //public interface IBadge { }
    //public class RemoteIconPart : IBadge
    //{
    //    public string Url { get; }
    //    public int Width { get; }
    //    public int Height { get; }
    //    public string Alt { get; }
    //    public RemoteIconPart(string url, int width, int height, string alt)
    //    {
    //        Url = url;
    //        Width = width;
    //        Height = height;
    //        Alt = alt;
    //    }
    //    public override bool Equals(object? obj)
    //    {
    //        if (obj is not RemoteIconPart icon)
    //        {
    //            return false;
    //        }
    //        return Url == icon.Url && Width == icon.Width && Height == icon.Height && Alt == icon.Alt;
    //    }
    //    public override int GetHashCode()
    //    {
    //        return Url.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode() ^ Alt.GetHashCode();
    //    }
    //}
    //public class SvgIconPart : IBadge
    //{
    //    public string Data { get; }
    //    public int Width { get; }
    //    public int Height { get; }
    //    public string Alt { get; }
    //    public SvgIconPart(string data, int width, int height, string alt)
    //    {
    //        Data = data;
    //        Width = width;
    //        Height = height;
    //        Alt = alt;
    //    }
    //    public override bool Equals(object? obj)
    //    {
    //        if(obj is not SvgIconPart icon)
    //        {
    //            return false;
    //        }
    //        return Data==icon.Data && Width == icon.Width && Height == icon.Height && Alt == icon.Alt;
    //    }
    //    public override int GetHashCode()
    //    {
    //        return Data.GetHashCode() ^ Width.GetHashCode() ^ Height.GetHashCode() ^ Alt.GetHashCode();
    //    }
    //}
}
