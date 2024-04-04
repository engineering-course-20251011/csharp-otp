using System;
using System.Collections.Generic;
using System.Text;

namespace csharp_otp
{
    public class BudgetService
    {
        private readonly IBudgetRepo _budgetRepo;

        public BudgetService(IBudgetRepo budgetRepo)
        {
            _budgetRepo = budgetRepo;
        }

        public double Query(DateTime start, DateTime end)
        {
            List<Budget> budgets = _budgetRepo.GetAll();

            string startYearMonth = start.ToString("yyyyMM");
            string endYearMonth = end.ToString("yyyyMM");
            int startMonthDays = DateTime.DaysInMonth(start.Year, start.Month);
            int endMonthDays = DateTime.DaysInMonth(end.Year, end.Month);

            if (startYearMonth != endYearMonth)
            {
                DateTime temp = start.AddMonths(1);
                DateTime nextMonthFirst = new DateTime(temp.Year, temp.Month, 1);
                double sum = 0;
                while (nextMonthFirst < new DateTime(end.Year, end.Month, 1))
                {
                    Budget budget = GetBudget(budgets, nextMonthFirst.ToString("yyyyMM"));
                    if (budget != null) sum += budget.Amount;
                    nextMonthFirst = nextMonthFirst.AddMonths(1);
                }

                Budget startBudget = GetBudget(budgets, startYearMonth);
                Budget endBudget = GetBudget(budgets, endYearMonth);

                double startBudgetPerDay = startBudget.Amount / startMonthDays;
                double endBudgetPerDay = endBudget.Amount / endMonthDays;

                sum += startBudgetPerDay * (startMonthDays - start.Day + 1) + endBudgetPerDay * (end.Day);
                return sum;
            }

            Budget oneMonthBudget = GetBudget(budgets, startYearMonth);
            if (oneMonthBudget == null) return 0;

            int amount = oneMonthBudget.Amount;
            double amountPerDay = amount / startMonthDays;
            return amountPerDay * ((end - start).Days + 1);
        }

        private static Budget GetBudget(List<Budget> budgets, string yearMonth)
        {
            Budget budget = budgets.Find(delegate(Budget b) { return b.YearMonth == yearMonth; });
            if (budget == null)
            {
                budget = new Budget();
                budget.YearMonth = yearMonth;
                budget.Amount = 0;
            }
            return budget;
        }
    }
}
