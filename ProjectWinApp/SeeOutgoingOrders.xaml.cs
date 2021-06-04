using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectWinApp
{
    /// <summary>
    /// Interaction logic for SeeOutgoingOrders.xaml
    /// </summary>
    public partial class SeeOutgoingOrders : Page
    {
        public List<ComboBoxIndexContent> Orders { get; set; }
        public SeeOutgoingOrders()
        {
            InitializeComponent();
            FillOrders();
        }

        private void FillOrders()
        {
            Orders = new List<ComboBoxIndexContent>();

            using (DataContext data = new DataContext())
            {
                var collection = data.OrderMagazijn.Select(om => om).ToList(); ;
                foreach (var item in collection)
                {
                    var magazijnOrder = data.Magazijn.Where(p => p.MagazijnId == item.MagazijnId).FirstOrDefault();

                    Orders.Add(new ComboBoxIndexContent(item.OrderMagazijnId, $"Product {item.ProductName} in Magazijn {magazijnOrder.Adress} werd {item.Amount} aan {item.UnitPrice}€ per stuk aangekocht om {item.OrderDate.ToString("dd,MM,yyyy")}"));
                }
            }
            lbProductOrders.ItemsSource = Orders;
        }
    }
}
