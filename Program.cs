using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SampleAPI.Middlewares;
using SampleAPI.Repository;
using SampleAPI.Services;
using Serilog;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add logging provider
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.

// TODO: for distributed system use a distributed caching (as second layer caching of EF) like Redis, MemCache, HazelCast etc.
//builder.Services.AddMemoryCache();

builder.Services.AddControllers();


// TODO: warning To protect potentially sensitive information in your connection string, 
// you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 
// for guidance on storing connection strings.
builder.Services.AddDbContext<PersonRepositoryInMemory>(options =>
{
    options.UseInMemoryDatabase("PersonList");
    //options.UseMemoryCache(new MemoryCache(new MemoryCacheOptions()));
});
builder.Services.AddDbContext<PersonRepositoryPgSql>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PgSql"));
    // using default MemoryCacheOptions
    //options.UseMemoryCache(new MemoryCache(new MemoryCacheOptions()));
    
});
builder.Services.AddDbContext<PersonRepositoryMySql>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("MySql") ?? string.Empty);
    // using default MemoryCacheOptions
    //options.UseMemoryCache(new MemoryCache(new MemoryCacheOptions()));
});

builder.Services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(("127.0.0.1:6379")));

builder.Services.AddScoped<PersonRepositoryRedis>();
builder.Services.AddScoped<PersonService>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<ApiNotFoundExceptionHandler>();
builder.Services.AddProblemDetails();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<RequestEntryPointHandler>();
app.UseExceptionHandler();

app.Run();
