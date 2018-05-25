using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.ViewModel;

namespace TestWebApp.BL
{
    public class TxtFile : IGen
    {

        public void Create(List<LendViewModel> LendViewModels)
        {
            string path = @"C:\Users\Vlad\Desktop\test.txt";

            using (var tw = new StreamWriter(path, true))
            {
                foreach(var lend in LendViewModels)
                {
                    tw.WriteLine(" Game name " + lend.Game.Name + " Customer name " + lend.Customers.Name + " Date " + lend.Date.ToString());
                }

                tw.Close();
            }

        }
    }
}
