using System.ComponentModel;
//using ToPlugin = Mcv.Messages.Plugin;
using Plugin;

namespace Mcv.Plugin.MainViewPlugin
{
    class ConnectionNameViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private string _name = default!;

        public ConnectionNameViewModel(ConnectionId id, string name)
        {
            ConnId = id;
            Name = name;
        }
        public ConnectionId ConnId { get; }
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                RaisePropertyChanged();
            }
        }
    }
}
