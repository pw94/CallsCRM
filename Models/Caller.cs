using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CallsCRM.Models
{
    public class Caller
    {
        public int CallerId { get; set; }
        [Required]
        public string Login { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public ICollection<Call> Calls { get; set; }        
    }
}