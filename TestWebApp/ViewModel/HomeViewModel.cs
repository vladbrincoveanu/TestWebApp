using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Model;

namespace TestWebApp.ViewModel
{
    public class HomeViewModel
    {
        public int CustomerCount { get; set; }
        public int AuthorCount { get; set; }
        public int GameCount { get; set; }
        public int LendCount { get; set; }

        public Customer Customer { get; set; }
    }
}
