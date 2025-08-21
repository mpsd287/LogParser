using System;
using System.Collections.Generic;
using System.Linq;
using LogParser.Web.Models;
using LogParser.Web.Services;

namespace LogParser.Web.SearchProviders;

public class DateRangeSearchProvider : ISearchProvider
{
    public string Name => "DateRange";

    public IReadOnlyList<SearchParameter> Parameters { get; } = new[]
    {
        new SearchParameter("from", ParameterType.DateTime),
        new SearchParameter("to", ParameterType.DateTime)
    };

    public IEnumerable<LogEntry> Search(IEnumerable<LogEntry> logs, IReadOnlyDictionary<string, object> parameters)
    {
        var from = parameters.ContainsKey("from") ? (DateTime)parameters["from"] : DateTime.MinValue;
        var to = parameters.ContainsKey("to") ? (DateTime)parameters["to"] : DateTime.MaxValue;
        return logs.Where(l => l.Date >= from && l.Date <= to);
    }
}
