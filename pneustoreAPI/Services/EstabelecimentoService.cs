using System;
using System.Collections.Generic;
using System.Linq;
using pneustoreAPI.Data;
using pneustoreAPI.Models;

namespace pneustoreAPI.Services
{
    public class EstabelecimentoService : IService<Estabelecimento>
    {
        private readonly Context _context;
        public EstabelecimentoService(Context context)
        {
            _context = context;
        }

        #region Estabelecimento
        public bool Create(Estabelecimento objeto)
        {
            if (_context.Estabelecimentos.FirstOrDefault(c => c.Equals(objeto)) != null)
                return false;

            try
            {
                _context.Estabelecimentos.Add(objeto);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Update(Estabelecimento estab)
        {
            try
            {
                if (!_context.Estabelecimentos.Any(p => p.id == estab.id)) throw new Exception("Estabelecimento não existe!");

                _context.Estabelecimentos.Update(estab);
                _context.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Estabelecimento estab)
        {
            try
            {
                if (!_context.Estabelecimentos.Any(p => p.id == estab.id)) throw new Exception("Estabelecimento não existe!");

                _context.Estabelecimentos.Remove(estab);
                _context.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }
        }

        public Estabelecimento Get(int? id)
        {
            return _context.Estabelecimentos.FirstOrDefault(p => p.id == id);
        }

        public List<Estabelecimento> GetAll()
        {
            return _context.Estabelecimentos.ToList();
        }
        #endregion

        #region Estoque
        public bool CreateEstoque(EstabPneu objeto)
        {
            if (_context.EstabPneu.FirstOrDefault(c => c.Equals(objeto)) != null)
                return false;

            try
            {
                _context.EstabPneu.Add(objeto);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<EstabPneu> GetAllEstoque()
        {
            return _context.EstabPneu.ToList();
        }

        public List<EstabPneu> GetEstoque(int? id)
        {
            return _context.EstabPneu.Where(u => u.ProductId == id).ToList();
        }

        public EstabPneu GetSingleEstoque(int? productId, int? estabId)
        {
            return _context.EstabPneu.FirstOrDefault(p => p.EstabelecimentoId == estabId && p.ProductId == productId);
        }

        public bool EditEstoque(EstabPneu objeto)
        {
            try
            {
                if (!_context.EstabPneu.Any(p => p.ProductId == objeto.ProductId)) throw new Exception("Produto n�o existe!");

                if (objeto.Quantity == 0)
                {
                    _context.EstabPneu.Remove(objeto);
                    _context.SaveChanges();
                    return true;
                }
                _context.EstabPneu.Update(objeto);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteEstoque(EstabPneu estoque)
        {
            try
            {
                if (!_context.EstabPneu.Any(p => p.EstabelecimentoId == estoque.EstabelecimentoId && p.ProductId == estoque.ProductId)) 
                    throw new Exception("Estabelecimento não existe!");

                _context.EstabPneu.Remove(estoque);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    #endregion
}