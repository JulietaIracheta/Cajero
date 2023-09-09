using Cajero.DataAccess;
using Cajero.Service;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    var env = hostingContext.HostingEnvironment;

    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

    if (env.IsDevelopment())
    {
        var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
        if (appAssembly != null)
        {
            config.AddUserSecrets(appAssembly, optional: true);
        }
    }

    config.AddEnvironmentVariables();

    if (args != null)
    {
        config.AddCommandLine(args);
    }
});

// ------------------------------ Conexion a DB: ---------------------------
//  Extraer ConnectionString del appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
var connectionString = builder.Configuration.GetConnectionString("CajeroDB");
builder.Services.AddDbContext<ContextDB>(x => x.UseSqlServer(connectionString));
// -------------------------------------------------------------------------

builder.Services.AddScoped<CajeroLogic>();
builder.Services.AddScoped<UsuarioLogic>();
builder.Services.AddScoped<OperacionesLogic>();

builder.Services.AddScoped<CajeroAccess>();
builder.Services.AddScoped<UsuarioAccess>();
builder.Services.AddScoped<OperacionesAccess>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();