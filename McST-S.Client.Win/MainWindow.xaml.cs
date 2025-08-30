using System.Windows;
using McST_S.Client.Common;
using McST_S.Shared.Models;
using System.Threading.Tasks;

namespace McST_S.Client.Win
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadConfigAsync();
        }

        private async Task LoadConfigAsync()
        {
            var config = await ServiceLocator.ConfigService.LoadConfigAsync();
            VersionText.Text = $"版本: {config.CurrentVersion}";
            StatusText.Text = "就绪";
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LaunchButton_Click(object sender, RoutedEventArgs e)
        {
            StatusText.Text = "启动游戏中...";
            // 这里将实现启动逻辑
            MessageBox.Show("启动游戏功能尚未实现", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            StatusText.Text = "就绪";
        }
    }
}