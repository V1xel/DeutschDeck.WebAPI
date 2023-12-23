using DeutschDeck.WebAPI;
using dotenv.net;
using GraphQL;

var envVars = DotEnv.Read();
var WEB_APP_ENDPOINT = envVars["WEB_APP_ENDPOINT"];

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQL((builder) =>
{
    builder.AddSystemTextJson();
    builder.AddErrorInfoProvider((opts, serviceProvider) => opts.ExposeExceptionDetails = true);
    builder.AddSchema<DDSchema>();
});

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
app.UseHttpsRedirection();

app.UseGraphQL("/graphql");
app.UseGraphQLAltair();
app.Run();
