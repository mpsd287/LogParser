using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LogParser.Web.Models;

public class RawLogEntry
{
    [JsonPropertyName("line")]
    public string Line { get; set; } = string.Empty;

    [JsonPropertyName("timestamp")]
    public string Timestamp { get; set; } = string.Empty;

    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    [JsonPropertyName("fields")]
    public Dictionary<string, string> Fields { get; set; } = new();
}
