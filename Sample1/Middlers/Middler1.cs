using Common;
using Common.CAP;
using System.Diagnostics;

namespace WebAppl2.Middlers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class Middler1 : CapSubscribeMiddlerAttribute
{
    public override async Task ExecuteAsync(CapSubscribeExecutingContext context, CapSubscribeMiddlerExecuteFunc next)
    {
        var log = context.ServiceProvider.GetService<ILogger<Middler1>>();

        Debugger.Break();        
        log.LogInformation("before call middler {middler}", nameof(Middler1));
        var ar = await next();
        if (ar.Exception == null) log.LogInformation("after call middler {middler}", nameof(Middler1));
        else log.LogError(ar.Exception, "after call middler {middler}", nameof(Middler1));
        Debugger.Break();
    }
}
