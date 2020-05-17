using System;
using CompoundInterest.Controllers;
using CompoundInterest.Domain;
using CompoundInterest.Models;
using CompoundInterest.Services;
using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CompoundInterest.Tests
{
    public class LoanCalculatorControllerTests
    {
        private IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddScoped<ILoanInterestBandService, InMemoryLoanInterestBandService>();
            services.AddScoped<ILoanCalculatorService, LoanCalculatorService>();
            return services.BuildServiceProvider();
        }

      [Fact]  public void Calculate_LoanAmountDue_Should_Return_Ok_With_Valid_Payload()
      {
          var serviceCollection = CreateServiceProvider();
          var loanCalculatorService = serviceCollection.GetRequiredService<ILoanCalculatorService>();
            var controller=new LoanCalculatorController(loanCalculatorService);

            var actionResult = controller.CalculateLoanAmountDue(CustomerType.Individual, 50001, 1);
            var expected = new {amountDue= 56376.13m, interest = 6375.13m };
            actionResult.Should().BeOkObjectResult().Value.Should().BeEquivalentTo(expected);

      }
        [Fact]
        public void Calculate_LoanAmountDue_Should_Return_BadRequest_ForMalformed_Request()
        {
            var serviceCollection = CreateServiceProvider();
            var loanCalculatorService = serviceCollection.GetRequiredService<ILoanCalculatorService>();
            var controller = new LoanCalculatorController(loanCalculatorService);

            var actionResult = controller.CalculateLoanAmountDue((CustomerType)4, 100, 1);
            actionResult.Should().BeBadRequestObjectResult().ErrorAs<string>();
        }


        [Fact] public void Calculate_LoanAmountDue_Should_Return_BadRequest_ForNegative_Values()
        {
            var serviceCollection = CreateServiceProvider();
            var loanCalculatorService = serviceCollection.GetRequiredService<ILoanCalculatorService>();
            var controller = new LoanCalculatorController(loanCalculatorService);

       var result=  controller.CalculateLoanAmountDue(CustomerType.Business, -1, -1);
            result.Should().BeBadRequestObjectResult().ErrorAs<string>();
        }
    }
}