var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Swagger UI endpoint - available in all environments
app.MapGet("/swagger", () => Results.Content(@"
<!DOCTYPE html>
<html>
<head>
    <title>API Documentation</title>
    <style>
        body { font-family: Arial, sans-serif; margin: 20px; background: #f5f5f5; }
        .container { max-width: 1000px; margin: 0 auto; background: white; padding: 20px; border-radius: 8px; }
        h1 { color: #333; }
        .endpoint { background: #f9f9f9; padding: 15px; margin: 15px 0; border-left: 4px solid #0066cc; }
        .method { font-weight: bold; color: #0066cc; }
        .path { font-family: monospace; background: #eee; padding: 5px 10px; border-radius: 3px; }
        .description { color: #666; margin: 10px 0; }
        .example { background: #f0f0f0; padding: 10px; border-radius: 3px; margin: 10px 0; font-family: monospace; font-size: 12px; overflow-x: auto; }
    </style>
</head>
<body>
    <div class=""container"">
        <h1>📚 Weather API Documentation</h1>
        <div class=""endpoint"">
            <div class=""method"">GET</div>
            <div class=""path"">/weatherforecast</div>
            <div class=""description"">Returns a 5-day weather forecast</div>
            <div class=""example"">
                <strong>Response:</strong><br/>
                [<br/>
                &nbsp;&nbsp;{ ""date"": ""2026-08-13"", ""temperatureC"": 4, ""summary"": ""Sweltering"", ""temperatureF"": 39 },<br/>
                &nbsp;&nbsp;{ ""date"": ""2026-08-14"", ""temperatureC"": 20, ""summary"": ""Hot"", ""temperatureF"": 67 }<br/>
                ]
            </div>
        </div>
    </div>
</body>
</html>
", "text/html"));


var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
