using System;

namespace CompoundInterest.Models
{
    public class LoanInterestBand
    {
        private decimal _lowerBand;
        private decimal _upperBand;
        private decimal _businessRate;
        private decimal _individualRate;

        public LoanInterestBand(decimal lowerBand, decimal upperBand, decimal businessRate, decimal individualRate)
        {
            LowerBand = lowerBand;
            UpperBand = upperBand;
            BusinessRate = businessRate;
            IndividualRate = individualRate;
        }

        public decimal LowerBand
        {
            get => _lowerBand;
            private set
            {
                if (value>=0) _lowerBand = value;
                else
                {
                    throw new ArgumentException(nameof(LowerBand), $"{nameof(LowerBand)} must contain a valid value");
                }
            }
        }

        public decimal UpperBand
        {
            get => _upperBand;
            private set
            {
                if (value >= 0) _upperBand = value;
                else
                {
                    throw new ArgumentException(nameof(UpperBand), $"{nameof(UpperBand)} must contain a valid value");
                }
            }
        }

        public decimal BusinessRate
        {
            get => _businessRate;
            private set
            {
                if (value >= 0) _businessRate = value;
                else
                {
                    throw new ArgumentException(nameof(BusinessRate), $"{nameof(BusinessRate)} must contain a valid value");
                }
            }
        }

        public decimal IndividualRate
        {
            get => _individualRate;
            private set
            {
                if (value >= 0) _individualRate = value;
                else
                {
                    throw new ArgumentException(nameof(IndividualRate), $"{nameof(IndividualRate)} must contain a valid value");
                }
            }
        }
    }
}