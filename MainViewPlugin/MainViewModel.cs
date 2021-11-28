using Common;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using Plugin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Mcv.Plugin.MainViewPlugin
{
    internal class ShowUserInfoViewMessage : Microsoft.Toolkit.Mvvm.Messaging.Messages.RequestMessage<string>
    {
        public ShowUserInfoViewMessage(UserInfoViewModel userInfoVm)
        {
            UserInfoVm = userInfoVm;
        }

        public UserInfoViewModel UserInfoVm { get; }
    }
    class UserViewModel : ViewModelBase, INotifyPropertyChanged
    {

    }
    class UserInfoViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public UserInfoViewModel(ICollectionView comments, UserViewModel userVm, IAdapter adapter)
        {
            CommentVm = new CommentDataGridViewModel(adapter, comments);
            Comments = comments;
        }
        public CommentDataGridViewModel CommentVm { get; }
        public ICollectionView Comments { get; }
    }
    class MainViewModel : /*CommentDataGridViewModelBase,*/ ViewModelBase, INotifyPropertyChanged
    {
        public Brush MenuBackground => new SolidColorBrush(_adapter.MenuBackColor);
        public Brush MenuForeground => new SolidColorBrush(_adapter.MenuForeColor);
        public Brush MenuSeparatorBackground => new SolidColorBrush(_adapter.MenuSeparatorBackColor);
        public Brush MenuPopupBorderBrush => new SolidColorBrush(_adapter.MenuPopupBorderColor);
        public Brush MenuItemMouseOverBackground => new SolidColorBrush(_adapter.MenuItemMouseOverBackColor);

        public Brush MetadataDataGridBackground => new SolidColorBrush(_adapter.CommentListBackColor);
        public Brush MetadataDataGridBorderBrush => new SolidColorBrush(_adapter.CommentListBorderColor);

        public bool Topmost
        {
            get { return _adapter.IsTopmost; }
            set { _adapter.IsTopmost = value; }
        }
        public double MainViewHeight
        {
            get { return _adapter.MainViewHeight; }
            set { _adapter.MainViewHeight = value; }
        }
        public double MainViewWidth
        {
            get { return _adapter.MainViewWidth; }
            set { _adapter.MainViewWidth = value; }
        }
        public double MainViewLeft
        {
            get { return _adapter.MainViewLeft; }
            set { _adapter.MainViewLeft = value; }
        }
        public double MainViewTop
        {
            get { return _adapter.MainViewTop; }
            set { _adapter.MainViewTop = value; }
        }
        public double ConnectionViewHeight
        {
            get { return _adapter.ConnectionViewHeight; }
            set { _adapter.ConnectionViewHeight = value; }
        }
        public double MetadataViewHeight
        {
            get { return _adapter.MetadataViewHeight; }
            set { _adapter.MetadataViewHeight = value; }
        }
        public double MetadataViewConnectionNameColumnWidth
        {
            get { return _adapter.MetadataViewConnectionNameColumnWidth; }
            set { _adapter.MetadataViewConnectionNameColumnWidth = value; }
        }
        public double MetadataViewTitleColumnWidth
        {
            get { return _adapter.MetadataViewTitleColumnWidth; }
            set { _adapter.MetadataViewTitleColumnWidth = value; }
        }
        public double MetadataViewElapsedColumnWidth
        {
            get { return _adapter.MetadataViewElapsedColumnWidth; }
            set { _adapter.MetadataViewElapsedColumnWidth = value; }
        }
        public double MetadataViewCurrentViewersColumnWidth
        {
            get { return _adapter.MetadataViewCurrentViewersColumnWidth; }
            set { _adapter.MetadataViewCurrentViewersColumnWidth = value; }
        }
        public double MetadataViewTotalViewersColumnWidth
        {
            get { return _adapter.MetadataViewTotalViewersColumnWidth; }
            set { _adapter.MetadataViewTotalViewersColumnWidth = value; }
        }
        public double MetadataViewActiveColumnWidth
        {
            get { return _adapter.MetadataViewActiveColumnWidth; }
            set { _adapter.MetadataViewActiveColumnWidth = value; }
        }
        public double MetadataViewOthersColumnWidth
        {
            get { return _adapter.MetadataViewOthersColumnWidth; }
            set { _adapter.MetadataViewOthersColumnWidth = value; }
        }
        public bool IsShowMetaConnectionName
        {
            get => _adapter.IsShowMetaConnectionName;
            set => _adapter.IsShowMetaConnectionName = value;
        }
        public int MetadataViewConnectionNameDisplayIndex
        {
            get => _adapter.MetadataViewConnectionNameDisplayIndex;
            set => _adapter.MetadataViewConnectionNameDisplayIndex = value;
        }

        public bool IsShowMetaTitle
        {
            get => _adapter.IsShowMetaTitle;
            set => _adapter.IsShowMetaTitle = value;
        }
        public int MetadataViewTitleDisplayIndex
        {
            get => _adapter.MetadataViewTitleDisplayIndex;
            set => _adapter.MetadataViewTitleDisplayIndex = value;
        }

        public bool IsShowMetaElapse
        {
            get => _adapter.IsShowMetaElapse;
            set => _adapter.IsShowMetaElapse = value;
        }
        public int MetadataViewElapsedDisplayIndex
        {
            get => _adapter.MetadataViewElapsedDisplayIndex;
            set => _adapter.MetadataViewElapsedDisplayIndex = value;
        }

        public bool IsShowMetaCurrentViewers
        {
            get => _adapter.IsShowMetaCurrentViewers;
            set => _adapter.IsShowMetaCurrentViewers = value;
        }
        public int MetadataViewCurrentViewersDisplayIndex
        {
            get => _adapter.MetadataViewCurrentViewersDisplayIndex;
            set => _adapter.MetadataViewCurrentViewersDisplayIndex = value;
        }

        public bool IsShowMetaTotalViewers
        {
            get => _adapter.IsShowMetaTotalViewers;
            set => _adapter.IsShowMetaTotalViewers = value;
        }
        public int MetadataViewTotalViewersDisplayIndex
        {
            get => _adapter.MetadataViewTotalViewersDisplayIndex;
            set => _adapter.MetadataViewTotalViewersDisplayIndex = value;
        }

        public bool IsShowMetaActive
        {
            get => _adapter.IsShowMetaActive;
            set => _adapter.IsShowMetaActive = value;
        }
        public int MetadataViewActiveDisplayIndex
        {
            get => _adapter.MetadataViewActiveDisplayIndex;
            set => _adapter.MetadataViewActiveDisplayIndex = value;
        }

        public bool IsShowMetaOthers
        {
            get => _adapter.IsShowMetaOthers;
            set => _adapter.IsShowMetaOthers = value;
        }
        public int MetadataViewOthersDisplayIndex
        {
            get => _adapter.MetadataViewOthersDisplayIndex;
            set => _adapter.MetadataViewOthersDisplayIndex = value;
        }



        #region Commands
        public ICommand MainViewClosingCommand { get; }
        public ICommand ShowOptionsWindowCommand { get; }
        public ICommand ExitCommand { get; }
        public ICommand ShowWebSiteCommand { get; }
        public ICommand ShowDevelopersTwitterCommand { get; }
        public ICommand CheckUpdateCommand { get; }
        public ICommand ShowUserInfoCommand { get; }
        public ICommand ShowUserListCommand { get; }
        public ICommand RemoveSelectedConnectionCommand { get; }
        public ICommand AddNewConnectionCommand { get; }
        public ICommand ClearAllCommentsCommand { get; }
        public ICommand CommentCopyCommand { get; }
        public ICommand OpenUrlCommand { get; }

        #endregion //Commands

        #region Fields
        //private readonly Dictionary<IPlugin, PluginMenuItemViewModel> _pluginMenuItemDict = new Dictionary<IPlugin, PluginMenuItemViewModel>();
        //private readonly ILogger _logger;
        //private IPluginManager _pluginManager;
        //private readonly ISitePluginLoader _sitePluginLoader;
        //public ISiteContext GetSiteContext(Guid siteContextGuid)
        //{
        //    return _sitePluginLoader.GetSiteContext(siteContextGuid);
        //}
        //private readonly IBrowserLoader _browserLoader;
        //private readonly IIo _io;
        ////IEnumerable<ISiteContext> _siteContexts;
        //IEnumerable<SiteViewModel> _siteVms;
        //IEnumerable<BrowserViewModel> _browserVms;

        //Dictionary<ConnectionViewModel, MetadataViewModel> _metaDict = new Dictionary<ConnectionViewModel, MetadataViewModel>();
        //ConnectionSerializerLoader _connectionSerializerLoader = new ConnectionSerializerLoader("settings\\connections.txt");
        #endregion //Fields


        #region Methods
        private void Activated()
        {

        }
        private void Loaded()
        {

        }
        private void ClearAllComments()
        {
            try
            {
                //Comments.Clear();
                //個別ユーザのコメントはどうしようか

            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
        private void ShowOptionsWindow()
        {
            _adapter.RequestSitePluginSettings();
        }
        private void LogException(Exception ex)
        {

        }
        private void Closing(CancelEventArgs? e)
        {
            _adapter.RequestCloseApp();
            //try
            //{
            //    _connectionSerializerLoader.Save(Connections);
            //}
            //catch (Exception ex)
            //{
            //    LogException(ex);
            //    Debug.WriteLine(ex.Message);
            //}
            //foreach (var site in GetSiteContexts())
            //{
            //    try
            //    {
            //        var path = GetSiteOptionsPath(site.DisplayName);
            //        site.SaveOptions(path, _io);
            //    }
            //    catch (Exception ex)
            //    {
            //        LogException(ex);
            //        Debug.WriteLine(ex.Message);
            //    }
            //}
            //try
            //{
            //    _pluginManager?.OnClosing();
            //}
            //catch (Exception ex)
            //{
            //    LogException(ex);
            //    Debug.WriteLine(ex.Message);
            //}

            //try
            //{
            //    _sitePluginLoader.Save();
            //}
            //catch (Exception ex)
            //{
            //    LogException(ex);
            //    Debug.WriteLine(ex.Message);
            //}
        }
        private void RemoveSelectedConnection()
        {
            var selectedConnections = ConnectionsVm.GetSelectedConnections();
            _adapter.RemoveConnections(selectedConnections);
            //try
            //{
            //    var toRemove = Connections.Where(conn => conn.IsSelected).ToList();
            //    foreach (var conn in toRemove)
            //    {
            //        Connections.Remove(conn);
            //        var meta = _metaDict[conn];
            //        _metaDict.Remove(conn);
            //        MetaCollection.Remove(meta);
            //        OnConnectionDeleted(conn.ConnectionName);
            //    }
            //    //TODO:この接続に関連するコメントも全て消したい

            //}
            //catch (Exception ex)
            //{
            //    LogException(ex);
            //}
        }
        private void SetSystemInfo(string message, InfoType type)
        {
            //var context = InfoMessageContext.Create(new InfoMessage
            //{
            //    Text = message,
            //    SiteType = SiteType.Unknown,
            //    Type = type,
            //}, _options);
            //AddComment(context, null);
        }

        private void RequestAddNewConnection()
        {
            _adapter.RequestAddConnection();
        }

        private void AddConnection(ConnectionId connId, IAdapter adapter, string name, SiteViewModel selectedSite, BrowserViewModel selectedBrowser, bool isConnected)
        {
            var vm = new ConnectionViewModel(connId, _adapter, name, Sites, Browsers, selectedSite, selectedBrowser, isConnected);
            _connDict.Add(connId, vm);
            ConnectionsVm.AddConnection(vm);
        }

        private void OnConnectionAdded(ConnectionId connId, INormalConnection connSt)
        {
            var selectedSite = GetSiteViewModel(connSt.SelectedSite);
            var selectedBrowser = GetBrowserViewModel(connSt.SelectedBrowser);
            AddConnection(connId, _adapter, connSt.Name, selectedSite, selectedBrowser, connSt.IsConnected);
        }

        private void OnConnectionRemoved(ConnectionId connId)
        {
            var vm = _connDict[connId];
            _connDict.Remove(connId);
            ConnectionsVm.RemoveConnection(vm);
        }
        private void OnConnectionStatusChanged(ConnectionId connId, INormalConnectionDiff connStDiff)
        {
            //var conn = _connDict[connId];
            //if (connStDiff.Name != null)
            //{
            //    conn.Name = connStDiff.Name;
            //}
            //if (connStDiff.Input != null)
            //{
            //    conn.Input = connStDiff.Input;
            //}
            //if (connStDiff.SelectedSite != null)
            //{
            //    var site = _siteDict[connStDiff.SelectedSite];
            //    conn.SelectedSite = site;
            //}
            //if (connStDiff.SelectedBrowser != null)
            //{
            //    var browser = _browserDict[connStDiff.SelectedBrowser];
            //    conn.SelectedBrowser = browser;
            //}
            //if (connStDiff.IsConnected.HasValue)
            //{
            //    var isConnected = connStDiff.IsConnected.Value;
            //    conn.CanConnect = !isConnected;
            //    conn.CanDisconnect = isConnected;
            //}
            var conn = _connDict[connId];
            if (connStDiff.Name != null)
            {
                conn.Name = connStDiff.Name;
            }
            if (connStDiff.Input != null)
            {
                conn.Input = connStDiff.Input;
            }
            if (connStDiff.SelectedSite != null)
            {
                var site = _siteDict[connStDiff.SelectedSite];
                conn.SelectedSite = site;
            }
            if (connStDiff.SelectedBrowser != null)
            {
                var browser = _browserDict[connStDiff.SelectedBrowser];
                conn.SelectedBrowser = browser;
            }
            if (connStDiff.IsConnected.HasValue)
            {
                var isConnected = connStDiff.IsConnected.Value;
                conn.CanConnect = !isConnected;
                conn.CanDisconnect = isConnected;
            }
        }

        private readonly Dictionary<SiteId, SiteViewModel> _siteDict = new();
        private readonly Dictionary<BrowserProfileId, BrowserViewModel> _browserDict = new();
        private readonly Dictionary<ConnectionId, ConnectionViewModel> _connDict = new();
        private SiteViewModel GetSiteViewModel(SiteId id)
        {
            return _siteDict[id];
        }
        private BrowserViewModel GetBrowserViewModel(BrowserProfileId id)
        {
            return _browserDict[id];
        }

        private void OnConnectionAdded(ConnectionViewModel connection)
        {
            //TODO:プラグインに通知
            Debug.WriteLine($"ConnectionAdded:{connection.Id}");

            //var context = connection.GetCurrent();
            //SetDict(context);

            if (SelectedConnection == null)
            {
                SelectedConnection = connection;
            }
        }
        //private bool IsNicoGuid(Guid guid)
        //{
        //    return new Guid("5A477452-FF28-4977-9064-3A4BC7C63252").Equals(guid);
        //}
        //private bool IsMildomGuid(Guid guid)
        //{
        //    return new Guid("DBBA654F-0A5D-41CC-8153-5DB2D5869BCF").Equals(guid);
        //}
        //private bool IsTwitchGuid(Guid guid)
        //{
        //    return new Guid("22F7824A-EA1B-411E-85CA-6C9E6BE94E39").Equals(guid);
        //}
        //private bool IsMirrativGuid(Guid guid)
        //{
        //    return new Guid("6DAFA768-280D-4E70-8494-FD5F31812EF5").Equals(guid);
        //}
        //private void SetDict(ConnectionContext context)
        //{
        //    var newSiteContext = context.SiteContext;
        //    var newCommentProvider = context.CommentProvider;
        //    var userStore = _dic1[newSiteContext];
        //    if (!_dict2.ContainsKey(newCommentProvider))
        //    {
        //        _dict2.Add(newCommentProvider, userStore);
        //    }
        //}
        //private void OnConnectionDeleted(ConnectionName connectionName)
        //{
        //    //TODO:プラグインに通知
        //    Debug.WriteLine($"ConnectionDeleted:{connectionName.Guid}");
        //}
        string Name
        {
            get { return "MultiCommentViewer"; }
        }
        string Fullname
        {
            get { return $""; }
        }
        string AppDirName
        {
            get
            {
#if BETA
                return Name + "_Beta";
#elif Alpha
                return Name + "_Alpha";
#else
                return Name;
#endif
            }
        }
        private async Task CheckIfUpdateExists(bool isAutoCheck)
        {
            await Task.CompletedTask;
        }
        #endregion //Methods
        public event EventHandler<EventArgs>? CloseRequested;
        public void RequestClose()
        {
            OnCloseRequested(EventArgs.Empty);
        }

        protected virtual void OnCloseRequested(EventArgs e)
        {
            CloseRequested?.Invoke(this, e);
        }

        public async Task InitializeAsync()
        {
            //use this to test the exception handling
            //throw new NotImplementedException();
            await Task.CompletedTask;
        }

        #region Properties
        public ObservableCollection<MetadataViewModel> MetaCollection { get; } = new ObservableCollection<MetadataViewModel>();
        public ObservableCollection<PluginMenuItemViewModel> PluginMenuItemCollection { get; } = new ObservableCollection<PluginMenuItemViewModel>();
        private readonly ObservableCollection<IMcvCommentViewModel> _comments = new ObservableCollection<IMcvCommentViewModel>();


        private ConnectionViewModel? _selectedConnection;
        public ConnectionViewModel? SelectedConnection
        {
            get { return _selectedConnection; }
            set
            {
                _selectedConnection = value;
                //if (_selectedConnection == null)
                //{
                //    MessengerInstance.Send(new SetPostCommentPanel(null));
                //}
                //else
                //{
                //    MessengerInstance.Send(new SetPostCommentPanel(_selectedConnection.CommentPostPanel));
                //}
                RaisePropertyChanged();
            }
        }
        private string GetVersionNumber()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var ver = asm.GetName().Version!;
            var s = $"{ver.Major}.{ver.Minor}.{ver.Build}";
            return s;
        }
        private string GetAppName()
        {
            var asm = System.Reflection.Assembly.GetExecutingAssembly();
            var title = asm.GetName().Name!;
            return title;
        }
        private string GetUserAgent()
        {
            return $"{GetAppName()}/{GetVersionNumber()} contact-> twitter.com/kv510k";
        }
        public string Title
        {
            get
            {
                var s = $"{GetAppName()} v{GetVersionNumber()}";
#if BETA
                s += " (ベータ版)";
#elif ALPHA
                s += " (アルファ版)";
#elif DEBUG
                s += " (DEBUG)";
#endif
                return s;
            }
        }

        public bool ContainsUrl
        {
            get
            {
                return !string.IsNullOrEmpty(GetUrlFromSelectedComment());
            }
        }
        private string? GetUrlFromSelectedComment()
        {
            //var selectedComment = SelectedComment;
            //if (selectedComment == null)
            //{
            //    return null;
            //}
            //var message = selectedComment.MessageItems.ToText();
            //if (message == null)
            //{
            //    return null;
            //}
            //var match = Regex.Match(message, "(https?://([\\w-]+.)+[\\w-]+(?:/[\\w- ./?%&=]))?");
            //if (match.Success)
            //{
            //    return match.Groups[1].Value;
            //}
            return null;
        }
        private void OpenUrl()
        {
            //var url = GetUrlFromSelectedComment();
            //Process.Start(url);
            //SetSystemInfo("open: " + url, InfoType.Debug);
        }
        private void CopyComment()
        {
            //var message = SelectedComment.MessageItems.ToText();
            //try
            //{
            //    System.Windows.Clipboard.SetText(message);
            //}
            //catch (System.Runtime.InteropServices.COMException) { }
            //SetSystemInfo("copy: " + message, InfoType.Debug);
        }




        private readonly Color _myColor = new Color { A = 0xFF, R = 45, G = 45, B = 48 };
        private readonly IAdapter _adapter;

        public double RawMessagePostPanelHeight
        {
            get
            {
#if DEBUG
                return 50;
#else
                return 0;
#endif
            }
        }
        #endregion //Properties
        private bool IsDesignMode
        {
            get
            {
                return (bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(System.Windows.DependencyObject)).DefaultValue);
            }
        }

        public MainViewModel()
        {
            if (!IsDesignMode)
            {
                throw new NotSupportedException();
            }
            _adapter = new AdapterForDesignMode();
            //_siteVms = new List<SiteViewModel> { };// new SiteViewModel("DesignSite", Guid.NewGuid()) };
            //_browserVms = new List<BrowserViewModel>();// { new BrowserViewModel()}
            //AddConnection("test", "YouTubeLive", "https://google.com", "Chrome", false, Colors.Blue, Colors.Red);
            //SetSystemInfo("test", InfoType.Notice);
            var connId = new ConnectionId(Guid.NewGuid());
            var name = new ConnectionNameViewModel(connId, "abc");
            var items = new List<IMessagePart>()
                {
                    new CommonText("hey"),
                };
            var comment = new DesignTimeCommentViewModel(name, items);
            OnMessageReceived(comment);

        }
        private readonly Settings.SettingsContext _settingsContext;
        public CommentDataGridViewModel CommentVm { get; }
        public ConnectionsViewModel ConnectionsVm { get; }
        public MainViewModel(IAdapter adapter, Settings.SettingsContext settingsContext)
        {
            var collectionView = CollectionViewSource.GetDefaultView(_comments);
            CommentVm = new CommentDataGridViewModel(adapter, collectionView);
            ConnectionsVm = new ConnectionsViewModel(adapter);
            //_io = io;
            //_logger = logger;
            //_sitePluginLoader = sitePluginLoader;
            //_browserLoader = browserLoader;
            _adapter = adapter;
            _settingsContext = settingsContext;
            settingsContext.Applied += (s, e) =>
            {
                _adapter.OnSettingsApplied(e.ModifiedData);
            };
            MainViewClosingCommand = new RelayCommand<CancelEventArgs>(Closing);
            ShowOptionsWindowCommand = new RelayCommand(ShowOptionsWindow);
            ExitCommand = new RelayCommand(Exit);
            ShowWebSiteCommand = new RelayCommand(ShowWebSite);
            ShowDevelopersTwitterCommand = new RelayCommand(ShowDevelopersTwitter);
            CheckUpdateCommand = new RelayCommand(CheckUpdate);
            AddNewConnectionCommand = new RelayCommand(RequestAddNewConnection);
            RemoveSelectedConnectionCommand = new RelayCommand(RemoveSelectedConnection);
            ClearAllCommentsCommand = new RelayCommand(ClearAllComments);
            ShowUserInfoCommand = new RelayCommand(ShowUserInfo);
            ShowUserListCommand = new RelayCommand(ShowUserList);
            CommentCopyCommand = new RelayCommand(CopyComment);
            OpenUrlCommand = new RelayCommand(OpenUrl);

            adapter.ConnectionAdded += (s, e) => OnConnectionAdded(e.ConnId, e.ConnSt);
            adapter.ConnectionRemoved += (s, e) => OnConnectionRemoved(e.ConnId);
            adapter.SiteAdded += (s, e) => OnSiteAdded(e.SitePluginId, e.Name);
            adapter.SiteRemoved += (s, e) => OnSiteRemoved(e.SitePluginId);
            adapter.BrowserAdded += (s, e) => OnBrowserAdded(e.PluginId, e.Name, e.ProfileName);
            adapter.BrowserRemoved += (s, e) => OnBrowserRemoved(e.PluginId);
            adapter.ConnectionStatusChanged += (s, e) => OnConnectionStatusChanged(e.ConnId, e.ConnStDiff);
            adapter.MessageReceived += (s, e) => OnMessageReceived(e.Vm);
            adapter.SitePluginSettingsReceived += (s, e) => OnSitePluginSettingsReceived(e.PluginSettingsList);



            //_options.PropertyChanged += (s, e) =>
            //{
            //    switch (e.PropertyName)
            //    {
            //        case nameof(_options.MainViewLeft):
            //            RaisePropertyChanged(nameof(MainViewLeft));
            //            break;
            //        case nameof(_options.MainViewTop):
            //            RaisePropertyChanged(nameof(MainViewTop));
            //            break;
            //        case nameof(_options.MainViewHeight):
            //            RaisePropertyChanged(nameof(MainViewHeight));
            //            break;
            //        case nameof(_options.MainViewWidth):
            //            RaisePropertyChanged(nameof(MainViewWidth));
            //            break;
            //        case nameof(_options.IsShowThumbnail):
            //            RaisePropertyChanged(nameof(IsShowThumbnail));
            //            break;
            //        case nameof(_options.IsShowUsername):
            //            RaisePropertyChanged(nameof(IsShowUsername));
            //            break;
            //        case nameof(_options.IsShowConnectionName):
            //            RaisePropertyChanged(nameof(IsShowConnectionName));
            //            break;
            //        case nameof(_options.IsShowCommentId):
            //            RaisePropertyChanged(nameof(IsShowCommentId));
            //            break;
            //        case nameof(_options.IsShowMessage):
            //            RaisePropertyChanged(nameof(IsShowMessage));
            //            break;
            //        case nameof(_options.IsShowPostTime):
            //            RaisePropertyChanged(nameof(IsShowPostTime));
            //            break;
            //        case nameof(_options.IsShowInfo):
            //            RaisePropertyChanged(nameof(IsShowInfo));
            //            break;

            //        case nameof(_options.TitleBackColor):
            //            RaisePropertyChanged(nameof(TitleBackground));
            //            break;
            //        case nameof(_options.TitleForeColor):
            //            RaisePropertyChanged(nameof(TitleForeground));
            //            break;
            //        case nameof(_options.ViewBackColor):
            //            RaisePropertyChanged(nameof(ViewBackground));
            //            break;
            //        case nameof(_options.WindowBorderColor):
            //            RaisePropertyChanged(nameof(WindowBorderBrush));
            //            break;
            //        case nameof(_options.SystemButtonBackColor):
            //            RaisePropertyChanged(nameof(SystemButtonBackground));
            //            break;
            //        case nameof(_options.SystemButtonForeColor):
            //            RaisePropertyChanged(nameof(SystemButtonForeground));
            //            break;
            //        case nameof(_options.SystemButtonBorderColor):
            //            RaisePropertyChanged(nameof(SystemButtonBorderBrush));
            //            break;
            //        case nameof(_options.SystemButtonMouseOverBackColor):
            //            RaisePropertyChanged(nameof(SystemButtonMouseOverBackground));
            //            break;
            //        case nameof(_options.SystemButtonMouseOverForeColor):
            //            RaisePropertyChanged(nameof(SystemButtonMouseOverForeground));
            //            break;
            //        case nameof(_options.SystemButtonMouseOverBorderColor):
            //            RaisePropertyChanged(nameof(SystemButtonMouseOverBorderBrush));
            //            break;

            //        case nameof(_options.MenuBackColor):
            //            RaisePropertyChanged(nameof(MenuBackground));
            //            RaisePropertyChanged(nameof(ContextMenuBackground));
            //            break;
            //        case nameof(_options.MenuForeColor):
            //            RaisePropertyChanged(nameof(MenuForeground));
            //            RaisePropertyChanged(nameof(ContextMenuForeground));
            //            break;
            //        case nameof(_options.MenuPopupBorderColor):
            //            RaisePropertyChanged(nameof(MenuPopupBorderBrush));
            //            break;
            //        case nameof(_options.MenuSeparatorBackColor):
            //            RaisePropertyChanged(nameof(MenuSeparatorBackground));
            //            break;
            //        case nameof(_options.MenuItemCheckMarkColor):
            //            RaisePropertyChanged(nameof(MenuItemCheckMarkBrush));
            //            break;
            //        case nameof(_options.MenuItemMouseOverBackColor):
            //            RaisePropertyChanged(nameof(MenuItemMouseOverBackground));
            //            break;
            //        case nameof(_options.MenuItemMouseOverForeColor):
            //            RaisePropertyChanged(nameof(MenuItemMouseOverForeground));
            //            break;
            //        case nameof(_options.MenuItemMouseOverBorderColor):
            //            RaisePropertyChanged(nameof(MenuItemMouseOverBorderBrush));
            //            break;
            //        case nameof(_options.MenuItemMouseOverCheckMarkColor):
            //            RaisePropertyChanged(nameof(MenuItemMouseOverCheckMarkBrush));
            //            break;


            //        case nameof(_options.ButtonBackColor):
            //            RaisePropertyChanged(nameof(ButtonBackground));
            //            break;
            //        case nameof(_options.ButtonForeColor):
            //            RaisePropertyChanged(nameof(ButtonForeground));
            //            break;
            //        case nameof(_options.ButtonBorderColor):
            //            RaisePropertyChanged(nameof(ButtonBorderBrush));
            //            break;
            //        case nameof(_options.CommentListBackColor):
            //            RaisePropertyChanged(nameof(CommentListBackground));
            //            RaisePropertyChanged(nameof(ConnectionListBackground));
            //            break;
            //        case nameof(_options.CommentListHeaderBackColor):
            //            RaisePropertyChanged(nameof(CommentListHeaderBackground));
            //            RaisePropertyChanged(nameof(ConnectionListHeaderBackground));
            //            break;
            //        case nameof(_options.CommentListHeaderForeColor):
            //            RaisePropertyChanged(nameof(CommentListHeaderForeground));
            //            RaisePropertyChanged(nameof(ConnectionListHeaderForeground));
            //            break;
            //        case nameof(_options.CommentListHeaderBorderColor):
            //            RaisePropertyChanged(nameof(CommentListHeaderBorderBrush));
            //            RaisePropertyChanged(nameof(ConnectionListHeaderBorderBrush));
            //            break;
            //        case nameof(_options.CommentListBorderColor):
            //            RaisePropertyChanged(nameof(CommentListBorderBrush));
            //            RaisePropertyChanged(nameof(ConnectionListBorderBrush));
            //            break;
            //        case nameof(_options.CommentListSeparatorColor):
            //            RaisePropertyChanged(nameof(CommentListSeparatorBrush));
            //            RaisePropertyChanged(nameof(ConnectionListSeparatorBrush));
            //            break;
            //        //case nameof(_options.ConnectionListBackColor):
            //        //    RaisePropertyChanged(nameof(ConnectionListBackground));
            //        //    break;
            //        //case nameof(_options.ConnectionListHeaderBackColor):
            //        //    RaisePropertyChanged(nameof(ConnectionListHeaderBackground));
            //        //    break;
            //        //case nameof(_options.ConnectionListHeaderForeColor):
            //        //    RaisePropertyChanged(nameof(ConnectionListHeaderForeground));
            //        //    break;
            //        case nameof(_options.ConnectionListRowBackColor):
            //            RaisePropertyChanged(nameof(ConnectionListRowBackground));
            //            break;

            //        case nameof(_options.ScrollBarBackColor):
            //            RaisePropertyChanged(nameof(ScrollBarBackground));
            //            break;
            //        case nameof(_options.ScrollBarBorderColor):
            //            RaisePropertyChanged(nameof(ScrollBarBorderBrush));
            //            break;
            //        case nameof(_options.ScrollBarThumbBackColor):
            //            RaisePropertyChanged(nameof(ScrollBarThumbBackground));
            //            break;
            //        case nameof(_options.ScrollBarThumbMouseOverBackColor):
            //            RaisePropertyChanged(nameof(ScrollBarThumbMouseOverBackground));
            //            break;
            //        case nameof(_options.ScrollBarThumbPressedBackColor):
            //            RaisePropertyChanged(nameof(ScrollBarThumbPressedBackground));
            //            break;


            //        case nameof(_options.ScrollBarButtonBackColor):
            //            RaisePropertyChanged(nameof(ScrollBarButtonBackground));
            //            break;
            //        case nameof(_options.ScrollBarButtonForeColor):
            //            RaisePropertyChanged(nameof(ScrollBarButtonForeground));
            //            break;
            //        case nameof(_options.ScrollBarButtonBorderColor):
            //            RaisePropertyChanged(nameof(ScrollBarButtonBorderBrush));
            //            break;


            //        case nameof(_options.ScrollBarButtonDisabledBackColor):
            //            RaisePropertyChanged(nameof(ScrollBarButtonDisabledBackground));
            //            break;
            //        case nameof(_options.ScrollBarButtonDisabledForeColor):
            //            RaisePropertyChanged(nameof(ScrollBarButtonDisabledForeground));
            //            break;
            //        case nameof(_options.ScrollBarButtonDisabledBorderColor):
            //            RaisePropertyChanged(nameof(ScrollBarButtonDisabledBorderBrush));
            //            break;

            //        case nameof(_options.ScrollBarButtonMouseOverBackColor):
            //            RaisePropertyChanged(nameof(ScrollBarButtonMouseOverBackground));
            //            break;
            //        case nameof(_options.ScrollBarButtonPressedBackColor):
            //            RaisePropertyChanged(nameof(ScrollBarButtonPressedBackground));
            //            break;
            //        case nameof(_options.ScrollBarButtonPressedBorderColor):
            //            RaisePropertyChanged(nameof(ScrollBarButtonPressedBorderBrush));
            //            break;

            //        case nameof(_options.IsEnabledSiteConnectionColor):
            //        case nameof(_options.SiteConnectionColorType):
            //            RaisePropertyChanged(nameof(ConnectionColorColumnWidth));
            //            RaisePropertyChanged(nameof(IsShowConnectionsViewConnectionBackground));
            //            RaisePropertyChanged(nameof(IsShowConnectionsViewConnectionForeground));
            //            break;
            //        case nameof(_options.IsTopmost):
            //            _pluginManager.OnTopmostChanged(_options.IsTopmost);
            //            RaisePropertyChanged(nameof(Topmost));
            //            break;

            //        case nameof(_options.IsShowHorizontalGridLine):
            //            break;
            //        case nameof(_options.HorizontalGridLineColor):
            //            RaisePropertyChanged(nameof(HorizontalGridLineBrush));
            //            break;
            //        case nameof(_options.IsShowVerticalGridLine):
            //            break;
            //        case nameof(_options.VerticalGridLineColor):
            //            RaisePropertyChanged(nameof(VerticalGridLineBrush));
            //            break;

            //        case nameof(_options.IsShowMetaConnectionName):
            //            RaisePropertyChanged(nameof(IsShowMetaConnectionName));
            //            break;
            //        case nameof(_options.IsShowMetaTitle):
            //            RaisePropertyChanged(nameof(IsShowMetaTitle));
            //            break;
            //        case nameof(_options.IsShowMetaElapse):
            //            RaisePropertyChanged(nameof(IsShowMetaElapse));
            //            break;
            //        case nameof(_options.IsShowMetaCurrentViewers):
            //            RaisePropertyChanged(nameof(IsShowMetaCurrentViewers));
            //            break;
            //        case nameof(_options.IsShowMetaTotalViewers):
            //            RaisePropertyChanged(nameof(IsShowMetaTotalViewers));
            //            break;
            //        case nameof(_options.IsShowMetaActive):
            //            RaisePropertyChanged(nameof(IsShowMetaActive));
            //            break;
            //        case nameof(_options.IsShowMetaOthers):
            //            RaisePropertyChanged(nameof(IsShowMetaOthers));
            //            break;
            //    }
            //};
            //RaisePropertyChanged(nameof(Topmost));
        }

        private void OnSitePluginSettingsReceived(List<IPluginSettings> pluginSettingsList)
        {
            //viewmodelを作る
            //viewmodelを渡してviewを表示
            _settingsContext.SetPluginSettings(pluginSettingsList);
            WeakReferenceMessenger.Default.Send(new Settings.ShowSettingsViewMessage(_settingsContext));
        }

        private void OnMessageReceived(IMcvCommentViewModel vm)
        {
            _comments.Add(vm);
        }

        private void OnBrowserRemoved(BrowserProfileId browserId)
        {
            var browser = GetBrowserViewModel(browserId);
            _browserDict.Remove(browser.Id);
            Browsers.Remove(browser);
        }

        private void OnBrowserAdded(BrowserProfileId browserId, string name, string? profileName)
        {
            if (_browserDict.ContainsKey(browserId))
            {
                return;
            }
            var browser = new BrowserViewModel(browserId, name, profileName);
            _browserDict.Add(browserId, browser);
            Browsers.Add(browser);
        }

        private void OnSiteRemoved(SiteId sitePluginId)
        {
            var site = GetSiteViewModel(sitePluginId);
            _siteDict.Remove(site.Id);
            Sites.Remove(site);
        }
        ObservableCollection<SiteViewModel> Sites { get; } = new ObservableCollection<SiteViewModel>();
        ObservableCollection<BrowserViewModel> Browsers { get; } = new ObservableCollection<BrowserViewModel>();

        private void OnSiteAdded(SiteId sitePluginId, string name)
        {
            if (_siteDict.ContainsKey(sitePluginId))
            {
                return;
            }
            var site = new SiteViewModel(sitePluginId, name);
            _siteDict.Add(sitePluginId, site);
            Sites.Add(site);
        }

        //private async void PluginManager_PluginAdded(object sender, IPlugin e)
        //{
        //    try
        //    {
        //        await _dispatcher.BeginInvoke((Action)(() =>
        //        {
        //            var vm = new PluginMenuItemViewModel(e, _options);
        //            _pluginMenuItemDict.Add(e, vm);
        //            PluginMenuItemCollection.Add(vm);
        //        }), DispatcherPriority.Normal);
        //    }
        //    catch (Exception ex)
        //    {
        //        LogException(ex);
        //    }
        //}
        //private readonly Dictionary<string, UserViewModel> _userViewModelDict = new Dictionary<string, UserViewModel>();
        //private readonly ObservableCollection<UserViewModel> _userViewModels = new ObservableCollection<UserViewModel>();
        //private async void UserStoreManager_UserAdded(object sender, IUser e)
        //{
        //    //IUserStore.UserAddedに配信サイトかSiteContextを識別するものが必要かも
        //    await _dispatcher.BeginInvoke((Action)(() =>
        //    {
        //        try
        //        {
        //            var uvm = CreateUserViewModel(e);
        //            _userViewModelDict.Add(e.UserId, uvm);
        //            _userViewModels.Add(uvm);
        //        }
        //        catch (Exception ex)
        //        {
        //            LogException(ex);
        //        }
        //    }), DispatcherPriority.Normal);
        //}
        //private UserViewModel CreateUserViewModel(IUser user)
        //{
        //    var uvm = new UserViewModel(user, _options);
        //    return uvm;
        //}
        public void ShowUserInfo(string userId)
        {
            //    if (!_userViewModelDict.TryGetValue(userId, out var uvm))
            //    {
            //        Debug.WriteLine($"{nameof(_userViewModelDict)}にuserId={userId}が存在しない");
            //        return;
            //    }
            //    var view = new CollectionViewSource { Source = _comments }.View;
            //    view.Filter = obj =>
            //    {
            //        if (!(obj is IMcvCommentViewModel cvm))
            //        {
            //            return false;
            //        }
            //        return cvm.UserId == userId;
            //    };
            //    uvm.Comments = view;
            //    MessengerInstance.Send(new ShowUserViewMessage(uvm));
        }
        private void ShowUserInfo()
        {

            var current = CommentVm.SelectedComment;
            if (current is null) return;
            var userId = current.UserId;
            if (string.IsNullOrEmpty(userId))
            {
                Debug.WriteLine("UserIdがnull");
                return;
            }
            var userVm = new UserViewModel();
            var view = new CollectionViewSource { Source = _comments }.View;
            view.Filter = obj =>
            {
                if (!(obj is IMcvCommentViewModel cvm))
                {
                    return false;
                }
                return cvm.UserId == userId;
            };
            var userInfoVm = new UserInfoViewModel(view, userVm, _adapter);
            WeakReferenceMessenger.Default.Send(new ShowUserInfoViewMessage(userInfoVm));
        }
        private void ShowUserList()
        {
            //MessengerInstance.Send(new ShowUserListViewMessage(_userViewModels, this, _options));
            //WeakReferenceMessenger.Default.Send(new LoggedInUserChangedMessage(user));
        }
        private async void CheckUpdate()
        {
            try
            {
                await CheckIfUpdateExists(false);
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
        private void ShowDevelopersTwitter()
        {
            try
            {
                System.Diagnostics.Process.Start("https://twitter.com/kv510k");
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
        private void ShowWebSite()
        {
            try
            {
                System.Diagnostics.Process.Start("https://ryu-s.github.io/app/multicommentviewer");
            }
            catch (Exception ex)
            {
                LogException(ex);
            }
        }
        private void Exit()
        {
            this.RequestClose();
        }
    }
}

