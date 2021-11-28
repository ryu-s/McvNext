using Akka.Actor;
using Plugin;
using System.Diagnostics;
using Mcv.Plugin.A17Live.Network;
using System.Text.RegularExpressions;

namespace Mcv.Plugin.A17Live;
public class A17LiveSitePlugin : IPlugin
{
    public string Name { get; } = "17LiveSitePlugin";
    public PluginId Id { get; } = new PluginId(Guid.NewGuid());
    public PluginTypeSite PluginType { get; } = new PluginTypeSite(new SiteId(Guid.NewGuid()), "17LIVE");
    public Props GetProps()
    {
        return Props.Create(() => new A17LiveSitePluginActor(Id, PluginType));
    }
}
record RequestStartConnectionMessage(string Input);
record RequestStopConnectionMessage();
record ConnectionStoppedMessage();

class SendMessage
{
    public string Text { get; }

    public SendMessage(string text)
    {
        Text = text;
    }
    public SendMessage(InternalMessage.Attach attach)
    {
        Text = attach.Raw;
    }
}
record InternalMessageReceived(InternalMessage.IInternalMessage message);
class A17LiveConnectionActor : ReceiveActor
{
    bool isFirst = true;
    IActorRef? _wsActor;
    IActorRef? _parent;
    private string? _channel;
    public A17LiveConnectionActor()
    {
        Receive<RequestStartConnectionMessage>(m =>
        {
            _parent = Sender;
            _channel = ExtractChannel(m.Input);
            _wsActor = Context.ActorOf<WebsocketActor>();
            _wsActor.Tell(new RequestConnect("wss://17media-realtime.ably.io/?key=qvDtFQ.0xBeRA:iYWpd3nD2QHE6Sjm&format=json&heartbeats=true&v=1.1&lib=js-web-1.1.22"));
        });
        Receive<RequestStopConnectionMessage>(m =>
        {
            _wsActor?.Tell(new Network.RequestDisconnect());
        });
        Receive<MessageReceived>(m =>
        {
            if (_channel == null) return;

            var internalMessage = InternalMessage.MessageParser.Parse(m.ReceivedData);
            Debug.WriteLine(m.ReceivedData);
            if (isFirst)
            {
                _wsActor.Tell(new SendMessage(new InternalMessage.Attach(_channel)));
                isFirst = false;
            }
            _parent?.Tell(new InternalMessageReceived(internalMessage));
        });
        Receive<Network.Disconnected>(m =>
        {
            _parent?.Tell(new ConnectionStoppedMessage());
        });
    }
    private void SendMessage(string text)
    {

    }
    private static string? ExtractChannel(string input)
    {
        var match = Regex.Match(input, "17\\.live/ja/live/(\\d+)");
        if (!match.Success) return null;
        return match.Groups[1].Value;
    }
}

