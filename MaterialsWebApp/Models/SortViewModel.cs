using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialsWebApp.Models
{
    public class SortViewModel
    {
        public SortViewModel(Sort sortOrder)
        {
            IdSort = sortOrder == Sort.IdAsc ? Sort.IdDesc : Sort.IdAsc;
            NameSort = sortOrder == Sort.NameAsc ? Sort.NameDesc : Sort.NameAsc;
            CodeSort = sortOrder == Sort.CodeAsc ? Sort.CodeDesc : Sort.CodeAsc;
            UnitSort = sortOrder == Sort.UnitAsc ? Sort.UnitDesc : Sort.UnitAsc;
            MCodeSort = sortOrder == Sort.MCodeAsc ? Sort.MCodeDesc : Sort.MCodeAsc;
            IncomeSort = sortOrder == Sort.IncomeAsc ? Sort.IncomeDesc : Sort.IncomeAsc;
            ExpenseSort = sortOrder == Sort.ExpenseAsc ? Sort.ExpenseDesc : Sort.ExpenseAsc;
            CurrentState = sortOrder;
        }
        public enum Sort
        {
            IdAsc,
            IdDesc,
            NameAsc,
            NameDesc,
            CodeAsc,
            CodeDesc,
            UnitAsc,
            UnitDesc,
            MCodeAsc,
            MCodeDesc,
            IncomeDesc,
            IncomeAsc,
            ExpenseAsc,
            ExpenseDesc
        }

        public Sort IdSort { get; private set; }
        public Sort NameSort { get; private set; }
        public Sort CodeSort { get; private set; }
        public Sort UnitSort { get; private set; }
        public Sort MCodeSort { get; private set; }
        public Sort IncomeSort { get; private set; }
        public Sort ExpenseSort { get; private set; }
        public Sort CurrentState { get; private set; }
    }
}

