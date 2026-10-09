using AutoMapper;
using LoanApp_API.Application.DTO;
using LoanApp_API.Application.Interfaces;
using LoanApp_API.Domain.Entities;
using LoanApp_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApp_API.Infrastructure.Repositories
{
    public class LoanAccountService : ILoanAccountService
    {
        AppDbContext db;
        IMapper mapper;
        public LoanAccountService(AppDbContext  db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task CreateLoanAccount(int disbursementId)
        {
            var disbursement = await db.Disbursements
            .Include(d => d.deals)
            .FirstOrDefaultAsync(d => d.DisbursementId == disbursementId);

            if (disbursement == null)
                throw new Exception("Disbursement not found");

            if (disbursement.Status != "Disbursed")
                throw new Exception("Loan is not disbursed yet");

            var loanAccount = new LoanAccount
            {
                DealId = disbursement.DealId,

                CustomerId = disbursement.deals.CustomerId,

                LoanAccountNo = "LA" + disbursement.DealId,

                LoanAmount = disbursement.DisburseAmount,

                OutstandingPrincipal = disbursement.DisburseAmount,

                LoanStatus = "Active",

                InterestRate = disbursement.deals.InterestRate,

                TenureMonths = disbursement.deals.TenureMonths,

                EmiAmount = disbursement.deals.EmiAmount,
                   
                DisbursementDate = disbursement.DisbursementDate,

                TotalPaidAmount = 0,

                CreatedAt = DateTime.Now
            };

            db.LoanAccounts.Add(loanAccount);

            await db.SaveChangesAsync();
        }




        public async Task<List<LoanAccountDto>> FetchAllLoanAccounts()
        {
            var loanAccounts = await db.LoanAccounts
                .Include(x => x.Customer)
                .ToListAsync();

            return mapper.Map<List<LoanAccountDto>>(loanAccounts);
        }
    }
}
