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
        public CarrinhoServices(Context context)
        {
            this.context = context;
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
    }
}
