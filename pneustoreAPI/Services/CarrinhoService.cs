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
        private readonly Context _context;
        private readonly UserManager<PneuUser> _userManager;
        public CarrinhoService(Context context, UserManager<PneuUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public bool Create(Carrinho objeto)
        {
            if (_context.Carrinho.FirstOrDefault(c => c.Equals(objeto)) != null)
                return false;

            List<EstabPneu> estoqueProduto = _context.EstabPneu.Where(p => p.ProductId == objeto.ProductId).ToList();
            EstabPneu estoqueAtualizado = new EstabPneu();

            if (estoqueProduto == null) return false;

            foreach(EstabPneu estoque in estoqueProduto)
            {
                if(estoque.Quantity - objeto.Quantity >= 0)
                {
                    estoqueAtualizado = estoque;
                    estoqueAtualizado.Quantity = estoque.Quantity - objeto.Quantity;
                    break;
                }
            }

            try
            {
                if (estoqueAtualizado.Quantity >= 0 && estoqueAtualizado.ProductId != 0)
                {
                    _context.EstabPneu.Update(estoqueAtualizado);
                    _context.Carrinho.Add(objeto);
                    _context.SaveChanges();
                    return true;
                }
                else  
                    return false;
            }
            catch
            {
                return false;
            }
        }

        public Carrinho Get(string userName, int? productId)
        {
            return _context.Carrinho.Include(c => c.Product).FirstOrDefault(p => p.ProductId == productId && p.UserId == GetCurrentUserByUsername(userName).Id);
        }

        public List<Carrinho> GetFromUser(string userName)
        {
            try {
                var user = GetCurrentUserByUsername(userName);
                return _context.Carrinho.Where(u => u.UserId == user.Id).Include(c => c.Product).ToList();
            }
            catch { 
                return null;
            }
        }

        public List<Carrinho> GetAll()
        {
            return _context.Carrinho.Include(c => c.Product).ToList();
        }


        public bool Update(Carrinho prod)
        {
            try
            {
                if (!_context.Carrinho.Any(p => p.ProductId == prod.ProductId)) throw new Exception("Produto não existe!");

                _context.Carrinho.Update(prod);
                _context.SaveChanges();
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
                Carrinho carrinho = Get(userId, id);
                EstabPneu estoque = _context.EstabPneu.FirstOrDefault(p => p.ProductId == id && p.Quantity < carrinho.Quantity);
                estoque.Quantity += carrinho.Quantity;
                _context.EstabPneu.Update(estoque);
                _context.Carrinho.Remove(Get(userId, id));
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public PneuUser GetCurrentUserByUsername(string userName)
        {
            return _userManager.FindByNameAsync(userName).Result;
        }

        public double TotalCarrinho(string userName)
        {
            var carrinho = GetFromUser(userName);
            return carrinho.Sum(p => p.Product.preco * p.Quantity);
        }
    }
}
