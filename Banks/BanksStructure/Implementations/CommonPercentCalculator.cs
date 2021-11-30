using System;
using System.Collections.Generic;

namespace Banks.BanksStructure.Implementations
{
    public class CommonPercentCalculator : IPercentCalculator
    {
        private List<DepositSumWithPercent> _depositSumsWithPercents;

        private float _debitPercent;

        private float _creditCommission;

        public CommonPercentCalculator(List<DepositSumWithPercent> depositSumsWithPercents, float debitPercent, float creditCommission)
        {
            if (depositSumsWithPercents.Count != 3)
            {
                throw new ArgumentException("List should contain three pairs of sums with percents", nameof(depositSumsWithPercents));
            }

            _depositSumsWithPercents = depositSumsWithPercents;

            if (debitPercent <= 0)
            {
                throw new ArgumentException("Debit percent should be a positive float!", nameof(debitPercent));
            }

            _debitPercent = debitPercent;
            if (creditCommission <= 0)
            {
                throw new ArgumentException("Credit comission should be a positive float!", nameof(creditCommission));
            }

            _creditCommission = creditCommission;
        }

        public float CalculateDepositPercent(float amountOfMoney)
        {
            if (amountOfMoney < _depositSumsWithPercents[0].Sum)
            {
                return _depositSumsWithPercents[0].Percent;
            }

            if (_depositSumsWithPercents[0].Sum <= amountOfMoney && amountOfMoney < _depositSumsWithPercents[1].Sum)
            {
                return _depositSumsWithPercents[1].Percent;
            }

            return _depositSumsWithPercents[2].Percent;
        }

        public float CalculateDebitPercent(float amountOfMoney)
        {
            return _debitPercent;
        }

        public float CalculateCreditCommission(float amountOfMoney)
        {
            return _creditCommission;
        }
    }
}