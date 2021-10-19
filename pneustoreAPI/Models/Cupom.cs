using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pneustoreAPI.Models
{

    public class Cupom
    {
        
        [Key]
        public int id;
        public string Nome {get;set;}

        public double? Desconto { get; set; }
    }
}
