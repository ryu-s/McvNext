using Akka.Actor;
using Plugin;
using ToPlugin = Plugin.Message.ToPlugin;
using ToCore = Plugin.Message.ToCore;
using System.Diagnostics;
using Mcv.A17Live;

namespace Mcv.Plugin.A17Live;

class ConnectionManager
{
    private readonly Dictionary<ConnectionId, IActorRef> _connDict = new();
    public void Add(ConnectionId connId, IActorRef conn)
    {
        _connDict.Add(connId, conn);
    }
    public void Remove(ConnectionId connId)
    {
        _connDict.Remove(connId);
    }
    public IActorRef? Get(ConnectionId connId)
    {
        if (_connDict.TryGetValue(connId, out var conn))
        {
            return conn;
        }
        else
        {
            return null;
        }
    }
    public ConnectionId? Get(IActorRef conn)
    {
        foreach (var kv in _connDict)
        {
            if (kv.Value == conn)
            {
                return kv.Key;
            }
        }
        return null;
    }
}
class A17LiveSitePluginActor : ReceiveActor
{
    private readonly ConnectionManager _connMgr = new();
    private readonly PluginId _pluginId;
    private readonly PluginTypeSite _pluginType;
    private readonly A17LiveSiteSettings _settings = new A17LiveSiteSettings();
    private readonly IActorRef _core;
    public A17LiveSitePluginActor(PluginId pluginId, PluginTypeSite pluginType)
    {
        _pluginId = pluginId;
        _pluginType = pluginType;
        _core = Context.Parent;
        Receive<ToPlugin.Hello>(m =>
        {
            var types = new List<IPluginType>
                {
                    pluginType,
                };
            _core.Tell(new ToCore.Hello(_pluginId, types));
        });
        Receive<ToPlugin.RequestOpenConnectionMessage>(m =>
        {
            var conn = Context.ActorOf<A17LiveConnectionActor>();
            _connMgr.Add(m.ConnId, conn);
        });
        Receive<ToPlugin.RequestCloseConnectionMessage>(m =>
        {
            _connMgr.Remove(m.ConnId);
        });
        Receive<ToPlugin.RequestStartConnectionMessage>(m =>
        {
            var conn = _connMgr.Get(m.ConnId);
            if (conn == null) return;
            conn.Tell(new RequestStartConnectionMessage(m.Input), Self);
        });
        Receive<ToPlugin.RequestStopConnectionMessage>(m =>
        {
            var conn = _connMgr.Get(m.ConnId);
            conn.Tell(new RequestStopConnectionMessage());
        });
        Receive<ConnectionStoppedMessage>(m =>
        {
            var connId = _connMgr.Get(Sender);
            if (connId == null)
            {
                return;
            }
            _core.Tell(new ToCore.RequestChangeConnectionStatus(connId, new NormalConnectionDiff
            {
                IsConnected = false,
            }));
        });
        ReceiveAsync<ToPlugin.AskPluginSettings>(async m =>
        {
            Sender.Tell(new ToCore.AnswerPluginSettings(_settings));
        });
        Receive<ToPlugin.RequestUpdateSettings>(m =>
        {
            if (m.Modified is not IA17LiveSettingsDiff ytDiff)
            {
                return;
            }
            _settings.Set(ytDiff);
            //Coreに保存要求
            _core.Tell(new ToCore.RequestSaveSettings("17LiveSitePlugin", _settings.Serialize()));
        });
        Receive<InternalMessageReceived>(m =>
        {
            var connId = _connMgr.Get(Sender);
            if (connId == null)
            {
                return;
            }
            if (m.message is InternalMessage.Comment comment)
            {
                var a = new A17LiveComment(comment.Text);
                _core.Tell(new ToCore.NotifyMessageReceived(_pluginId, connId, a));
            }
            else if (m.message is InternalMessage.UnknownMessage unknown)
            {
#if DEBUG
                using (var sw = new StreamWriter("17live_unknown_message.txt", true))
                {
                    sw.WriteLine(unknown.Raw);
                }
#else
                _core.Tell(new ToCore.AddLog($"A17LiveSitePlugin unknown message: {unknown.Raw}"));
#endif
            }
        });

    }
    protected override void Unhandled(object message)
    {
        Debug.WriteLine($"17LiveSitePlugin::A17LiveSitePluginActor::Unhandled() {message}");
    }
    private void AddConnection()
    {

    }
}

