using System.Collections.Generic;
using CompoundInterest.Models;

namespace CompoundInterest.Domain
{
    public interface ILoanInterestBandService
    {
        List<LoanInterestBand> GetAllInterestBands();
    }
}