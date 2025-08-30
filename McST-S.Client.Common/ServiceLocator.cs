using McST_S.Client.Common.Services;

namespace McST_S.Client.Common
{
    public static class ServiceLocator
    {
        private static IConfigService? _configService;

        public static IConfigService ConfigService
        {
            get => _configService ??= new ConfigService();
            set => _configService = value;
        }
    }
}