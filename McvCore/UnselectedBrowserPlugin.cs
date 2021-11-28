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

class UnselectedBrowserPluginMain : IPlugin
{
    public string Name { get; } = "SelectedBrowserのデフォルト値用プラグイン";
    public PluginId Id { get; } = new PluginId(Guid.NewGuid());
    public PluginTypeCookieStore PluginType { get; } = new PluginTypeCookieStore(new BrowserProfileId(Guid.NewGuid()), "未選択", null);

    public Props GetProps()
    {
        return UnselectedBrowserPluginActor.Props(Id, PluginType);
    }
}
class UnselectedBrowserPluginActor : ReceiveActor
{
    private readonly PluginId _pluginId;
    private readonly PluginTypeCookieStore _pluginType;

    public UnselectedBrowserPluginActor(PluginId pluginId, PluginTypeCookieStore pluginType)
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
    }
    public static Props Props(PluginId pluginId, PluginTypeCookieStore pluginType)
    {
        return Akka.Actor.Props.Create(() => new UnselectedBrowserPluginActor(pluginId,pluginType));
    }
}
