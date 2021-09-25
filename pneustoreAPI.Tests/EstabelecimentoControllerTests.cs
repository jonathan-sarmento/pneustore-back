
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using pneustoreAPI.API;
using pneustoreAPI.Controllers;
using pneustoreAPI.Models;
using pneustoreAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EstabelecimentoControllerTest
{
    public class EstabelecimentoControllerTest
    {

        int EstabelecimentoQuantity = 10;
        List<Estabelecimento> fakeEstabelecimento;

        public EstabelecimentoControllerTest()
        {
            fakeEstabelecimento = new List<Estabelecimento>();
            for (var i = 1; i <= EstabelecimentoQuantity; i++)
                fakeEstabelecimento.Add(new Estabelecimento { id = i, nome = $"Estab {i}" });
        }

        [Fact]
        public void GetEstabelecimento_Returns_The_Correct_Estabelecimento()
        {
            var estabelecimentoService = A.Fake<IService<Estabelecimento>>();
            A.CallTo(() => estabelecimentoService.GetAll()).Returns(fakeEstabelecimento);
            var controller = new EstabelecimentoController(estabelecimentoService);

            var result = controller.Index() as ObjectResult;

            var values = result.Value as APIResponse<List<Estabelecimento>>;
            Assert.True(
                values.Results == fakeEstabelecimento &&
                values.Message == "" &&
                values.Succeed
            );
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(0, "Estabelecimento não existe.", false)]
        [InlineData(579, "Estabelecimento não existe.", false)]
        [InlineData(-55, "Estabelecimento não existe.", false)]
        [InlineData(null, "Estabelecimento não existe.", false)]
        [InlineData(14, "Estabelecimento não existe.", false)]
        public void GetProduct_Return_Product_By_Id(int? id, string message = "", bool succeed = true)
        {
            var estabelecimentoService = A.Fake<IService<Estabelecimento>>();
            A.CallTo(() => estabelecimentoService.Get(id)).Returns(fakeEstabelecimento.Find(e => e.id == id));

            var controller = new EstabelecimentoController(estabelecimentoService);

            ObjectResult result = controller.Get(id) as ObjectResult;

            var exists = fakeEstabelecimento.Find(e => e.id == id) != null;
            if (exists)
            {

                var values = result.Value as APIResponse<Estabelecimento>;
                Assert.True(
                    values.Message == message &&
                    values.Succeed == succeed &&
                    values.Results == fakeEstabelecimento.Find(e => e.id == id)
                );
            }
            else
            {
                var values = result.Value as APIResponse<string>;
                Assert.True(
                    values.Message == message &&
                    values.Succeed == succeed &&
                    values.Results == null
                );
            }
        }

    }
}
