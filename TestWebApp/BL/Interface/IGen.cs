using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.ViewModel;

namespace TestWebApp.BL
{
    public interface IGen
    {
        void Create(List<LendViewModel> LendViewModels);
    }
}
