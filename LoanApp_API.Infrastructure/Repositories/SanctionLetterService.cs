using LoanApp_API.Application.Interfaces;
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

        public SanctionLetterService(AppDbContext db)
        {
            this.db = db;
        }

        
    }
}
