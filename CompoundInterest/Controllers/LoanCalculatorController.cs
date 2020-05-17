using System;
using System.ComponentModel;
using System.Diagnostics;
using CompoundInterest.Domain;
using CompoundInterest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompoundInterest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanCalculatorController : ControllerBase
    {
        private readonly ILoanCalculatorService _loanCalculatorService;

        public LoanCalculatorController(ILoanCalculatorService loanCalculatorService)
        {
            _loanCalculatorService = loanCalculatorService;
        }

        [HttpGet("LoanAmountDue")]
        public ActionResult CalculateLoanAmountDue(CustomerType customerType, decimal loanAmount, int tenure)
        {
            
            if (!ModelState.IsValid|| !Enum.IsDefined(typeof(CustomerType), customerType)) return BadRequest("something went wrong. \n please check your input.");

            try
            {
                var amountDue = _loanCalculatorService.CalculateLoanAmountDue(customerType, loanAmount, tenure);
                if (amountDue == 0)
                {
                    return BadRequest("something went wrong");
                }

                return Ok(new {amountDue, interest = (amountDue - loanAmount)});
            }
            catch (ArgumentOutOfRangeException e)
            {
                Debug.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}