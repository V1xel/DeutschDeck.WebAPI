using DeutschDeck.WebAPI.Database;
using DeutschDeck.WebAPI.Emails;
using DeutschDeck.WebAPI.Graphql;
using DeutschDeck.WebAPI.Utilities;
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

var EMAIL_DELIVERY_TOKEN = envVars["EMAIL_DELIVERY_TOKEN"];
var EMAIL_DELIVERY_ENDPOINT = envVars["EMAIL_DELIVERY_ENDPOINT"];
var EMAIL_DELIVERY_SENDER = envVars["EMAIL_DELIVERY_SENDER"];
var EMAIL_DELIVERY_SIGNIN_TEMPLATE = envVars["EMAIL_DELIVERY_SIGNIN_TEMPLATE"];
var EMAIL_DELIVERY_SIGNIN_SUBJECT = envVars["EMAIL_DELIVERY_SIGNIN_SUBJECT"];

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
