using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.ViewModel;

namespace TestWebApp.BL
{
    public class PdfFIle : IGen
    {
        public void Create(List<LendViewModel> LendViewModels)
        {
            string path = @"C:\Users\Vlad\Desktop\test2.pdf";
            //Create a System.IO.FileStream object:
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);

            iTextSharp.text.Document doc = new iTextSharp.text.Document();

            PdfWriter writer = PdfWriter.GetInstance(doc, fs);

            doc.Open();

            LendViewModels.ForEach(lend =>
            {
                doc.Add(new Paragraph(" Game name " + lend.Game.Name + " Customer name " + lend.Customers.Name + " Date " + lend.Date.ToString()));
            });

           

            doc.Close();
        
        }
    }
}
