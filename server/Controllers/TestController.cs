using Microsoft.AspNetCore.Mvc;

namespace server.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TestController : ControllerBase
{
    public IActionResult GetTest()
    {
        return Ok(new
            {
                message = "Hello"
            }
        );
    }   
}