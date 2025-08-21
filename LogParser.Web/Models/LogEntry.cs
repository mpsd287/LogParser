using System;
using System.Collections.Generic;

namespace LogParser.Web.Models;

public class LogEntry
{
    public DateTime Date { get; set; }
    public string Severity { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public Dictionary<string, string> Fields { get; set; } = new();
}
