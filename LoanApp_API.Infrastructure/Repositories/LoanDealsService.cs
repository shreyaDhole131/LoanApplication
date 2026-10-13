using AutoMapper;
using LoanApp_API.Application.DTO;
using LoanApp_API.Application.Interfaces;
using LoanApp_API.Application.Mapper;
using LoanApp_API.Domain.Entities;
using LoanApp_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApp_API.Infrastructure.Repositories
{
    public class LoanDealsService : ILoanDeals
    {
        AppDbContext db;
        IMapper mapper;

        public LoanDealsService(AppDbContext db,IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;

        }

        public async Task applyLoan(LoanDealDTO dto, int userId)
        {
            var customerId = await db.Users.Where(u => u.UserId == userId)
                .Select(u => u.CustomerId).FirstOrDefaultAsync();

            if (customerId == null)
                throw new UnauthorizedAccessException("User is not a customer");

            var deals = mapper.Map<LoanDeals>(dto);

            deals.CurrentStatus = "Pending";
            deals.CustomerId = customerId.Value;
            deals.AppliedDate = DateTime.UtcNow;
            deals.ApprovedAmount = 0;
            await db.LoanDeals.AddAsync(deals);
            await db.SaveChangesAsync();

        }
    }
}
