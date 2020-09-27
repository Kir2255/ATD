using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialsWebApp.Models
{
    public class IndexViewModel
    {
        public List<Unit> Units { get; set; }
        public List<Component> Components { get; set; }
        public List<Detail> Details { get; set; }
        public List<IncomeExpenseBook> IncomeExpenseBooks { get; set; }
        public List<Material> Materials { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }

    }
}
