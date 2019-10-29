using System;
using System.ComponentModel.DataAnnotations;

namespace IT703_Assignment2.Models { 
    public class CheckSheet
    {
        [Key]
        public string ID { get; set; }
        public virtual Schedule Schedule
        {
            get; set;
        }



        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }
    }
}