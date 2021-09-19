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
    public class ChartController : Controller
    {
        private readonly IGlobalService _globalService;
        private readonly IWeeklySummaryService _weeklySummaryService;

        public ChartController(IGlobalService globalService, IWeeklySummaryService weeklySummaryService)
        {
            _globalService = globalService;
            _weeklySummaryService = weeklySummaryService;
        }
        // GET: ChartController
        [HttpGet]
        public async Task<List<SummaryByDayInWeek>> GetChartSummary()
        {
            return await _weeklySummaryService.GetChartSummary();
        }
    }
}
