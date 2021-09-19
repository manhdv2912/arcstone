using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTOs.ScheduleDTOs
{
    public class ChartDto
    {
        public List<SummaryByDayInWeek> ListDay { get; set; }
    }

    public class SummaryByDayInWeek
    {
        public List<SummaryByProjectInDay> ListProject { get; set; }
        public DateTime Day { get; set; }
        public string DayStr { get; set; }
    }

    public class SummaryByProjectInDay {
        public double TotalHours { get; set; }
        public int ProjectId { get; set; }

    }
}
