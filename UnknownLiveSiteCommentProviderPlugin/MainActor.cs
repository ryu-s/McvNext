using Akka.Actor;
using Plugin;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnknownLiveSiteCommentProviderPluginTrait;
using ToCore = Plugin.Message.ToCore;
using ToPlugin = Plugin.Message.ToPlugin;
namespace UnknownLiveSiteCommentProviderPlugin;
class UnknownLiveSiteSettings : IUnknownLiveSiteSettings
{
    public string Memo { get; set; } = "";    
}
class MainActor : ReceiveActor
{
    private readonly PluginId _pluginId;
    private readonly Dictionary<ConnectionId, IActorRef> _connDict = new();
    private readonly UnknownLiveSiteSettings _settings = new UnknownLiveSiteSettings();
    public MainActor(PluginId pluginId)
    {
        _pluginId = pluginId;
        Receive<ToPlugin.Hello>(m =>
        {
            var types = new List<IPluginType>
            {
                new PluginTypeSite(new SiteId(Guid.NewGuid()), "未知のサイト"),
            };
            Context.Parent.Tell(new ToCore.Hello(_pluginId, types));
        });
        Receive<ToPlugin.RequestOpenConnectionMessage>(m =>
        {
            var conn = Context.ActorOf(Akka.Actor.Props.Create(() => new ConnectionActor(m.ConnId)));
            _connDict.Add(m.ConnId, conn);
        });
        Receive<ToPlugin.RequestCloseConnectionMessage>(m =>
        {
            _connDict.Remove(m.ConnId);
        });
        Receive<ToPlugin.RequestStartConnectionMessage>(m =>
        {
            var conn = _connDict[m.ConnId];
            conn.Tell(new StartMessage());
        });
        Receive<ToPlugin.RequestStopConnectionMessage>(m =>
        {
            var conn = _connDict[m.ConnId];
            conn.Tell(new StopMessage());
        });
        Receive<DataMessage>(m =>
        {
            Context.Parent.Tell(new ToCore.NotifyMessageReceived(_pluginId, m.ConnId, m.Data));
        });
        Receive<ToPlugin.AskPluginSettings>(m =>
        {
            Sender.Tell(new ToCore.AnswerPluginSettings(_settings));
        });
    }
    public static Props Props(PluginId pluginId)
    {
        return Akka.Actor.Props.Create(() => new MainActor(pluginId)).WithDispatcher("akka.actor.synchronized-dispatcher");
    }
}
class StartMessage { }
class StopMessage { }
class DataMessage
{
    public DataMessage(ConnectionId connId, NormalComment data)
    {
        ConnId = connId;
        Data = data;
    }

    public ConnectionId ConnId { get; }
    public NormalComment Data { get; }
}
class ConnectionActor : ReceiveActor
{
    private readonly ConnectionId _connId;
    private IActorRef? _dataProvider;
    public ConnectionActor(ConnectionId connId)
    {
        _connId = connId;

        Receive<StartMessage>(m =>
        {
            if (_dataProvider != null) return;
            _dataProvider = Context.ActorOf(Props.Create(() => new DataProvider(_connId)));
            _dataProvider.Tell(new StartMessage());
        });
        Receive<StopMessage>(m =>
        {
            if (_dataProvider == null) return;
            Context.Stop(_dataProvider);
        });
        Receive<DataMessage>(m =>
        {
            Context.Parent.Tell(m, Self);
        });
 
    }
}
class DataProvider : ReceiveActor
{
    private readonly ConnectionId _connId;

    public DataProvider(ConnectionId connId)
    {
        _connId = connId;
        ReceiveAsync<StartMessage>(async m =>
        {
            while (true)
            {
                Context.Parent.Tell(new DataMessage(_connId, new NormalComment("ryu-s", DateTime.Now.ToString("HH:mm:ss"))));
                await Task.Delay(500);
            }
        });

    }
}