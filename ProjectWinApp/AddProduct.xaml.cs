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
        public AddProduct()
        {
            InitializeComponent();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dupdPrijs.Value != null && tbName.Text != null)
            {
                using (DataContext data = new DataContext())
                {
                    data.Product.Add(new Product() { Name = tbName.Text, Price = (double)dupdPrijs.Value });
                    data.SaveChanges();
                }
                dupdPrijs.Value = null;
                tbName.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Een van de velden is leeg deze moeten allemaal ingevuld worden");
            }          
        }
    }
}
