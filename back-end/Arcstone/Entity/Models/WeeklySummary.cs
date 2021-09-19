using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Entity.Models
{
    [Table("WeeklySummary")]
    public class WeeklySummary : BaseEntity
    {
        [NotNull]
        public int WeekIndex { get; set; }
        [NotNull]
        public int Year { get; set; }
        [NotNull]
        public int EmployeeId { get; set; }
        public double? TotalHours { get; set; }

    }
}
