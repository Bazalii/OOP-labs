using Banks.Tools;

namespace Banks.BanksStructure.Implementations
{
    public class DebitAccount : SavingsAccount
    {
        public DebitAccount(string id, int term, float percent, float amountOfMoney, bool doubtfulness, float limitIfIsDoubtful)
            : base(id, term, percent, amountOfMoney, doubtfulness, limitIfIsDoubtful)
        {
        }

        public override void WithdrawMoney(float amountOfMoney)
        {
            if (amountOfMoney > AmountOfMoney)
            {
                throw new NotEnoughMoneyToWithdrawException($"You can withdraw only {AmountOfMoney}!");
            }

            AddMoney(-amountOfMoney);
        }

        public override void ReduceDaysLeft()
        {
            DaysLeft -= 1;
        }
    }
}