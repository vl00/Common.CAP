using Common.CAP;
using DotNetCore.CAP;
using System.Diagnostics;
using WebAppl2.Middlers;
using WebAppl2.Models;

namespace WebAppl2.CapSubscribers;

//[Middler1]    
[CapSubscribe(FlowName, true)]
public partial class TestClass001(ILogger<TestClass001> log)
    : BaseExecCap
{
    const string FlowName = "group.test2.test1";

    [Middler1]             
    [CapSubscribe("a", true)]  
    public async Task Fxa(Model1 dto, [FromCap] CapHeader headers, [FromCap] CancellationToken cancellation)
    {
        Debugger.Break();
        log.LogInformation("do {method}", nameof(Fxa));
        await default(ValueTask);
        Debugger.Break();
    }
    
}
