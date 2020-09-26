using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialsWebApp.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
