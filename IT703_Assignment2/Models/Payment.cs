using System;
using System.ComponentModel.DataAnnotations;

namespace IT703_Assignment2.Models
{
    public class Payment
    {
        [Key]
        public string PaymentID { get; set; }
        public Guest Guest { get; set; }
        //public Room Room { get; set; }
        public Booking Booking { get; set; }
        public string ReferenceNum { get; set; }
        public int AdditionFee { get; set; }
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