using Microsoft.AspNetCore.Identity;
using pneustoreAPI.Data;
using pneustoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pneustoreAPI.Services
{
    public class CarrinhoServices : IService<Carrinho>
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
            throw new NotImplementedException();
        }

        public Carrinho Get(int? id)
        {
            throw new NotImplementedException();
        }

        public List<Carrinho> GetAll()
        {
            throw new NotImplementedException();
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
