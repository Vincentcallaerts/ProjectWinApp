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
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Page
    {
        public List<ComboBoxIndexContent> Customers { get; set; }
        public List<ComboBoxIndexContent> Suppliers { get; set; }
        public List<ComboBoxIndexContent> Warehouses { get; set; }       
        public List<ComboBoxIndexContent> Products { get; set; }
        public AddOrder()
        {
            InitializeComponent();
            Fill();
        }

        private void Fill()
        {
            Customers = new List<ComboBoxIndexContent>();
            Suppliers = new List<ComboBoxIndexContent>();

            using (DataContext data = new DataContext())
            {
                var collection = data.Customer.Select(m => m);

                foreach (var item in collection)
                {
                    Customers.Add(new ComboBoxIndexContent(item.CustomerId, item.Name));
                }
                var collectionSuppliers = data.Supplier.Select(s => s);
                foreach (var item in collectionSuppliers)
                {
                    Suppliers.Add(new ComboBoxIndexContent(item.SupplierId,item.Name));
                }
            }
            cmbCustomers.ItemsSource = Customers;
            cmbCustomers.SelectedIndex = 0;

            cmbSuppliers.ItemsSource = Suppliers;
            cmbSuppliers.SelectedIndex = 0;
            UpdateProducts();
        }

        private void UpdatecmbSuppliers()
        {
            //throw new NotImplementedException();
        }
        private void UpdateProducts()
        {
            cmbProducts.ItemsSource = null;
            Products = new List<ComboBoxIndexContent>();

            int selectedSupplier = Convert.ToInt32(cmbSuppliers.SelectedValue);

            using (DataContext data = new DataContext())
            {
                var collection = data.Product.Join(data.ProductsSupplier, p => p.ProductId, ps => ps.ProductId, (p,ps) => new { ProductId = p.ProductId, SupplierId = ps.SupplierId, ProductName = p.Name }).Where(p => p.SupplierId == selectedSupplier);

                foreach (var item in collection)
                {
                    Products.Add(new ComboBoxIndexContent(item.ProductId, item.ProductName));
                }
                
            }
            cmbProducts.ItemsSource = Products;
            cmbProducts.SelectedIndex = 0;
            //UpdateWarehouses();

        }
        private void UpdateWarehouses()
        {
            //throw new NotImplementedException();
        }
        private void btnAddToList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCreateFullOrder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void cmbSuppliers_DropDownClosed(object sender, EventArgs e)
        {
            UpdateProducts();
        }

       

        private void cmbProducts_DropDownClosed(object sender, EventArgs e)
        {

        }

        private void cmbWarehouses_DropDownClosed(object sender, EventArgs e)
        {
            UpdateWarehouses();
        }

        
    }
}
