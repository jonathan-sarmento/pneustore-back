using pneustoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pneustoreAPI.Services
{
    public interface ICupomService
    {
        public bool Create(Cupom cupom);
        public bool Delete(string nome);
        public Cupom Get(string nome);
    }
}
