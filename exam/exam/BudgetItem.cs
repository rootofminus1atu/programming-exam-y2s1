using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam
{
    public enum BudgetItemType
    {
        Income,
        Expense
    }

    internal class BudgetItem : IComparable<BudgetItem>
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public BudgetItemType ItemType { get; set; }
        public DateTime Date { get; set; }
        public bool Recurring {  get; set; }

        public string RecurringStr => Recurring ? "Recurring" : "One Off";
        

        public BudgetItem(string name, decimal amount, BudgetItemType itemType, DateTime date, bool recurring)
        {
            Name = name;
            Amount = amount;
            ItemType = itemType;
            Date = date;
            Recurring = recurring;
        }

        public int CompareTo(BudgetItem? other)
        {
            return this.Date.Day.CompareTo(other?.Date.Day);
        }

        public override string ToString()
        {
            return $"{Date.Day:00}: {Name} €{Amount} - ({RecurringStr})";
        }

    }
}
