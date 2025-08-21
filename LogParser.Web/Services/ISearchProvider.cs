using System.Collections.Generic;
using LogParser.Web.Models;

namespace LogParser.Web.Services;

public interface ISearchProvider
{
    string Name { get; }
    IReadOnlyList<SearchParameter> Parameters { get; }
    IEnumerable<LogEntry> Search(IEnumerable<LogEntry> logs, IReadOnlyDictionary<string, object> parameters);
}
