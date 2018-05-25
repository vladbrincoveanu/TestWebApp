using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp.Data.Model
{
    public class Favorites
    {
        public int FavoritesId { get; set; }
        public NamePrice NewGame { get; set; }
        public NamePrice OldGame { get; set; }
        public string Change { get; set; }
        public DateTime Date { get; set; }
        
    }
}
