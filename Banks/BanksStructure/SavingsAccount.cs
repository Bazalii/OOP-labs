using System;

namespace Banks.BanksStructure
{
    public abstract class SavingsAccount : Account
    {
        protected SavingsAccount(string id, int term, float percent, float amountOfMoney, bool doubtfulness, float limitIfIsDoubtful)
        {
            Id = id ??
                 throw new ArgumentNullException(
                     nameof(id), "Id cannot be null!");
            if (term <= 0)
            {
                throw new ArgumentException("Term of account should be a positive integer!", nameof(term));
            }

            Term = term;
            DaysLeft = term;
            if (percent <= 0)
            {
                throw new ArgumentException("Percent should be a positive float!", nameof(percent));
            }

            Percent = percent;
            if (amountOfMoney <= 0)
            {
                throw new ArgumentException("Amount of money should be a positive float!", nameof(amountOfMoney));
            }

            AmountOfMoney = amountOfMoney;
            IsDoubtful = doubtfulness;
            LimitIfIsDoubtful = limitIfIsDoubtful;
            if (limitIfIsDoubtful <= 0)
            {
                throw new ArgumentException("Limit for account should be a positive float!", nameof(limitIfIsDoubtful));
            }

            DailyPercent = Percent * AmountOfMoney / 365;
        }

        protected float MonthlyIncome { get; set; }

        protected float Percent { get; }

        protected float DailyPercent { get; }

        public override void AddDailyIncome()
        {
            MonthlyIncome += DailyPercent;
        }

        public void AddMonthlyIncome()
        {
            AmountOfMoney += MonthlyIncome;
        }
    }
}