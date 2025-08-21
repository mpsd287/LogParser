using System;
using System.Collections.Generic;
using System.Linq;
using LogParser.Web.Models;
using LogParser.Web.Services;

namespace LogParser.Web.SearchProviders;

public class ContainsTextSearchProvider : ISearchProvider
{
    public string Name => "ContainsText";

    public IReadOnlyList<SearchParameter> Parameters { get; } = new[]
    {
        new SearchParameter("text", ParameterType.String)
    };

    public IEnumerable<LogEntry> Search(IEnumerable<LogEntry> logs, IReadOnlyDictionary<string, object> parameters)
    {
        var text = parameters.ContainsKey("text") ? parameters["text"]?.ToString() ?? string.Empty : string.Empty;
        return logs.Where(l => l.Message.Contains(text, StringComparison.OrdinalIgnoreCase));
    }
}
