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
    public class CarrinhoServices
    {
        Context context;
        public readonly UserManager<PneuUser> userManager;
        public CarrinhoServices(Context context, UserManager<PneuUser> _userManager)
        {
            this.context = context;
            userManager = _userManager;
        }
        public bool Create(Carrinho objeto)
        {
            if (context.Carrinho.FirstOrDefault(c => c.Equals(objeto)) == null)
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
            return context.Carrinho.FirstOrDefault(p => p.ProductId == productId && p.UserId == GetCurrentUserId(userName));
        }

        public List<Carrinho> GetFromUser(string userName)
        {
            return context.Carrinho.Where(u => u.UserId == GetCurrentUserId(userName)).ToList();
        }

        public List<Carrinho> GetAll()
        {
            return context.Carrinho.ToList();
        }

        public string GetCurrentUserId(string userName)
        {
            return context.Users.FirstOrDefault(u => u.UserName == userName).Id;
        }
        public bool Delete(int? id)
        {
            try
            {
                context.Products.Remove(Get(id));
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
