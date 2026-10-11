using LoanApp_API.Application.DTO.ReportsDTOs;
using LoanApp_API.Application.Interfaces.Reports;
using LoanApp_API.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApp_API.Infrastructure.Repositories
{
    public class DisburseReportService : IDisburseReport
    {
        AppDbContext db;
        public DisburseReportService(AppDbContext db)
        {
            this.db = db;
        }

        public Task<List<DisbursementReportDTO>> fetchDisbursmentReport(DateOnly? fromDate, DateOnly? toDate)
        {
            
        }
    }
}
