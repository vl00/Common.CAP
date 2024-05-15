using Common.CAP;
using DotNetCore.CAP;
using System.Diagnostics;
using WebAppl2.Middlers;
using WebAppl2.Models;

namespace WebAppl2.CapSubscribers;

//[Middler1]    
[CapSubscribe(FlowName, true)]
public partial class TestClass001 : BaseExecCap
{
    const string FlowName = "test1";

    [Middler1]                  
    [CapSubscribe("a", true)]  
    public async Task Fxa(Model1 dto, [FromCap] CapHeader headers, [FromCap] CancellationToken cancellation)
    {
        Debugger.Break();
        await default(ValueTask);
        Debugger.Break();
    }
    
}
