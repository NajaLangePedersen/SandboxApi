using Infrastructure.Data;
using LinqToDB;
using LinqToDB.AspNet;
using Service;

var builder = WebApplication.CreateBuilder(args);

// 1. Register controllers and OpenApi/Swagger
builder.Services.AddControllers();
builder.Services.AddOpenApi();

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

app.UseAuthorization();
app.MapControllers();

app.Run();
