const string outputConsoleTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message}{NewLine}";
const string outputFileTemplate = outputConsoleTemplate + "{Exception}{NewLine}";

var builder = WebApplication.CreateBuilder(args);

var loggerConfiguration = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: outputConsoleTemplate)
    .WriteTo.File("logs/all_logs_.log", outputTemplate: outputFileTemplate, rollingInterval: RollingInterval.Day);

builder.Services
    .AddCors()
    .AddSignalR().AddNewtonsoftJsonProtocol();

builder.Services
    .AddBus(builder)
    .AddServices(builder, loggerConfiguration, builder.Configuration)
    .AddCache(builder)
    .AddEvents(builder, loggerConfiguration)
    .AddDomain(builder)
    .AddIdentity(builder)
    .AddHostedService<ServiceWorker>();

builder.Services
    .AddSerilog(loggerConfiguration.CreateLogger())
    .AddControllers()
    .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DescriptionContractResolver())
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Select(entry => string.Join(' ', entry.Value?.Errors.Select(error => error.ErrorMessage) ?? []));
            var errorMessage = string.Join(' ', errors);
            return new BadRequestObjectResult(errorMessage);
        };
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var openApiSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        BearerFormat = "JWT",
        In = ParameterLocation.Cookie,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http,
        Reference = new OpenApiReference {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, openApiSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { openApiSecurityScheme, [] }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(configurePolicy => configurePolicy
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

app.UseAuthorization();
app.UseAuthentication();

app
    .MapHubs()
    .MapControllers();

app.Run();

public partial class Program;
