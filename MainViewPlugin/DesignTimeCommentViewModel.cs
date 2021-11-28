using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Mcv.Plugin.MainViewPlugin
{
    class DesignTimeCommentViewModel : IMcvCommentViewModel
    {
        public DesignTimeCommentViewModel(ConnectionNameViewModel name, IEnumerable<Common.IMessagePart> messageItems)
        {
            ConnectionName = name;
            MessageItems = messageItems;
        }
        public SolidColorBrush Background { get; } = new SolidColorBrush(Colors.White);
        public ConnectionNameViewModel ConnectionName { get; }
        public FontFamily FontFamily { get; } = new FontFamily("Meiryo");
        public int FontSize { get; } = 10;
        public FontStyle FontStyle { get; } = FontStyles.Normal;
        public FontWeight FontWeight { get; } = FontWeights.Normal;
        public SolidColorBrush Foreground { get; } = new SolidColorBrush(Colors.Black);
        public string Id { get; } = "";
        public string Info { get; } = "";
        public bool IsVisible { get; } = true;
        public IEnumerable<Common.IMessagePart> MessageItems { get; set; } = new List<Common.IMessagePart>();
        public IEnumerable<Common.IMessagePart> NameItems { get; } = new List<Common.IMessagePart>();
        public string PostTime { get; } = "";
        public Common.IMessageImage Thumbnail { get; } = null;
        public string UserId { get; } = "";
        public TextWrapping UserNameWrapping { get; } = TextWrapping.Wrap;
        public bool IsTranslated { get; set; } = false;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Task AfterCommentAdded()
        {
            throw new NotImplementedException();
        }
    }
}

