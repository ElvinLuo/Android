namespace MortgageCalculator
{
    using System;

    public class MortgageCalculator
    {
        private int totalNumberOfYears = 30;
        private int totalNumberOfMonths;
        private int currentNumberOfMonths;

        private double totalMortgate = 1260000;
        private double rateOfTheYear = 0.0655;
        private double rateOfTheMonth;
        private double currentMonthlyRate;
        private double discount = 0.85;

        public MortgageCalculator()
        {
            totalNumberOfMonths = 12 * totalNumberOfYears;
            rateOfTheMonth = rateOfTheYear / 12;
            currentMonthlyRate = rateOfTheMonth * discount;
        }

        public double GetMonthlyMortgage()
        {
            double ratePow = Math.Pow((1 + currentMonthlyRate), totalNumberOfMonths);
            return totalMortgate * currentMonthlyRate * (ratePow / (ratePow - 1));
        }

        public double GetMonthlyMortgage(double totalMortgate, int totalNumberOfMonths)
        {
            double ratePow = Math.Pow((1 + currentMonthlyRate), totalNumberOfMonths);
            return totalMortgate * currentMonthlyRate * (ratePow / (ratePow - 1));
        }

        public double GetTotalMoneyNeedToPay(double totalMortgate, int totalNumberOfMonths)
        {
            return totalNumberOfMonths * GetMonthlyMortgage(totalMortgate, totalNumberOfMonths);
        }

        public double GetTotalMoneyNeedToPay(double totalMortgate, int totalNumberOfMonths, int currentMonth, double prepaidMoney)
        {
            double monthlyMortgage = GetMonthlyMortgage(totalMortgate, totalNumberOfMonths);
            double alreadyPaidMonthly = currentMonth * monthlyMortgage;
            double faxi = 0;
            if (currentMonth <= 36)
            {
                faxi = 3 * (monthlyMortgage - GetPaidMortgate(currentMonth));
            }
            double leftover = GetLeftOverMortgate(currentMonth) - prepaidMoney;
            return alreadyPaidMonthly + prepaidMoney + faxi + GetTotalMoneyNeedToPay(leftover, totalNumberOfMonths - currentMonth);
        }

        public double GetLeftOverMortgate(int paidNumberOfMonths)
        {
            double currentMonthlyRatePlus1 = currentMonthlyRate + 1;
            double totalNumberOfMonthsPow = Math.Pow(currentMonthlyRatePlus1, totalNumberOfMonths);
            double paidNumberOfMonthsPow = Math.Pow(currentMonthlyRatePlus1, paidNumberOfMonths);
            return totalMortgate * (totalNumberOfMonthsPow - paidNumberOfMonthsPow) / (totalNumberOfMonthsPow - 1);
        }

        public double GetPaidMortgate(int paidNumberOfMonths)
        {
            double currentMonthlyRatePlus1 = currentMonthlyRate + 1;
            double totalNumberOfMonthsPow = Math.Pow(currentMonthlyRatePlus1, totalNumberOfMonths) - 1;
            double paidNumberOfMonthsPow = Math.Pow(currentMonthlyRatePlus1, paidNumberOfMonths - 1);
            return totalMortgate * currentMonthlyRate * paidNumberOfMonthsPow / totalNumberOfMonthsPow;
        }
    }
}
