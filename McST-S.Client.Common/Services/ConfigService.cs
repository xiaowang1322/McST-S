using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using McST_S.Shared.Models;

namespace McST_S.Client.Common.Services
{
    public class ConfigService : IConfigService
    {
        private readonly string _configFilePath;

        // 缓存 JsonSerializerOptions 实例
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };

        public ConfigService()
        {
            // 使用应用程序数据目录存储配置文件
            var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var appDirectory = Path.Combine(appDataPath, "McST-S");
            Directory.CreateDirectory(appDirectory); // 确保目录存在

            _configFilePath = Path.Combine(appDirectory, "client_config.json");
        }

        public async Task<ClientConfig> LoadConfigAsync()
        {
            try
            {
                if (!File.Exists(_configFilePath))
                {
                    // 如果配置文件不存在，返回默认配置
                    return new ClientConfig { IsFirstRun = true };
                }

                var json = await File.ReadAllTextAsync(_configFilePath);
                var config = JsonSerializer.Deserialize<ClientConfig>(json);

                return config ?? new ClientConfig { IsFirstRun = true };
            }
            catch (Exception)
            {
                // 如果读取失败，返回默认配置
                return new ClientConfig { IsFirstRun = true };
            }
        }

        public async Task SaveConfigAsync(ClientConfig config)
        {
            try
            {
                // 使用缓存的 JsonSerializerOptions 实例
                var json = JsonSerializer.Serialize(config, _jsonOptions);
                await File.WriteAllTextAsync(_configFilePath, json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("保存配置失败", ex);
            }
        }

        public bool ValidateServerUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            // 简单的URL格式验证
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public bool ValidateVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version))
                return false;

            // 验证版本号格式 (x.x.x.x)
            var versionPattern = @"^\d+(\.\d+){3}$";
            return Regex.IsMatch(version, versionPattern);
        }
    }
}