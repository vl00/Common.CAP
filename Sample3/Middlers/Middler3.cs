using Common.CAP;

namespace WebAppl2.Middlers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class Middler3 : CapSubscribeMiddlerAttribute
{
    public override async Task ExecuteAsync(CapSubscribeExecutingContext context, CapSubscribeMiddlerExecuteFunc next)
    {
        var log = context.ServiceProvider.GetService<ILogger<Middler3>>();
        log.LogInformation("before call middler {middler}", nameof(Middler3));

        var dto = context.Arguments.OfType<ICapParams>().FirstOrDefault();

        var ar = await next();

        if (dto != null)
        {
            dto.Res = ar.Result;
            context.DeliverMessage.Value = dto;
        }

        if (ar.Exception == null) log.LogInformation("after call middler {middler}", nameof(Middler3));
        else log.LogError(ar.Exception, "after call middler {middler}", nameof(Middler3));
    }
}
