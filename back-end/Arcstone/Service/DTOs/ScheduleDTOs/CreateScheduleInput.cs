using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DTOs.ScheduleDTOs
{
    public class CreateScheduleInput
    {
        public int? Id { get; set; }
        public DateTime ScheduleDate { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
        public int ProjectId { get; set; }
        public string Description { get; set; }
    }
}
