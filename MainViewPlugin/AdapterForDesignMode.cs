using Common;
//using Mcv.Core;
//using Mcv.Core.SiteMessage;
using Microsoft.Toolkit.Mvvm.Input;
using Plugin;
using Plugin.Message.ToPlugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Mcv.Plugin.MainViewPlugin
{
    class AdapterForDesignMode : IAdapter
    {
        public Color ButtonBackColor { get; set; }
        public Color ButtonBorderColor { get; set; }
        public Color ButtonForeColor { get; set; }
        public int CommentIdDisplayIndex { get; set; }
        public double CommentIdWidth { get; set; }
        public Color CommentListBackColor { get; set; } = Colors.White;
        public Color CommentListBorderColor { get; set; } = Colors.Black;
        public Color CommentListHeaderBackColor { get; set; } = Colors.White;
        public Color CommentListHeaderBorderColor { get; set; } = Colors.Black;
        public Color CommentListHeaderForeColor { get; set; } = Colors.Black;
        public Color CommentListSeparatorColor { get; set; } = Colors.Black;
        public Color ConnectionListRowBackColor { get; set; } = Colors.White;
        public int ConnectionNameDisplayIndex { get; set; }
        public double ConnectionNameWidth { get; set; }
        public int ConnectionsViewBrowserDisplayIndex { get; set; }
        public double ConnectionsViewBrowserWidth { get; set; }
        public int ConnectionsViewConnectionBackgroundDisplayIndex { get; set; }
        public double ConnectionsViewConnectionBackgroundWidth { get; set; }
        public int ConnectionsViewConnectionDisplayIndex { get; set; }
        public int ConnectionsViewConnectionForegroundDisplayIndex { get; set; }
        public double ConnectionsViewConnectionForegroundWidth { get; set; }
        public int ConnectionsViewConnectionNameDisplayIndex { get; set; }
        public double ConnectionsViewConnectionNameWidth { get; set; }
        public double ConnectionsViewConnectionWidth { get; set; }
        public int ConnectionsViewDisconnectionDisplayIndex { get; set; }
        public double ConnectionsViewDisconnectionWidth { get; set; }
        public int ConnectionsViewInputDisplayIndex { get; set; }
        public double ConnectionsViewInputWidth { get; set; }
        public int ConnectionsViewLoggedinUsernameDisplayIndex { get; set; }
        public double ConnectionsViewLoggedinUsernameWidth { get; set; }
        public int ConnectionsViewSaveDisplayIndex { get; set; }
        public double ConnectionsViewSaveWidth { get; set; }
        public int ConnectionsViewSelectionDisplayIndex { get; set; }
        public double ConnectionsViewSelectionWidth { get; set; }
        public int ConnectionsViewSiteDisplayIndex { get; set; }
        public double ConnectionsViewSiteWidth { get; set; }
        public double ConnectionViewHeight { get; set; }
        public Color HorizontalGridLineColor { get; set; }
        public int InfoDisplayIndex { get; set; }
        public double InfoWidth { get; set; }
        public bool IsEnabledSiteConnectionColor { get; set; }
        public bool IsPixelScrolling { get; set; }
        public bool IsShowCommentId { get; set; }
        public bool IsShowConnectionName { get; set; }
        public bool IsShowConnectionsViewBrowser { get; set; }
        public bool IsShowConnectionsViewConnection { get; set; }
        public bool IsShowConnectionsViewConnectionName { get; set; }
        public bool IsShowConnectionsViewDisconnection { get; set; }
        public bool IsShowConnectionsViewInput { get; set; }
        public bool IsShowConnectionsViewLoggedinUsername { get; set; }
        public bool IsShowConnectionsViewSave { get; set; }
        public bool IsShowConnectionsViewSelection { get; set; }
        public bool IsShowConnectionsViewSite { get; set; }
        public bool IsShowHorizontalGridLine { get; set; }
        public bool IsShowInfo { get; set; }
        public bool IsShowMessage { get; set; }
        public bool IsShowMetaActive { get; set; }
        public bool IsShowMetaConnectionName { get; set; }
        public bool IsShowMetaCurrentViewers { get; set; }
        public bool IsShowMetaElapse { get; set; }
        public bool IsShowMetaOthers { get; set; }
        public bool IsShowMetaTitle { get; set; }
        public bool IsShowMetaTotalViewers { get; set; }
        public bool IsShowPostTime { get; set; }
        public bool IsShowThumbnail { get; set; }
        public bool IsShowUsername { get; set; }
        public bool IsShowVerticalGridLine { get; set; }
        public bool IsTopmost { get; set; }
        public double MainViewHeight { get; set; }
        public double MainViewLeft { get; set; }
        public double MainViewTop { get; set; }
        public double MainViewWidth { get; set; }
        public Color MenuBackColor { get; set; }
        public Color MenuForeColor { get; set; }
        public Color MenuItemCheckMarkColor { get; set; }
        public Color MenuItemMouseOverBackColor { get; set; }
        public Color MenuItemMouseOverBorderColor { get; set; }
        public Color MenuItemMouseOverCheckMarkColor { get; set; }
        public Color MenuItemMouseOverForeColor { get; set; }
        public Color MenuPopupBorderColor { get; set; }
        public Color MenuSeparatorBackColor { get; set; }
        public int MessageDisplayIndex { get; set; }
        public double MessageWidth { get; set; }
        public double MetadataViewActiveColumnWidth { get; set; }
        public int MetadataViewActiveDisplayIndex { get; set; }
        public double MetadataViewConnectionNameColumnWidth { get; set; }
        public int MetadataViewConnectionNameDisplayIndex { get; set; }
        public double MetadataViewCurrentViewersColumnWidth { get; set; }
        public int MetadataViewCurrentViewersDisplayIndex { get; set; }
        public double MetadataViewElapsedColumnWidth { get; set; }
        public int MetadataViewElapsedDisplayIndex { get; set; }
        public double MetadataViewHeight { get; set; }
        public double MetadataViewOthersColumnWidth { get; set; }
        public int MetadataViewOthersDisplayIndex { get; set; }
        public double MetadataViewTitleColumnWidth { get; set; }
        public int MetadataViewTitleDisplayIndex { get; set; }
        public double MetadataViewTotalViewersColumnWidth { get; set; }
        public int MetadataViewTotalViewersDisplayIndex { get; set; }
        public int PostTimeDisplayIndex { get; set; }
        public double PostTimeWidth { get; set; }
        public Color ScrollBarBackColor { get; set; }
        public Color ScrollBarBorderColor { get; set; }
        public Color ScrollBarButtonBackColor { get; set; }
        public Color ScrollBarButtonBorderColor { get; set; }
        public Color ScrollBarButtonDisabledBackColor { get; set; }
        public Color ScrollBarButtonDisabledBorderColor { get; set; }
        public Color ScrollBarButtonDisabledForeColor { get; set; }
        public Color ScrollBarButtonForeColor { get; set; }
        public Color ScrollBarButtonMouseOverBackColor { get; set; }
        public Color ScrollBarButtonMouseOverBorderColor { get; set; }
        public Color ScrollBarButtonMouseOverForeColor { get; set; }
        public Color ScrollBarButtonPressedBackColor { get; set; }
        public Color ScrollBarButtonPressedBorderColor { get; set; }
        public Color ScrollBarButtonPressedForeColor { get; set; }
        public Color ScrollBarThumbBackColor { get; set; }
        public Color ScrollBarThumbMouseOverBackColor { get; set; }
        public Color ScrollBarThumbPressedBackColor { get; set; }
        public Color SelectedRowBackColor { get; set; }
        public Color SelectedRowForeColor { get; set; }
        public SiteConnectionColorType SiteConnectionColorType { get; set; }
        public Color SystemButtonBackColor { get; set; }
        public Color SystemButtonBorderColor { get; set; }
        public Color SystemButtonForeColor { get; set; }
        public Color SystemButtonMouseOverBackColor { get; set; }
        public Color SystemButtonMouseOverBorderColor { get; set; }
        public Color SystemButtonMouseOverForeColor { get; set; }
        public int ThumbnailDisplayIndex { get; set; }
        public double ThumbnailWidth { get; set; }
        public Color TitleBackColor { get; set; }
        public Color TitleForeColor { get; set; }
        public int UsernameDisplayIndex { get; set; }
        public double UsernameWidth { get; set; }
        public Color VerticalGridLineColor { get; set; }
        public Color ViewBackColor { get; set; }
        public Color WindowBorderColor { get; set; }

        public event EventHandler<ConnectionAddedEventArgs>? ConnectionAdded;
        public event EventHandler<SitePluginAddedEventArgs>? SiteAdded;
        public event EventHandler<SitePluginRemovedEventArgs>? SiteRemoved;
        public event EventHandler<BrowserAddedEventArgs>? BrowserAdded;
        public event EventHandler<BrowserRemovedEventArgs>? BrowserRemoved;
        public event EventHandler<ConnectionStatusChangedEventArgs>? ConnectionStatusChanged;
        public event EventHandler<ConnectionRemovedEventArgs>? ConnectionRemoved;
        public event EventHandler<MessageReceivedEventArgs>? MessageReceived;
        public event EventHandler<SitePluginSettingsReceivedEventArgs>? SitePluginSettingsReceived;

        public void BrowserChanged(ConnectionId connId, BrowserProfileId id)
        {
            throw new NotImplementedException();
        }

        public bool CanConnect(ConnectionId id)
        {
            throw new NotImplementedException();
        }

        public bool CanDisconnect(ConnectionId id)
        {
            throw new NotImplementedException();
        }

        public void Connect(ConnectionId id)
        {
            throw new NotImplementedException();
        }

        public void Disconnect(ConnectionId id)
        {
            throw new NotImplementedException();
        }

        public void InputChanged(ConnectionId id, string value)
        {
            throw new NotImplementedException();
        }

        public void NameChanged(ConnectionId id, string value)
        {
            throw new NotImplementedException();
        }

        public void OnBrowserAdded(BrowserProfileId PluginId, string name, string? profileName)
        {
            throw new NotImplementedException();
        }

        public void OnBrowserRemoved(BrowserProfileId PluginId)
        {
            throw new NotImplementedException();
        }

        public void OnConnectionAdded()
        {
            throw new NotImplementedException();
        }

        public void OnConnectionAdded(ConnectionId id)
        {
            throw new NotImplementedException();
        }

        public void OnConnectionAdded(ConnectionId connId, IConnectionStatus connSt)
        {
            throw new NotImplementedException();
        }

        public void OnConnectionRemoved(ConnectionId connId)
        {
            throw new NotImplementedException();
        }

        public void OnConnectionStatusChanged(ConnectionId id, IConnectionStatusDiff diff)
        {
            throw new NotImplementedException();
        }

        public void OnMessageReceived(ConnectionId connId, ISiteMessage message)
        {
            throw new NotImplementedException();
        }

        public void OnMessageReceived(ConnectionId connId, ISiteMessage message, SiteMessageParser siteMessageParser)
        {
            throw new NotImplementedException();
        }

        public void OnMessageReceived(ConnectionId connId, ISiteMessage message, SiteMessageParser siteMessageParser, IAdapter adapter)
        {
            throw new NotImplementedException();
        }

        public void OnNotifyNormalPluginAdded(NotifyNormalPluginAdded m)
        {
            throw new NotImplementedException();
        }

        public void OnSettingsApplied(List<IPluginSettingsDiff> modifiedData)
        {
            throw new NotImplementedException();
        }

        public void OnSiteAdded(SiteId PluginId, string name)
        {
            throw new NotImplementedException();
        }

        public void OnSitePluginSettingsReceived(List<IPluginSettings> pluginSettingsList)
        {
            throw new NotImplementedException();
        }

        public void OnSiteRemoved(SiteId PluginId)
        {
            throw new NotImplementedException();
        }

        public void RemoveConnections(List<ConnectionId> selectedConnections)
        {
            throw new NotImplementedException();
        }

        public void RequestAddConnection()
        {
            throw new NotImplementedException();
        }

        public void RequestCloseApp()
        {
            throw new NotImplementedException();
        }

        public void RequestSitePluginSettings()
        {
            throw new NotImplementedException();
        }

        public void SiteChanged(ConnectionId connId, SiteId id)
        {
            throw new NotImplementedException();
        }
    }
}
