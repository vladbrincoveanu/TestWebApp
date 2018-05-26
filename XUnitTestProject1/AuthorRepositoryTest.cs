
using Moq;
using System;
using System.Collections.Generic;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Model;
using Xunit;

namespace XUnitTestProject1
{
    public class AuthorRepositoryTest
    {
        [Fact]
        public void Create_Game_Calls_GameRepositoryCreate()
        {
            var authorTest = new Author
            {
                AuthorId = 1,
                Name = "DADA",
            };

            var mock = new Mock<IAuthorRepository>();
            mock.Setup(foo => foo.Create(authorTest));
            mock.Object.Create(authorTest);
            mock.Setup(foo => foo.Create(authorTest));
            mock.Verify(x => x.Create(authorTest), Times.Once());

        }

        [Fact]
        public void Get_Game_By_Id()
        {
            var authorTest = new Author
            {
                AuthorId = 1,
                Name = "DADA",
            };

            var mock = new Mock<IAuthorRepository>();

            mock.Setup(x => x.getById(1))
            .Returns(authorTest);
            NUnit.Framework.Assert.AreEqual(mock.Object.getById(1), authorTest);
            mock.Verify(x => x.getById(authorTest.AuthorId), Times.Once());
        }

        [Fact]
        public void Delete_Game_By_Id()
        {
            var authorTest = new Author
            {
                AuthorId = 1,
                Name = "DADA",
            };

            var mock = new Mock<IAuthorRepository>();


            mock.Setup(x => x.Delete(authorTest));
            mock.Object.Delete(authorTest);

            mock.Setup(x => x.getById(1));
            NUnit.Framework.Assert.Null(mock.Object.getById(1));
            mock.Verify(x => x.Delete(authorTest), Times.Once());
        }

        [Fact]
        public void GetAllWithGames()
        {
            var game = new Game { GameId = 1, Name = "da", Price = 100 };
            var authorTest = new Author
            {
                AuthorId = 1,
                Name = "DADA",
                Game = new List<Game>(){ game }
            };

            var list = new List<Author>() { authorTest };
            var mock = new Mock<IAuthorRepository>();


            mock.Setup(x => x.GetAllWithGames()).Returns(list);
          
            NUnit.Framework.Assert.AreEqual(mock.Object.GetAllWithGames(),list);
            mock.Verify(x => x.GetAllWithGames(), Times.Once());
        }

        [Fact]
        public void GetWithGames()
        {
            var game = new Game { GameId = 1, Name = "da", Price = 100 };
            var authorTest = new Author
            {
                AuthorId = 1,
                Name = "DADA",
                Game = new List<Game>() { game }
            };

           // var list = new List<Author>() { authorTest };
            var mock = new Mock<IAuthorRepository>();


            mock.Setup(x => x.GetWithGames(authorTest.AuthorId)).Returns(authorTest);

            NUnit.Framework.Assert.AreEqual(mock.Object.GetWithGames(authorTest.AuthorId), authorTest);
            mock.Verify(x => x.GetWithGames(authorTest.AuthorId), Times.Once());
        }
    }
}
