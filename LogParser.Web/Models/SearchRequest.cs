using System.Collections.Generic;

namespace LogParser.Web.Models;

public record SearchRequest(string Combinator, List<ProviderSelection> Providers);

public record ProviderSelection(string Name, Dictionary<string, string> Parameters);
