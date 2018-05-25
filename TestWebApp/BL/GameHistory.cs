using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Model;

namespace TestWebApp.BL
{
    public class GameHistory
    {
        public IEnumerable<Favorites> List { get; set; }
    }
}
