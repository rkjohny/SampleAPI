using Microsoft.EntityFrameworkCore;
using SampleAPI.Core;
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

// Configure thread pool
ThreadPool.GetMinThreads(out int minWorkerThreads, out int minIoThreads);
ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxIoThreads);

var threadPoolSettings = builder.Configuration.GetSection("ThreadPoolSettings");
int minThreads = threadPoolSettings.GetValue<int>("MinThreads");
int maxThreads = threadPoolSettings.GetValue<int>("MaxThreads");
int minThreadsIo = threadPoolSettings.GetValue<int>("MinIoThreads");
int maxThreadsIo = threadPoolSettings.GetValue<int>("MaxIoThreads");

ThreadPool.SetMinThreads(minThreads, minThreadsIo);
ThreadPool.SetMaxThreads(maxThreads, maxThreadsIo);


// Add services to the container.
builder.Services.AddControllers();

// TODO: warning To protect potentially sensitive information in your connection string, 
// you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 
// for guidance on storing connection strings.
builder.Services.AddDbContext<PersonRepositoryInMemory>(options =>
{
    options.UseInMemoryDatabase("PersonList");
});
builder.Services.AddDbContext<PersonRepositoryPgSql>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("PgSql"));
});
builder.Services.AddDbContext<PersonRepositoryMySql>(options =>
{
    options.UseMySQL(builder.Configuration.GetConnectionString("MySql") ?? string.Empty);
});

builder.Services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(("127.0.0.1:6379")));

builder.Services.AddScoped<PersonRepositoryRedis>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<AddPersonHelper>();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddExceptionHandler<BadRequestExceptionHandler>();
builder.Services.AddExceptionHandler<ApiNotFoundExceptionHandler>();
builder.Services.AddProblemDetails();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<PrepareCacheHostedService>();
//builder.Services.AddHostedService<PrepareCacheHostedService>(implementationFactory: x => x.GetService<PrepareCacheHostedService>() ?? throw new InvalidOperationException());
builder.Services.AddHostedService<PrepareCacheHostedService>();

builder.Services.AddSingleton<ICacheService, PersonCacheService>();

builder.Services.AddScoped<Api>();

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
