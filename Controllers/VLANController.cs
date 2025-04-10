using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/vlans")]
public class VLANController : ControllerBase
{
    [HttpGet]
    public IActionResult GetVlans()
    {
        return Ok(new { message = "API de VLANs funcionando!" });
    }
}
