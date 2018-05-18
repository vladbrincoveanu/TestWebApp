using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestWebApp.Data.Model;
using TestWebApp.Data.Repository;

namespace TestWebApp.Data.Interfaces
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {

        public AuthorRepository(LibraryDbContext context) : base(context)
        {

        }

        public IEnumerable<Author> GetAllWithGames()
        {
            return _context.Authors.Include(a => a.Game);
        }

        public Author GetWithGames(int id)
        {
            return _context.Authors
                   .Where(a => a.AuthorId == id)
                   .Include(a => a.Game)
                   .FirstOrDefault();
        }
    }
}
