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

        public UpdateProduct()
        {
            InitializeComponent();
            FillProducts();
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
            FillProducts();           
        }

        private void FillProducts()
        {
            Products = new List<ComboBoxIndexContent>();
            cmbProduct.ItemsSource = null;
            using (DataContext data = new DataContext())
            {
                var collection = data.Product.Select(u => u);
                foreach (var item in collection)
                {
                    Products.Add(new ComboBoxIndexContent(item.ProductId, item.Name));
                }
            }
            cmbProduct.ItemsSource = Products;
            cmbProduct.SelectedIndex = 0;
        }

        private void cmbRole_DropDownClosed(object sender, EventArgs e)
        {
            Update();
        }        

        private void Update()
        {
            ComboBoxIndexContent selected = cmbProduct.SelectedItem as ComboBoxIndexContent;
            tbProduct.Text = selected.Content;
        }
    }
}
