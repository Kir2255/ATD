using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MaterialsWebApp.Models
{
    public partial class Material
    {
        public Material()
        {
            Components = new HashSet<Component>();
        }
        [Display(Name = "ID")]
        public int MaterialId { get; set; }
        [Required]
        [Display(Name = "Code")]
        public int Code { get; set; }
        [Required]
        [Display(Name = "Material")]
        public string Name { get; set; }

        public virtual ICollection<Component> Components { get; set; }
    }
}
