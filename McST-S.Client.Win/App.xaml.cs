using System.Windows;
using McST_S.Client.Common;
using McST_S.Client.Win.Views;

namespace McST_S.Client.Win
{
    public partial class App : Application
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 加载配置
            var config = await ServiceLocator.ConfigService.LoadConfigAsync();

            if (config.IsFirstRun)
            {
                // 如果是第一次运行，显示配置窗口
                var configWindow = new FirstRunConfigWindow();
                configWindow.ShowDialog();
            }
            else
            {
                // 如果不是第一次运行，显示主窗口
                var mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
    }
}