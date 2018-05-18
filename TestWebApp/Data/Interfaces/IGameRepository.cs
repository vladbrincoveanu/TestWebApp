using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Model;

namespace TestWebApp.Data.Interfaces
{
    public interface IGameRepository :IRepository<Game>
    {
        IEnumerable<Game> GetAllWithAuthor();

        IEnumerable<Game> FindWithAuthor(Func<Game, bool> predicate);

        IEnumerable<Game> FindWithAuthorAndBorrower(Func<Game, bool> predicate);
    }
}
