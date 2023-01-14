namespace WebApplication1.Endpoints;


public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public static class WeatherEndpoints
{
    public static void AddWeatherEndpoint(this WebApplication app)
    {
        app.MapGet("/weatherforecast", GetWeatherForecast)
        .WithOpenApi();
    }

    public static async Task<IResult> GetWeatherForecast(ILogger<WeatherForecast> logger)
    {
        var summaries = new[]{
            "Freezing", "Bracing",
            "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot",
            "Sweltering", "Scorching"
        };
        await Task.CompletedTask;
        var forecast = Enumerable.Range(1, 5).Select(index =>
               new WeatherForecast
               (
                   DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                   Random.Shared.Next(-20, 55),
                   summaries[Random.Shared.Next(summaries.Length)]
               ))
               .ToArray();

        logger.LogInformation("Weather called");

        return Results.Ok(forecast);
    }
}
