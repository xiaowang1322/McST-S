using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using McST_S.Client.Common;
using McST_S.Shared.Models;
using System;
using System.Threading.Tasks;

namespace McST_S.Client.Linux.Views
{
    public partial class MainWindow : Window
    {
        // 控件引用 - 添加 readonly 修饰符
        private readonly TextBlock? _statusText;
        private readonly TextBlock? _versionText;

        public MainWindow()
        {
            InitializeComponent();
            this.Opened += MainWindow_Opened;

            // 在初始化后查找控件
            _statusText = this.FindControl<TextBlock>("StatusText");
            _versionText = this.FindControl<TextBlock>("VersionText");
        }

        private async void MainWindow_Opened(object? sender, System.EventArgs e)
        {
            await LoadConfigAsync();
        }

        private async Task LoadConfigAsync()
        {
            var config = await ServiceLocator.ConfigService.LoadConfigAsync();
            if (_versionText != null)
                _versionText.Text = $"版本: {config.CurrentVersion}";
            if (_statusText != null)
                _statusText.Text = "就绪";
        }

        private static void ExitMenuItem_Click(object? sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void LaunchButton_Click(object? sender, RoutedEventArgs e)
        {
            if (_statusText != null)
                _statusText.Text = "启动游戏中...";

            // 这里将实现启动逻辑
            // 暂时显示消息

            if (_statusText != null)
                _statusText.Text = "就绪";
        }
    }
}