using System.Threading.Tasks;
using McST_S.Shared.Models;

namespace McST_S.Client.Common.Services
{
    public interface IConfigService
    {
        /// <summary>
        /// 加载客户端配置
        /// </summary>
        Task<ClientConfig> LoadConfigAsync();

        /// <summary>
        /// 保存客户端配置
        /// </summary>
        Task SaveConfigAsync(ClientConfig config);

        /// <summary>
        /// 验证服务端地址格式
        /// </summary>
        bool ValidateServerUrl(string url);

        /// <summary>
        /// 验证版本号格式 (x.x.x.x)
        /// </summary>
        bool ValidateVersion(string version);
    }
}