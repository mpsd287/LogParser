using System;
using System.Collections.Generic;
using System.Linq;
using LogParser.Web.Models;
using LogParser.Web.Services;

namespace LogParser.Web.SearchProviders;

public class SeveritySearchProvider : ISearchProvider
{
    public string Name => "Severity";

    public IReadOnlyList<SearchParameter> Parameters { get; } = new[]
    {
        new SearchParameter("level", ParameterType.String)
    };

    public IEnumerable<LogEntry> Search(IEnumerable<LogEntry> logs, IReadOnlyDictionary<string, object> parameters)
    {
        var level = parameters.ContainsKey("level") ? parameters["level"]?.ToString() ?? string.Empty : string.Empty;
        return logs.Where(l => l.Severity.Equals(level, StringComparison.OrdinalIgnoreCase));
    }
}
