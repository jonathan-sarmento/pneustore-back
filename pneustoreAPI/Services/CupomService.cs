using pneustoreAPI.Data;
using pneustoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pneustoreAPI.Services
{
    public class CupomService : ICupomService
    {
        
        Context _context; 
        public CupomService(Context context)
        {
            _context = context;
        }
        public bool Create(Cupom cupom)
        {
            try
            {
                _context.Add(cupom);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        public bool Delete(string nome)
        {
            var cupom = _context.Cupoms.FirstOrDefault(c => c.Nome == nome);
            try
            {
                _context.Cupoms.Remove(cupom);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Cupom Get(string nome)
         => _context.Cupoms.FirstOrDefault(p => p.Nome == nome);
    }
}
