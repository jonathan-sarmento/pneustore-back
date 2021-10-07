using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public readonly UserManager<IdentityUser> userManager;
        public CarrinhoService(Context context, UserManager<IdentityUser> _userManager)
        {
            this.context = context;
            userManager = _userManager;
        }
        public bool Create(Carrinho objeto)
        {
            if (context.Carrinho.FirstOrDefault(c => c.Equals(objeto)) != null)
                return false;

            try
            {
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
            return context.Carrinho.Include(c => c.Product).FirstOrDefault(p => p.ProductId == productId && p.UserId == GetCurrentUserId(userName));
        }

        public List<Carrinho> GetFromUser(string userName)
        {
            try {
                var userId = GetCurrentUserId(userName);
                return context.Carrinho.Where(u => u.UserId == userId).Include(c => c.Product).ToList();
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
            catch
            {
                return false;
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

        public string GetCurrentUserId(string userName)
        {
            return context.Users.FirstOrDefault(u => u.UserName == userName).Id;
        }

        public double TotalCarrinho(string userName)
        {
            var carrinho = GetFromUser(userName);
            return carrinho.Sum(p => p.Product.preco);
        }
    }
}
