using CompoundInterest.Models;

namespace CompoundInterest.Domain
{
    public interface ILoanCalculatorService
    {
        decimal CalculateLoanAmountDue(CustomerType customerType, decimal loanAmount,int tenure);
    }
}