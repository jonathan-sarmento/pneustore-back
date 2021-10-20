using pneustoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pneustoreAPI.Services
{
    public interface ICupomService
    {
        bool Create(Cupom cupom);
        bool Delete(int? id);
        Cupom Get(string nome);
        List<Cupom> GetAll();
    }
}
