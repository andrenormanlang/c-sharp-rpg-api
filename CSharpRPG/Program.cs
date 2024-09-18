using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using CSharpRPG.Data;
using CSharpRPG.Repositories;
using CSharpRPG.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB connection
var mongoDbConnectionString = builder.Configuration.GetConnectionString("MongoDbConnection");
var databaseName = builder.Configuration.GetSection("DatabaseSettings:DatabaseName").Value;

// MongoDB Client Setup
var mongoClient = new MongoClient(mongoDbConnectionString);
builder.Services.AddSingleton<IMongoClient>(mongoClient);

// Add MongoDB context to DI container
builder.Services.AddScoped(sp => new MongoDbContext(mongoClient, databaseName));

// Add services and repositories to the DI container
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IClassRepository, ClassRepository>();
builder.Services.AddScoped<IClassService, ClassService>();  // ClassService
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();  // InventoryService
builder.Services.AddScoped<IBattleRepository, BattleRepository>();
builder.Services.AddScoped<IBattleService, BattleService>();  // BattleService
builder.Services.AddScoped<IEnemyRepository, EnemyRepository>();
builder.Services.AddScoped<IEnemyService, EnemyService>();

// Register SeedData to populate initial data
builder.Services.AddScoped<SeedData>();

// Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("https://localhost:5173", "http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});


// Add controllers and services to the container
builder.Services.AddControllers();

// === JWT Authentication ===
// Fetch JWT secret from the environment or appsettings.json
var jwtSecret = builder.Configuration["Jwt:Secret"]
    ?? throw new ArgumentNullException("Jwt:Secret", "JWT Secret is missing from configuration");

// Configure JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,  // Set to true for real environments, to validate the token issuer
            ValidateAudience = false,  // Set to true for real environments, to validate the token audience
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret)),
            ValidateLifetime = true // Ensure token hasn't expired
        };
    });

// Configure Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Seed the data when the app starts
using (var scope = app.Services.CreateScope())
{
    var seedData = scope.ServiceProvider.GetRequiredService<SeedData>();
    await seedData.InitializeAsync();  // Seed the initial data
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS
app.UseCors("AllowFrontend");

// Enable Authentication and Authorization middle ware
app.UseAuthentication(); // This should come before UseAuthorization
app.UseAuthorization();

app.MapControllers();
app.Run();
