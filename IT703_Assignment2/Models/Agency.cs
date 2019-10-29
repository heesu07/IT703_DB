using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IT703_Assignment2.Models
{
    public class Agency
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }


        [DataType(DataType.MultilineText)]
        public string Detail { get; set; }
        public virtual ICollection<Guest> Guests { get; set; }

    }


}
