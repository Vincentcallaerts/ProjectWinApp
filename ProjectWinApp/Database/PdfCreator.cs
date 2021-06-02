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
            XPen lineRed = new XPen(XColors.Black, 2);

            List<ProductsOrder> products = new List<ProductsOrder>();
            Order order;
            Customer customer;

            using (DataContext data = new DataContext())
            {
                products = data.ProductsOrder.Where(po => po.OrderId == orderId).ToList();
                order = data.Order.Where(o => o.OrderId == orderId).FirstOrDefault();
                customer = data.Customer.Where(o => o.CustomerId == order.CustomerId).FirstOrDefault();
            }

            //tekent de header
            gfx.DrawImage(logo, new XRect(10, 0, 150, 150));
            gfx.DrawString("Project B.V.", font, XBrushes.Black,
              new XRect(page.Width - 140, 40,100,0));
            gfx.DrawString("Stuivenbergvaart 110", font, XBrushes.Black,
              new XRect(page.Width - 140, 50, 100, 0));
            gfx.DrawString("2800", font, XBrushes.Black,
              new XRect(page.Width-140, 60, 100, 0));
            gfx.DrawString("Mechelen", font, XBrushes.Black,
              new XRect(page.Width - 140, 70, 100, 0));
            
            //teken de Factuur nummer en datum
            gfx.DrawString($"{customer.Name}", font, XBrushes.Black,
              new XRect(10, 200, 100, 0));
            gfx.DrawString($"{customer.Email}", font, XBrushes.Black,
              new XRect(10, 220, 100, 0));
            gfx.DrawString($"FactuurNummer: {order.OrderId}", font, XBrushes.Black,
              new XRect(10, 360, 100, 0));
            gfx.DrawString($"Order date: {order.OrderDate.ToString("dd,MM,yyyy")}", font, XBrushes.Black,
              new XRect(page.Width - 200, 350, 100, 0));
            DateTime orderBy = order.OrderDate.AddDays(31);
            gfx.DrawString($"Order date by:{orderBy.ToString("dd,MM,yyyy")}", font, XBrushes.Black,
              new XRect(page.Width - 200, 360, 100, 0));
           
            //teken tabel header
            gfx.DrawString("Aantal", font, XBrushes.Black,
              new XRect(10, 390, 100, 0));
            gfx.DrawString("Beschrijving", font, XBrushes.Black,
              new XRect(50, 390, 100, 0));
            gfx.DrawString("Bedrag excl. Btw", font, XBrushes.Black,
              new XRect(380, 390, 100, 0));
            gfx.DrawString("Bedrag incl. Btw", font, XBrushes.Black,
              new XRect(480, 390, 100, 0));
            //405 is page height dus y 500 is de x tweede punt 10 x eerste punt
            gfx.DrawLine(lineRed, 10, 405, 580, 405);

            

            //teken table content
            for (int i = 0; i <= products.Count-1; i++)
            {
                gfx.DrawString($"{products[i].Amount}", font, XBrushes.Black,
                new XRect(10, 420 + (i*20), 100, 0));
                gfx.DrawString($"{products[i].CurrentProductName}", font, XBrushes.Black,
                new XRect(50, 420 + (i * 20), 100, 0));
                gfx.DrawString($"{products[i].OrderUnitPrice * products[i].Amount}€", font, XBrushes.Black,
                new XRect(380, 420 + (i * 20), 100, 0));
                gfx.DrawString($"{products[i].OrderUnitPrice * products[i].Amount * 1.21}€", font, XBrushes.Black,
                new XRect(480, 420 + (i * 20), 100, 0));
                gfx.DrawLine(lineRed, 10, 425 + (i * 20), 580, 425 + (i * 20));
            }

            gfx.DrawString($"Totaalbedrag excl BTW", font, XBrushes.Black,
            new XRect(280, 420 + (products.Count * 20), 100, 0));
            gfx.DrawString($"{TotaalProducts(products)}€", font, XBrushes.Black,
            new XRect(480, 420 + (products.Count * 20), 100, 0));

            gfx.DrawString($"21% BTW", font, XBrushes.Black,
            new XRect(355, 420 + (products.Count * 20) + 20, 100, 0));
            gfx.DrawString($"{TotaalProducts(products) * 0.21}€", font, XBrushes.Black,
            new XRect(480, 420 + (products.Count * 20)+20, 100, 0));

            gfx.DrawLine(lineRed, 10, 425 + (products.Count * 20) + 20, 580, 425 + (products.Count * 20) + 20);

            gfx.DrawString($"Gelieven het bedrag van {TotaalProducts(products)*1.21}€ over te maken voor: {orderBy.ToString("dd,MM,yyyy")}", font, XBrushes.Black,
            new XRect(10, 420 + (products.Count * 20) + 100, 100, 0));
            gfx.DrawString($"Rekeningnummer: BE39 7330 5924 5193", font, XBrushes.Black,
            new XRect(10, 420 + (products.Count * 20) + 120, 100, 0));
            //"footer voor tegen waneer dit te betalen is"
            //geeft de file een naam slaagt deze op en opent dit vervolgens
            string filename = "C:/Users/Gebruiker/source/repos/ProjectWinApp/ProjectWinApp/Pages/HelloWorld.pdf";
            document.Save(filename);
           
            Process.Start(filename);
            
        }
        private double TotaalProducts(List<ProductsOrder> products)
        {
            double temp = 0;
            foreach (var item in products)
            {
                temp += (item.OrderUnitPrice * item.Amount);
            }
            return temp;
        }
    }
}
