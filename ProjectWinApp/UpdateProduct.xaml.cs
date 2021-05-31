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
    /// Interaction logic for UpdateProduct.xaml
    /// </summary>
    public partial class UpdateProduct : Page
    {
        public List<ComboBoxIndexContent> Products { get; set; }
        public List<ComboBoxIndexContent> Suppliers { get; set; }


        public UpdateProduct()
        {
            InitializeComponent();
            FillLeveranciers();
            Update();
        }

        private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = Convert.ToInt32(cmbProduct.SelectedValue);
            using (DataContext data = new DataContext())
            {
                data.Product.Where(p => p.ProductId == selectedValue).FirstOrDefault().Name = tbProduct.Text;
                data.SaveChanges();
            }
            UpdateProducts();       
        }

        private void FillLeveranciers()
        {
            Suppliers = new List<ComboBoxIndexContent>();
            using (DataContext data = new DataContext())
            {
                var collection = data.Supplier.Select(u => u);
                foreach (var item in collection)
                {
                    Suppliers.Add(new ComboBoxIndexContent(item.SupplierId, item.Name));
                }
            }
            cmbLeveranciers.ItemsSource = Suppliers;
            cmbLeveranciers.SelectedIndex = 0;
        }

        private void cmbRole_DropDownClosed(object sender, EventArgs e)
        {
            Update();
        }        

        private void Update()
        {
            ComboBoxIndexContent selected = cmbProduct.SelectedItem as ComboBoxIndexContent;
            if (selected != null)
            {
                tbProduct.Text = selected.Content;
            }
            else
            {
                tbProduct.Text = string.Empty;
            }
        }

        private void cmbLeveranciers_DropDownClosed(object sender, EventArgs e)
        {
            UpdateProducts();
        }

        private void UpdateProducts()
        {
            cmbProduct.ItemsSource = null;
            Products = new List<ComboBoxIndexContent>();

            int selectedSupplier = Convert.ToInt32(cmbLeveranciers.SelectedValue);

            using (DataContext data = new DataContext())
            {
                var collection = data.Product.Join(data.ProductsSupplier, p => p.ProductId, ps => ps.ProductId, (p, ps) => new {ProductId = p.ProductId, ProductName = p.Name , SupplierId = ps.SupplierId }).Where(s => s.SupplierId == selectedSupplier);
                foreach (var item in collection)
                {
                    Products.Add(new ComboBoxIndexContent(item.SupplierId, item.ProductName));
                }
            }
            cmbProduct.ItemsSource = Products;
            cmbProduct.SelectedIndex = 0;
            Update();
        }     
    }
}
