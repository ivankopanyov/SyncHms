var builder = WebApplication.CreateBuilder(args);

var loggerConfiguration = new LoggerConfiguration()
    .WriteTo.File("logs/all_logs_.log", rollingInterval: RollingInterval.Day);

builder.Services
    .AddBus(builder)
    .AddServices(builder, loggerConfiguration)
    .AddCache(builder)
    .AddEvents(builder, loggerConfiguration)
    .AddDomain(builder)
    .AddIdentity(builder);

builder.Services.AddSerilog(loggerConfiguration.CreateLogger());
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
