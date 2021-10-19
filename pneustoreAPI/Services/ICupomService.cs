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
        public bool Delete(int? id);
        public Cupom Get(string nome);
        public List<Cupom> GetAll();
    }
}
