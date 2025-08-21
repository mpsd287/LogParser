using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using LogParser.Web.Models;

namespace LogParser.Web.Services;

public static class LogFileParser
{
    public static List<LogEntry> Parse(Stream stream)
    {
        using var doc = JsonDocument.Parse(stream);
        var list = new List<LogEntry>();
        foreach (var element in doc.RootElement.EnumerateArray())
        {
            var raw = JsonSerializer.Deserialize<RawLogEntry>(element);
            if (raw == null) continue;
            using var lineDoc = JsonDocument.Parse(raw.Line);
            var template = lineDoc.RootElement.GetProperty("body").GetString() ?? string.Empty;
            Dictionary<string, object>? parameters = null;
            if (lineDoc.RootElement.TryGetProperty("attributes", out var attrs))
            {
                parameters = attrs.EnumerateObject()
                    .ToDictionary(p => p.Name, p => (object?)p.Value.ToString() ?? string.Empty);
            }
            var message = FormatMessage(template, parameters);
            var severity = lineDoc.RootElement.TryGetProperty("severity", out var sev) ? sev.GetString() ?? string.Empty : string.Empty;
            var date = DateTime.Parse(raw.Date, null, DateTimeStyles.RoundtripKind);
            list.Add(new LogEntry
            {
                Date = date,
                Severity = severity,
                Message = message,
                Fields = raw.Fields
            });
        }
        return list;
    }

    public static string FormatMessage(string template, IDictionary<string, object>? parameters)
    {
        if (parameters == null) return template;
        foreach (var kv in parameters)
        {
            template = template.Replace("{" + kv.Key + "}", kv.Value?.ToString());
        }
        return template;
    }
}
