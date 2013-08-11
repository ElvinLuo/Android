using System.Windows;
using System.Windows.Controls;

namespace MortgageCalculatorWpfApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            MortgageCalculator.MortgageCalculator mortgageCalculator = new MortgageCalculator.MortgageCalculator();
            this.tbMonthlyMortgage.Text = mortgageCalculator.GetMonthlyMortgage().ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MortgageCalculator.MortgageCalculator mortgageCalculator = new MortgageCalculator.MortgageCalculator();
            this.lblLeftOverMortgage.Content =
                "个月后剩余本金：" + mortgageCalculator.GetLeftOverMortgate(int.Parse(this.tbPaidNumberOfMonths.Text));
            this.lblPaidMortgage.Content =
                "个月还本金：" + mortgageCalculator.GetPaidMortgate(int.Parse(this.tbPaidNumberOfMonths.Text));
            this.finalPaidMoney.Content = "共还款：" + mortgageCalculator.GetTotalMoneyNeedToPay(
                1260000,
                360,
                int.Parse(this.alreadyPaidMonths.Text),
                double.Parse(this.totalPrepaidMoney.Text));
        }
    }
}
