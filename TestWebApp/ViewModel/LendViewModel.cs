using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Model;

namespace TestWebApp.ViewModel
{
    public class LendViewModel
    {
        public Game Game { get; set; }
        public Customer Customers { get; set; }
        public DateTime Date { get; set; }

    }
}
