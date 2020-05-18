using System;
using System.Collections.Generic;
using System.Linq;
using CompoundInterest.Domain;
using CompoundInterest.Models;
using CompoundInterest.Services;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CompoundInterest.Tests
{
    public class LoanInterestBandServiceTests
    {

        private IServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddScoped<ILoanInterestBandService, InMemoryLoanInterestBandService>();
            return services.BuildServiceProvider();
        }
        static Action Constructor<T>(Func<T> func)
        {
            return () => func();
        }

        [Fact] public void LoanInterestBand_Throws_Exception_When_Initialized_With_Negative_Numbers()

        {
            Constructor(() => new LoanInterestBand(-1, -1, -1, -1)).Should().Throw<ArgumentException>();
        }



        [Fact]
        public void Loan_Interest_Configuration_IsNotEmpty_And_Has_Five_Configurations()
        {
            var serviceCollection = CreateServiceProvider();
            var loanInterestBandService=serviceCollection.GetRequiredService<ILoanInterestBandService>();


            var allInterestBands=loanInterestBandService.GetAllInterestBands();

            allInterestBands.Should().HaveCount(5).And.OnlyHaveUniqueItems();
        }
        
        [Fact]
        public void Loan_Interest_Configuration_IsAType_Of_Loan_Interest_Band()
        {
            var serviceCollection = CreateServiceProvider();
            var loanInterestBandService = serviceCollection.GetRequiredService<ILoanInterestBandService>();


            var allInterestBands = loanInterestBandService.GetAllInterestBands();

            allInterestBands.Should().ContainItemsAssignableTo<LoanInterestBand>();



        }

        [Fact]
        public void Loan_Interest_BandService_DoesNotReturn_DuplicateItems()
        {
            var serviceCollection = CreateServiceProvider();
            var loanInterestBandService = serviceCollection.GetRequiredService<ILoanInterestBandService>();


            var allInterestBands = loanInterestBandService.GetAllInterestBands();
            allInterestBands.Should().OnlyHaveUniqueItems();

            
        }
        
        [Fact]
        public void Loan_Interest_BandService_Should_Return_Expected_Items()
        {
            var serviceCollection = CreateServiceProvider();
            var loanInterestBandService = serviceCollection.GetRequiredService<ILoanInterestBandService>();


            var allInterestBands = loanInterestBandService.GetAllInterestBands().First();
            var expected = new LoanInterestBand(decimal.One, 5000, 0.15m, 0.1655m);

            allInterestBands.Should().BeEquivalentTo(expected);

            
        } 
        
        
        [Fact]
        public void Loan_Interest_Band_Service_Should_Return_Five_Unique_Items()
        {
            var serviceCollection = CreateServiceProvider();
            var loanInterestBandService = serviceCollection.GetRequiredService<ILoanInterestBandService>();


            var allInterestBands = loanInterestBandService.GetAllInterestBands();

            allInterestBands.Should().HaveCount(5).And.OnlyHaveUniqueItems();

            
        }
        [Fact]
        public void Loan_Interest_Band_Service_Should_Return_The_Exact_List_Of_Items()
        {
            var serviceCollection = CreateServiceProvider();
            var loanInterestBandService = serviceCollection.GetRequiredService<ILoanInterestBandService>();


            var allInterestBands = loanInterestBandService.GetAllInterestBands();
            var expected = new List<LoanInterestBand>
            {
                new LoanInterestBand(Decimal.One, 5000,0.15m,0.1655m),
                new LoanInterestBand(5001,15000,0.1375m,0.14m),
                new LoanInterestBand(15001,50000,0.125m,0.135m),
                new LoanInterestBand(50001,100000,0.1175m,0.1275m),
                new LoanInterestBand(100001,Decimal.MaxValue, 0.1m,0.1125m),
            };
            allInterestBands.Should().BeEquivalentTo(expected);

        }
    }
}
