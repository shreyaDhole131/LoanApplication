using LoanApp_API.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LoanApp_API.Controllers;

[ApiController]
[Route("api/customer/payment-history")]
public class CustomerPaymentHistoryController : ControllerBase
{
    private readonly IPaymentHistoryRepository _repo;

    public CustomerPaymentHistoryController(IPaymentHistoryRepository repo)
    {
        _repo = repo;
    }

    // "Select Loan" dropdown
    [HttpGet("loans/{customerId:int}")]
    public async Task<IActionResult> GetLoans(int customerId)
        => Ok(await _repo.GetCustomerLoans(customerId));

    // Table: api/customer/payment-history/1?loanAccountId=1&months=3
    [HttpGet("{customerId:int}")]
    public async Task<IActionResult> GetPaymentHistory(int customerId, int? loanAccountId, int months = 0)
        => Ok(await _repo.GetPaymentHistory(customerId, loanAccountId, months));
}