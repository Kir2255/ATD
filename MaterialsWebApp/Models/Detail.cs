using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MaterialsWebApp.Models
{
    public partial class Detail
    {
        public Detail()
        {
            Components = new HashSet<Component>();
        }
        [Display(Name = "ID")]
        public int DetailId { get; set; }
        [Required]
        [Display(Name = "Detail Code")]
        public int Code { get; set; }

        public virtual ICollection<Component> Components { get; set; }
    }
}
