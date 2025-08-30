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
                // ��������
                var config = await ServiceLocator.ConfigService.LoadConfigAsync();

                if (config.IsFirstRun)
                {
                    // ����ǵ�һ�����У���ʾ���ô���
                    desktop.MainWindow = new FirstRunConfigWindow();
                }
                else
                {
                    // ������ǵ�һ�����У���ʾ������
                    desktop.MainWindow = new MainWindow();
                }
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}