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

namespace exam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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
            CultureInfo ci = new CultureInfo("ie-IE");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            Refresh();
        }

        private void Refresh()
        {
            lbxExpenses.Items.Refresh();
            lbxIncome.Items.Refresh();

            lbxIncome.ItemsSource = budgetItems.Where(i => i.ItemType is BudgetItemType.Income).ToList();
            lbxExpenses.ItemsSource = budgetItems.Where(i => i.ItemType is BudgetItemType.Expense).ToList();

            txtTotalIncome.Text = $"{budgetItems.TotalIncome():c}";
            txtTotalOutgoings.Text = $"{budgetItems.TotalOutgoings():c}";
            txtCurrentBalance.Text = $"{budgetItems.TotalBalance():c}";
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string name = tbxName.Text;
            if (string.IsNullOrWhiteSpace(name) )
            {
                MessageBox.Show("Name is required");
                return;
            }

            DateTime? date = dpDate.SelectedDate;
            if (date == null)
            {
                MessageBox.Show("Date is required");
                return;
            }

            if (!decimal.TryParse(tbxAmount.Text, out decimal amount))
            {
                MessageBox.Show("Not a valid amount");
                return;
            }

            BudgetItemType? itemType = (radioIncome.IsChecked, radioExpense.IsChecked) switch
            {
                (true, false) => BudgetItemType.Income,
                (false, true) => BudgetItemType.Expense,
                _ => null
            };
            if (itemType == null)
            {
                MessageBox.Show("Select either an Income or an Expense");
                return;
            }

            bool recurring = cbxRecurring.IsChecked ?? false;

            BudgetItem b = new(name, amount, (BudgetItemType)itemType, (DateTime)date, recurring);



            budgetItems.Add(b);
            Refresh();
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            BudgetItem? selected = lbxIncome.SelectedValue as BudgetItem;
        }
    }
}
