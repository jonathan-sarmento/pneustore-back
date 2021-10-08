using Microsoft.AspNetCore.Identity;
using System;

namespace pneustoreapi.Models
{
    public class PneuUser : IdentityUser
    {
        public bool IsAnonymous { get; set; }

        public DateTime Created { get; set; }
    }
}