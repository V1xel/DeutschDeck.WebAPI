using DeutschDeck.WebAPI;
using DeutschDeck.WebAPI.Database;
using dotenv.net;
using GraphQL;
using Microsoft.EntityFrameworkCore;

var envVars = DotEnv.Read();
var WEB_APP_ENDPOINT = envVars["WEB_APP_ENDPOINT"];

var POSTGRES_HOST = envVars["POSTGRES_HOST"];
var POSTGRES_DB = envVars["POSTGRES_DB"];
var POSTGRES_USER = envVars["POSTGRES_USER"];
var POSTGRES_PASSWORD = envVars["POSTGRES_PASSWORD"];
var POSTGRES_CONNECTION = string.Format("Host={0};Database={1};Username={2};Password={3}", POSTGRES_HOST, POSTGRES_DB, POSTGRES_USER, POSTGRES_PASSWORD);

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQL((builder) =>
{
    builder.AddSystemTextJson();
    builder.AddErrorInfoProvider((opts, serviceProvider) => opts.ExposeExceptionDetails = true);
    builder.AddSchema<DDSchema>();
});

builder.Services.AddSingleton<DDSchema>();
builder.Services.AddDbContext<DDContext>(options => options.UseNpgsql(POSTGRES_CONNECTION), ServiceLifetime.Singleton);

var OriginsPolicy = "_AllowLocalOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(OriginsPolicy,
                      builder =>
                      {
                          builder.WithOrigins(WEB_APP_ENDPOINT)
                                 .AllowAnyHeader()
                                 .AllowAnyMethod();
                      });
});

var app = builder.Build();

app.UseCors(OriginsPolicy);
app.UseHttpsRedirection();

app.UseGraphQL("/graphql");
app.UseGraphQLAltair();
app.Run();
