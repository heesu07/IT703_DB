using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IT703_Assignment2.Models
{
    public class Schedule
    {
        [Key]
        public string ID { get; set; }
        public virtual ICollection<CheckSheet> CheckSheets { get; set; }

        public virtual Room Room { get; set; }
        public virtual HouseKeeper HouseKeeper { get; set; }
        
        [Required]
        public bool QualityChecked { get; set; }

     }
}