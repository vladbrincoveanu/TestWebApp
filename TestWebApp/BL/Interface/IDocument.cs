using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.ViewModel;

namespace TestWebApp.BL
{
    public interface IDocument
    {
        IGen CreateDocument(String type);
    }
}
