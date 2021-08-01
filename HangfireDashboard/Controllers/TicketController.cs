using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireDashboard.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly ILogger<TicketController> _logger;
        private readonly IOptions<TicketConfig> _config;

        public TicketController(ILogger<TicketController> logger, IOptions<TicketConfig> config)
        {
            _logger = logger;
            _config = config;
        }

        [HttpGet]
        public IActionResult Get()
        {

            string storagePath = _config.Value.TicketPath;

            BackgroundAppService.TicketGenerator ticketGenerator = new BackgroundAppService.TicketGenerator();
            ticketGenerator.RegisterCreateNewTicket(storagePath);
            return Ok(storagePath);
        }
    }
}
