using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pneustoreAPI.Models
{
    public class EstabPneu
    {
        public int Quantity { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int EstabelecimentoId { get; set; }

        public Estabelecimento Estabelecimento { get; set; }
    }
}
