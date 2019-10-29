using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IT703_Assignment2.Models
{
    public class HouseKeeper
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Grade { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
