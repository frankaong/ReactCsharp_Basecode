using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Services;
using ASI.Basecode.WebApp.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class PreferenceController : ControllerBase
    {
        private readonly IPreferenceService _preferenceService;

        public PreferenceController(IPreferenceService preferenceService)
        {
            _preferenceService = preferenceService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(int userId)
        {
            var pref = await _preferenceService.GetByUserIdAsync(userId);
            if (pref == null)
            {
                // Return default preferences if not found
                return Ok(new
                {
                    userId,
                    showStats = true,
                    showSatisfaction = true,
                    cardOrder = new List<string> { "unresolved", "overdue", "dueToday", "resolved" }
                });
            }

            // Return as array for frontend
            return Ok(new
            {
                userId = pref.UserId,
                showStats = pref.ShowStats ?? true,
                showSatisfaction = pref.ShowSatisfaction ?? true,
                cardOrder = pref.CardOrderList
            });
            //return Ok(new { message = "Preference endpoint is working", userId });
        }

        [HttpPatch("{userId}")]
        public async Task<IActionResult> Patch(int userId, [FromBody] PreferenceDto dto)
        {
            if (dto == null || dto.UserId != userId)
                return BadRequest();

            // Get or create preference
            var pref = await _preferenceService.GetByUserIdAsync(userId) ?? new Preference { UserId = userId };

            pref.ShowStats = dto.ShowStats;
            pref.ShowSatisfaction = dto.ShowSatisfaction;
            pref.CardOrderList = dto.CardOrder;

            await _preferenceService.AddOrUpdateAsync(pref);
            return Ok();
        }

        // DTO for PATCH
        public class PreferenceDto
        {
            public int UserId { get; set; }
            public bool? ShowStats { get; set; }
            public bool? ShowSatisfaction { get; set; }
            public List<string> CardOrder { get; set; }
        }
    }
}
