using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Bussiness;
using Service.DTOs;
using Service.DTOs.ScheduleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arcstone.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;
        private readonly IWeeklySummaryService _weeklySummaryService;

        public ScheduleController(IScheduleService scheduleService,
                                IWeeklySummaryService weeklySummaryService
            )
        {
            _scheduleService = scheduleService;
            _weeklySummaryService = weeklySummaryService;
        }
        // GET: ScheduleController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ScheduleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: ScheduleController/Create
        [HttpPost]
        public async Task<BaseResponse> Create([FromBody] CreateScheduleInput input)
        {
            try
            {
                var checkDuplicate = await _scheduleService.CheckDuplicateSchedule(input);
                if (checkDuplicate)
                {
                    return new BaseResponse()
                    {
                        Status = false,
                        Message = "Working time is duplicate"
                    };
                }
                await _scheduleService.Create(input);
                return new BaseResponse() { 
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }

        [HttpPut]
        public async Task<BaseResponse> Update([FromBody] CreateScheduleInput input)
        {
            try
            {
                var checkDuplicate = await _scheduleService.CheckDuplicateSchedule(input);
                if (checkDuplicate)
                {
                    return new BaseResponse()
                    {
                        Status = false,
                        Message = "Working time is duplicate"
                    };
                }
                await _scheduleService.Update(input);
                return new BaseResponse()
                {
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }

        [HttpGet]
        public async Task<List<ScheduleInWeekDto>> GetAllScheduleInWeek()
        {
            try
            {
                return await _scheduleService.GetAllScheduleInWeek();
            }
            catch (Exception ex)
            {
                return new List<ScheduleInWeekDto>();
            }
        }

        [HttpDelete]
        public async Task<BaseResponse> DeleteScheduleById(int id)
        {
            try
            {
                await _weeklySummaryService.UpdateBeforeDeleteSchedule(id);
                await _scheduleService.DeleteScheduleById(id);
                return new BaseResponse()
                {
                    Status = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Status = false,
                    Message = ex.Message
                };
            }
        }
    }


}
