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

    public class BudgetItem : IComparable<BudgetItem>
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
            return $"{Date.Day:00}: {Name} {Amount:c} - ({RecurringStr})";
        }

    }


    public static class BudgetItemExtension
    {
        public static decimal TotalIncome(this List<BudgetItem> items)
        {
            return items
                .Where(i => i.ItemType is BudgetItemType.Income)
                .Select(i => i.Amount)
                .Sum();
        }

        public static decimal TotalOutgoings(this List<BudgetItem> items)
        {
            return items
                .Where(i => i.ItemType is BudgetItemType.Expense)
                .Select(i => i.Amount)
                .Sum();
        }

        public static decimal TotalBalance(this List<BudgetItem> items)
        {
            return items.TotalIncome() - items.TotalOutgoings();
        }
    }
}
