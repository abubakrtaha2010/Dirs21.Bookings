var builder = WebApplication.CreateBuilder(args);

// Load Configuration
builder.Configuration
    .AddConfiguration(new ConfigurationBuilder().AddJsonFile("appsettings.json", false).Build())
    .AddConfiguration(new ConfigurationBuilder().AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false).Build())
    .AddEnvironmentVariables();

// Add Services
builder.Services
    .AddApiServices()
    .AddAppServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddLogging()
    .AddMemoryCache();

// Build Application
var app = builder.Build();

// Configure Middlewares
app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(_ => _.SwaggerEndpoint("/swagger/v1.0/swagger.json", "API - V1"));

app.UseRouting();

app.UseCors(builder => builder
    .SetIsOriginAllowed(_ => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

app.UseAuthorization();

app.MapControllers();

// Run Application
await app.RunAsync().ConfigureAwait(true);