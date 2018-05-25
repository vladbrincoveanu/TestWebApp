using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Model;

namespace TestWebApp.Data.Repository
{
    public class FavoritesRepository : Repository<Favorites>, IFavoriteRepository
    {
        public FavoritesRepository(LibraryDbContext context) : base(context)
        {
        }

        public IEnumerable<Favorites> GetAllChangesForGame(int id)
        => _context.Favorites
            .Include(a => a.NewGame)
            .Include(a => a.OldGame)
            .Where(a => a.NewGame.GameID == id)
            .OrderBy(a => a.Date);
    }
}
