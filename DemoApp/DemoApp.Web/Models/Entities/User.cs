using Microsoft.AspNetCore.Identity;
using System;

namespace DemoApp.Web.Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegisterDate { get; set; }
        public bool Status { get; set; }
    }
}
