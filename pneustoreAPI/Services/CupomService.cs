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
        
        private readonly Context _context; 
        public CupomService(Context context)
        {
            _context = context;
        }

        public List<Cupom> GetAll() => _context.Cupoms.ToList();

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

        public bool Delete(int? id)
        {
            var cupom = _context.Cupoms.FirstOrDefault(c => c.id == id);
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
        {
            Cupom cupom = new Cupom();
            cupom.Nome = "";
            cupom.Desconto = 0;
            if (nome.Length >= 3)
            {
                return _context.Cupoms.FirstOrDefault(c => c.Nome == nome);
            }
            else
            {
                return cupom;
            }
        }
         
    }
}
