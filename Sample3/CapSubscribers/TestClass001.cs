using Common.CAP;
using DotNetCore.CAP;
using System.Diagnostics;
using WebAppl2.Middlers;
using WebAppl2.Models;

namespace WebAppl2.CapSubscribers;
   
[CapSubscribe(FlowName, true)]
public partial class TestClass001(ILogger<TestClass001> log)
    : BaseExecCap
{
    const string FlowName = "group.test2.test1";

    [Middler3]             
    [CapSubscribe("a3", true)]
    public async Task<object> Fx3(Model3 dto)
    {
        var data = dto.Data;
        await default(ValueTask);
        log.LogInformation("do {method}", nameof(Fx3));
        
        try
        {
            // do invoke async for db or httpapi or ...
            dto.S = null;
        }
        catch (Exception ex)
        {
            dto.S = ex.Message;
            throw;
        }
        
        return new { Code = 200, Data = DateTime.Now.Ticks };
    }
    
}
