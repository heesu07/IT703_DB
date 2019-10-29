using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IT703_Assignment2.Models
{
    public class Company
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [DataType(DataType.MultilineText)]
        public string Detail { get; set; }

        public virtual ICollection<Guest> Guests { get; set; }
    }
}