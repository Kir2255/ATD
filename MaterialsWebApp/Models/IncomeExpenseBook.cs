using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MaterialsWebApp.Models
{
    public partial class IncomeExpenseBook
    {
        [Display(Name = "ID")]
        public int RecordId { get; set; }
        [Required]
        [Display(Name = "Component")]
        public int ComponentId { get; set; }
        [Required]
        [Display(Name = "Income")]
        public double? Income { get; set; }
        [Required]
        [Display(Name = "Expense")]
        public double? Expense { get; set; }

        public virtual Component Component { get; set; }
    }
}
