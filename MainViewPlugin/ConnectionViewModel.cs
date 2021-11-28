using System;
using System.Collections.Generic;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Common;
using System.Windows.Threading;
using System.Windows.Media;
using Microsoft.Toolkit.Mvvm.Input;
using System.ComponentModel;
using Plugin;

namespace Mcv.Plugin.MainViewPlugin
{
    public class SiteViewModel : ViewModelBase
    {
        public string DisplayName { get; }
        public SiteId Id { get; }
        public SiteViewModel(SiteId siteId, string name)
        {
            Id = siteId;
            DisplayName = name;
        }
    }
    public class BrowserViewModel : ViewModelBase
    {
        public string BrowserName { get; }
        public string? ProfileName { get; }
        public string DisplayName => string.IsNullOrEmpty(ProfileName) ? $"{BrowserName}" : $"{BrowserName} ({ProfileName})";

        public BrowserProfileId Id { get; }
        public BrowserViewModel(BrowserProfileId browserId, string name, string? profileName)
        {
            Id = browserId;
            BrowserName = name;
            ProfileName = profileName;
        }
    }
    class ConnectionViewModel : ViewModelBase,INotifyPropertyChanged//, IConnectionStatus
    {
        private readonly ConnectionNameViewModel _connectionName;
        public string Name
        {
            get { return _connectionName.Name; }
            set { if (_connectionName.Name == value) return; _connectionName.Name=value; _adapter.NameChanged(_id, value); }
        }
        public ConnectionId Id => _id;
        #region IsSelected
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                RaisePropertyChanged();
            }
        }
        #endregion //IsSelected
        public ObservableCollection<SiteViewModel> Sites { get; }
        public ObservableCollection<BrowserViewModel> Browsers { get; }
        private SiteViewModel _selectedSite;
        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }
        public SiteViewModel SelectedSite
        {
            get { return _selectedSite; }
            set
            {
                if (_selectedSite == value)
                    return;
                _selectedSite = value;
                _adapter.SiteChanged(_id, value.Id);
                //TODO:CommentPostPanelを新たに選択されたサイトのものに変更する

            }
        }

        private Color _backColor;
        public Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
                RaisePropertyChanged();
            }
        }
        private Color _foreColor;
        public Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                _foreColor = value;
                RaisePropertyChanged();
            }
        }
        /// <summary>
        /// 配信者のユーザIDとかコミュニティIDのような毎回そこからリアルタイムの配信に接続できる文字列であるか
        /// 配信IDだと毎回変わるため保存しても無意味。
        /// </summary>
        public bool IsInputStoringNeeded { get; private set; }
        /// <summary>
        /// 保存して次回起動時にリストアする文字列
        /// </summary>
        public string UrlToRestore { get; private set; }

        private string _loggedInUsername;
        public string LoggedInUsername
        {
            get => _loggedInUsername;
            set
            {
                _loggedInUsername = value;
                RaisePropertyChanged();
            }
        }

        private System.Windows.Controls.UserControl _commentPostPanel;
        public System.Windows.Controls.UserControl CommentPostPanel
        {
            get { return _commentPostPanel; }
            set
            {
                if (_commentPostPanel == value) return;
                _commentPostPanel = value;
                RaisePropertyChanged();
            }
        }

        private BrowserViewModel _selectedBrowser;
        public BrowserViewModel SelectedBrowser
        {
            get { return _selectedBrowser; }
            set
            {
                if (_selectedBrowser == value)
                    return;
                _selectedBrowser = value;

                _adapter.BrowserChanged(_id, value.Id);
                //UpdateLoggedInInfo();
                RaisePropertyChanged();
            }
        }
        public bool CanConnect
        {
            get => _canConnect; 
            set
            { 
                if (_canConnect == value) return; 
                _canConnect = value; 
                RaisePropertyChanged();
            }
        }
        public bool CanDisconnect 
        {
            get => _canDisconnect;
            set 
            { 
                if (_canDisconnect == value) return; 
                _canDisconnect = value;
                RaisePropertyChanged();
            }
        }
        private bool _needSave;
        /// <summary>
        /// ユーザがこのConnectionの情報の保存を要求しているか
        /// </summary>
        public bool NeedSave
        {
            get => _needSave;
            set
            {
                _needSave = value;
                RaisePropertyChanged();
            }
        }

        private string _input;
        private bool _canConnect;
        private bool _canDisconnect;

        public string Input
        {
            get { return _input; }
            set
            {
                if (_input == value)
                    return;
                _input = value;

                _adapter.InputChanged(_id, value);

                //var guid = _sitePluginLoader.GetValidSiteGuid(value);
                //if (guid != Guid.Empty)
                //{
                //    var vm = _siteVmDict[guid];
                //    SelectedSite = vm;
                //}
            }
        }
        /// <summary>
        /// 自動サイト選択機能が無い版
        /// </summary>
        public string InputWithNoAutoSiteSelect
        {
            get => _input;
            set
            {
                if (_input == value)
                    return;
                _input = value;
                RaisePropertyChanged();
            }
        }

        //public ConnectionContext GetCurrent()
        //{
        //    if (SelectedSite == null)
        //    {
        //        return null;
        //    }
        //    var context = new ConnectionContext { ConnectionName = this.ConnectionName, SiteGuid = SelectedSite.Guid, CommentProvider = _commentProvider };
        //    return context;
        //}

        private /*async*/ void Connect()
        {
            _adapter.Connect(_id);
            //try
            //{
            //    //接続中は削除できないように選択を外す
            //    IsSelected = false;
            //    var input = Input;
            //    var browser = SelectedBrowser.Browser;
            //    await _commentProvider.ConnectAsync(input, browser);


            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //    LogException(ex);
            //}
        }
        private void Disconnect()
        {
            _adapter.Disconnect(_id);
            //try
            //{
            //    _commentProvider.Disconnect();
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(ex.Message);
            //    LogException(ex);
            //}
        }
        private void LogException(Exception ex)
        {

        }
        //private readonly ILogger _logger;
        //private readonly ISitePluginLoader _sitePluginLoader;
        //private readonly IOptions _options;

        //private readonly IEnumerable<ISiteContext> _sites;
        //private readonly Dictionary<Guid, SiteViewModel> _siteVmDict = new Dictionary<Guid, SiteViewModel>();
        /// <summary>
        /// ConnectionNameは重複可だから一意識別のために必要
        /// </summary>
        public Guid Guid { get; }
        private readonly ConnectionId _id;
        private readonly IAdapter _adapter;
        public ConnectionViewModel(ConnectionId id, IAdapter adapter, string name, ObservableCollection<SiteViewModel> sites, ObservableCollection<BrowserViewModel> browsers, SiteViewModel selectedSite, BrowserViewModel selectedBrowser, bool isConnected)
        {
            _id = id;
            _adapter = adapter;
            Sites = sites;
            Browsers = browsers;
            _selectedSite = selectedSite;
            _selectedBrowser = selectedBrowser;
            _connectionName = new ConnectionNameViewModel(id, name);
            CanConnect = !isConnected;
            CanDisconnect = isConnected;
            ConnectCommand = new RelayCommand(Connect);
            DisconnectCommand = new RelayCommand(Disconnect);
        }

    }
    public class RenamedEventArgs : EventArgs
    {
        public string NewValue { get; }
        public string OldValue { get; }
        public RenamedEventArgs(string oldValue, string newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }
}
