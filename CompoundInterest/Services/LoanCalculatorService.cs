using System;
using System.Linq;
using CompoundInterest.Domain;
using CompoundInterest.Models;

namespace CompoundInterest.Services
{
   
    public class LoanCalculatorService:ILoanCalculatorService
    {
       
        private readonly ILoanInterestBandService _loanInterestBandService;

        
        public LoanCalculatorService(ILoanInterestBandService loanInterestBandService)
        {
            _loanInterestBandService = loanInterestBandService;
        }
        public decimal CalculateLoanAmountDue(CustomerType customerType, decimal loanAmount,int tenure)
        {
           
            var loanInterestBands = _loanInterestBandService.GetAllInterestBands();
            var customerLoanInterestBand =
                loanInterestBands.FirstOrDefault(x => x.LowerBand <= loanAmount && x.UpperBand >= loanAmount);
           
            
            switch (customerType)
            {
                case CustomerType.Business:
                    if (customerLoanInterestBand != null)
                        return GetLoanBalance(tenure, customerLoanInterestBand.BusinessRate, loanAmount);
                    break;
                case CustomerType.Individual:
                    if (customerLoanInterestBand != null)
                        return GetLoanBalance(tenure, customerLoanInterestBand.IndividualRate, loanAmount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(customerType), customerType, "there's no loan configuration for the requested customer type");
            }
            return 0;
        }

        private decimal GetLoanBalance(int tenure,decimal interest,  decimal balance)
        {
            for (var i = 0; i < tenure; i++)
            {
                balance *= (1 + interest);
            }
            return Math.Round(balance,2);
        }

       
    }
}