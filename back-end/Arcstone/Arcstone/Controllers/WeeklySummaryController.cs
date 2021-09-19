using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Bussiness;
using Service.DTOs.ScheduleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arcstone.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeeklySummaryController : Controller
    {
        private readonly IWeeklySummaryService _weeklySummaryService;

        public WeeklySummaryController(IWeeklySummaryService weeklySummaryService
            )
        {
            _weeklySummaryService = weeklySummaryService;
        }

        [HttpGet]
        public async Task<WeeklySummaryDto> FindCurrentWeeklySummary()
        {
            return await _weeklySummaryService.FindCurrentWeek();
        }
    }
}
