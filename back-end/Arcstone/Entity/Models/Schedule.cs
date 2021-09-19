using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Entity.Models
{
    [Table("Schedule")]
    public class Schedule : BaseEntity
    {
        [NotNull]
        public int EmployeeId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Description { get; set; }
        public int ProjectId { get; set; }
        public double? TotalHours { get; set; }
        public int? WeekIndex { get; set; }
        public int? Year { get; set; }
        
    }
}
