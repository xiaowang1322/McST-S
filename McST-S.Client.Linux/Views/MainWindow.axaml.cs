using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using McST_S.Client.Common;
using McST_S.Shared.Models;
using System;
using System.Threading.Tasks;

namespace McST_S.Client.Linux.Views
{
    public partial class FirstRunConfigView : UserControl
    {
        public FirstRunConfigView()
        {
            InitializeComponent();

            // �ֶ����ҿؼ������¼�
            var serverUrlTextBox = this.FindControl<TextBox>("ServerUrlTextBox");
            var versionTextBox = this.FindControl<TextBox>("VersionTextBox");
            var saveButton = this.FindControl<Button>("SaveButton");
            var errorMessageText = this.FindControl<TextBlock>("ErrorMessageText");

            if (serverUrlTextBox != null)
                serverUrlTextBox.TextChanged += ServerUrlTextBox_TextChanged;

            if (versionTextBox != null)
                versionTextBox.TextChanged += VersionTextBox_TextChanged;

            if (saveButton != null)
                saveButton.Click += SaveButton_Click;
        }

        private void ServerUrlTextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            ValidateInput();
        }

        private void VersionTextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            ValidateInput();
        }

        private void ValidateInput()
        {
            var serverUrlTextBox = this.FindControl<TextBox>("ServerUrlTextBox");
            var versionTextBox = this.FindControl<TextBox>("VersionTextBox");
            var saveButton = this.FindControl<Button>("SaveButton");
            var errorMessageText = this.FindControl<TextBlock>("ErrorMessageText");

            if (serverUrlTextBox == null || versionTextBox == null || saveButton == null || errorMessageText == null)
                return;

            var serverUrl = serverUrlTextBox.Text?.Trim() ?? "";
            var version = versionTextBox.Text?.Trim() ?? "";

            var configService = ServiceLocator.ConfigService;
            var isServerUrlValid = configService.ValidateServerUrl(serverUrl);
            var isVersionValid = configService.ValidateVersion(version);

            if (!isServerUrlValid && !string.IsNullOrEmpty(serverUrl))
            {
                ShowError(errorMessageText, "����˵�ַ��ʽ����ȷ����������Ч��HTTP/HTTPS URL");
                saveButton.IsEnabled = false;
                return;
            }

            if (!isVersionValid && !string.IsNullOrEmpty(version))
            {
                ShowError(errorMessageText, "�汾�Ÿ�ʽ����ȷ����ʹ��x.x.x.x��ʽ");
                saveButton.IsEnabled = false;
                return;
            }

            if (isServerUrlValid && isVersionValid)
            {
                HideError(errorMessageText);
                saveButton.IsEnabled = true;
            }
            else
            {
                saveButton.IsEnabled = false;
            }
        }

        private static void ShowError(TextBlock errorMessageText, string message)
        {
            errorMessageText.Text = message;
            errorMessageText.IsVisible = true;
        }

        private static void HideError(TextBlock errorMessageText)
        {
            errorMessageText.IsVisible = false;
        }

        private async void SaveButton_Click(object? sender, RoutedEventArgs e)
        {
            var serverUrlTextBox = this.FindControl<TextBox>("ServerUrlTextBox");
            var versionTextBox = this.FindControl<TextBox>("VersionTextBox");
            var errorMessageText = this.FindControl<TextBlock>("ErrorMessageText");

            if (serverUrlTextBox == null || versionTextBox == null)
                return;

            var serverUrl = serverUrlTextBox.Text?.Trim() ?? "";
            var version = versionTextBox.Text?.Trim() ?? "";

            var config = new ClientConfig
            {
                ServerUrl = serverUrl,
                CurrentVersion = version,
                IsFirstRun = false
            };

            try
            {
                await ServiceLocator.ConfigService.SaveConfigAsync(config);
                // ��Avalonia���˳�Ӧ��
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                if (errorMessageText != null)
                {
                    ShowError(errorMessageText, $"��������ʱ����: {ex.Message}");
                }
            }
        }
    }
}