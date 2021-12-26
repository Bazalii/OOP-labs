using System;

namespace Banks.BanksStructure.Implementations
{
    public class CreditAccount : Account
    {
        private readonly float _commission;

        public CreditAccount(string id, int term, float commission, float amountOfMoney, bool doubtfulness, float limitIfIsDoubtful)
        {
            Id = id ??
                 throw new ArgumentNullException(
                     nameof(id), "Id cannot be null!");
            if (term <= 0)
            {
                throw new ArgumentException("Term of account should be a positive integer!", nameof(term));
            }

            Term = term;
            if (commission <= 0)
            {
                throw new ArgumentException("Commission should be a positive float!", nameof(commission));
            }

            _commission = commission;
            if (amountOfMoney <= 0)
            {
                throw new ArgumentException("Amount of money should be a positive float!", nameof(amountOfMoney));
            }

            AmountOfMoney = amountOfMoney;
            IsDoubtful = doubtfulness;
            if (limitIfIsDoubtful <= 0)
            {
                throw new ArgumentException("Limit for account should be a positive float!", nameof(limitIfIsDoubtful));
            }

            LimitIfIsDoubtful = limitIfIsDoubtful;
        }

        public override void AddDailyIncome()
        {
            if (AmountOfMoney < 0)
            {
                AmountOfMoney -= _commission;
            }
        }

        public override void WithdrawMoney(float amountOfMoney)
        {
            AmountOfMoney -= amountOfMoney;
        }

        public override void ReduceDaysLeft()
        {
            DaysLeft -= 1;
            if (DaysLeft == 0)
            {
                DaysLeft += 180;
            }
        }
    }
}