using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace McST_S.Shared.Models
{
    public class UpdateInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("version")]
        public string Version { get; set; } = string.Empty;

        [JsonPropertyName("upgrade")]
        public string UpgradeUrl { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public UpdateType Type { get; set; } = UpdateType.Update;

        [JsonPropertyName("time")]
        public DateTime ReleaseTime { get; set; }

        [JsonPropertyName("text")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("Delete")]
        public List<string> FilesToDelete { get; set; } = [];
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UpdateType
    {
        [JsonPropertyName("all")]
        Full,

        [JsonPropertyName("update")]
        Update
    }
}