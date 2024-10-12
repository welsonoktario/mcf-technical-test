using System.Text.Json.Serialization;

namespace Frontend.Models;

public class ApiResponse<T> {
    [JsonPropertyName("message")]
    public string Message { get; set; }
    
    [JsonPropertyName("data")]
    public T Data { get; set; }
    
    [JsonPropertyName("errors")]
    public Dictionary<string, string[]?>? Errors { get; set; }
}