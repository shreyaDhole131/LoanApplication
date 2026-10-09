using LoanApp_API.Application.DTO.ForeClosure;
using LoanApp_API.Application.DTO.PaymentHistory;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApp_API.Application.Interfaces
{
    public interface IPaymentHistoryRepository
    {
        // "Select Loan" dropdown (all loans of the customer)
        Task<List<CustomerLoanDto>> GetCustomerLoans(int customerId);

        // Table. loanAccountId = null -> all loans. months = 0 -> all time
        Task<List<PaymentHistoryDto>> GetPaymentHistory(int customerId, int? loanAccountId, int months);
    }
}
