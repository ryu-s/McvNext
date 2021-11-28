using Akka.Actor;
using Mcv.Plugin.MainViewPlugin.Settings;
using Plugin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ToCore = Plugin.Message.ToCore;
using ToPlugin = Plugin.Message.ToPlugin;
namespace Mcv.Plugin.MainViewPlugin
{
    interface IMainViewActor
    {
        void RequestUpdateSettingsList(List<IPluginSettingsDiff> modifiedData);
        void RequestSitePluginSettings();
        void RequestCloseApp();
        void RequestRemoveConnection(ConnectionId id);
        void RequestChangeConnectionStatus(ConnectionId id, NormalConnectionDiff normalConnectionDiff);
        void RequestAddNormalConnection();
    }
    class MainViewActor : ReceiveActor, IMainViewActor
    {
        readonly MainViewModel _viewModel;
        readonly MainWindow _mainView;
        //protected override void OnReceive(object message)
        //{
        //    switch(message)
        //    {
        //        case ToCore.RequestAddConnectionMessage reqAddConn:
        //            break;
        //    }
        //}
        void WindowClosed(object? sender, EventArgs e)
        {
            _viewModel.RequestClose();
        }
        void ViewModel_CloseRequested(object? sender, EventArgs e)
        {
            OnExitRequested(EventArgs.Empty);
        }
        public event EventHandler<EventArgs>? ExitRequested;


        protected virtual void OnExitRequested(EventArgs e)
        {
            ExitRequested?.Invoke(this, e);
        }
        private readonly IAdapter _adapter;
        private readonly ICanTell _parent;
        private readonly PluginId _pluginId;
        private readonly IActorRef _self;

        public MainViewActor(PluginId pluginId)
        {
            _pluginId = pluginId;
            _self = Self;

            var strategies = new List<ISiteStrategy>
            {
                new Site.YouTubeLive.YouTubeLiveSiteStrategy(),
                new Site.A17Live.A17LiveSiteStrategy(),
            };
            var settingsContext = new SettingsContext(strategies);
            var siteMessageParser = new SiteMessageParser(strategies);
            _adapter = new Adapter(this);
            _parent = Context.Parent;
            _viewModel = new MainViewModel(_adapter, settingsContext);
            _viewModel.CloseRequested += ViewModel_CloseRequested;


            //await viewModel.InitializeAsync();

            //_splashActor = _actorSystem.ActorOf(Props.Create(() => new SplashActor(viewModel)).WithDispatcher("akka.actor.synchronized-dispatcher"), "splash");
            //var a = _splashActor.Path;

            _mainView = new MainWindow();
            _mainView.Closed += WindowClosed;
            _mainView.DataContext = _viewModel;
            _mainView.Show();
            Receive<ToPlugin.Hello>(m =>
            {
                var types = new List<IPluginType>
                {
                    new PluginTypeMainView(),
                };
                Context.Parent.Tell(new ToCore.Hello(_pluginId, types));
                //設定を読み込む
                //var answerSettings = _parent.Ask<ToPlugin.AnswerLoadSettings>(new ToCore.AskLoadSettings("MainViewPlugin"));
            });
            Receive<ToPlugin.NotifyMessageReceived>(m =>
            {
                _adapter.OnMessageReceived(m.ConnId, m.Message, siteMessageParser,_adapter);
            });
            Receive<ToCore.RequestAddConnection>((message) =>
            {
                _parent.Tell(message, Self);
            });
            Receive<ToCore.RequestAddNormalConnection>((message) =>
            {
                _parent.Tell(message, Self);
            });
            Receive<ToCore.RequestRemoveConnection>((message) =>
            {
                _parent.Tell(message, Self);
            });
            Receive<ToCore.RequestChangeConnectionStatus>((message) =>
            {
                _parent.Tell(message, Self);
            });
            Receive<ToPlugin.AnswerSitePluginSettings>(m =>
            {
                //var answer = await _parent.Ask<ToPlugin.AnswerSitePluginSettings>(m);
                _adapter.OnSitePluginSettingsReceived(m.PluginSettingsList);
            });
            Receive<ToPlugin.RequestUpdateSettings>(m =>
            {
                //外部からの設定値の変更は想定していない。
                return;
            });
            Receive<ToCore.RequestUpdateSettingsList>(m =>
            {
                _parent.Tell(m, Self);
            });
            //Receive<ToCore.RequestSitePluginSettings>(m =>
            //{
            //    //var answer = await _parent.Ask<ToPlugin.AnswerSitePluginSettings>(m);
            //    //_adapter.OnSitePluginSettingsReceived();
            //});
            //Receive<ToPlugin.NotifyConnectionAdded>((message) =>
            //{
            //    _adapter.OnConnectionAdded(message.ConnId, message.);
            //});
            ReceiveAsync<ToPlugin.NotifyConnectionAdded>(async m =>
            {
                var answer = await _parent.Ask<ToPlugin.AnswerConnectionStatus>(new ToCore.AskConnectionStatus(m.ConnId));


                if (answer.ConnSt is INormalConnection normal)
                {
                    //vm.OnNormalConnectionAdded(m.ConnId, normal);
                    _adapter.OnConnectionAdded(m.ConnId, normal);
                }
                else
                {
                    //vm.OnUnknownConnectionAdded(m.ConnId, answer.ConnSt.Name);
                }
            });
            Receive<ToPlugin.NotifyConnectionRemoved>((message) =>
            {
                _adapter.OnConnectionRemoved(message.ConnId);
            });
            Receive<ToPlugin.NotifyConnectionStatusChanged>((message) =>
            {
                _adapter.OnConnectionStatusChanged(message.ConnId, message.Diff);
            });
            Receive<ToPlugin.NotifySitePluginAdded>((message) =>
            {
                _adapter.OnSiteAdded(message.PluginId, message.Name);
            });
            Receive<ToPlugin.NotifyBrowserAdded>((message) =>
            {
                _adapter.OnBrowserAdded(message.BrowserProfileId, message.Name, message.ProfileName);
            });
            Receive<ToPlugin.NotifySitePluginRemoved>((message) =>
            {
                _adapter.OnSiteRemoved(message.PluginId);
            });
            Receive<ToPlugin.NotifyBrowserRemoved>((message) =>
            {
                _adapter.OnBrowserRemoved(message.PluginId);
            });
            Receive<ToPlugin.NotifyNormalPluginAdded>(m =>
            {
                _adapter.OnNotifyNormalPluginAdded(m);
            });

            //Receive<ToPlugin.NotifyPlugins>((message) =>
            //{
            //    foreach (var site in message.Infos)
            //    {
            //        if (site.Type == ToPlugin.PluginType.Site)
            //        {
            //            _adapter.OnSiteAdded(site.Id, site.Name);
            //        }
            //        else if(site.Type == ToPlugin.PluginType.Browser)
            //        {
            //            //_adapter.OnBrowserAdded(browser.Id, browser.BrowserName, browser.ProfileName);
            //        }
            //    }
            //});
            Receive<ToCore.RequestCloseApp>(m =>
            {
                _parent.Tell(m, Self);
            });

        }
        protected override void Unhandled(object message)
        {
            Debug.WriteLine($"MainViewPlugin::MainViewActor::Unhandled() {message}");
        }

        public void RequestUpdateSettingsList(List<IPluginSettingsDiff> modifiedData)
        {
            _parent.Tell(new ToCore.RequestUpdateSettingsList(modifiedData),_self);
        }

        public void RequestSitePluginSettings()
        {
            _parent.Tell(new ToCore.AskSitePluginSettings(),_self);
        }

        public void RequestCloseApp()
        {
            _parent.Tell(new ToCore.RequestCloseApp(),_self);
        }

        public void RequestRemoveConnection(ConnectionId id)
        {
            _parent.Tell(new ToCore.RequestRemoveConnection(id),_self);
        }

        public void RequestChangeConnectionStatus(ConnectionId id, NormalConnectionDiff normalConnectionDiff)
        {
            _parent.Tell(new ToCore.RequestChangeConnectionStatus(id, normalConnectionDiff), _self);
        }

        public void RequestAddNormalConnection()
        {
            _parent.Tell(new ToCore.RequestAddNormalConnection(),_self);
        }
    }
}
