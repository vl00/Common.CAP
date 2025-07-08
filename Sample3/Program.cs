using Serilog;
using System.Diagnostics;
using WebAppl2;
using WebAppl2.Controllers;
using WebAppl2.Common;

//-----------------------------------------------------------------------------------------------------------
var appBuilder = WebApplication.CreateBuilder(args);
ConfigureAppsettings(appBuilder.Environment, appBuilder.Configuration);
ConfigureLogging(appBuilder.Host, appBuilder.Logging, appBuilder.Configuration);
appBuilder.Services.AddHostedService(sp => 
{
    var f = new FuncHostedLifecycleService(sp);
    f.OnApplicationStarting = (s, _) => OnApplicationStarting(s);
    f.OnApplicationStopping = (s, _) => OnApplicationStopping(s);
    return f;
});
ConfigureServices(appBuilder.Services, appBuilder.Configuration);
//-----------------------------------------------------------------------------------------------------------
await using var app = appBuilder.Build();
ConfigureAppUse(app);
await app.RunAsync();
Log.CloseAndFlush();
//-----------------------------------------------------------------------------------------------------------

void ConfigureAppsettings(IWebHostEnvironment environment, IConfigurationBuilder configuration)
{
}

void ConfigureLogging(IHostBuilder hostBuilder, ILoggingBuilder builder, IConfiguration configuration)
{
    builder.ClearProviders();

    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(configuration)
        .Enrich.FromLogContext()
        //
        .WriteTo.Console(
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3} ({SourceContext})] {Message:lj}{NewLine}{Exception}"
        )
        //      
        //.WriteTo.Console(new Serilog.Formatting.Elasticsearch.ElasticsearchJsonFormatter())
        //
        .CreateLogger();

    hostBuilder.UseSerilog();
}

void ConfigureServices(IServiceCollection services, IConfiguration config)
{
    services.AddOptions();
    services.AddHttpContextAccessor();

    services.AddControllersWithViews()
        .AddNewtonsoftJson(opts => 
        {
            opts.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            opts.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
        })
        ; //

    services.AddSwaggerEx(typeof(TestController), "Sample3", "v1");

    // cap
    ProgramUtils.AddCap(services, config);
    
}

void ConfigureAppUse(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        // ...
        app.UseDeveloperExceptionPage();
    }
    else
    {
        // ...
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();
    app.UseRouting();

    //app.UseAuthorization();


    app.MapGet("/", () => "Hello World");

    app.UseSwaggerEx("Sample3", "v1");

    app.MapControllerRoute(
        name: "Area",
        pattern: "{area:exists}/{controller=Values}/{action=Test}/{id?}");

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
}

async Task OnApplicationStarting(IServiceProvider services)
{
    services.GetService<ILoggerFactory>().CreateLogger("app").LogInformation("web app started");
    //Debugger.Break();
    await default(ValueTask);
}

async Task OnApplicationStopping(IServiceProvider services)
{
    services.GetService<ILoggerFactory>().CreateLogger("app").LogInformation("web app stopping");
    await default(ValueTask);
}
