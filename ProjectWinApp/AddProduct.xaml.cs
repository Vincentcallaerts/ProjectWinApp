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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Page
    {
        public List<ComboBoxIndexContent> Suppliers { get; set; }

        public AddProduct()
        {
            InitializeComponent();
            FillRoles();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = Convert.ToInt32(cmbSuppliers.SelectedValue);


            if (dupdPrijs.Value.HasValue && tbName.Text != null)
            {

                double price = (double)dupdPrijs.Value;
                if (price != 0)
                {
                    using (DataContext data = new DataContext())
                    {
                        Product insertedProduct = new Product() { Name = tbName.Text, Price = price };
                        data.Product.Add(insertedProduct);
                        data.SaveChanges();

                        data.ProductsSupplier.Add(new ProductsSupplier() { SupplierId = selectedValue, ProductId = insertedProduct.ProductId });
                        data.SaveChanges();
                    }
                    dupdPrijs.Value = null;
                    tbName.Text = string.Empty;
                }
                else
                {
                    MessageBox.Show("De prijs mag niet 0 zijn");
                }             
            }
            else
            {
                MessageBox.Show("Een van de velden is leeg of heeft een foute waarde");
            }          
        }
        private void FillRoles()
        {
            Suppliers = new List<ComboBoxIndexContent>();
            
            using (DataContext data = new DataContext())
            {
                var collectionMagazijns = data.Supplier.Select(m => m);
                foreach (var item in collectionMagazijns)
                {
                    Suppliers.Add(new ComboBoxIndexContent(item.SupplierId, item.Name));
                }           
            }
            cmbSuppliers.ItemsSource = Suppliers;
            cmbSuppliers.SelectedIndex = 0;
            
        }
    }
}
