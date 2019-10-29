using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IT703_Assignment2.Models
{
    public class Room
    {
        [Key]
        public string RoomID { get; set; }
        public roomType RoomType { get; set; }
        //public virtual Booking Booking { get; set; }

        //public virtual ICollection<Schedule> Schedules { get; set; }
        //public virtual ICollection<Payment> Payments { get; set; }
        //[Required]
        //public string Floor { get; set; }
        [Required]
        public string RoomNum { get; set; }
        //[Required]
        public Status Status { get; set; }
        //public HouseKeeper HouseKeeper { get; set; }
        //[Required]

        public double Price { get; set; }
        //[Required]
        //public bool Available { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

    }
  
    public enum Status {
        VacantClean, VacantDirty, OccupiedClean, OccupiedService, OnMaintenance  
    }
}