using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Model;
using TestWebApp.ViewModel;

namespace TestWebApp.BL
{
    public interface ISubscriber
    {
        void Update(LendViewModel data);
        List<LendViewModel> GetList();
    }
}
