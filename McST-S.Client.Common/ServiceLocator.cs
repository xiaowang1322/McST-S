using McST_S.Client.Common.Services;
using McST_S.Client.Common.ViewModels;

namespace McST_S.Client.Common
{
    public static class ServiceLocator
    {
        private static IConfigService? _configService;
        private static MainWindowViewModel? _mainWindowViewModel;

        public static IConfigService ConfigService
        {
            get => _configService ??= new ConfigService();
            set => _configService = value;
        }

        public static MainWindowViewModel MainWindowViewModel
        {
            get => _mainWindowViewModel ??= new MainWindowViewModel();
            set => _mainWindowViewModel = value;
        }
    }
}