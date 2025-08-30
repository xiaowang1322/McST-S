using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using McST_S.Client.Common;
using McST_S.Client.Linux.Views;
using System.Threading.Tasks;

namespace McST_S.Client.Linux
{
    public class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override async void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                // 加载配置
                var config = await ServiceLocator.ConfigService.LoadConfigAsync();

                if (config.IsFirstRun)
                {
                    // 如果是第一次运行，显示配置窗口
                    desktop.MainWindow = new FirstRunConfigWindow();
                }
                else
                {
                    // 如果不是第一次运行，显示主窗口
                    desktop.MainWindow = new MainWindow();
                }
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}