using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using LogParser.Web.Models;

namespace LogParser.Web.Services;

public class SearchService
{
    private readonly IEnumerable<ISearchProvider> _providers;

    public SearchService(IEnumerable<ISearchProvider> providers)
    {
        _providers = providers;
    }

    public IEnumerable<LogEntry> Search(IEnumerable<LogEntry> logs, SearchRequest request)
    {
        IEnumerable<LogEntry> result = request.Combinator?.Equals("or", StringComparison.OrdinalIgnoreCase) == true
            ? Enumerable.Empty<LogEntry>()
            : logs;

        foreach (var selection in request.Providers)
        {
            var provider = _providers.First(p => p.Name == selection.Name);
            var parameters = new Dictionary<string, object>();
            foreach (var param in provider.Parameters)
            {
                if (selection.Parameters.TryGetValue(param.Name, out var value))
                {
                    parameters[param.Name] = ConvertValue(value, param.Type);
                }
            }
            var matches = provider.Search(logs, parameters);
            if (request.Combinator?.Equals("or", StringComparison.OrdinalIgnoreCase) == true)
            {
                result = result.Union(matches);
            }
            else
            {
                result = result.Intersect(matches);
            }
        }
        return result;
    }

    private static object ConvertValue(string value, ParameterType type) => type switch
    {
        ParameterType.Int => int.Parse(value, CultureInfo.InvariantCulture),
        ParameterType.DateTime => DateTime.Parse(value, null, DateTimeStyles.RoundtripKind),
        _ => value
    };
}
