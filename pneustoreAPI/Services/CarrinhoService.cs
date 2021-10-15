using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using pneustoreapi.Models;
using pneustoreAPI.Data;
using pneustoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pneustoreAPI.Services
{
    public class CarrinhoService
    {
        Context context;
        private readonly UserManager<PneuUser> userManager;
        public CarrinhoService(Context context, UserManager<PneuUser> _userManager)
        {
            this.context = context;
            userManager = _userManager;
        }
        public bool Create(Carrinho objeto)
        {
            if (context.Carrinho.FirstOrDefault(c => c.Equals(objeto)) != null)
                return false;

            //EstabPneu estoqueProduto = context.EstabPneu.FirstOrDefault(p => p.ProductId == objeto.ProductId);

            //if (estoqueProduto == null) return false;

            //estoqueProduto.Quantity -= objeto == null ? 0 : objeto.Quantity;

            //if (estoqueProduto.Quantity <= 0) return false;

            try
            {
                //context.EstabPneu.Update(estoqueProduto);
                context.Carrinho.Add(objeto);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Carrinho Get(string userName, int? productId)
        {
            return context.Carrinho.Include(c => c.Product).FirstOrDefault(p => p.ProductId == productId && p.UserId == GetCurrentUserByUsername(userName).Id);
        }

        public List<Carrinho> GetFromUser(string userName)
        {
            try {
                var user = GetCurrentUserByUsername(userName);
                return context.Carrinho.Where(u => u.UserId == user.Id).Include(c => c.Product).ToList();
            }
            catch { 
                return null;
            }
        }

        public List<Carrinho> GetAll()
        {
            return context.Carrinho.Include(c => c.Product).ToList();
        }


        public bool Update(Carrinho prod)
        {
            try
            {
                if (!context.Carrinho.Any(p => p.ProductId == prod.ProductId)) throw new Exception("Produto não existe!");

                context.Carrinho.Update(prod);
                context.SaveChanges();
                return true;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Delete(string userId, int? id)
        {
            try
            {
                context.Carrinho.Remove(Get(userId, id));
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public PneuUser GetCurrentUserByUsername(string userName)
        {
            return userManager.FindByNameAsync(userName).Result;
        }

        public double TotalCarrinho(string userName)
        {
            var carrinho = GetFromUser(userName);
            return carrinho.Sum(p => p.Product.preco);
        }
    }
}
