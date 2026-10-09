using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;

namespace LoanApp_API.Domain.Entities
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        [ForeignKey("Role")]
        public int RoleId { get; set; }

        [ForeignKey("Customer")]
        public int? CustomerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Password { get; set; }

        public Role? Role { get; set; }

        public Customers? Customer { get; set; }

        public List<DealReviews>? DealReviews { get; set; }

    }

}
