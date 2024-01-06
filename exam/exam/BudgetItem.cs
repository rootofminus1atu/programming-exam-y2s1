using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exam
{
    /// <summary>
    /// Represents whether a budget item is an Income or Expense.
    /// </summary>
    public enum BudgetItemType
    {
        Income,
        Expense
    }

    /// <summary>
    /// Represents a Budget Item type.
    /// </summary>
    public class BudgetItem : IComparable<BudgetItem>
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public BudgetItemType ItemType { get; set; }
        public DateTime Date { get; set; }
        public bool Recurring { get; set; }

        /// <summary>
        /// The Recurring property, but as a user-friendly string.
        /// </summary>
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


    /// <summary>
    /// Helpful methods for a list of BudgetItems
    /// </summary>
    public static class BudgetItemExtension
    {
        /// <summary>
        /// Calculates the total income for a given list of BudgetItems.
        /// </summary>
        /// <param name="items">The provided list of BudgetItems.</param>
        /// <returns>The total income.</returns>
        public static decimal TotalIncome(this List<BudgetItem> items)
        {
            return items
                .Where(i => i.ItemType is BudgetItemType.Income)
                .Select(i => i.Amount)
                .Sum();
        }

        /// <summary>
        /// Calculates the total outgoing expenses for a given list of BudgetItems.
        /// </summary>
        /// <param name="items">The provided list of BudgetItems.</param>
        /// <returns>The total outgoing expenses.</returns>
        public static decimal TotalOutgoings(this List<BudgetItem> items)
        {
            return items
                .Where(i => i.ItemType is BudgetItemType.Expense)
                .Select(i => i.Amount)
                .Sum();
        }

        /// <summary>
        /// Calculates the balance for a given list of BudgetItems.
        /// </summary>
        /// <param name="items">The provided list of BudgetItems.</param>
        /// <returns>The total balance.</returns>
        public static decimal TotalBalance(this List<BudgetItem> items)
        {
            return items.TotalIncome() - items.TotalOutgoings();
        }
    }
}
