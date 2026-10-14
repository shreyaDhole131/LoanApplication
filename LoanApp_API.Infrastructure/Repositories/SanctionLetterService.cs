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
    public class SanctionLetterService : ISanctionLetter
    {
        AppDbContext db;
        IMapper mapper;

        public SanctionLetterService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<SanctionLetters> AddSanctionLetter(CreateSanctionDTO dto)
        {
            var letter = mapper.Map<SanctionLetters>(dto);
            await db.SanctionLetters.AddAsync(letter);
            await db.SaveChangesAsync();
            return letter;
        }

        public async Task<List<PendingSanctionDTO>> fetchCustomers()
        {
            return await db.LoanDeals.Where(d => d.CurrentStatus == "Approved" && !db.SanctionLetters.Any(s => s.DealId == d.DealId))
                .Select(d =>
                
                    new PendingSanctionDTO { 
                        DealId = d.DealId,
                        Name = d.customers.FirstName + " " + d.customers.LastName
                }).ToListAsync();

        }

        public async Task<SanctionPrefillDTO?> fetchSanctionDetails(int dealId)
        {
            var deal = await db.LoanDeals.FirstOrDefaultAsync(d => d.DealId == dealId);

            if (deal == null) return null;

            return mapper.Map<SanctionPrefillDTO>(deal);

        }

        //public async Task<List<SanctionGridDTO>> fetchSanctionGrids()
        //{
        //    return await db.SanctionLetters.Include(s => s.deals).ThenInclude(s => s.customers).Select(s => new SanctionGridDTO
        //    {
        //        CustomerID = s.deals.customers.CustomerId,
        //        CustomerName = s.deals.customers.FirstName + " " + s.deals.customers.LastName,
        //        LoanAmount = s.LoanAmount,
        //        LoanType = s.deals.LoanType,
        //        InterestRate = s.InterestRate,
        //        TenureMonths = s.TenureMonths,
        //        EmiAmount = s.EmiAmount,
        //        AppliedDate = s.CreatedAt,
        //        IsDisbursed = s.deals.disbursement != null,      
        //        DisbursementStatus = s.deals.disbursement == null
        //                    ? "Not Disbursed"
        //                    : "Disbursed"
        //    }).ToListAsync();
        //}

        public async Task<List<SanctionGridDTO>> fetchSanctionsGrid(string? name, string? FromDate, string? ToDate, string? Filter, string? sort)
        {
            var query = db.LoanDeals.Where(d => d.CurrentStatus == "Approved").AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(d => (d.customers.FirstName + " " + d.customers.LastName).Contains(name));
            }

            if (!string.IsNullOrEmpty(FromDate) && DateTime.TryParse(FromDate, out var from))
            {
                query = query.Where(d => d.AppliedDate  >= from);

            }

            if(!string.IsNullOrWhiteSpace(ToDate) &&  DateTime.TryParse(ToDate, out var to))
            {
                var toEnd = to.Date.AddDays(1);

                query = query.Where(d => d.AppliedDate < toEnd);
            }

            if (!string.IsNullOrWhiteSpace(Filter))
            {
                query = query.Where(d => d.LoanType == Filter);
            }

            if (!string.IsNullOrEmpty(sort))
            {
                query = sort?.ToLower() == "ascending" ? query.OrderBy(d => d.AppliedDate) : query.OrderByDescending(d => d.AppliedDate);
            }

            return await query.Select(d => new SanctionGridDTO
            {
                SanctionLetterId = d.sanctionLetters == null
                        ? (int?)null
                        : d.sanctionLetters.SanctionId,
                DealId = d.DealId,
                CustomerID = d.customers.CustomerId,
                CustomerName = d.customers.FirstName + " " + d.customers.LastName,
                LoanAmount = d.LoanAmount,
                LoanType = d.LoanType,
                InterestRate = d.InterestRate,
                TenureMonths = d.TenureMonths,
                EmiAmount = d.EmiAmount,
                AppliedDate = d.AppliedDate,

                IsDisbursed = d.disbursement != null,
                DisbursementStatus = db.Disbursements.Any(x => x.DealId == d.DealId)
                        ? "Disbursed"
                        : "Not Disbursed"
            }).ToListAsync();
        }

        public async Task<SanctionLetterDTO?> GetSanctionLetter(int letterId)
        {
            return await db.SanctionLetters
        .Where(l => l.SanctionId== letterId)
        .Select(l => new SanctionLetterDTO
        {
            SanctionLetterId = l.SanctionId,
            DealId = l.DealId,
            LoanAmount = l.LoanAmount,
            LoanType = l.deals.LoanType,
            TenureMonths = l.TenureMonths,
            InterestRate = l.InterestRate,
            EmiAmount = l.EmiAmount,
            CreatedAt = l.CreatedAt,
            CustomerName = l.deals.customers.FirstName + " " + l.deals.customers.LastName,
            MobileNo = l.deals.customers.MobileNo
        })
        .FirstOrDefaultAsync();
        }
    }
}
