using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MaterialsWebApp.Models
{
    public partial class Unit
    {
        public Unit()
        {
            Components = new HashSet<Component>();
        }
        [Display(Name = "ID")]
        public int UnitId { get; set; }
        [Required]
        [Display(Name = "Unit")]
        public string Description { get; set; }

        public virtual ICollection<Component> Components { get; set; }
    }
}
