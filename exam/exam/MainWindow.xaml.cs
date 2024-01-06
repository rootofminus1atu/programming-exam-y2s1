using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


/*
 * Find this repo on:
 * https://github.com/rootofminus1atu/programming-exam-y2s1
 */


namespace exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The budget items we're keeping track of.
        /// </summary>
        private List<BudgetItem> budgetItems = new()
        {
            new("Grant", 300m, BudgetItemType.Income, new DateTime(2024, 1, 5), true),
            new("Bonus", 300m, BudgetItemType.Income, new DateTime(2024, 1, 15), false),
            new("Wages", 100m, BudgetItemType.Income, new DateTime(2024, 1, 25), true),


            new("Rent", 400m, BudgetItemType.Expense, new DateTime(2024, 1, 1), true),
            new("Flight", 100m, BudgetItemType.Expense, new DateTime(2024, 1, 5), false),
            new("Netflix", 10m, BudgetItemType.Expense, new DateTime(2024, 1, 15), true),
            new("Spotify", 8m, BudgetItemType.Expense, new DateTime(2024, 1, 20), true),
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // so that we get euros instead of pounds or dollars
            CultureInfo ci = new("ie-IE");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            DisplayItems(budgetItems);
        }

        /// <summary>
        /// Renders a provided list of items to the screen.
        /// </summary>
        /// <param name="items">The items to render.</param>
        private void DisplayItems(List<BudgetItem> items)
        {
            items.Sort();

            // I don't think this is necessary but it doesn't hurt
            lbxExpenses.Items.Refresh();
            lbxIncome.Items.Refresh();

            // splitting and rendering the list
            lbxIncome.ItemsSource = items.Where(i => i.ItemType is BudgetItemType.Income).ToList();
            lbxExpenses.ItemsSource = items.Where(i => i.ItemType is BudgetItemType.Expense).ToList();

            // rendering the additional properties
            txtTotalIncome.Text = $"{items.TotalIncome():c}";
            txtTotalOutgoings.Text = $"{items.TotalOutgoings():c}";
            txtCurrentBalance.Text = $"{items.TotalBalance():c}";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // first verifying the form data

            string name = tbxName.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is required");
                return;
            }

            DateTime? date = dpDate.SelectedDate;
            if (date is null)
            {
                MessageBox.Show("Date is required");
                return;
            }

            if (!decimal.TryParse(tbxAmount.Text, out decimal amount))
            {
                MessageBox.Show("Not a valid amount");
                return;
            }

            // there's probably a better way to do this but it does the job
            BudgetItemType? itemType = (radioIncome.IsChecked, radioExpense.IsChecked) switch
            {
                (true, false) => BudgetItemType.Income,
                (false, true) => BudgetItemType.Expense,
                _ => null
            };
            if (itemType is null)
            {
                MessageBox.Show("Select either an Income or an Expense");
                return;
            }

            bool recurring = cbxRecurring.IsChecked ?? false;



            // then creating the object and adding it to the list

            BudgetItem b = new(name, amount, (BudgetItemType)itemType, (DateTime)date, recurring);

            budgetItems.Add(b);
            DisplayItems(budgetItems);
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            // again verifying first
            BudgetItem? selected = lbxIncome.SelectedValue as BudgetItem ?? lbxExpenses.SelectedValue as BudgetItem;

            if (selected is null)
            {
                MessageBox.Show("Select an item to remove");
                return;
            }

            // and displaying
            budgetItems.Remove(selected);
            DisplayItems(budgetItems);
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = tbxSearch.Text.ToLower();

            List<BudgetItem> items = budgetItems
                .Where(i => i.ToString().ToLower().Contains(text))
                .ToList();

            DisplayItems(items);
        }
    }
}
