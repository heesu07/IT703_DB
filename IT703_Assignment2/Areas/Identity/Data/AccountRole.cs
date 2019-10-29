using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IT703_Assignment2.Areas.Identity.Data

{
    public class AccountRole : IdentityRole
    {
        public AccountRole() : base() { }
        public AccountRole(string roleName) : base(roleName) { }
        public AccountRole(string roleName, string description) : base(roleName)
        {
            this.Description = description;
        }
        public string Description { get; set; }

    }
}
