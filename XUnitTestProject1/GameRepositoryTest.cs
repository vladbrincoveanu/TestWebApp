using Moq;
using System;
using System.Collections.Generic;
using TestWebApp.Controllers;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Model;
using Xunit;

namespace XUnitTestProject1
{
    public class GameRepositoryTest
    {
        [Fact]
        public void Create_Game_Calls_GameRepositoryCreate()
        {
            var gameTest = new Game
            {
                GameId = 1,
                Name = "DADA",
                Price = 100
            };

            var mock = new Mock<IGameRepository>();
            mock.Setup(foo => foo.Create(gameTest));
            mock.Object.Create(gameTest);
            mock.Setup(foo => foo.Create(gameTest));
            mock.Verify(x => x.Create(gameTest), Times.Once());

        }

        [Fact]
        public void Get_Game_By_Id()
        {
            var gameTest = new Game
            {
                GameId = 1,
                Name = "DADA",
                Price = 100
            };

            var mock = new Mock<IGameRepository>();

            mock.Setup(x => x.getById(1))
            .Returns(gameTest); 
            Assert.Equal(mock.Object.getById(1),gameTest); 
            mock.Verify(x => x.getById(gameTest.GameId), Times.Once()); 
        }

        [Fact]
        public void Delete_Game_By_Id()
        {
            var gameTest = new Game
            {
                GameId = 1,
                Name = "DADA",
                Price = 100
            };

            var mock = new Mock<IGameRepository>();

            mock.Setup(x => x.Delete(gameTest));
            mock.Object.Delete(gameTest);

            mock.Setup(x => x.getById(1));
            Assert.Null(mock.Object.getById(1));
            mock.Verify(x => x.Delete(gameTest), Times.Once());
        }

        [Fact]
        public void FindByAuthor()
        {
            var author = new Author {AuthorId = 1, Name = "Da" };
            var gameTest = new Game
            {
                Author = author,
                AuthorID = 1,
                GameId = 1,
                Name = "DADA",
                Price = 100
            };

            var mock = new Mock<IGameRepository>();

            var listgame = new List<Game> { gameTest };

            //mock.Setup(foo => foo.Create(gameTest));
            //mock.Object.Create(gameTest);
            //mock.Setup(foo => foo.Create(gameTest));
            //mock.Verify(x => x.Create(gameTest), Times.Once());

            mock.Setup(x => x.FindWithAuthor(It.IsAny<Func<Game, bool>>())).Returns(listgame);
            
            Assert.Equal(mock.Object.FindWithAuthor(It.IsAny<Func<Game, bool>>()), listgame);
            mock.Verify(x => x.FindWithAuthor(It.IsAny<Func<Game, bool>>()), Times.Once());
        }

        [Fact]
        public void GetAllWithAuthor()
        {
            var author = new Author { AuthorId = 1, Name = "Da" };
            var gameTest = new Game
            {
                Author = author,
                AuthorID = 1,
                GameId = 1,
                Name = "DADA",
                Price = 100
            };

            var gameTest1 = new Game
            {
                Author = author,
                AuthorID = 1,
                GameId = 2,
                Name = "DADADA",
                Price = 105
            };

            var mock = new Mock<IGameRepository>();

            var listgame = new List<Game> { gameTest,gameTest1 };

            mock.Setup(x => x.GetAllWithAuthor()).Returns(listgame);

            Assert.Equal(mock.Object.GetAllWithAuthor(), listgame);
            mock.Verify(x => x.GetAllWithAuthor(), Times.Once());
        }

        [Fact]
        public void FindWithAuthorAndBorrower()
        {
            var author = new Author { AuthorId = 1, Name = "Da" };
            var customer = new Customer { CustomerId = 1, Name = "Dada" };
            var gameTest = new Game
            {
                Author = author,
                AuthorID = 1,
                GameId = 1,
                Name = "DADA",
                Customer = customer,
                CustomerID = 1,
                Price = 100
            };

            var mock = new Mock<IGameRepository>();

            var listgame = new List<Game> { gameTest };

            mock.Setup(x => x.FindWithAuthorAndBorrower(It.IsAny<Func<Game, bool>>())).Returns(listgame);

            Assert.Equal(mock.Object.FindWithAuthorAndBorrower(It.IsAny<Func<Game, bool>>()), listgame);
            mock.Verify(x => x.FindWithAuthorAndBorrower(It.IsAny<Func<Game, bool>>()), Times.Once());
        }

    }
}
