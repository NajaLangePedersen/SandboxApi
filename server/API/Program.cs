using Infrastructure.Data;
using LinqToDB;
using LinqToDB.AspNet;
using Service;

var builder = WebApplication.CreateBuilder(args);

// 1. Register controllers and OpenApi/Swagger
builder.Services.AddControllers();
builder.Services.AddOpenApi();

//This (lines 13-21 + 37) is added in step 13 (sandbox), after changing appsetting.Development.json.
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod();
    });
});

// 2. Register linq2db context
builder.Services.AddLinqToDBContext<AppDataConnection>((provider, options) =>
    options.UseSQLite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=app.db"));

// 3. Register the Service layer in a DI container
builder.Services.AddScoped<ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowFrontend");

app.UseAuthorization();
app.MapControllers();

app.Run();
