using Service.DTOs.ScheduleDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.Bussiness
{
    public interface IGlobalService
    {
        double GetTotalHour(List<WorkingTimeDto> lstWorkingTime);
        int GetWeekIndex(DateTime dateValue);
        List<DateTime> GetAllDayInCurrentWeek();

    }
}
