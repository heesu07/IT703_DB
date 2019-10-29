using System;
using System.ComponentModel.DataAnnotations;

namespace IT703_Assignment2.Models
{
    public class Payment
    {
        [Key]
        public string ID { get; set; }
        public virtual Guest Guest { get; set; }
        public virtual Room Room { get; set; }
        [Required]
        public string CardHolder { get; set; }
        [Required]
        public string CardNum { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        [Required]
        public PaymentType PaymentType { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string PaymentTerm { get; set; }

        [Timestamp]
        public DateTime PaymentDate { get; set; }
    }
    public enum PaymentType {
        Cash, EFTPOS, Voucher, CreditCard
    }
}