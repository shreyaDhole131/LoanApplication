using LoanApp_API.Application.Interfaces;
using LoanApp_API.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanApp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanAccountController : ControllerBase
    {
        ILoanAccountService service;
        public LoanAccountController(ILoanAccountService service)
        {
            this.service = service; 
        }

        [HttpPost]
        [Route("CreateLoanAccount/{disbursementId}")]
        public async Task<IActionResult> CreateLoanAccount(int disbursementId)
        {
            try
            {
                await service.CreateLoanAccount(disbursementId);

                return Ok(new
                {
                    message = "Loan account created successfully"
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("getA")]
        public async Task<IActionResult> GetAllLoanAccounts()
        {
            try
            {
                var loanAccounts = await service.FetchAllLoanAccounts();

                return Ok(loanAccounts);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
