using System.Collections.Generic;
using LogParser.Web.Models;

namespace LogParser.Web.Services;

public class LogStore
{
    private List<LogEntry> _logs = new();

    public IReadOnlyList<LogEntry> Logs => _logs;

    public void SetLogs(IEnumerable<LogEntry> entries)
    {
        _logs = new List<LogEntry>(entries);
    }
}
