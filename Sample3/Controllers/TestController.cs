using DotNetCore.CAP;
using Microsoft.AspNetCore.Mvc;
using WebAppl2.Models;

namespace WebAppl2.Controllers;

[Route("/api/[controller]")]
[ApiController]
public partial class TestController(ILogger<TestController> _log, ICapPublisher _cap) 
    : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpPost(nameof(Index3))]
    public async Task Index3()
    {
        var m = new Model3 { Data = new() { Str = "rgrgrg..." } };
        await _cap.PublishAsync("group.test2.test1.a3", m);
    }

    
}
