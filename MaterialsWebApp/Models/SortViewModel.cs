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
            /*CommentSort = sortOrder == Sort.CommentAsc ? Sort.CommentDesc : Sort.CommentAsc;
            AmountSort = sortOrder == Sort.AmountAsc ? Sort.AmountDesc : Sort.AmountAsc;
            DateSort = sortOrder == Sort.DateAsc ? Sort.DateDesc : Sort.DateAsc;
            AgeSort = sortOrder == Sort.AgeAsc ? Sort.AgeDesc : Sort.AgeAsc;
            SexSort = sortOrder == Sort.SexAsc ? Sort.SexDesc : Sort.SexAsc;
            BalanceSort = sortOrder == Sort.BalanceAsc ? Sort.BalanceDesc : Sort.BalanceAsc;
            IncomeSort = sortOrder == Sort.IncomeAsc ? Sort.IncomeDesc : Sort.IncomeAsc;
            ExpenseSort = sortOrder == Sort.ExpenseAsc ? Sort.ExpenseDesc : Sort.ExpenseAsc;
            PhoneSort = sortOrder == Sort.PhoneAsc ? Sort.PhoneDesc : Sort.PhoneAsc;*/
            CurrentState = sortOrder;
        }
        public enum Sort
        {
            IdAsc,
            IdDesc,
            NameAsc,
            NameDesc
            /*CommentAsc,
            CommentDesc,
            AmountAsc,
            AmountDesc,
            DateAsc,
            DateDesc,
            SexAsc,
            SexDesc,
            AgeAsc,
            AgeDesc,
            BalanceAsc,
            BalanceDesc,
            IncomeDesc,
            IncomeAsc,
            ExpenseAsc,
            ExpenseDesc,
            PhoneAsc,
            PhoneDesc*/

        }

        public Sort IdSort { get; private set; }
        public Sort NameSort { get; private set; }
        /*public Sort CommentSort { get; private set; }
        public Sort AmountSort { get; private set; }
        public Sort DateSort { get; private set; }
        public Sort AgeSort { get; private set; }
        public Sort SexSort { get; private set; }
        public Sort BalanceSort { get; private set; }
        public Sort IncomeSort { get; private set; }
        public Sort ExpenseSort { get; private set; }
        public Sort PhoneSort { get; private set; }*/
        public Sort CurrentState { get; private set; }
    }
}

