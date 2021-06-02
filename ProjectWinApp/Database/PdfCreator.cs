using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWinApp
{
    class PdfCreator
    {
        public void CreatePdf(int orderId)
        {


            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 10, XFontStyle.Bold);
            XImage logo = XImage.FromFile("C:/Users/Gebruiker/source/repos/ProjectWinApp/ProjectWinApp/Images/logo.png");

            //tekent de header
            gfx.DrawImage(logo, new XRect(10, 0, 150, 150));
            gfx.DrawString("Project B.V.", font, XBrushes.Black,
              new XRect(page.Width - 100, 20,100,0));
            gfx.DrawString("Stuivenbergvaart 110", font, XBrushes.Black,
              new XRect(page.Width - 100, 30, 100, 0));
            gfx.DrawString("2800", font, XBrushes.Black,
              new XRect(page.Width-100, 40, 100, 0));
            gfx.DrawString("Mechelen", font, XBrushes.Black,
              new XRect(page.Width - 100, 50, 100, 0));

            gfx.DrawString("Naam Customer", font, XBrushes.Black,
              new XRect(10, 200, 100, 0));
            gfx.DrawString("Email Customer", font, XBrushes.Black,
              new XRect(10, 220, 100, 0));
            //teken de Factuur nummer en datum
            //teken tabel hiervoor met alle producten in
            //"footer voor tegen waneer dit te betalen is"
            //geeft de file een naam slaagt deze op en opent dit vervolgens
            string filename = "HelloWorld.pdf";
            document.Save(filename);
           
            Process.Start(filename);
            
        }
    }
}
