using Common;
using Mcv.A17Live;
//using YtMessage = ryu_s.YouTubeLive.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Mcv.Plugin.MainViewPlugin.Site.A17Live;
class A17LiveCommentViewModel : IMcvCommentViewModel
{
    public A17LiveCommentViewModel(IA17LiveComment comment, ConnectionNameViewModel nameVm, IAdapter adapter)
    {
        ConnectionName = nameVm;
        NameItems = Common.MessagePartFactory.CreateMessageItems("name");
        MessageItems = Common.MessagePartFactory.CreateMessageItems(comment.Message);
        UserId = "";
        Id = "";
        PostTime = "";
        Info = "";
        FontFamily = new FontFamily("Meiryo");
        FontSize = 12;
        FontWeight = FontWeights.Normal;
        FontStyle = FontStyles.Normal;
        IsVisible = true;
        Background = new SolidColorBrush(Colors.White);
        Foreground = new SolidColorBrush(Colors.Black);
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
