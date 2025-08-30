using System.Text.Json.Serialization;

namespace McST_S.Shared.Models
{
    public class ClientConfig
    {
        [JsonPropertyName("ServerUrl")]
        public string ServerUrl { get; set; } = string.Empty;

        [JsonPropertyName("CurrentVersion")]
        public string CurrentVersion { get; set; } = string.Empty;

        [JsonPropertyName("ClientName")]
        public string ClientName { get; set; } = string.Empty;

        [JsonPropertyName("GameDirectory")]
        public string GameDirectory { get; set; } = ".minecraft";

        [JsonPropertyName("UpdateCheckInterval")]
        public int UpdateCheckInterval { get; set; } = 3600; // 默认1小时

        [JsonPropertyName("IsFirstRun")]
        public bool IsFirstRun { get; set; } = true;
    }
}