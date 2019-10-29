using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT703_Assignment2.Areas.Identity.Data
{ 
    public class AccountUser : IdentityUser
    {

        public AccountUser() : base() { }

        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Gender { get; set; }
        public string Type { get; set; }

        public DateTime CreationDate { get; set; }

   

    }
}
