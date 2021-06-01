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
        public List<ComboBoxIndexContent> ProductsOrders { get; set; }

        public List<ProductsOrder> productsOrders = new List<ProductsOrder>();
        public AddOrder()
        {
            InitializeComponent();
            Fill();
            ProductsOrders = new List<ComboBoxIndexContent>();
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
            UpdateWarehouses();

        }
        private void UpdateWarehouses()
        {
            cmbWarehouses.ItemsSource = null;
            Warehouses = new List<ComboBoxIndexContent>();

            int selectedProduct = Convert.ToInt32(cmbProducts.SelectedValue);

            using (DataContext data = new DataContext())
            {
                var collection = data.Magazijn.Join(data.ProductsMagazijn, p => p.MagazijnId, ps => ps.MagazijnId, (p, ps) => new { ProductId = ps.ProductId, MagazijnId = ps.MagazijnId, MagazijnAdress = p.Adress }).Where(p => p.ProductId == selectedProduct);

                foreach (var item in collection)
                {
                    Warehouses.Add(new ComboBoxIndexContent(item.ProductId, item.MagazijnAdress));
                }

            }
            cmbWarehouses.ItemsSource = Warehouses;
            cmbWarehouses.SelectedIndex = 0;
        }
        private void btnAddToList_Click(object sender, RoutedEventArgs e)
        {
            
            lbProductOrders.ItemsSource = null;

            int selectedProduct = Convert.ToInt32(cmbProducts.SelectedValue);
            int selectedSupplier = Convert.ToInt32(cmbSuppliers.SelectedValue);
            int selectedWarehouse = Convert.ToInt32(cmbWarehouses.SelectedValue);
            int amount = (int)iupdAantal.Value;

            using (DataContext data = new DataContext())
            {
                Product product = data.Product.Where(p => p.ProductId == selectedProduct).FirstOrDefault();
                Supplier suplier = data.Supplier.Where(p => p.SupplierId == selectedSupplier).FirstOrDefault();
                Magazijn warehouse = data.Magazijn.Where(p => p.MagazijnId == selectedWarehouse).FirstOrDefault();

                productsOrders.Add(new ProductsOrder((int)cmbProducts.SelectedValue, (int)cmbWarehouses.SelectedValue, (int)iupdAantal.Value, product.Price));

                ProductsOrders.Add(new ComboBoxIndexContent(productsOrders.Count - 1, $"Product: {product.Name} van {suplier.Name}  {amount}X voor {product.Price}€"));

            }

            lbProductOrders.ItemsSource = ProductsOrders;

        }

        private void btnCreateFullOrder_Click(object sender, RoutedEventArgs e)
        {
            int selectedCustomer = Convert.ToInt32(cmbCustomers.SelectedValue);
            MessageBox.Show(selectedCustomer.ToString()); ;
            using (DataContext data = new DataContext())
            {
                Order temp = new Order() { CustomerId = selectedCustomer, Betaald = false };
                data.Order.Add(temp);
                data.SaveChanges();

                //for (int i = 0; i < productsOrders.Count-1; i++)
                //{
                //    productsOrders[i].CustomerId = temp.CustomerId;
                //    productsOrders[i].OrderId = temp.OrderId;

                //}
                //data.ProductsOrder.AddRange(productsOrders.ToArray());
                //data.SaveChanges();
            }

        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            lbProductOrders.ItemsSource = null;
            productsOrders.Clear();
            ProductsOrders.Clear();
            lbProductOrders.ItemsSource = ProductsOrders;

        }       

        private void cmbSuppliers_DropDownClosed(object sender, EventArgs e)
        {
            UpdateProducts();
        }

       

        private void cmbProducts_DropDownClosed(object sender, EventArgs e)
        {
            UpdateWarehouses();
        }           
    }
}
