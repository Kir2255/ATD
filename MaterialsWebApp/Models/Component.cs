using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MaterialsWebApp.Models
{
    public partial class Component
    {
        public Component()
        {
            IncomeExpenseBook = new HashSet<IncomeExpenseBook>();
        }
        [Display(Name = "ID")]
        public int ComponentId { get; set; }
        [Required]
        [Display(Name = "Detail Code")]
        public int DetailId { get; set; }
        [Required]
        [Display(Name = "Material")]
        public int MaterialId { get; set; }
        [Required]
        [Display(Name = "Unit")]
        public int UnitId { get; set; }

        public virtual Detail Detail { get; set; }
        public virtual Material Material { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<IncomeExpenseBook> IncomeExpenseBook { get; set; }
    }
}
