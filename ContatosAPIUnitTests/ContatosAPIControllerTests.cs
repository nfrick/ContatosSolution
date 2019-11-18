using ContatosAPI.Controllers;
using ContatosAPI.Models;
using ContatosAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ContatosAPIUnitTests {
    public class ContatoControllerTests {
        [Fact]
        public void GetAll_NoCondition_ReturnsAllContatoModels() {
            var repositoryMock = new Mock<IContatosRepository>();
            var apiController = new ContatoController(repositoryMock.Object);

            apiController.GetAll();

            repositoryMock.Verify(x => x.GetAll());
        }

        [Fact]
        public void Get_IdPassed_ReturnsProperContatoModel() {
            var contato = new ContatoModel();

            var contatoMock = new Mock<IContatosRepository>();
            contatoMock.Setup(x => x.GetById(It.IsAny<int>())).Returns(contato);

            var apiController = new ContatoController(contatoMock.Object);

            var result = apiController.GetById(0);

            Assert.Equal(result?.Value, contato);
        }

        [Fact]
        public void Get_IdPassed_ReturnsProperContatoModel2() {
            var contato1 = new ContatoModel { Id = 1, Nome = "Primeiro" };
            var contato2 = new ContatoModel { Id = 2, Nome = "Segundo" };
            var contatos = new List<ContatoModel>() { contato1, contato2 };

            var contatoMock = new Mock<IContatosRepository>();
            contatoMock.Setup(x => x.GetById(It.Is<int>(y => y == 2))).Returns(contatos.First(x => x.Id == 2));

            var apiController = new ContatoController(contatoMock.Object);

            var result = apiController.GetById(2);

            Assert.Equal(result?.Value, contato2);
        }

        [Fact]
        public void Get_NoRequestedContatoModel_ReturnsEmptyResponseContatoModel() {
            var contatoMock = new Mock<IContatosRepository>();
            contatoMock.Setup(x => x.GetById(It.IsAny<int>())).Returns((ContatoModel)null);

            var apiController = new ContatoController(contatoMock.Object);

            var result = apiController.GetById(0);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void Create_ContatoModelPassed_ProperResponseReturned() {
            var contato = new ContatoModel();

            var contatoMock = new Mock<IContatosRepository>();
            contatoMock.Setup(x => x.Create(It.Is<ContatoModel>(y => y == contato)))
                .Returns(contato);

            var apiController = new ContatoController(contatoMock.Object);

            var result = apiController.Add(contato);

            contatoMock.Verify(x => x.Create(It.Is<ContatoModel>(y => y == contato)));
            Assert.True(result is OkResult);
        }

        [Fact]
        public void Create_NullPassed_BadResponseReturned() {
            var contato = new ContatoModel();

            var contatoMock = new Mock<IContatosRepository>();
            contatoMock.Setup(x => x.Create(It.Is<ContatoModel>(y => y == contato)))
                .Returns(contato);

            var apiController = new ContatoController(contatoMock.Object);

            var result = apiController.Add(null);

            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public void Update_ContatoModelPassed_ReturnedProperContatoModel() {
            const int id = 1;
            var contato = new ContatoModel() {
                Id = id
            };

            var contatoMock = new Mock<IContatosRepository>();
            contatoMock.Setup(x => x.Update(It.IsAny<int>(), It.Is<ContatoModel>(c => c == contato)))
                .Returns(contato);

            var apiController = new ContatoController(contatoMock.Object);

            var result = apiController.Update(id, contato);

            contatoMock.Verify(x => x.Update(It.Is<int>(i => i == id), It.Is<ContatoModel>(c => c == contato)));
            Assert.True(result is OkResult);
        }

        [Fact]
        public void Update_WrongIDPassed_BadRequestReturned() {
            var contato = new ContatoModel();
            const int id = 1;

            var contatoMock = new Mock<IContatosRepository>();
            contatoMock.Setup(x => x.Update(It.IsAny<int>(), It.Is<ContatoModel>(ab => ab == contato)))
                .Returns(contato);

            var apiController = new ContatoController(contatoMock.Object);

            var result = apiController.Update(id, contato);

            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public void Update_NullPassed_BadRequestReturned() {
            var contato = new ContatoModel();
            var id = 1;

            var contatoMock = new Mock<IContatosRepository>();
            contatoMock.Setup(x => x.Update(It.IsAny<int>(), It.Is<ContatoModel>(c => c == contato)))
                .Returns(contato);

            var apiController = new ContatoController(contatoMock.Object);

            var result = apiController.Update(id, null);

            Assert.True(result is BadRequestResult);
        }

        [Fact]
        public void Update_ExceptionTrowed_NotFoundReturned() {
            const int id = 1;
            var contato = new ContatoModel() {
                Id = id
            };

            var contatoMock = new Mock<IContatosRepository>();
            contatoMock
                .Setup(x => x.Update(It.IsAny<int>(), It.Is<ContatoModel>(ab => ab == contato)))
                .Throws(new Exception());

            var apiController = new ContatoController(contatoMock.Object);

            var result = apiController.Update(id, contato);

            Assert.True(result is NotFoundResult);
        }

        [Fact]
        public void Delete_GoodIdPassed_ProperFunctionsCalled() {
            const int id = 1;

            var contatoMock = new Mock<IContatosRepository>();
            contatoMock.Setup(x => x.Delete(It.IsAny<int>()));

            var apiController = new ContatoController(contatoMock.Object);

            var result = apiController.Delete(id);

            contatoMock.Verify(x => x.Delete(It.Is<int>(i => i == id)));
            Assert.True(result is OkResult);
        }

        [Fact]
        public void Delete_BadIdPassed_NotFoundReturned() {
            const int id = 1;

            var contatoMock = new Mock<IContatosRepository>();
            contatoMock.Setup(x => x.Delete(It.IsAny<int>())).Throws(new Exception());

            var apiController = new ContatoController(contatoMock.Object);

            var result = apiController.Delete(id);

            contatoMock.Verify(x => x.Delete(It.Is<int>(i => i == id)));
            Assert.True(result is NotFoundResult);
        }
    }
}