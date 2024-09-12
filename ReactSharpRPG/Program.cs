using MongoDB.Driver;
using ReactSharpRPG.Data;
using ReactSharpRPG.Repositories;
using ReactSharpRPG.Services;

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
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();  // InventoryService
builder.Services.AddScoped<IBattleRepository, BattleRepository>();
builder.Services.AddScoped<IBattleService, BattleService>();  // BattleService
builder.Services.AddScoped<IEnemyRepository, EnemyRepository>();
builder.Services.AddScoped<IEnemyService, EnemyService>();

// Register SeedData to populate initial data
builder.Services.AddScoped<SeedData>();

// Add controllers and services to the container
builder.Services.AddControllers();

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
app.UseAuthorization();
app.MapControllers();
app.Run();
