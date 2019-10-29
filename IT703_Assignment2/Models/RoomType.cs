using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IT703_Assignment2.Models
{


    public class RoomType
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public roomType Type { get; set; }

        public string ImageName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int MaxGuests { get; set; }
        public int Price { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }



    }
    public enum roomType
    {
        Single, TwoBedRooms, Superior
    }
}