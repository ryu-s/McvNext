//using Mcv.Core;
//using Mcv.Core.SiteMessage;
using Plugin;
using Plugin.Message.ToPlugin;
using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace Mcv.Plugin.MainViewPlugin
{
    interface IAdapter
    {
        Color ButtonBackColor { get; set; }
        Color ButtonBorderColor { get; set; }
        Color ButtonForeColor { get; set; }
        int CommentIdDisplayIndex { get; set; }
        double CommentIdWidth { get; set; }
        Color CommentListBackColor { get; set; }
        Color CommentListBorderColor { get; set; }
        Color CommentListHeaderBackColor { get; set; }
        Color CommentListHeaderBorderColor { get; set; }
        Color CommentListHeaderForeColor { get; set; }
        Color CommentListSeparatorColor { get; set; }
        Color ConnectionListRowBackColor { get; set; }
        int ConnectionNameDisplayIndex { get; set; }
        double ConnectionNameWidth { get; set; }
        int ConnectionsViewBrowserDisplayIndex { get; set; }
        double ConnectionsViewBrowserWidth { get; set; }
        int ConnectionsViewConnectionBackgroundDisplayIndex { get; set; }
        double ConnectionsViewConnectionBackgroundWidth { get; set; }
        int ConnectionsViewConnectionDisplayIndex { get; set; }
        int ConnectionsViewConnectionForegroundDisplayIndex { get; set; }
        double ConnectionsViewConnectionForegroundWidth { get; set; }
        int ConnectionsViewConnectionNameDisplayIndex { get; set; }
        double ConnectionsViewConnectionNameWidth { get; set; }

        void NameChanged(ConnectionId id, string value);

        double ConnectionsViewConnectionWidth { get; set; }
        int ConnectionsViewDisconnectionDisplayIndex { get; set; }
        double ConnectionsViewDisconnectionWidth { get; set; }
        int ConnectionsViewInputDisplayIndex { get; set; }
        double ConnectionsViewInputWidth { get; set; }
        int ConnectionsViewLoggedinUsernameDisplayIndex { get; set; }

        void OnMessageReceived(ConnectionId connId, ISiteMessage message, SiteMessageParser siteMessageParser, IAdapter adapter);

        double ConnectionsViewLoggedinUsernameWidth { get; set; }
        int ConnectionsViewSaveDisplayIndex { get; set; }
        double ConnectionsViewSaveWidth { get; set; }
        int ConnectionsViewSelectionDisplayIndex { get; set; }
        double ConnectionsViewSelectionWidth { get; set; }
        int ConnectionsViewSiteDisplayIndex { get; set; }
        double ConnectionsViewSiteWidth { get; set; }
        double ConnectionViewHeight { get; set; }
        Color HorizontalGridLineColor { get; set; }

        void SiteChanged(ConnectionId connId, SiteId id);

        int InfoDisplayIndex { get; set; }
        double InfoWidth { get; set; }
        bool IsEnabledSiteConnectionColor { get; set; }

        void OnSiteAdded(SiteId PluginId, string name);

        bool IsPixelScrolling { get; set; }
        bool IsShowCommentId { get; set; }

        void OnBrowserAdded(BrowserProfileId PluginId, string name, string? profileName);

        bool IsShowConnectionName { get; set; }
        bool IsShowConnectionsViewBrowser { get; set; }

        void OnSiteRemoved(SiteId PluginId);
        void OnSitePluginSettingsReceived(List<IPluginSettings> pluginSettingsList);

        bool IsShowConnectionsViewConnection { get; set; }
        bool IsShowConnectionsViewConnectionName { get; set; }

        void OnBrowserRemoved(BrowserProfileId PluginId);

        bool IsShowConnectionsViewDisconnection { get; set; }
        bool IsShowConnectionsViewInput { get; set; }
        bool IsShowConnectionsViewLoggedinUsername { get; set; }
        bool IsShowConnectionsViewSave { get; set; }
        bool IsShowConnectionsViewSelection { get; set; }
        bool IsShowConnectionsViewSite { get; set; }
        bool IsShowHorizontalGridLine { get; set; }
        bool IsShowInfo { get; set; }
        bool IsShowMessage { get; set; }
        bool IsShowMetaActive { get; set; }
        bool IsShowMetaConnectionName { get; set; }
        bool IsShowMetaCurrentViewers { get; set; }
        bool IsShowMetaElapse { get; set; }
        bool IsShowMetaOthers { get; set; }
        bool IsShowMetaTitle { get; set; }
        bool IsShowMetaTotalViewers { get; set; }
        bool IsShowPostTime { get; set; }
        bool IsShowThumbnail { get; set; }
        bool IsShowUsername { get; set; }
        bool IsShowVerticalGridLine { get; set; }
        bool IsTopmost { get; set; }
        double MainViewHeight { get; set; }
        double MainViewLeft { get; set; }
        double MainViewTop { get; set; }
        double MainViewWidth { get; set; }

        void BrowserChanged(ConnectionId connId, BrowserProfileId id);

        Color MenuBackColor { get; set; }
        Color MenuForeColor { get; set; }

        void RequestSitePluginSettings();

        Color MenuItemCheckMarkColor { get; set; }
        Color MenuItemMouseOverBackColor { get; set; }
        Color MenuItemMouseOverBorderColor { get; set; }
        Color MenuItemMouseOverCheckMarkColor { get; set; }
        Color MenuItemMouseOverForeColor { get; set; }
        Color MenuPopupBorderColor { get; set; }
        Color MenuSeparatorBackColor { get; set; }
        int MessageDisplayIndex { get; set; }
        double MessageWidth { get; set; }
        double MetadataViewActiveColumnWidth { get; set; }
        int MetadataViewActiveDisplayIndex { get; set; }
        double MetadataViewConnectionNameColumnWidth { get; set; }
        int MetadataViewConnectionNameDisplayIndex { get; set; }
        double MetadataViewCurrentViewersColumnWidth { get; set; }
        int MetadataViewCurrentViewersDisplayIndex { get; set; }
        double MetadataViewElapsedColumnWidth { get; set; }
        int MetadataViewElapsedDisplayIndex { get; set; }
        double MetadataViewHeight { get; set; }
        double MetadataViewOthersColumnWidth { get; set; }
        int MetadataViewOthersDisplayIndex { get; set; }
        double MetadataViewTitleColumnWidth { get; set; }
        int MetadataViewTitleDisplayIndex { get; set; }
        double MetadataViewTotalViewersColumnWidth { get; set; }
        int MetadataViewTotalViewersDisplayIndex { get; set; }
        int PostTimeDisplayIndex { get; set; }
        double PostTimeWidth { get; set; }
        Color ScrollBarBackColor { get; set; }
        Color ScrollBarBorderColor { get; set; }
        Color ScrollBarButtonBackColor { get; set; }
        Color ScrollBarButtonBorderColor { get; set; }
        Color ScrollBarButtonDisabledBackColor { get; set; }
        Color ScrollBarButtonDisabledBorderColor { get; set; }

        void OnNotifyNormalPluginAdded(NotifyNormalPluginAdded m);

        Color ScrollBarButtonDisabledForeColor { get; set; }
        Color ScrollBarButtonForeColor { get; set; }
        Color ScrollBarButtonMouseOverBackColor { get; set; }

        void Connect(ConnectionId id);

        Color ScrollBarButtonMouseOverBorderColor { get; set; }
        Color ScrollBarButtonMouseOverForeColor { get; set; }
        Color ScrollBarButtonPressedBackColor { get; set; }
        Color ScrollBarButtonPressedBorderColor { get; set; }
        Color ScrollBarButtonPressedForeColor { get; set; }
        Color ScrollBarThumbBackColor { get; set; }
        Color ScrollBarThumbMouseOverBackColor { get; set; }
        Color ScrollBarThumbPressedBackColor { get; set; }

        void Disconnect(ConnectionId id);

        Color SelectedRowBackColor { get; set; }
        Color SelectedRowForeColor { get; set; }
        SiteConnectionColorType SiteConnectionColorType { get; set; }
        Color SystemButtonBackColor { get; set; }
        Color SystemButtonBorderColor { get; set; }
        Color SystemButtonForeColor { get; set; }
        Color SystemButtonMouseOverBackColor { get; set; }
        Color SystemButtonMouseOverBorderColor { get; set; }
        Color SystemButtonMouseOverForeColor { get; set; }
        int ThumbnailDisplayIndex { get; set; }
        double ThumbnailWidth { get; set; }
        Color TitleBackColor { get; set; }
        Color TitleForeColor { get; set; }
        int UsernameDisplayIndex { get; set; }
        double UsernameWidth { get; set; }
        Color VerticalGridLineColor { get; set; }
        Color ViewBackColor { get; set; }
        Color WindowBorderColor { get; set; }

        event EventHandler<ConnectionAddedEventArgs>? ConnectionAdded;
        event EventHandler<ConnectionRemovedEventArgs>? ConnectionRemoved;
        event EventHandler<SitePluginAddedEventArgs>? SiteAdded;
        event EventHandler<SitePluginRemovedEventArgs>? SiteRemoved;
        event EventHandler<BrowserAddedEventArgs>? BrowserAdded;
        event EventHandler<BrowserRemovedEventArgs>? BrowserRemoved;
        event EventHandler<ConnectionStatusChangedEventArgs>? ConnectionStatusChanged;
        event EventHandler<MessageReceivedEventArgs>? MessageReceived;
        event EventHandler<SitePluginSettingsReceivedEventArgs>? SitePluginSettingsReceived;
        bool CanConnect(ConnectionId id);
        bool CanDisconnect(ConnectionId id);
        void InputChanged(ConnectionId id, string value);
        void RequestAddConnection();
        void OnConnectionAdded(ConnectionId connId, IConnectionStatus connSt);
        void OnConnectionRemoved(ConnectionId connId);
        void OnConnectionStatusChanged(ConnectionId id, IConnectionStatusDiff diff);
        void RemoveConnections(List<ConnectionId> selectedConnections);
        void RequestCloseApp();
        void OnSettingsApplied(List<IPluginSettingsDiff> modifiedData);
    }
}