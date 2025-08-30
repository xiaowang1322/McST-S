using System.Text.Json.Serialization;

namespace McST_S.Shared.Models
{
    public class ApiResponse<T>(bool success, T? data, string message = "")
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; } = success;

        [JsonPropertyName("data")]
        public T? Data { get; set; } = data;

        [JsonPropertyName("message")]
        public string Message { get; set; } = message;

        public static ApiResponse<T> Ok(T data, string message = "")
        {
            return new ApiResponse<T>(true, data, message);
        }

        public static ApiResponse<T> Fail(string message)
        {
            return new ApiResponse<T>(false, default, message);
        }
    }
}