using DHCPManagerAPI.Models;
using DHCPManagerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DHCPManagerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostController : ControllerBase
    {
        private readonly GoogleSheetsService _googleSheetsService;

        public HostController(GoogleSheetsService googleSheetsService)
        {
            _googleSheetsService = googleSheetsService;
        }

        // 📌 GET: api/host
        [HttpGet]
        public async Task<ActionResult<List<DHCPHost>>> GetHosts()
        {
            var hosts = await _googleSheetsService.GetHostsFromSheet();
            return Ok(hosts);
        }

        // 📌 POST: api/host
        [HttpPost]
        public async Task<IActionResult> AddHost([FromBody] DHCPHost host)
        {
            if (host == null)
            {
                return BadRequest("Dados inválidos.");
            }

            await _googleSheetsService.AddHostToSheet(host.IpAddress, host.NomeNetBIOS, host.MacAddress, host.Vlan);
            return CreatedAtAction(nameof(GetHosts), new { ip = host.IpAddress }, host);
        }
    }
}
