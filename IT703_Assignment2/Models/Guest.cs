using System.ComponentModel.DataAnnotations;

namespace IT703_Assignment2.Models
{
    public class Guest
    {

        [Key]
        public string GuestID { get; set; }
        
        public int NumChildren { get; set; }
        public int NumAdults { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual Company Company { get; set; }

    }
}