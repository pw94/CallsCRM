namespace CallsCRM.Models
{
    public class Call
    {
        public int CallId { get; set; }
        public CallTime Time { get; set; }
        public int CustomerId { get; set; }
        public Customer Callee { get; set; }
        public int CallerId { get; set; }
        public Caller Caller { get; set; }
    }
}