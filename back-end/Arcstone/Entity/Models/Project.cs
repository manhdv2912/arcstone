using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Entity.Models
{
    //[Table("Project")]
    public partial class Project : BaseEntity
    {
        [NotNull]
        public string ProjectName { get; set; }
    }
}
