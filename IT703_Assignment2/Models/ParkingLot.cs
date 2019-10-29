using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IT703_Assignment2.Models
{
    public class ParkingLot
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public bool Available { get; set; }
        [Required]
        public string BlockNum { get; set; }
            
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
