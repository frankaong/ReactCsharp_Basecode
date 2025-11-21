using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.DTO;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Services;
using ASI.Basecode.WebApp.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text.Json;
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateTicket([FromForm] CreateTicketDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var ticket = await _ticketService.CreateTicketAsync(dto);

            return Ok(new
            {
                message = "Ticket created successfully!",
                ticket
            });
        }


        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            await _ticketService.AutoMarkOverdueAsync();
            var tickets = _ticketService.GetTickets();
            return Ok(tickets);
        }


        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var ticket = _ticketService.GetById(id);
            if (ticket == null)
                return NotFound(new { message = "Ticket not found." });

            return Ok(ticket);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> AssignTicket(int id, [FromBody] JsonElement data)
        {
            try
            {
                if (!data.TryGetProperty("assignedTo", out var assignedToElement))
                {
                    return BadRequest(new { message = "assignedTo is required." });
                }

                var assignedTo = assignedToElement.GetInt32();

                await _ticketService.AssignTicketAsync(id, assignedTo);

                return Ok(new { message = "Ticket assigned successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }

        [HttpPut("Unassign/{id}")]
        public async Task<IActionResult> UnassignTicket(int id)
        {
            try
            {
                await _ticketService.UnassignTicketAsync(id);
                return Ok(new { message = "Ticket unassigned successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = _ticketService.GetById(id);
            if (ticket == null)
            {
                return NotFound(new { message = "Ticket not found." });
            }

            await _ticketService.DeleteAsync(ticket);
            return Ok(new { message = "Ticket deleted successfully!" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicket(int id, [FromBody] Ticket updatedTicket)
        {
            if (updatedTicket == null || updatedTicket.Id != id)
                return BadRequest(new { message = "Invalid ticket data." });

            try
            {
                await _ticketService.UpdateAsync(updatedTicket);
                return Ok(new { message = "Ticket updated successfully!" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AutoMarkOverdue()
        {
            try
            {
                await _ticketService.AutoMarkOverdueAsync();
                return Ok(new { message = "Overdue tickets updated successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> UploadAttachment([FromForm] IFormFile file)
        {
            try
            {
                var savedFileName = await _ticketService.UploadAttachmentAsync(file);
                return Ok(new { fileName = savedFileName });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "File upload failed.", error = ex.Message });
            }
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
