using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Model;

namespace TestWebApp.Data.Interfaces
{
    public interface IFavoriteRepository : IRepository<Favorites>
    {
        IEnumerable<Favorites> GetAllChangesForGame(int id);

    }
}
