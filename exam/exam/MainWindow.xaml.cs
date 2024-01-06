using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            BudgetItem b1 = new("Grant", 300m, BudgetItemType.Income, new DateTime(2024, 1, 5), true);



            foreach (var bi in budgetItems)
            {
                Trace.WriteLine(bi);
            }
        }
    }
}
