using LoanApp_API.Application.DTO;
using LoanApp_API.Application.Interfaces;
using LoanApp_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApp_API.Infrastructure.Repositories
{
    public class PendingDealsService : IPendingDeals
    {
        AppDbContext db;

        public PendingDealsService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<PendingDealsDTO>> fetchPendingDeals()
        {
           return await db.LoanDeals.Include(ld => ld.customers).Where(ld => ld.CurrentStatus == "Pending").Select(ld => new PendingDealsDTO
            {
                DealId = ld.DealId,
                CustomerName = ld.customers.FirstName + " " + ld.customers.LastName,
                LoanType = ld.LoanType,
                LoanAmount = ld.LoanAmount,
                Status = "Pending"
            }).ToListAsync();
        }


    }
}
