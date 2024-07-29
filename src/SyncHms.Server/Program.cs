var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddSqliteBus()
    .AddApplicationServices()
    .AddCache<MemoryCache>()
    .AddApplicationEvents()
    .AddSqliteDomain<ApplicationEnvironment>()
    .AddSqliteIdentity();

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
