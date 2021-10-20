using System.Collections.Generic;
using System.Linq;
using pneustoreAPI.Data;
using pneustoreAPI.Models;

namespace pneustoreAPI.Services
{
    public class ProductService : IService<Product>
    {
        private readonly Context _context;

        public ProductService(Context context)
        {
            _context = context;
        }

        public bool Create(Product objeto)
        {
            throw new System.NotImplementedException();
        }

        public Product Get(int? id) => _context.Product.FirstOrDefault(p => p.id == id);
        
        public List<Product> GetAll() => _context.Product.ToList();
    }
}