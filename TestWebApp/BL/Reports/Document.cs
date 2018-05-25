using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.ViewModel;

namespace TestWebApp.BL
{
    public class Document : IDocument
    {
        //abstract method.
        public IGen CreateDocument(string type)
        {

            if (type.Equals("pdf"))
            {
                return new PdfFIle();
            }
            else if (type.Equals("txt"))
            {
                return new TxtFile();
            }
            else
            {
                return null;
            }

        }

    }
}
