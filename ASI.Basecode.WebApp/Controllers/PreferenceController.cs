using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.Services;
using ASI.Basecode.WebApp.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ASI.Basecode.Data.Models;
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
        public async Task<IActionResult> GetPreferences(int userId)
        {
            var pref = await _preferenceService.GetByUserIdAsync(userId);
            if (pref == null)
            {
                return Ok(new
                {
                    userId,
                    showStats = true,
                    showSatisfaction = true,
                    cardOrder = new List<string> { "unresolved", "overdue", "dueToday", "resolved" }
                });
            }

            return Ok(new
            {
                userId = pref.UserId,
                showStats = pref.ShowStats ?? true,
                showSatisfaction = pref.ShowSatisfaction ?? true,
                cardOrder = pref.CardOrderList
            });
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdatePreferences(int userId, [FromBody] PreferenceDto dto)
        {
            if (dto == null || dto.UserId != userId)
                return BadRequest();

            var pref = await _preferenceService.GetByUserIdAsync(userId);

            if (pref == null)
            {
                pref = new Preference { UserId = userId };
            }

            pref.ShowStats = dto.ShowStats;
            pref.ShowSatisfaction = dto.ShowSatisfaction;
            pref.CardOrderList = dto.CardOrder;

            await _preferenceService.AddOrUpdateAsync(pref);
            return Ok();
        }

        public class PreferenceDto
        {
            public int UserId { get; set; }
            public bool? ShowStats { get; set; }
            public bool? ShowSatisfaction { get; set; }
            public List<string> CardOrder { get; set; }
        }
    }
}
