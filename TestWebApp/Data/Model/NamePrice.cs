using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWebApp.Data.Model
{
    public class NamePrice
    {
        public int NamePriceID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int GameID { get; set; }
    }
}
