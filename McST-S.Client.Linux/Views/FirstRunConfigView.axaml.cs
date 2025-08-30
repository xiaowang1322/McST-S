using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using McST_S.Client.Common;
using McST_S.Shared.Models;
using System;

namespace McST_S.Client.Linux.Views
{
    public partial class FirstRunConfigView : UserControl
    {
        public FirstRunConfigView()
        {
            InitializeComponent();

            // 手动查找控件并绑定事件
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

        // 添加 ? 表示参数可以为 null
        private void ServerUrlTextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            ValidateInput();
        }

        // 添加 ? 表示参数可以为 null
        private void VersionTextBox_TextChanged(object? sender, TextChangedEventArgs e)
        {
            ValidateInput();
        }

        // 添加 async 关键字以支持 await
        private async void SaveButton_Click(object? sender, RoutedEventArgs e)
        {
            var serverUrlTextBox = this.FindControl<TextBox>("ServerUrlTextBox");
            var versionTextBox = this.FindControl<TextBox>("VersionTextBox");

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
                // 在Avalonia中退出应用
                Environment.Exit(0);
            }
            catch (System.Exception ex)
            {
                var errorMessageText = this.FindControl<TextBlock>("ErrorMessageText");
                if (errorMessageText != null)
                {
                    ShowError(errorMessageText, $"保存配置时出错: {ex.Message}");
                }
            }
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
                ShowError(errorMessageText, "服务端地址格式不正确，请输入有效的HTTP/HTTPS URL");
                saveButton.IsEnabled = false;
                return;
            }

            if (!isVersionValid && !string.IsNullOrEmpty(version))
            {
                ShowError(errorMessageText, "版本号格式不正确，请使用x.x.x.x格式");
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
    }
}