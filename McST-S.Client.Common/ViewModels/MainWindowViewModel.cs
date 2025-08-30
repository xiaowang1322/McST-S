using McST_S.Shared.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace McST_S.Client.Common.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _status = "就绪";
        private string _version = "版本: 1.0.0.0";

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public string Version
        {
            get => _version;
            set
            {
                _version = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadConfigAsync()
        {
            var config = await ServiceLocator.ConfigService.LoadConfigAsync();
            Version = $"版本: {config.CurrentVersion}";
            Status = "就绪";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}