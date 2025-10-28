using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Data.Repositories;
using WebAPI.Data.Repositories.Impl;
using WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure CORS - Allow all origins for flexibility
// For production: Restrict to specific origins like:
// policy.WithOrigins("https://yourfrontend.com")
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

// Configure PostgreSQL Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException(
        "Database connection string not found. "
            + "Please set ConnectionStrings__DefaultConnection environment variable or configure DefaultConnection in appsettings.json"
    );
}

// Convert PostgreSQL URI format to Npgsql connection string format if needed
// Supports both formats:
// - postgresql://user:password@host:port/database
// - Host=host;Port=port;Database=database;Username=user;Password=password
if (connectionString.StartsWith("postgres://") || connectionString.StartsWith("postgresql://"))
{
    connectionString = ConvertPostgresUriToConnectionString(connectionString);
}
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

// Register repositories
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Auto-migrate database on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        Console.WriteLine("🔄 DEBUG: Applying database migrations...");
        
        // Check if database exists, if not create it
        if (!context.Database.CanConnect())
        {
            Console.WriteLine("🆕 DEBUG: Database doesn't exist, creating...");
            context.Database.EnsureCreated();
            Console.WriteLine("✅ DEBUG: Database created successfully!");
        }
        else
        {
            // Apply any pending migrations
            context.Database.Migrate();
            Console.WriteLine("✅ DEBUG: Database migrations applied successfully!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ DEBUG: Failed to setup database: {ex.Message}");
        throw;
    }
}

// Configure middleware
app.ConfigureExceptionHandler(app.Logger);
app.UseCors();
app.MapControllers();

app.Run();

// Helper method to convert PostgreSQL URI to Npgsql connection string
static string ConvertPostgresUriToConnectionString(string postgresUri)
{
    try
    {
        var uri = new Uri(postgresUri);
        var userInfo = uri.UserInfo.Split(':');
        var username = userInfo.Length > 0 ? userInfo[0] : "postgres";
        var password = userInfo.Length > 1 ? userInfo[1] : "";
        var host = uri.Host;
        var port = uri.Port > 0 ? uri.Port : 5432;
        var database = uri.AbsolutePath.TrimStart('/');

        return $"Host={host};Port={port};Database={database};Username={username};Password={password};";
    }
    catch (Exception ex)
    {
        throw new InvalidOperationException(
            $"Invalid PostgreSQL URI format. Expected: postgresql://user:password@host:port/database. Error: {ex.Message}"
        );
    }
}
