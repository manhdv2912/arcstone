using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTOs.ScheduleDTOs
{
    public class ScheduleInDayDto
    {
        public int Id { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TotalHourStr { get; set; }
        public double TotalHours { get; set; }
        public string StartTimeStr { get; set; }
        public string EndTimeStr { get; set; }
        public int ProjectId { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

    }
}
