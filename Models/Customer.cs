using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CallsCRM.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public ICollection<Call> Calls { get; set; }
    }
}