using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pneustoreAPI.Models
{
    
    public class Carrinho
    {
        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
      
        public string UserId { get; set; }
        
        [JsonIgnore]
        public IdentityUser User { get; set; }

    }

}
