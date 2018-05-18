using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Model;

namespace TestWebApp.Data.Repository
{
    public class GameRepository : Repository<Game>, IGameRepository
    {
        public GameRepository(LibraryDbContext context) : base(context)
        {
        }

        public IEnumerable<Game> FindWithAuthor(Func<Game, bool> predicate)
        {
           return  _context.Games
              .Include(a => a.Author)
              .Where(predicate);

        }
        public IEnumerable<Game> FindWithAuthorAndBorrower(Func<Game, bool> predicate)
        {
            return _context.Games
            .Include(a => a.Author)
            .Include(a => a.Customer)
            .Where(predicate);

        }
        public IEnumerable<Game> GetAllWithAuthor()
        { return _context.Games.Include(a=> a.Author); }

    }
}
