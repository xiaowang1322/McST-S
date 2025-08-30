using System.Windows;
using McST_S.Client.Common;
using McST_S.Shared.Models;

namespace McST_S.Client.Win.Views
{
    public partial class FirstRunConfigWindow : Window
    {
        public FirstRunConfigWindow()
        {
            InitializeComponent();
        }

        private void ServerUrlTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ValidateInput();
        }

        private void VersionTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ValidateInput();
        }

        private void ValidateInput()
        {
            var serverUrl = ServerUrlTextBox.Text.Trim();
            var version = VersionTextBox.Text.Trim();

            var configService = ServiceLocator.ConfigService;
            var isServerUrlValid = configService.ValidateServerUrl(serverUrl);
            var isVersionValid = configService.ValidateVersion(version);

            if (!isServerUrlValid && !string.IsNullOrEmpty(serverUrl))
            {
                ShowError("服务端地址格式不正确，请输入有效的HTTP/HTTPS URL");
                SaveButton.IsEnabled = false;
                return;
            }

            if (!isVersionValid && !string.IsNullOrEmpty(version))
            {
                ShowError("版本号格式不正确，请使用x.x.x.x格式");
                SaveButton.IsEnabled = false;
                return;
            }

            if (isServerUrlValid && isVersionValid)
            {
                HideError();
                SaveButton.IsEnabled = true;
            }
            else
            {
                SaveButton.IsEnabled = false;
            }
        }

        private void ShowError(string message)
        {
            ErrorMessageText.Text = message;
            ErrorMessageText.Visibility = Visibility.Visible;
        }

        private void HideError()
        {
            ErrorMessageText.Visibility = Visibility.Collapsed;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var serverUrl = ServerUrlTextBox.Text.Trim();
            var version = VersionTextBox.Text.Trim();

            var config = new ClientConfig
            {
                ServerUrl = serverUrl,
                CurrentVersion = version,
                IsFirstRun = false
            };

            try
            {
                await ServiceLocator.ConfigService.SaveConfigAsync(config);
                MessageBox.Show("配置已保存，程序将退出。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"保存配置时出错: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}