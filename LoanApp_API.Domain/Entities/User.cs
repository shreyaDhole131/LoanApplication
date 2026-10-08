using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LoanApp_API.Domain.Entities
{
    public class User
    {

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public int? CustomerId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;


        public List<DealReviews> dealReviews { get; set; }
    }
}
