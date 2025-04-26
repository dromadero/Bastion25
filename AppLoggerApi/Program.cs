using AppLoggerApi.Data;
using AppLoggerApi.Mapping;
using AppLoggerApi.Model;
using AppLoggerApi.Model.Dto;
using AppLoggerApi.Services;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    // WebRootPath = "./wwwroot",
    // EnvironmentName = Environment.GetEnvironmentVariable("env"),
    // ApplicationName = "Library.Api"
});


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AnyOrigin", x => x.AllowAnyOrigin());
});

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.IncludeFields = true;
});

builder.Configuration.AddJsonFile("appsettings.Local.json", true, true);

builder.Services.AddSingleton<IDbConnectionFactory>(_ =>
    //new SqliteConnectionFactory(builder.Configuration.GetValue<string>("ConnectionStrings:LiteSql"))
    new NpgsqlConnectionFactory(builder.Configuration.GetValue<string>("ConnectionStrings:Postgresql"))
    );
builder.Services.AddSingleton<ILoggerAppService, LoggerAppService>();
//builder.Services.AddSingleton<DatabaseInitializer>();
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


app.MapGet("/applogs", async (ILoggerAppService service) =>
{
    var items = await service.GetAllAsync();
    return Results.Ok(items);
})
.Produces<IEnumerable<LogItem>>(200)
.WithSummary("This is a summary.")
.WithDescription("This is a description.");

app.MapGet("/applogs/getapps", async (ILoggerAppService service) =>
{
    var items = await service.GetAllApps();
    return Results.Ok(items);
})
.Produces<IEnumerable<string>>(200)
.WithSummary("This is a summary.")
.WithDescription("This is a description.");



app.MapGet("/applogs/{appid}", async (string appid, ILoggerAppService service) =>
{
    var items = await service.GetByAppIdAsync(appid);
    return Results.Ok(items);
})
.Produces<IEnumerable<LogItem>>(200)
.WithSummary("This is a summary.")
.WithDescription("This is a description."); ;


app.MapPost("/applogs",
    async (CreateLogItemRequestDto itemDto, ILoggerAppService service) =>
    {
        var item = itemDto.MapToItem();

        var created = await service.CreateAsync(item, default);

        if (!created)
        {
            return Results.BadRequest(":(");
        }
        return Results.Ok(item);

    })
.Accepts<LogItem>("application/json");


app.MapDelete("applogs/{id}", async (int id, ILoggerAppService service) =>
{
    var deleted = await service.DeleteAsync(id);
    return deleted ? Results.NoContent() : Results.NotFound();
})
.WithName("DeleteAppLog").Produces(204).Produces(404);



app.MapGet("/", () => "Hello world!!!!!")
.WithSummary("This is a summary.")
.WithDescription("This is a description."); ;


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
