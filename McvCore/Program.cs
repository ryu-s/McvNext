using Akka.Actor;
using Akka.Util.Internal;
using Plugin;
//using Common;

//using Mcv.SitePlugin;
//using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
//using ToCore = Mcv.Messages.Core;
//using ToPlugin = Mcv.Messages.Plugin;
//using ToSite = Mcv.Messages.SitePlugin;
using ToCore = Plugin.Message.ToCore;
using ToPlugin = Plugin.Message.ToPlugin;
namespace Mcv.Core
{
    class PluginLoadContext : AssemblyLoadContext
    {
        public PluginLoadContext(string? name, bool isCollectible = false) : base(name, isCollectible)
        {
        }
        protected override Assembly? Load(AssemblyName assemblyName)
        {
            var asm = base.Load(assemblyName);
            return asm;
        }
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            return base.LoadUnmanagedDll(unmanagedDllName);
        }
    }
    class ZippedPluginContext2
    {
        public ZippedPluginContext2(IPlugin plugin, AssemblyLoadContext loadContext, ZipArchive zipArchive)
        {
            Plugin = plugin;
            LoadContext = loadContext;
            ZipArchive = zipArchive;
        }
        public IPlugin Plugin { get; }
        public AssemblyLoadContext LoadContext { get; }
        public ZipArchive ZipArchive { get; }//AssemblyLoadContext::Resolvingで使うからDisposeされないように一応持っておく。最後にCloseとかDisposeは多分しなくても大丈夫
    }
    static class PluginLoader
    {
        public static List<ZippedPluginContext2> LoadPlugins(string pluginDirPath)
        {
            var fs = Directory.GetFiles(pluginDirPath).Where(f => f.EndsWith(".zip"));
            var plugins = fs.Select(f => LoadPlugin(f)).Where(pc => pc != null).Cast<ZippedPluginContext2>().ToList();
            return plugins;
        }
        public static ZippedPluginContext2? LoadPlugin(string zipFilePath)
        {
            var zipFileNameWithoutExt = Path.GetFileNameWithoutExtension(zipFilePath);
            var zipToOpen = new FileStream(zipFilePath, FileMode.Open);
            ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read);
            foreach (var entry in archive.Entries)
            {
                var path = entry.FullName;
                if (!Regex.IsMatch(path, ".+Plugin\\.dll$"))
                {
                    continue;
                }
                var dllPath = path;
                var pdbPath = path.Replace(".dll", ".pdb");//名前の途中に".dll"があったらそれも置換されちゃうけどまあいいや

                var innerDir = Path.GetDirectoryName(dllPath) ?? "";

                var pdbEntry = archive.GetEntry(pdbPath);
                var dllBytes = ZipEntry2ByteArray(entry);
                var pdbBytes = pdbEntry != null ? ZipEntry2ByteArray(pdbEntry) : null;

                using var dllMs = new MemoryStream(dllBytes);
                using var pdbMs = pdbBytes != null ? new MemoryStream(pdbBytes) : null;

                var pluginContext = AssemblyLoadContext.Default;// new PluginLoadContext("plugin", true);//AssemblyLoadContextのバグでWPFの依存dllの解決要求がDefaultに行ってしまう。早く直してほしい。

                var asm = pluginContext.LoadFromStream(dllMs, pdbMs);
                pluginContext.Resolving += (s, e) =>
                {
                    //現状、AssemblyLoadContext.Default.Resolvingを購読しているから、McvCoreとか全てのプラグインの解決要求が来る。どうにかして選別できないだろうか。
                    var dllName = string.Join($"{Path.DirectorySeparatorChar}", innerDir, e.Name + ".dll");
                    var pdbName = string.Join($"{Path.DirectorySeparatorChar}", innerDir, e.Name + ".pdb");
                    var dllEntry = archive.GetEntry(dllName)!;
                    var pdbEntry = archive.GetEntry(pdbName)!;
                    if (dllEntry == null)
                    {
                        return null;
                    }

                    var dllBytes = ZipEntry2ByteArray(dllEntry);
                    var pdbBytes = pdbEntry != null ? ZipEntry2ByteArray(pdbEntry) : null;


                    using var dllMs = new MemoryStream(dllBytes);
                    using var pdbMs = pdbBytes != null ? new MemoryStream(pdbBytes) : null;
                    var asm = pluginContext.LoadFromStream(dllMs, pdbMs);
                    return asm;
                };
                var type = typeof(IPlugin);
                var types = asm.GetTypes();
                var types2 = types.Where(p => type.IsAssignableFrom(p) && p.IsClass).ToList();

                foreach (var cckpz in types2)
                {
                    if (cckpz == null) continue;
                    var instance = (IPlugin?)Activator.CreateInstance(cckpz);
                    if (instance != null)
                    {
                        return new ZippedPluginContext2(instance, pluginContext, archive);
                    }
                }
            }
            return null;
        }
        static byte[] ZipEntry2ByteArray(ZipArchiveEntry entry)
        {
            using var ms = new MemoryStream();
            using var stream = entry.Open();
            stream.CopyTo(ms);
            var bytes = ms.ToArray();
            return bytes;
        }
    }
    //core
    //-SplashWindow
    //-SitePluginManager
    //--SitePlugin
    //-UserStore
    //PluginManager //普通のdispathcerのActorから.WithDispatcher("akka.actor.synchronized-dispatcher")を作成しようとするとInvalidOperationExceptionが投げられた。これはtop-levelにしないといけなさそう。
    //-Plugin

    class Program
    {
        [STAThread]
        //static async Task Main(string[] args)
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            var app = new AppNoStartupUri
            {
                ShutdownMode = ShutdownMode.OnExplicitShutdown
            };
            app.InitializeComponent();
            SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext());

            var p = new Program();
            p.ExitRequested += (sender, e) =>
            {
                app.Shutdown();
            };

            //pluginsディレクトリが無ければ作成する
            Directory.CreateDirectory("plugins");

            //settingsディレクトリが無ければ作成する
            Directory.CreateDirectory("settings");

            var actorSystem = ActorSystem.Create("mcv");
            var core = actorSystem.ActorOf(Props.Create(() => new CoreActor(app)).WithDispatcher("akka.actor.synchronized-dispatcher"));
            core.Tell(new LoadPlugins());

            app.Run();

        }

        public event EventHandler<EventArgs>? ExitRequested;
        void ViewModel_CloseRequested(object? sender, EventArgs e)
        {
            OnExitRequested(EventArgs.Empty);
        }

        protected virtual void OnExitRequested(EventArgs e)
        {
            ExitRequested?.Invoke(this, e);
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;

        }
    }
    class PluginContext
    {
        public PluginContext(IPlugin plugin, IMcvActor actor)
        {
            Plugin = plugin;
            Actor = actor;
            LoadContext = null;
        }
        public PluginContext(IPlugin plugin, IMcvActor actor, AssemblyLoadContext? loadContext)
        {
            Plugin = plugin;
            Actor = actor;
            LoadContext = loadContext;
        }

        public IPlugin Plugin { get; }
        public IMcvActor Actor { get; }
        public AssemblyLoadContext? LoadContext { get; }
        public List<IPluginType> Types { get; } = new List<IPluginType>();
    }
    interface ICore
    {
        void OnLoadPlugins();
        void OnRequestAddNormalConnection();
        void OnRequestChangeConnectionStatus(ConnectionId connId, IConnectionStatusDiff diff);
        void OnRequestRemoveConnection(ConnectionId connId);
        void OnAskConnectionStatus(ConnectionId connId, IMcvActor sender);
        void OnAskPlugins(IMcvActor sender);
        void OnHello(PluginId pluginId, List<IPluginType> pluginTypes);
        void OnNotifyMessageReceived(PluginId src, ConnectionId connId, ISiteMessage message);
        void OnUnhandled(object message);
        void OnAskConnections(IMcvActor sender);
        Task OnAskSitePluginSettings(IMcvActor sender);
        void OnRequestUpdateSettingsList(List<IPluginSettingsDiff> modifiedSettingsList);
        void OnRequestSaveSettings(string pluginName, string data);
    }
    class Core : ICore
    {
        //2021/09/28
        //Askのhandlerの戻り値をvoidではなくAnserにすれば綺麗では？
        //→Requestの場合、複数のNotifyを発行することがあって、単純にreturn一つでは対応できない。いやList<(Notify,Receiver)>なら可？

        private readonly Dictionary<PluginId, PluginContext> _plugins = new();
        private readonly Dictionary<SiteId, PluginContext> _sites = new();
        private readonly Dictionary<Type, PluginId> _connManagerDict = new();
        private readonly Dictionary<ConnectionId, IConnectionStatus> _connDict = new();
        private SiteId _defaultSitePluginId => _unselectedSitePlugin.PluginType.SiteId;
        private BrowserProfileId _defaultBrowserPluginId => _unselectedBrowserPlugin.PluginType.BrowserProfileId;
        private readonly IActorFactory _work;
        private readonly UnselectedSitePluginMain _unselectedSitePlugin = new UnselectedSitePluginMain();
        private readonly UnselectedBrowserPluginMain _unselectedBrowserPlugin = new UnselectedBrowserPluginMain();
        public Core(IActorFactory work)
        {
            _work = work;
            AddPlugin(_unselectedSitePlugin);
            AddPlugin(_unselectedBrowserPlugin);
        }
        private void Log(string str)
        {
            Debug.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")} {str}");
        }
        private void Log(Exception ex)
        {

        }
        public void OnRequestAddNormalConnection()
        {
            var id = new ConnectionId(Guid.NewGuid());
            var conn = new NormalConnection()
            {
                Name = GetDefaultName(_connDict.Values),
                Input = "",
                IsConnected = false,
                SelectedSite = _defaultSitePluginId,
                SelectedBrowser = _defaultBrowserPluginId,
            };
            _connDict.Add(id, conn);
            TellChildren(new ToPlugin.NotifyConnectionAdded(typeof(NormalConnection), id));
        }
        private void TellChildren(object message)
        {
            GetPlugins().ForEach(c => c.Tell(message));
        }
        public void OnAskConnectionStatus(ConnectionId connId, IMcvActor sender)
        {
            var conn = _connDict[connId];
            sender.Tell(new ToPlugin.AnswerConnectionStatus(connId, conn));
        }
        public void OnAskConnections(IMcvActor sender)
        {
            var connections = _connDict.Select(kv => new ToPlugin.ConnectionInfo(kv.Key, kv.Value)).ToList();
            sender.Tell(new ToPlugin.AnswerConnections(connections));
        }
        public void OnAskPlugins(IMcvActor sender)
        {
            var loadedPlugins = _plugins.Values.Where(p => p.Plugin.Id != _unselectedSitePlugin.Id && p.Plugin.Id != _unselectedBrowserPlugin.Id);
            var answer = new ToPlugin.AnswerPlugins(loadedPlugins.Select(p => new ToPlugin.PluginInfo(p.Plugin.Id, p.Plugin.Name, p.Types)).ToList());
            sender.Tell(answer);
        }
        public void AddPlugin(IPlugin plugin)
        {
            var actor = _work.CreateActor(plugin.GetProps());//.CreateActor(_context);
            var info = new PluginContext(plugin, actor);
            var id = plugin.Id;
            _plugins.Add(id, info);
            Log($"プラグイン\"{plugin.Name}\"の読み込み完了");
            actor.Tell(new ToPlugin.Hello());
        }
        public void AddPlugin(ZippedPluginContext2 pluginContext)
        {
            var plugin = pluginContext.Plugin;
            var actor = _work.CreateActor(plugin.GetProps());//.CreateActor(_context);
            var info = new PluginContext(plugin, actor, pluginContext.LoadContext);
            var id = plugin.Id;
            _plugins.Add(id, info);
            Log($"プラグイン\"{plugin.Name}\"の読み込み完了");
            actor.Tell(new ToPlugin.Hello());
        }
        private IEnumerable<IMcvActor> GetPlugins()
        {
            return _plugins.Select(n => n.Value.Actor);
        }
        private IEnumerable<IMcvActor> GetSitePlugins()
        {
            return _plugins.Where(n =>
            {
                foreach (var type in n.Value.Types)
                {
                    if (type is PluginTypeSite)
                    {
                        return true;
                    }
                }
                return false;
            }).Select(n => n.Value.Actor);
        }
        private static string GetDefaultName(IEnumerable<IConnectionStatus> conns)
        {
            for (var i = 1; i < int.MaxValue; i++)
            {
                var test = $"#{i}";
                if (!conns.Select(c => c.Name).Contains(test))
                {
                    return test;
                }
            }
            return "";
        }
        private void CloseSiteConnection(SiteId siteId, ConnectionId connId)
        {
            var sitePlugin = _sites[siteId];
            sitePlugin.Actor.Tell(new ToPlugin.RequestCloseConnectionMessage(connId));
        }
        private void OpenSiteConnection(SiteId siteId, ConnectionId connId)
        {
            var sitePlugin = _sites[siteId];
            sitePlugin.Actor.Tell(new ToPlugin.RequestOpenConnectionMessage(siteId, connId));
        }
        public void OnRequestChangeConnectionStatus(ConnectionId connId, IConnectionStatusDiff diff)
        {
            var conn = _connDict[connId];

            if (conn is INormalConnection normal && diff is INormalConnectionDiff normalDiff)
            {
                var beforeSite = normal.SelectedSite;
                var changed = (INormalConnectionDiff)conn.SetDiff(diff);
                Debug.WriteLine($"Core::OnRequestChangeConnectionStatus() {changed}");
                if (changed.SelectedSite != null)
                {
                    //以前のサイトを閉じる
                    CloseSiteConnection(beforeSite, connId);

                    //新しいサイトを開く
                    OpenSiteConnection(changed.SelectedSite, connId);
                }
                if (changed.IsConnected == true)
                {
                    var sitePlugin = _sites[normal.SelectedSite];
                    sitePlugin.Actor.Tell(new ToPlugin.RequestStartConnectionMessage(connId, normal));
                }
                else if (changed.IsConnected == false)
                {
                    var sitePlugin = _sites[normal.SelectedSite];
                    sitePlugin.Actor.Tell(new ToPlugin.RequestStopConnectionMessage(connId));
                }
                if (changed.IsChanged())
                {
                    TellChildren(new ToPlugin.NotifyConnectionStatusChanged(connId, changed));
                }
            }
        }

        public void OnLoadPlugins()
        {
            var plugins = PluginLoader.LoadPlugins("plugins");
            //AddPlugin(new BouyomiChanPlugin.BouyomiChanPlugin());
            foreach (var plugin in plugins)
            {
                AddPlugin(plugin);
            }
            //AddPlugin(new Mcv.Plugin.MainViewPlugin.MainViewPlugin());
            AddPlugin(new UnknownLiveSiteCommentProviderPlugin.PluginMain());
            AddPlugin(new Mcv.SitePlugin.YouTubeLive.YouTubeSitePlugin());
            AddPlugin(new Mcv.Plugin.A17Live.A17LiveSitePlugin());
        }

        public void OnHello(PluginId pluginId, List<IPluginType> pluginTypes)
        {
            //プラグインの仕様
            //1つのプラグインに複数の機能を持たせることができる。
            //1つのプラグインに複数のサイトプラグイン機能を持たせることもできる。
            //この時PluginIdだけだとどのサイトを指定しているのか分からなくなるからSiteIdを導入する。
            //
            //プラグインのライフサイクル
            //Coreが読み込む
            //CoreがHelloメッセージを送る
            //PluginはPluginTypeを返答


            var info = _plugins[pluginId];
            info.Types.Clear();//必要無いはずだけど一応やっておく
            info.Types.AddRange(pluginTypes);
            //var b = _plugins.SelectMany(p => p.Value.Types.Select(k => new ToPlugin.PluginInfo(p.Key, p.Value.Plugin.Name, k))).ToList();
            //info.Actor.Tell(new ToPlugin.NotifyPlugins(b));

            //if (IsSitePlugin(info.Types))
            //{
            //    GetPlugins().ForEach(a => a.Tell(new ToPlugin.NotifySitePluginAdded(m.PluginId, info.Plugin.Name)));
            //}
            //if (IsNormalPlugin(info.Types))
            //{
            //    GetPlugins().ForEach(a => a.Tell(new ToPlugin.NotifyNormalPluginAdded(m.PluginId, info.Plugin.Name)));
            //}
            //if (IsBrowserPlugin(info.Types))
            //{
            //    GetPlugins().ForEach(a => a.Tell(new ToPlugin.NotifyBrowserAdded(m.PluginId, info.Plugin.Name)));
            //}
            Debug.WriteLine($"プラグイン \"{info.Plugin.Name}\" からHelloを受信");
            foreach (var type in info.Types)
            {
                if (type is PluginTypeSite site)
                {
                    _sites.Add(site.SiteId, info);
                    TellChildren(new ToPlugin.NotifySitePluginAdded(site.SiteId, site.SiteName));
                }
                else if (type is PluginTypeNormal normal)
                {
                    TellChildren(new ToPlugin.NotifyNormalPluginAdded(pluginId, info.Plugin.Name));
                }
                else if (type is PluginTypeMainView mainView)
                {

                }
                else if (type is PluginTypeCookieStore cookieStore)
                {
                    TellChildren(new ToPlugin.NotifyBrowserAdded(cookieStore.BrowserProfileId, cookieStore.BrowserName, cookieStore.ProfileName));
                }
                else if (type is PluginTypeConnectionManager connMgr)
                {

                }
                else
                {
                    throw new NotImplementedException();
                }
            }
        }

        public void OnNotifyMessageReceived(PluginId src, ConnectionId connId, ISiteMessage message)
        {
            TellChildren(new ToPlugin.NotifyMessageReceived(src, connId, message));
        }

        public void OnRequestRemoveConnection(ConnectionId connId)
        {
            var conn = _connDict[connId];
            //TODO:selectedSiteを閉じる
            if (conn is INormalConnection normal)
            {
                CloseSiteConnection(normal.SelectedSite, connId);
            }
            else
            {
                throw new NotImplementedException();
            }

            _connDict.Remove(connId);
            TellChildren(new ToPlugin.NotifyConnectionRemoved(connId));
        }

        public void OnUnhandled(object message)
        {
            Debug.WriteLine($"Core unhandled: {message}");
        }

        public async Task OnAskSitePluginSettings(IMcvActor sender)
        {
            //全サイトプラグインに設定を聞く。タイムアウトは１秒くらい？
            //各プラグインは固有の設定情報を持っている。だから複数のプラグインが同じ情報を持っていることは無い。似て非なるもの。
            var tasks = new List<Task<ToCore.AnswerPluginSettings>>();
            foreach (var site in GetSitePlugins())
            {
                var t = site.Ask<ToCore.AnswerPluginSettings>(new ToPlugin.AskPluginSettings());
                tasks.Add(t);
            }
            var tasksT = Task.WhenAll(tasks);
            var timeoutT = Task.Delay(new TimeSpan(0, 0, 1));
            var tasksTimeoutT = await Task.WhenAny(tasksT, timeoutT);
            if (tasksTimeoutT == tasksT)
            {

            }
            else
            {
                //timeoutがあった
            }
            var settings = new List<IPluginSettings>();
            foreach (var t in tasks)
            {
                if (!t.IsCompleted) continue;
                var ans = await t;
                settings.Add(ans.PluginSettings);
            }

            sender.Tell(new ToPlugin.AnswerSitePluginSettings(settings));
        }

        public void OnRequestUpdateSettingsList(List<IPluginSettingsDiff> modifiedSettingsList)
        {
            //一つずつ、プラグインに対してブロードキャストする。
            foreach (var settings in modifiedSettingsList)
            {
                TellChildren(new ToPlugin.RequestUpdateSettings(settings));
            }
        }

        public void OnRequestSaveSettings(string pluginName, string data)
        {
            Debug.WriteLine($"Core::OnRequestSaveSettings() PluginName={pluginName}, data={data}");
        }
    }
    interface IActorFactory
    {
        IMcvActor CreateActor(Props props);
    }
    /// <summary>
    /// CoreからActor要素を完全排除する者
    /// </summary>
    class ActorFactory : IActorFactory
    {
        private readonly IActorContext _context;

        //public void Tell(SubActor actor, object message)
        //{

        //    actor.Tell(message);
        //}
        //public void Tell(IEnumerable<IActorRef> actors, object message)
        //{
        //    actors.ForEach(a => a.Tell(message));
        //}

        public IMcvActor CreateActor(Props props)
        {
            return new SubActor(_context.ActorOf(props));
        }
        public ActorFactory(IActorContext context)
        {
            _context = context;
        }
    }
    /// <summary>
    /// IActorRefのラッパー
    /// </summary>
    interface IMcvActor
    {
        void Tell(object message);
        Task<TAnswer> Ask<TAnswer>(object message);
    }
    class SubActor : IMcvActor
    {
        private readonly IActorRef _actor;

        public SubActor(IActorRef actor)
        {
            _actor = actor;
        }

        public Task<T> Ask<T>(object message)
        {
            return _actor.Ask<T>(message);
        }

        public void Tell(object message)
        {
            _actor.Tell(message);
        }
    }
    class CoreActor : ReceiveActor
    {
        private readonly ICore _core;
        private readonly Application _app;

        protected virtual ICore CreateCore()
        {
            return new Core(new ActorFactory(Context));
        }
        public CoreActor(Application app)
        {
            _core = CreateCore();
            _app = app;
            Receive<ToCore.RequestCloseApp>(m =>
            {
                _app.Shutdown();
            });
            Receive<LoadPlugins>(m =>
            {
                _core.OnLoadPlugins();
            });
            Receive<ToCore.RequestAddNormalConnection>(m =>
            {
                _core.OnRequestAddNormalConnection();
            });
            Receive<ToCore.RequestAddConnection>(m =>
            {
                //TODO:NormalConnectionは一時的にReceive<ToCore.RequestAddNormalConnection>で受付中。
                //var type = m.ConnType;
                //var managerPlugin = _connManagerDict[type];
                //var id = new ConnectionId(Guid.NewGuid());
            });
            Receive<ToCore.RequestChangeConnectionStatus>(m =>
            {
                _core.OnRequestChangeConnectionStatus(m.ConnId, m.Diff);
            });
            Receive<ToCore.RequestRemoveConnection>(m =>
            {
                _core.OnRequestRemoveConnection(m.ConnId);
            });
            Receive<ToCore.AskConnections>(m =>
            {
                _core.OnAskConnections(new SubActor(Sender));
            });
            Receive<ToCore.AskConnectionStatus>(m =>
            {
                _core.OnAskConnectionStatus(m.ConnId, new SubActor(Sender));
            });
            Receive<ToCore.AskPlugins>(m =>
            {
                _core.OnAskPlugins(new SubActor(Sender));
            });
            Receive<ToCore.Hello>(m =>
            {
                _core.OnHello(m.PluginId, m.PluginTypes);
            });
            Receive<ToCore.NotifyMessageReceived>(m =>
            {
                _core.OnNotifyMessageReceived(m.Src, m.ConnId, m.Message);
            });
            ReceiveAsync<ToCore.AskSitePluginSettings>(async m =>
            {
                await _core.OnAskSitePluginSettings(new SubActor(Sender));
            });
            Receive<ToCore.RequestUpdateSettingsList>(m =>
            {
                _core.OnRequestUpdateSettingsList(m.ModifiedSettingsList);
            });
            Receive<ToCore.RequestSaveSettings>(m =>
            {
                _core.OnRequestSaveSettings(m.PluginName, m.Data);
            });
            Receive<ToCore.AddLog>(m =>
            {

            });
        }
        protected override void Unhandled(object message)
        {
            _core.OnUnhandled(message);
        }

        private bool IsSitePlugin(List<IPluginType>? types)
        {
            if (types == null) return false;
            foreach (var type in types)
            {
                if (type is PluginTypeSite)
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsMainViewPlugin(List<IPluginType>? types)
        {
            if (types == null) return false;
            foreach (var type in types)
            {
                if (type is PluginTypeMainView)
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsNormalPlugin(List<IPluginType>? types)
        {
            if (types == null) return false;
            foreach (var type in types)
            {
                if (type is PluginTypeNormal)
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsBrowserPlugin(List<IPluginType>? types)
        {
            if (types == null) return false;
            foreach (var type in types)
            {
                if (type is PluginTypeCookieStore)
                {
                    return true;
                }
            }
            return false;
        }

    }
    class LoadPlugins { }


}

