using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.WebApp.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [AllowAnonymous]
    public class TicketController : ControllerBase<TicketController>
    {
        private readonly ITicketService _ticketService;

        public TicketController(
            IHttpContextAccessor httpContextAccessor,
            ILoggerFactory loggerFactory,
            IConfiguration configuration,
            IMapper mapper,
            ITicketService ticketService
        ) : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _ticketService = ticketService;
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateTicket([FromBody] Ticket ticket)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    ticket.CreatedAt = DateTime.Now;

        //    await _ticketService.CreateTicket(ticket); 

        //    return Ok(new
        //    {
        //        message = "Ticket created successfully!",
        //        ticket
        //    });
        //}

        public IActionResult Index()
        {
            return View();
        }
    }
}
