using LogParser.Web.Models;
using LogParser.Web.SearchProviders;
using LogParser.Web.Services;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<LogStore>();
builder.Services.AddSingleton<SearchService>();
builder.Services.AddSingleton<ISearchProvider, ContainsTextSearchProvider>();
builder.Services.AddSingleton<ISearchProvider, SeveritySearchProvider>();
builder.Services.AddSingleton<ISearchProvider, DateRangeSearchProvider>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/upload", async (HttpRequest request, LogStore store) =>
{
    if (!request.HasFormContentType)
        return Results.BadRequest();
    var form = await request.ReadFormAsync();
    var file = form.Files.FirstOrDefault();
    if (file == null) return Results.BadRequest();
    using var stream = file.OpenReadStream();
    var entries = LogFileParser.Parse(stream);
    store.SetLogs(entries);
    return Results.Ok(new { count = entries.Count });
});

app.MapGet("/providers", (IEnumerable<ISearchProvider> providers) =>
{
    return providers.Select(p => new
    {
        name = p.Name,
        parameters = p.Parameters.Select(param => new { name = param.Name, type = param.Type.ToString() })
    });
});

app.MapPost("/search", (SearchRequest request, SearchService service, LogStore store) =>
{
    var results = service.Search(store.Logs, request);
    return Results.Ok(results);
});

app.Run();
