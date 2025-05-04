using FxApi.Data;
using FxApi.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);





// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AnyOrigin", x => x.AllowAnyOrigin());
});

builder.Services.AddSingleton<IMarketContext, MarketContext>();
builder.Services.AddSingleton<IFcsService, FcsService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};



app.MapGet("/fxrun/symbol/{symbol}/period/{period}",
    async (string symbol, string period, IFcsService service) =>
{
    await service.TryCollectData(symbol, period);

    return Results.Ok("!!");
})
.WithOpenApi(x => new OpenApiOperation(x)
{
    Summary = "Execute FX RUN",
    Description = "Returns information about all the available books from the Amy's library.",
    Tags = new List<OpenApiTag> { new() { Name = "Amy's Library" } }
});


app.MapGet("/fxrun/symbol/{symbol}/period/{period}/get",
    async (string symbol, string period, IFcsService service) =>
    {
        var data = await service.Get(symbol, period);

        return Results.Ok(data);
    })
.WithOpenApi(x => new OpenApiOperation(x)
{
    Summary = "Execute FX RUN",
    Description = "Returns information about all the available books from the Amy's library.",
    Tags = new List<OpenApiTag> { new() { Name = "Amy's Library" } }
});


app.MapGet("/fx/run", async (IFcsService service) =>
{
    //await service.AddHistory();
    return Results.Ok("!!");
})
.WithOpenApi(x => new OpenApiOperation(x)
{
    Summary = "Execute FX RUN",
    Description = "Returns information about all the available books from the Amy's library.",
    Tags = new List<OpenApiTag> { new() { Name = "Amy's Library" } }
});



app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
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

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
