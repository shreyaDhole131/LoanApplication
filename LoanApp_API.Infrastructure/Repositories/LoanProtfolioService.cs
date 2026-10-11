using LoanApp_API.Application.DTO.ReportsDTOs;
using LoanApp_API.Application.Interfaces.Reports;
using LoanApp_API.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApp_API.Infrastructure.Repositories
{
    public class LoanProtfolioService : ILoanProtfolio
    {
        AppDbContext db;
        public LoanProtfolioService(AppDbContext db)
        {
            this.db = db;
        }

        public async Task<List<LoanProtfolioDTO>> fetchLoanProtofolio()
        {
            var rows = await db.LoanAccounts.Where(la => la.LoanStatus == "Active" && la.LoanDeal != null).Select(la => new
            {
                la.LoanDeal!.LoanType,
                la.OutstandingPrincipal
            }).ToListAsync();

            var productOrder = new[]
           {
                "Education Loan",
                "Home Loan",
                "Personal Loan"
            };


            var result = new List<LoanProtfolioDTO>();

            decimal grandTotal = rows.Sum(r => r.OutstandingPrincipal);

            foreach(var product in productOrder)
            {
                var productRows = rows.Where(r => r.LoanType == product).ToList();

                int activeLoans = productRows.Count;
                decimal outstanding = productRows.Sum(r => r.OutstandingPrincipal);

                decimal percent = grandTotal == 0 ? 0 : Math.Round((decimal) outstanding / grandTotal, 2);

                result.Add(new LoanProtfolioDTO
                {
                    Product = product,
                    AciveLoans = activeLoans,
                    Outstanding = outstanding,
                    Percent = percent
                });

            }

            result.Add(new LoanProtfolioDTO
            {
                Product = "Total",
                AciveLoans = result.Sum(r => r.AciveLoans),
                Outstanding = grandTotal,
                Percent = 100
            });

            return result;
        }
    }
}
