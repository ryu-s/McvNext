using Akka.Actor;
using Plugin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToCore = Plugin.Message.ToCore;
using ToPlugin = Plugin.Message.ToPlugin;

namespace Mcv.Core;

class UnselectedSitePluginMain : IPlugin
{
    public string Name { get; } = "SelectedSiteのデフォルト値用プラグイン";
    public PluginId Id { get; } = new PluginId(Guid.NewGuid());
    public PluginTypeSite PluginType { get; } = new PluginTypeSite(new SiteId(Guid.NewGuid()), "未選択");

    public Props GetProps()
    {
        return UnselectedPluginActor.Props(Id, PluginType);
    }
}
class DummySettings : IPluginSettings { }
class UnselectedPluginActor : ReceiveActor
{
    private readonly PluginId _pluginId;
    private readonly PluginTypeSite _pluginType;

    public UnselectedPluginActor(PluginId pluginId, PluginTypeSite pluginType)
    {
        _pluginId = pluginId;
        _pluginType = pluginType;
        Receive<ToPlugin.Hello>(m =>
        {
            var types = new List<IPluginType>
            {
                _pluginType,
            };
            Context.Parent.Tell(new ToCore.Hello(_pluginId, types));
        });
        Receive<ToPlugin.AskPluginSettings>(_ =>
        {
            Sender.Tell(new ToCore.AnswerPluginSettings(new DummySettings()));
        });
    }
    public static Props Props(PluginId pluginId, PluginTypeSite pluginType)
    {
        return Akka.Actor.Props.Create(() => new UnselectedPluginActor(pluginId,pluginType));
    }
}
