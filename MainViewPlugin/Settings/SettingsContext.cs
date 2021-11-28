using Plugin;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mcv.Plugin.MainViewPlugin.Settings
{
    /// <summary>
    /// 設定関連をまとめる場所。
    /// </summary>
    class SettingsContext
    {
        //List<IPluginSettings>? _pluginSettingsList;
        private readonly List<ISiteStrategy> _siteStrategies;
        private List<IOptionsTabPage>? _panels;
        /// <summary>
        /// Coreが送ってきたプラグインの設定のリストからMainViewPluginで扱うものだけを選んでそのパネルを作成する。
        /// </summary>
        /// <param name="pluginSettingsList"></param>
        public void SetPluginSettings(List<IPluginSettings> pluginSettingsList)
        {
            //_pluginSettingsList = pluginSettingsList;
            var tabPages = _siteStrategies.Select(s =>
            {
                foreach (var a in pluginSettingsList)
                {
                    if (s.IsTarget(a))
                    {
                        var tabPage = s.CreateTabPage();
                        var vm = s.CreateViewModel(a);
                        vm.SetSettings(a);
                        tabPage.TabPagePanel.DataContext = vm;
                        return tabPage;
                    }
                }
                return null;
            }).Where(k => k != null).Cast<IOptionsTabPage>().ToList();
            _panels = tabPages;
        }
        public IEnumerable<IOptionsTabPage> GetPanels()
        {
            if (_panels == null) return new List<IOptionsTabPage>();
            return _panels;
        }
        private List<IPluginSettingsDiff> GetPluginSettingsDiff()
        {
            if (_panels == null) return new List<IPluginSettingsDiff>();
            return _panels.Select(p => p.GetViewModel().GetSettingsDiff()).ToList();
        }
        public SettingsViewModel ViewModel { get; } = new SettingsViewModel();
        public SettingsContext(List<ISiteStrategy> siteStrategies)
        {
            _siteStrategies = siteStrategies;
            ViewModel.SettingsApplied += (s, e) => Applied?.Invoke(this, new AppliedEventArgs(GetPluginSettingsDiff()));
        }
        public event EventHandler<AppliedEventArgs>? Applied;
    }
    class AppliedEventArgs : EventArgs
    {
        public List<IPluginSettingsDiff> ModifiedData { get; }
        public AppliedEventArgs(List<IPluginSettingsDiff> modifiedData)
        {
            ModifiedData = modifiedData;
        }
    }
}

