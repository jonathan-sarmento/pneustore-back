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
        public readonly UserManager<PneuUser> userManager;
        public CarrinhoService(Context context, UserManager<PneuUser> _userManager)
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

    }
}
