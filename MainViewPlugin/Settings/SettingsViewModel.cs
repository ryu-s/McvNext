using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace Mcv.Plugin.MainViewPlugin.Settings
{
    class SettingsViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ICommand OkCommand { get; }
        public ICommand ApplyCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand LoadedCommand { get; }
        /// <summary>
        /// Okまたは適用ボタンを押された
        /// </summary>
        public event EventHandler? SettingsApplied;
        public SettingsViewModel()
        {
            //SiteVms = siteVms;
            OkCommand = new RelayCommand(Ok);
            ApplyCommand = new RelayCommand(Apply);
            CancelCommand = new RelayCommand(Cancel);
            LoadedCommand = new RelayCommand<System.Windows.Window>(Loaded);
        }
        private void Loaded(System.Windows.Window? window)
        {
            if (window == null)
            {
                //bug
                return;
            }
            _window = window;
        }
        System.Windows.Window _window = default!;
        private void Ok()
        {
            Apply();
            _window.Close();
        }
        private void Apply()
        {
            SettingsApplied?.Invoke(this, EventArgs.Empty);
        }
        private void Cancel()
        {
            _window.Close();
        }
    }
}

