using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Common;
namespace Mcv.Plugin.MainViewPlugin
{
    interface IMcvCommentViewModel : INotifyPropertyChanged
    {

        //ICommentProvider CommentProvider { get; }
        //IConnectionStatus ConnectionName { get; }
        ConnectionNameViewModel ConnectionName { get; }
        FontFamily FontFamily { get; }
        int FontSize { get; }
        FontStyle FontStyle { get; }
        FontWeight FontWeight { get; }
        SolidColorBrush Background { get; }
        SolidColorBrush Foreground { get; }
        string Id { get; }
        string Info { get; }
        bool IsVisible { get; }
        IEnumerable<IMessagePart> MessageItems { get; set; }
        IEnumerable<IMessagePart> NameItems { get; }
        string PostTime { get; }
        IMessageImage Thumbnail { get; }
        string UserId { get; }
        TextWrapping UserNameWrapping { get; }
        bool IsTranslated { get; set; }

        Task AfterCommentAdded();
    }
    public enum MessageType
    {
        Unknown,
        Comment,
        BroadcastInfo,
        SystemInfo,
    }
    /// <summary>
    /// 情報の種類。
    /// デバッグ情報や軽微なエラー情報が必要無い場合があるため分類する。
    /// </summary>
    /// <remarks>大小比較ができるように</remarks>
    public enum InfoType
    {
        /// <summary>
        /// 無し
        /// </summary>
        None,
        /// <summary>
        /// 致命的なエラーがあった場合だけ。必要最小限の情報
        /// </summary>
        Error,
        /// <summary>
        /// 
        /// </summary>
        Notice,
        /// <summary>
        /// 例外全て
        /// </summary>
        Debug,
    }
    //public interface ICommentViewModel : INotifyPropertyChanged
    //{
    //    MessageType MessageType { get; }
    //    IEnumerable<IMessagePart> NameItems { get; }
    //    IEnumerable<IMessagePart> MessageItems { get; }
    //    string Info { get; }
    //    string Id { get; }
    //    string UserId { get; }
    //    //IUser User { get; }
    //    bool Is184 { get; }
    //    string PostTime { get; }

    //    /// <summary>
    //    /// このユーザの最初のコメント
    //    /// </summary>
    //    //bool IsFirstComment { get; }

    //    IMessageImage Thumbnail { get; }

    //    FontFamily FontFamily { get; }
    //    FontStyle FontStyle { get; }
    //    FontWeight FontWeight { get; }
    //    int FontSize { get; }

    //    SolidColorBrush Foreground { get; }
    //    SolidColorBrush Background { get; }

    //    bool IsVisible { get; }

    //    bool IsFirstComment { get; }

    //    Task AfterCommentAdded();
    //}
}