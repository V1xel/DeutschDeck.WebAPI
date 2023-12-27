using DeutschDeck.WebAPI.Database;
using DeutschDeck.WebAPI.Database.Repositories;
using DeutschDeck.WebAPI.Domain;
using DeutschDeck.WebAPI.Emails;
using DeutschDeck.WebAPI.Graphql;
using DeutschDeck.WebAPI.Utilities;
using GraphQL;
using Microsoft.EntityFrameworkCore;

var WEB_APP_ENDPOINT = EnvironmentFallbackProvider.GetEnvironmentVariable("WEB_APP_ENDPOINT");

var RUNNING_IN_CONTAINER = EnvironmentFallbackProvider.GetEnvironmentVariable("RUNNING_IN_CONTAINER");
var POSTGRES_HOST = bool.Parse(RUNNING_IN_CONTAINER) ? EnvironmentFallbackProvider.GetEnvironmentVariable("POSTGRES_HOST") : EnvironmentFallbackProvider.GetEnvironmentVariable("POSTGRES_LOCALHOST");
var POSTGRES_DB = EnvironmentFallbackProvider.GetEnvironmentVariable("POSTGRES_DB");
var POSTGRES_USER = EnvironmentFallbackProvider.GetEnvironmentVariable("POSTGRES_USER");
var POSTGRES_PASSWORD = EnvironmentFallbackProvider.GetEnvironmentVariable("POSTGRES_PASSWORD");
var POSTGRES_CONNECTION = string.Format("Host={0};Database={1};Username={2};Password={3}", POSTGRES_HOST, POSTGRES_DB, POSTGRES_USER, POSTGRES_PASSWORD);

var EMAIL_DELIVERY_TOKEN = EnvironmentFallbackProvider.GetEnvironmentVariable("EMAIL_DELIVERY_TOKEN");
var EMAIL_DELIVERY_ENDPOINT = EnvironmentFallbackProvider.GetEnvironmentVariable("EMAIL_DELIVERY_ENDPOINT");
var EMAIL_DELIVERY_SENDER = EnvironmentFallbackProvider.GetEnvironmentVariable("EMAIL_DELIVERY_SENDER");
var EMAIL_DELIVERY_SIGNIN_TEMPLATE = EnvironmentFallbackProvider.GetEnvironmentVariable("EMAIL_DELIVERY_SIGNIN_TEMPLATE");
var EMAIL_DELIVERY_SIGNIN_SUBJECT = EnvironmentFallbackProvider.GetEnvironmentVariable("EMAIL_DELIVERY_SIGNIN_SUBJECT");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGraphQL((builder) =>
{
    builder.AddSystemTextJson();
    builder.AddErrorInfoProvider((opts, serviceProvider) => opts.ExposeExceptionDetails = true);
    builder.AddSchema<DDSchema>();
});

builder.Services.AddSingleton<PasswordGenerationUtility>();

builder.Services.AddSingleton(new SignupTemplateProviderConfiguration(EMAIL_DELIVERY_SIGNIN_TEMPLATE, EMAIL_DELIVERY_SIGNIN_SUBJECT));
builder.Services.AddSingleton<SignupTemplateProvider>();
builder.Services.AddSingleton(new EmailDeliveryAdapterConfiguration(EMAIL_DELIVERY_TOKEN, EMAIL_DELIVERY_ENDPOINT, EMAIL_DELIVERY_SENDER));
builder.Services.AddSingleton<IEmailDeliveryAdapter, MailerSendAdapter>();

builder.Services.AddDbContext<DDContext>(options => options.UseNpgsql(POSTGRES_CONNECTION), ServiceLifetime.Singleton);

builder.Services.AddSingleton<UserRepository>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<DDQueries>();
builder.Services.AddSingleton<DDMutations>();
builder.Services.AddSingleton<DDSchema>();

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

app.UseGraphQL("/graphql");
app.UseGraphQLAltair();
app.Run();
