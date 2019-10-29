using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IT703_Assignment2.Models
{
    public enum CheckState
    {
        Reserve,
        CheckIn,
        CheckOut,
    }
    public class Booking
    {
        [Key]
        public string ReferenceNum { get; set; }
        public  Guest Guest { get; set; }
        //public  ParkingLot ParkingLot { get; set; }
        public bool ParkingLot { get; set; }
        public  ICollection<Room> Rooms { get; set; }
        [Required]
        public string RoomID { get; set; }

            
        public DateTime CreatedAt { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckIn { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckOut { get; set; }
        public CheckState CheckStatus { get; set; }

        [Required]
        public int NumGuest { get; set; }
        public double restaurantFee { get; set; }
        [Required]
        public double RoomFee { get; set; }
        [Required]
        public bool Paid { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

    }
}