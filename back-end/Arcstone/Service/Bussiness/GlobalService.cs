using Service.DTOs.ScheduleDTOs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Service.Bussiness
{
    public class GlobalService : IGlobalService
    {
        public List<DateTime> GetAllDayInCurrentWeek()
        {
            DateTime startOfWeek = DateTime.Today.AddDays((int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek);
            var result = Enumerable.Range(0, 7).Select(i => startOfWeek.AddDays(i)).ToList();
            return result;
        }

        public double GetTotalHour(List<WorkingTimeDto> lstWorkingTime)
        {
            double totalHour = 0;
            foreach (var item in lstWorkingTime)
            {
                totalHour += item.EndTime.Subtract(item.StartTime).TotalHours;
            }
            return totalHour;
        }

        public int GetWeekIndex(DateTime dateValue)
        {
            var subDate = (dateValue.Date.Subtract((new DateTime(1970, 1, 4)).Date)).TotalDays;
            var weekIndex = (subDate % 7) == 0 ? ((subDate / 7) + 1) : (subDate / 7);
            return (int)weekIndex;
        }

    }
}
