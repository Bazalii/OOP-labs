namespace Banks.BanksStructure
{
    public abstract class Account
    {
        protected int Id { get; init; }

        protected int Term { get; init; }

        protected int DaysLeft { get; set; }

        protected float AmountOfMoney { get; set; }

        protected bool IsDoubtful { get; set; }

        protected float LimitIfIsDoubtful { get; set; }

        public abstract void AddDailyIncome();

        public abstract void WithdrawMoney(float amountOfMoney);
        public void AddMoney(float amountOfMoney)
        {
            AmountOfMoney += amountOfMoney;
        }

        public int GetId()
        {
            return Id;
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
    }
}