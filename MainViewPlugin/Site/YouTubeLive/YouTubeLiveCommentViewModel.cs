using Common;
using Mcv.YouTubeLive;
using YtMessage = ryu_s.YouTubeLive.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Mcv.Plugin.MainViewPlugin.Site.YouTubeLive;

class YouTubeLiveCommentViewModel : IMcvCommentViewModel
{
    public YouTubeLiveCommentViewModel(IYouTubeLiveComment comment, ConnectionNameViewModel nameVm, IAdapter adapter)
    {
        ConnectionName = nameVm;
        NameItems = Common.MessagePartFactory.CreateMessageItems(comment.AuthorName);
        MessageItems = Parse(comment.MessageItems);
        UserId = comment.AuthorChannelId;
        Id = comment.Id;
        PostTime = comment.TimeStampUsec.ToString();
        FontFamily = new FontFamily("Meiryo");
        FontSize = 12;
        FontWeight = FontWeights.Normal;
        FontStyle = FontStyles.Normal;
        IsVisible = true;
        Background = new SolidColorBrush(Colors.White);
        Foreground = new SolidColorBrush(Colors.Black);
    }
    private static IEnumerable<IMessagePart> Parse(IReadOnlyList<YtMessage.IMessagePart> parts)
    {
        foreach (var part in parts)
        {
            switch (part)
            {
                case YtMessage.ITextPart text:
                    yield return new MessageTextImpl(text.Raw);
                    break;
                case YtMessage.CustomEmojiPart customEmoji:
                    yield return new MessageImage(customEmoji.Url, customEmoji.Tooltip, customEmoji.Width, customEmoji.Height);
                    break;
                case YtMessage.EmojiPart emoji:
                    yield return new RemoteSvg(emoji.Url, emoji.EmojiId, 16, 16);
                    break;
                default:
                    break;
            }
        }
        yield break;
    }

    public ConnectionNameViewModel ConnectionName { get; }
    public FontFamily FontFamily { get; }
    public int FontSize { get; }
    public FontStyle FontStyle { get; }
    public FontWeight FontWeight { get; }
    public SolidColorBrush Background { get; }
    public SolidColorBrush Foreground { get; }
    public string Id { get; }
    public string Info { get; }
    public bool IsVisible { get; }
    public IEnumerable<IMessagePart> MessageItems { get; set; }
    public IEnumerable<IMessagePart> NameItems { get; }
    public string PostTime { get; }
    public IMessageImage Thumbnail { get; }
    public string UserId { get; }
    public TextWrapping UserNameWrapping { get; }
    public bool IsTranslated { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    public Task AfterCommentAdded()
    {
        throw new NotImplementedException();
    }
}