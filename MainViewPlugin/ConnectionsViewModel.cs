using Plugin;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mcv.Plugin.MainViewPlugin
{
    class ConnectionsViewModel
    {
        public ObservableCollection<ConnectionViewModel> Connections { get; } = new ObservableCollection<ConnectionViewModel>();

        public void AddConnection(ConnectionViewModel connVm)
        {
            Connections.Add(connVm);
        }
        public void RemoveConnection(ConnectionViewModel connVm)
        {
            Connections.Remove(connVm);
        }
        public List<ConnectionId> GetSelectedConnections()
        {
            return Connections.Where(c => c.IsSelected).Select(c => c.Id).ToList();
        }
        private readonly IAdapter _adapter;
        #region ConnectionsView
        #region ConnectionsViewSelection
        public int ConnectionsViewSelectionDisplayIndex
        {
            get { return _adapter.ConnectionsViewSelectionDisplayIndex; }
            set { _adapter.ConnectionsViewSelectionDisplayIndex = value; }
        }
        public double ConnectionsViewSelectionWidth
        {
            get { return _adapter.ConnectionsViewSelectionWidth; }
            set { _adapter.ConnectionsViewSelectionWidth = value; }
        }
        public bool IsShowConnectionsViewSelection
        {
            get { return _adapter.IsShowConnectionsViewSelection; }
            set { _adapter.IsShowConnectionsViewSelection = value; }
        }
        #endregion
        #region ConnectionsViewSite
        public int ConnectionsViewSiteDisplayIndex
        {
            get { return _adapter.ConnectionsViewSiteDisplayIndex; }
            set { _adapter.ConnectionsViewSiteDisplayIndex = value; }
        }
        public double ConnectionsViewSiteWidth
        {
            get { return _adapter.ConnectionsViewSiteWidth; }
            set { _adapter.ConnectionsViewSiteWidth = value; }
        }
        public bool IsShowConnectionsViewSite
        {
            get { return _adapter.IsShowConnectionsViewSite; }
            set { _adapter.IsShowConnectionsViewSite = value; }
        }
        #endregion
        #region ConnectionsViewConnectionName
        public int ConnectionsViewConnectionNameDisplayIndex
        {
            get { return _adapter.ConnectionsViewConnectionNameDisplayIndex; }
            set { _adapter.ConnectionsViewConnectionNameDisplayIndex = value; }
        }
        public double ConnectionsViewConnectionNameWidth
        {
            get { return _adapter.ConnectionsViewConnectionNameWidth; }
            set { _adapter.ConnectionsViewConnectionNameWidth = value; }
        }
        public bool IsShowConnectionsViewConnectionName
        {
            get { return _adapter.IsShowConnectionsViewConnectionName; }
            set { _adapter.IsShowConnectionsViewConnectionName = value; }
        }
        #endregion
        #region ConnectionsViewInput
        public int ConnectionsViewInputDisplayIndex
        {
            get { return _adapter.ConnectionsViewInputDisplayIndex; }
            set { _adapter.ConnectionsViewInputDisplayIndex = value; }
        }
        public double ConnectionsViewInputWidth
        {
            get { return _adapter.ConnectionsViewInputWidth; }
            set { _adapter.ConnectionsViewInputWidth = value; }
        }
        public bool IsShowConnectionsViewInput
        {
            get { return _adapter.IsShowConnectionsViewInput; }
            set { _adapter.IsShowConnectionsViewInput = value; }
        }
        #endregion
        #region ConnectionsViewBrowser
        public int ConnectionsViewBrowserDisplayIndex
        {
            get { return _adapter.ConnectionsViewBrowserDisplayIndex; }
            set { _adapter.ConnectionsViewBrowserDisplayIndex = value; }
        }
        public double ConnectionsViewBrowserWidth
        {
            get { return _adapter.ConnectionsViewBrowserWidth; }
            set { _adapter.ConnectionsViewBrowserWidth = value; }
        }
        public bool IsShowConnectionsViewBrowser
        {
            get { return _adapter.IsShowConnectionsViewBrowser; }
            set { _adapter.IsShowConnectionsViewBrowser = value; }
        }
        #endregion
        #region ConnectionsViewConnection
        public int ConnectionsViewConnectionDisplayIndex
        {
            get { return _adapter.ConnectionsViewConnectionDisplayIndex; }
            set { _adapter.ConnectionsViewConnectionDisplayIndex = value; }
        }
        public double ConnectionsViewConnectionWidth
        {
            get { return _adapter.ConnectionsViewConnectionWidth; }
            set { _adapter.ConnectionsViewConnectionWidth = value; }
        }
        public bool IsShowConnectionsViewConnection
        {
            get { return _adapter.IsShowConnectionsViewConnection; }
            set { _adapter.IsShowConnectionsViewConnection = value; }
        }
        #endregion
        #region ConnectionsViewDisconnection
        public int ConnectionsViewDisconnectionDisplayIndex
        {
            get { return _adapter.ConnectionsViewDisconnectionDisplayIndex; }
            set { _adapter.ConnectionsViewDisconnectionDisplayIndex = value; }
        }
        public double ConnectionsViewDisconnectionWidth
        {
            get { return _adapter.ConnectionsViewDisconnectionWidth; }
            set { _adapter.ConnectionsViewDisconnectionWidth = value; }
        }
        public bool IsShowConnectionsViewDisconnection
        {
            get { return _adapter.IsShowConnectionsViewDisconnection; }
            set { _adapter.IsShowConnectionsViewDisconnection = value; }
        }
        #endregion
        #region ConnectionsViewSave
        public int ConnectionsViewSaveDisplayIndex
        {
            get { return _adapter.ConnectionsViewSaveDisplayIndex; }
            set { _adapter.ConnectionsViewSaveDisplayIndex = value; }
        }
        public double ConnectionsViewSaveWidth
        {
            get { return _adapter.ConnectionsViewSaveWidth; }
            set { _adapter.ConnectionsViewSaveWidth = value; }
        }
        public bool IsShowConnectionsViewSave
        {
            get { return _adapter.IsShowConnectionsViewSave; }
            set { _adapter.IsShowConnectionsViewSave = value; }
        }
        #endregion
        #region ConnectionsViewLoggedinUsername
        public int ConnectionsViewLoggedinUsernameDisplayIndex
        {
            get { return _adapter.ConnectionsViewLoggedinUsernameDisplayIndex; }
            set { _adapter.ConnectionsViewLoggedinUsernameDisplayIndex = value; }
        }
        public double ConnectionsViewLoggedinUsernameWidth
        {
            get { return _adapter.ConnectionsViewLoggedinUsernameWidth; }
            set { _adapter.ConnectionsViewLoggedinUsernameWidth = value; }
        }
        public bool IsShowConnectionsViewLoggedinUsername
        {
            get { return _adapter.IsShowConnectionsViewLoggedinUsername; }
            set { _adapter.IsShowConnectionsViewLoggedinUsername = value; }
        }
        #endregion
        #region ConnectionsViewConnectionBackground
        public int ConnectionsViewConnectionBackgroundDisplayIndex
        {
            get { return _adapter.ConnectionsViewConnectionBackgroundDisplayIndex; }
            set { _adapter.ConnectionsViewConnectionBackgroundDisplayIndex = value; }
        }
        public double ConnectionsViewConnectionBackgroundWidth
        {
            get { return _adapter.ConnectionsViewConnectionBackgroundWidth; }
            set { _adapter.ConnectionsViewConnectionBackgroundWidth = value; }
        }
        public bool IsShowConnectionsViewConnectionBackground
        {
            get { return _adapter.IsEnabledSiteConnectionColor && _adapter.SiteConnectionColorType == SiteConnectionColorType.Connection; }
        }
        #endregion
        #region ConnectionsViewConnectionForeground
        public int ConnectionsViewConnectionForegroundDisplayIndex
        {
            get { return _adapter.ConnectionsViewConnectionForegroundDisplayIndex; }
            set { _adapter.ConnectionsViewConnectionForegroundDisplayIndex = value; }
        }
        public double ConnectionsViewConnectionForegroundWidth
        {
            get { return _adapter.ConnectionsViewConnectionForegroundWidth; }
            set { _adapter.ConnectionsViewConnectionForegroundWidth = value; }
        }
        public bool IsShowConnectionsViewConnectionForeground
        {
            get { return _adapter.IsEnabledSiteConnectionColor && _adapter.SiteConnectionColorType == SiteConnectionColorType.Connection; }
        }
        #endregion
        #endregion

        public double ConnectionColorColumnWidth
        {
            get
            {
                if (_adapter.IsEnabledSiteConnectionColor && _adapter.SiteConnectionColorType == SiteConnectionColorType.Connection)
                {
                    return 100;
                }
                else
                {
                    return 0;
                }
            }
        }
        public System.Windows.Controls.DataGridGridLinesVisibility GridLinesVisibility
        {
            get
            {
                if (_adapter.IsShowHorizontalGridLine && _adapter.IsShowVerticalGridLine)
                    return System.Windows.Controls.DataGridGridLinesVisibility.All;
                else if (_adapter.IsShowHorizontalGridLine)
                    return System.Windows.Controls.DataGridGridLinesVisibility.Horizontal;
                else if (_adapter.IsShowVerticalGridLine)
                    return System.Windows.Controls.DataGridGridLinesVisibility.Vertical;
                else
                    return System.Windows.Controls.DataGridGridLinesVisibility.None;
            }
        }
        public ConnectionsViewModel(IAdapter adapter)
        {
            _adapter = adapter;
        }
    }
}

