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
    /// Interaction logic for SeeIncommingOrder.xaml
    /// </summary>
    public partial class SeeIncommingOrder : Page
    {
        public List<ComboBoxIndexContent> Customers { get; set; }
        public List<ComboBoxIndexContent> Orders { get; set; }
        public List<ComboBoxIndexContent> ProductsOrder { get; set; }

        public SeeIncommingOrder()
        {
            InitializeComponent();
            FillCustomers();
        }

        private void FillCustomers()
        {
            
            Customers = new List<ComboBoxIndexContent>();
            using (DataContext data = new DataContext())
            {
                var collection = data.Customer.Select(c => c);
                foreach (var item in collection)
                {
                    Customers.Add(new ComboBoxIndexContent(item.CustomerId,item.Email));
                }
            }
            cmbCustomers.ItemsSource = Customers;
            cmbCustomers.SelectedIndex = 0;
            Update();
        }
        private void Update()
        {
            cmbOrders.ItemsSource = null;
            Orders = new List<ComboBoxIndexContent>();

            int selectedCustomerId = Convert.ToInt32(cmbCustomers.SelectedValue);

            using (DataContext data = new DataContext())
            {
                var collection = data.Order.Where(c => c.CustomerId == selectedCustomerId).OrderBy(c => c.OrderDate);
                foreach (var item in collection)
                {
                    Orders.Add(new ComboBoxIndexContent(item.OrderId, item.OrderDate.ToString("dd,MM,yyyy") + $"| {item.OrderId}"));
                }
            }
            cmbOrders.ItemsSource = Orders;
            cmbOrders.SelectedIndex = 0;
            UpdateProductsOrder();
        }

        private void cmbCustomers_DropDownClosed(object sender, EventArgs e)
        {
            Update();
        }

        private void lbOrders_DropDownClosed(object sender, EventArgs e)
        {
            UpdateProductsOrder();
        }

        private void UpdateProductsOrder()
        {
            lbProductOrders.ItemsSource = null;
            ProductsOrder = new List<ComboBoxIndexContent>();

            int selectedOrderId = Convert.ToInt32(cmbOrders.SelectedValue);

            using (DataContext data = new DataContext())
            {
                var collection = data.ProductsOrder.Where(c => c.OrderId == selectedOrderId).OrderBy(c => c.Amount);
                foreach (var item in collection)
                {
                    ProductsOrder.Add(new ComboBoxIndexContent(item.OrderId, item.ToString()));
                }
            }
            lbProductOrders.ItemsSource = ProductsOrder;
            
        }

        private void btnCreatePdf_Click(object sender, RoutedEventArgs e)
        {
            int selectedOrderId = Convert.ToInt32(cmbOrders.SelectedValue);

            PdfCreator pdf = new PdfCreator();
            pdf.CreatePdf(selectedOrderId);
        }
    }
}
