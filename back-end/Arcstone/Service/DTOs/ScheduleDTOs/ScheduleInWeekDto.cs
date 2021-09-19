using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.DTOs.ScheduleDTOs
{
    public class ScheduleInWeekDto
    {
        private List<ScheduleInDayDto> listSchedules;
        private string dayInWeek;
        private double totalHour;
        private string totalHourStr = "00:00";

        public string DayInWeek { get { return this.dayInWeek; } }
        public double TotalHour { get { return this.totalHour; } }
        public string TotalHourStr { get { return this.totalHourStr; } }


        public List<ScheduleInDayDto> ListSchedules 
        { 
            get { return this.listSchedules; } 
            set
            {
                this.listSchedules = value;
                if (this.listSchedules.Any())
                {
                    this.dayInWeek = this.listSchedules[0].StartTime.ToString("dddd, dd MMMM yyyy");
                    foreach(var item in this.listSchedules)
                    {
                        this.totalHour += item.TotalHours;
                        item.TotalHourStr = TimeSpan.FromHours(item.TotalHours).ToString(@"hh\:mm");
                    }
                    this.totalHourStr = TimeSpan.FromHours(this.totalHour).ToString(@"hh\:mm");
                }
            }
        }

        public ScheduleInWeekDto()
        {
            ListSchedules = new List<ScheduleInDayDto>();
        }
    }

}
