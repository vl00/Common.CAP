using Common.CAP;
using DotNetCore.CAP;
using DotNetCore.CAP.Messages;

namespace WebAppl2;

internal static partial class ProgramUtils
{
    public static void AddCap(IServiceCollection services, IConfiguration configuration)
    {
        services.AddCap(x =>
        {
            configuration.BindObj("cap", x, null);

            // utf8-json
            {
                x.JsonSerializerOptions.IncludeFields = true;
                x.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                x.JsonSerializerOptions.UnknownTypeHandling = System.Text.Json.Serialization.JsonUnknownTypeHandling.JsonElement;
            }

            x.UseMySql(m =>
            {
                m.ConnectionString = configuration["cap:mysql:conn"];
                m.ConnectionString = string.IsNullOrEmpty(m.ConnectionString) ? configuration[configuration["cap:mysql:ref-conn"]] : m.ConnectionString;
            });
            x.UseRabbitMQ(z =>
            {
                configuration.BindObj("cap:rabbitmq", z, (config, _) =>
                {
                    z.BasicQosOptions = new RabbitMQOptions.BasicQos(ushort.Parse(config["use-qos"]), false);
                });
            });
            x.FailedThresholdCallback = failed =>
            {
                var logger = failed.ServiceProvider.GetService<ILoggerFactory>().CreateLogger("app");
                logger.LogError("A message of type {type} failed after executing {failedRetryCount} several times, requiring manual troubleshooting. Message name: {messageName}", failed.MessageType, x.FailedRetryCount, failed.Message.GetName());
            };
        })
        //
        .AddSubscriberAssembly(AppDomain.CurrentDomain.GetAssemblies())
        ; ;

        // add support sub-midders
        services.AddSupportCapSubscribeFilterToMiddlers((_, f) => 
        {
            f.AddMiddler(new ExecCapResultAttr(), 1_0000_0000);
        });
        
        
    }
}
