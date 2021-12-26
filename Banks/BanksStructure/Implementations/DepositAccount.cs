using Banks.Tools;

namespace Banks.BanksStructure.Implementations
{
    public class DepositAccount : SavingsAccount
    {
        public DepositAccount(string id, int term, float percent, float amountOfMoney, bool doubtfulness, float limitIfIsDoubtful)
            : base(id, term, percent, amountOfMoney, doubtfulness, limitIfIsDoubtful)
        {
        }

        public override void WithdrawMoney(float amountOfMoney)
        {
            throw new CannotWithdrawMoneyException("You cannot withdraw money from deposit account!");
        }

        public override void ReduceDaysLeft()
        {
            DaysLeft -= 1;
        }
    }
}