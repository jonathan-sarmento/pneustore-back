using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace pneustoreAPI.Models
{
   
    public class Cupom
    {
        public Cupom(string Nome="", double? Desconto=0)
        {
            this.Nome = Nome;
            this.Desconto = Desconto;
        }
        [Key]
        public string Nome;
        public double? Desconto { get; set; }
    }
}
