using System;

namespace Banks.BanksStructure
{
    public abstract class Account
    {
        protected string Id { get; init; }

        protected int Term { get; init; }

        protected int DaysLeft { get; set; }

        protected float AmountOfMoney { get; set; }

        protected bool IsDoubtful { get; set; }

        protected float LimitIfIsDoubtful { get; set; }

        public abstract void AddDailyIncome();

        public abstract void WithdrawMoney(float amountOfMoney);

        public abstract void ReduceDaysLeft();

        public void AddMoney(float amountOfMoney)
        {
            AmountOfMoney += amountOfMoney;
        }

        public string GetId()
        {
            return Id;
        }

        public string GetBankId()
        {
            return Id[..Id.IndexOf("_", StringComparison.Ordinal)];
        }

        public int GetTerm()
        {
            return Term;
        }

        public int GetDaysLeft()
        {
            return DaysLeft;
        }

        public int GetTermAndDaysLeftDiff()
        {
            return Term - DaysLeft;
        }

        public float GetAmountOfMoney()
        {
            return AmountOfMoney;
        }

        public void SetDoubtfulness(bool doubtfulness)
        {
            IsDoubtful = doubtfulness;
        }

        public bool GetDoubtfulness()
        {
            return IsDoubtful;
        }

        public float GetLimitIfIsDoubtful()
        {
            return LimitIfIsDoubtful;
        }

        public override bool Equals(object obj)
        {
            return obj is Account account && Equals(account);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        private bool Equals(Account other)
        {
            return Id == other.Id;
        }
    }
}