using System;
using CompoundInterest.Domain;
using CompoundInterest.Models;
using CompoundInterest.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CompoundInterest.Tests
{
    public class LoanCalculatorServiceTests
    {
        private IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddScoped<ILoanInterestBandService, InMemoryLoanInterestBandService>();
            services.AddScoped<ILoanCalculatorService, LoanCalculatorService>();
            return services.BuildServiceProvider();
        }

        [Fact]
        public void Calculate_LoanAmountDue_ThrowsError_When_Wrong_Enum_IsPassed()
        {
            var serviceCollection = CreateServiceProvider();
            var loanCalculatorService = serviceCollection.GetRequiredService<ILoanCalculatorService>();

            Action function = () => loanCalculatorService.CalculateLoanAmountDue((CustomerType) 4, 50001, 1);
            function.Should().Throw<ArgumentOutOfRangeException>().Where(x=>x.Message.StartsWith("there's"));
        }


        [Fact]
        public void Calculate_LoanAmountDue_Returns_Accurate_Result_ForBusinesses()
        {
            var serviceCollection = CreateServiceProvider();
            var loanCalculatorService = serviceCollection.GetRequiredService<ILoanCalculatorService>();
            const decimal expected = 55876.12m;


            var amount = loanCalculatorService.CalculateLoanAmountDue(CustomerType.Business, 50001, 1);
            amount.Should().BePositive().And.Be(expected);
        }

        [Fact]
        public void Calculate_LoanAmountDue_Returns_Accurate_Result_ForIndividuals()
        {
            var serviceCollection = CreateServiceProvider();
            var loanCalculatorService = serviceCollection.GetRequiredService<ILoanCalculatorService>();
            const decimal expected = 56376.13m;

            var amount = loanCalculatorService.CalculateLoanAmountDue(CustomerType.Individual, 50001, 1);
            amount.Should().BePositive().And.Be(expected);
        }
    }
}