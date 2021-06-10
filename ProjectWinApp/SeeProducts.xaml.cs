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
    /// Interaction logic for SeeProducts.xaml
    /// </summary>
    public partial class SeeProducts : Page
    {
        public List<ComboBoxIndexContent> Suppliers { get; set; }
        public List<ComboBoxIndexContent> Products { get; set; }

        public SeeProducts()
        {
            InitializeComponent();
            FillSuppliers();
        }

        private void FillSuppliers()
        {
            Suppliers = new List<ComboBoxIndexContent>();

            using (DataContext data = new DataContext())
            {
                var collection = data.Supplier.Select(s => s);
                foreach (var item in collection)
                {
                    Suppliers.Add(new ComboBoxIndexContent(item.SupplierId, item.Name));
                }
            }
            cmbSupplier.ItemsSource = Suppliers;
            cmbSupplier.SelectedIndex = 0;
            FillProducts();

        }
        private void FillProducts()
        {
            lbProducts.ItemsSource = null;
            Products = new List<ComboBoxIndexContent>();

            int selectedSupplier = Convert.ToInt32(cmbSupplier.SelectedValue);

            using (DataContext data = new DataContext())
            {
                var collection = data.Product.Join(data.ProductsSupplier, p => p.ProductId, pm => pm.ProductId, (p,pm) => new { Contents = p.Name + " " + p.Price + "€", ProductId = p.ProductId, SupplierId = pm.SupplierId }).Where(s => s.SupplierId == selectedSupplier);
                foreach (var item in collection)
                {

                    Products.Add(new ComboBoxIndexContent(item.SupplierId, item.Contents));
                }
            }
            lbProducts.ItemsSource = Products;
        }
        private void cmbRole_DropDownClosed(object sender, EventArgs e)
        {
            FillProducts();

        }

        
    }
}
