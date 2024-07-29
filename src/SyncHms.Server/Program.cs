var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSqliteBus(options => options.InitDatabase = InitDatabase.EnsureCreated)
    .AddApplicationServices()
    .AddCache<MemoryCache>()
    .AddApplicationEvents()
    .AddSqliteDomain<ApplicationEnvironment>(options => options.InitDatabase = InitDatabase.EnsureCreated)
    .AddSqliteIdentity(options => options.InitDatabase = InitDatabase.EnsureCreated);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
