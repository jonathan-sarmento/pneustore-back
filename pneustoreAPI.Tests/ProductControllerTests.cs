using System;
using Xunit;
using FakeItEasy;
using pneustoreAPI.Models;
using System.Collections.Generic;
using pneustoreAPI.Controllers;
using pneustoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using pneustoreAPI.API;
using System.Linq;

namespace pneustoreAPI.Tests
{
    public class ProductControllerTests
    {
        readonly int quantProducts = 10;
        List<Product> fakeProducts;
        public ProductControllerTests()
        {
            fakeProducts = new();

            for (int i = 1; i <= quantProducts; i++)
                fakeProducts.Add(new Product() { id = i, nome = $"Produto {i}", imagemUrl = "", imagemUrlMarca = "", preco = 99.1 + i });
        }

        [Fact]
        public void Index_RetornarListaDeProdutos()
        {
            // Arrange
            var productService = A.Fake<IService<Product>>();
            A.CallTo(() => productService.GetAll()).Returns(fakeProducts);
            var controller = new ProductController(productService);

            // Act
            var result = controller.Index() as ObjectResult;
            var values = result.Value as APIResponse<List<Product>>;

            // Assert
            Assert.True(
                    values.Succeed &&
                    values.Message == "" &&
                    values.Results == fakeProducts
                );
        }

        [Theory]
        [InlineData(9)]
        [InlineData(10)]
        [InlineData(0, "Produto com id: 0 não existe.", false)]
        [InlineData(-19, "Produto com id: -19 não existe.", false)]
        [InlineData(1000, "Produto com id: 1000 não existe.", false)]
        public void Get_RetornarProdutoPorId(int? id, string message = "", bool succeed = true)
        {
            // Arrange
            var productService = A.Fake<IService<Product>>();
            A.CallTo(() => productService.Get(id)).Returns(fakeProducts.Find(p => p.id == id));
            var controller = new ProductController(productService);

            // Act
            var result = controller.Get(id) as ObjectResult;
            var exists = fakeProducts.FirstOrDefault(p => p.id == id) != null;

            // Assert 
            if (exists)
            {
                var value = result.Value as APIResponse<Product>;

                Assert.True(
                    value.Succeed == succeed &&
                    value.Message == message &&
                    value.Results == fakeProducts.Find(p => p.id == id)
                );
            } else
            {
                var value = result.Value as APIResponse<string>;

                Assert.True(
                    value.Succeed == succeed &&
                    value.Message == message &&
                    value.Results == null
                );
            }
            
        }
    }
}
