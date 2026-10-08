using LoanApp_API.Application.Interfaces;
using LoanApp_API.Domain.Entities;
using LoanApp_API.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApp_API.Infrastructure.Repositories
{
    public class LoanDealsService : ILoanDeals
    {
        AppDbContext db;

        public LoanDealsService(AppDbContext db)
        {
            this.db = db;
        }

        public void applyLoan(LoanDeals deals)
        {
            throw new NotImplementedException();
        }
    }
}
