using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApp_API.Application.DTO
{
    public class LoanDealDTO
    {

        public string LoanType { get; set; }

        public decimal LoanAmount { get; set; }

        public int TenureMonths { get; set; }

        public string BankName { get; set; }

        public string BankAccountNumber { get; set; }

        public string IFSCCode { get; set; }

        public int EmiDay { get; set; }
    }
}
