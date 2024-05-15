using Common;
using Common.CAP;
using System.Diagnostics;

namespace WebAppl2.Middlers;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class Middler1 : CapSubscribeMiddlerAttribute
{
    public override async Task ExecuteAsync(CapSubscribeExecutingContext context, CapSubscribeMiddlerExecuteFunc next)
    {
        Debugger.Break();        
        await next();
        Debugger.Break();
    }
}
