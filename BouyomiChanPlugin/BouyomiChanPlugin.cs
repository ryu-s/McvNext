
using Akka.Actor;
using Plugin;
using System;
using System.Collections.Generic;
using ToPlugin = Plugin.Message.ToPlugin;
using ToCore = Plugin.Message.ToCore;
namespace BouyomiChanPlugin;
public class BouyomiChanPlugin : IPlugin
{
    public string Name { get; } = "棒読みちゃんプラグイン";
    public PluginId Id { get; } = new PluginId(Guid.NewGuid());

    public Props GetProps()
    {
        return Props.Create(() => new MainActor(Id));
    }
}
class MainActor : ReceiveActor
{
    private readonly PluginId _pluginId;

    public MainActor(PluginId pluginId)
    {
        _pluginId = pluginId;
        Receive<ToPlugin.Hello>(m =>
        {
            var types = new List<IPluginType>
                {
                    new PluginTypeNormal(),
                };
            Context.Parent.Tell(new ToCore.Hello(_pluginId, types));
        });
        Receive<ToPlugin.NotifyMessageReceived>(m =>
        {
            //if (m.Message is IBouyomiChanReadableMessage readable)
            //{
            //    Console.WriteLine($"棒読みちゃん: {readable.Name}さん {readable.Comment}");
            //}
        });
    }
}