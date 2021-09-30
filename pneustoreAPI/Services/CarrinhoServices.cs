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
        public CarrinhoServices(Context context)
        {
            this.context = context;
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
    }
}
