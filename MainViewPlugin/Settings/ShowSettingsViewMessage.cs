using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.Messaging.Messages;
using Plugin;

namespace Mcv.Plugin.MainViewPlugin.Settings;

internal class ShowSettingsViewMessage: Microsoft.Toolkit.Mvvm.Messaging.Messages.RequestMessage<string>
{
    public ShowSettingsViewMessage(SettingsContext settingsContext)
    {
        SettingsContext = settingsContext;
    }
    public SettingsContext SettingsContext { get; }
}
