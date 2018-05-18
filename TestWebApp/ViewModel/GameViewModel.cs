using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Model;

namespace TestWebApp.ViewModel
{
    public class GameViewModel
    {
        public Game Game{ get; set; }
        public IEnumerable<Author> Authors { get; set; }
    }
}
