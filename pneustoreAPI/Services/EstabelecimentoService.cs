using System;
using System.Collections.Generic;
using System.Linq;
using pneustoreAPI.Data;
using pneustoreAPI.Models;

namespace pneustoreAPI.Services
{
    public class EstabelecimentoService : IService<Estabelecimento>
    {
        Context context;
        public EstabelecimentoService(Context context)
        {
            this.context = context;
        }
        public bool Create(Estabelecimento objeto)
        {
            if (context.Estabelecimentos.FirstOrDefault(c => c.Equals(objeto)) != null)
                return false;

            try
            {
                context.Estabelecimentos.Add(objeto);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateEstoque(EstabPneu objeto)
        {
            if (context.EstabPneu.FirstOrDefault(c => c.Equals(objeto)) != null)
                return false;

            try
            {
                context.EstabPneu.Add(objeto);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool EditEstoque(EstabPneu objeto)
        {
            try
            {
                if (!context.EstabPneu.Any(p => p.ProductId == objeto.ProductId)) throw new Exception("Produto n�o existe!");

                if(objeto.Quantity == 0)
                {
                    context.EstabPneu.Remove(objeto);
                    context.SaveChanges();
                    return true;
                }
                context.EstabPneu.Update(objeto);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Estabelecimento Get(int? id)
        {
            return context.Estabelecimentos.FirstOrDefault(p => p.id == id);
        }

        public List<EstabPneu> GetEstoque(int? id)
        {
            return context.EstabPneu.Where(u => u.ProductId == id).ToList();
        }

        public List<Estabelecimento> GetAll()
        {
            return context.Estabelecimentos.ToList();
        }
    }
}