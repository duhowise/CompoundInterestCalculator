using System;
using System.Collections.Generic;
using CompoundInterest.Domain;
using CompoundInterest.Models;

namespace CompoundInterest.Services
{
    public class InMemoryLoanInterestBandService : ILoanInterestBandService
    {
        public List<LoanInterestBand> GetAllInterestBands()
        {
            return new List<LoanInterestBand>
            {
                new LoanInterestBand(Decimal.One, 5000,0.15m,0.1655m),
                new LoanInterestBand(5001,15000,0.1375m,0.14m),
                new LoanInterestBand(15001,50000,0.125m,0.135m),
                new LoanInterestBand(50001,100000,0.1175m,0.1275m),
                new LoanInterestBand(100001,Decimal.MaxValue, 0.1m,0.1125m),
            };
        }
    }
}