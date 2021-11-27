using Banks.Tools;

namespace Banks.BanksStructure.Implementations
{
    public class DepositAccount : SavingsAccount
    {
        public DepositAccount(int id, int term, int percent, float amountOfMoney, bool doubtfulness, float limitIfIsDoubtful)
            : base(id, term, percent, amountOfMoney, doubtfulness, limitIfIsDoubtful)
        {
        }

        public override void WithdrawMoney(float amountOfMoney)
        {
            throw new CannotWithdrawMoneyException("You cannot withdraw money from deposit account!");
        }
    }
}