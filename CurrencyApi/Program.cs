using Common.Config;
using CurrencyApi;
using CurrencyDb;
using FixerApi;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Currency API", Version = "v1" });
});
builder.Services.Configure<FixerApiOptions>(builder.Configuration.GetSection("FixerApi"));
builder.Services.AddSingleton<ICurrencyApiService, CurrencyApiService>();
builder.Services.AddSingleton<IFixerApiClient, FixerApiClient>();
builder.Services.AddSingleton<IFixerRestClient, FixerRestClient>();
builder.Services.AddSingleton<ICurrencyDbClient, CurrencyDbClient>();

// TODO - Part 4 - At this place I would have added a background job that runs every 24 hours to retrieve the latest exchange rates.
// It would have used the DB (CurrencyDbClient) to get a list of symbols to get the info for, and then save it
// builder.Services.AddHostedService<RetrieveExchangeRates>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();