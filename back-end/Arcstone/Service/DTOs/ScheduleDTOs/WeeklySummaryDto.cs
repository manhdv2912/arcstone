using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTOs.ScheduleDTOs
{
    public class WeeklySummaryDto
    {
        public int Id { get; set; }
        public int WeekIndex { get; set; }
        public double TotalHours { get; set; }
        public string TotalHoursStr { get; set; } = "00:00";
    }
}
