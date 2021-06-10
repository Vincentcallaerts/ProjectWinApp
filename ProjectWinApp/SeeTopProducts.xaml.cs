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
    /// Interaction logic for SeeTopProducts.xaml
    /// </summary>
    public partial class SeeTopProducts : Page
    {
        public List<ComboBoxIndexContent> Products { get; set; }
        public SeeTopProducts()
        {
            InitializeComponent();
            FillProducts();
        }

        private void FillProducts()
        {
            lbProducts.ItemsSource = null;
            Products = new List<ComboBoxIndexContent>();

            using (DataContext data = new DataContext())
            {
                var collection = data.Product.Join(data.ProductsOrder, p => p.Name, pm => pm.CurrentProductName, (p, pm) => new { Contents = p.Name, ProductId = p.ProductId, Amount = pm.Amount, Price = p.Price }).GroupBy(p => p.ProductId);
                int amountProducts = 0;
                double productPrice = 0;
                string productName = string.Empty;

                List<ProductsOrder> temp = new List<ProductsOrder>();

                foreach (var item in collection)
                {
                    
                    foreach (var product in item)
                    {
                        productName = product.Contents;
                        productPrice = product.Price;
                        amountProducts += product.Amount;
                    }
                    temp.Add(new ProductsOrder() { Amount = amountProducts, CurrentProductName = productName, OrderUnitPrice = productPrice });
                    productPrice = 0;
                    productName = string.Empty;
                    amountProducts = 0;
                }
                temp = temp.OrderByDescending(t => t.Amount).Take(5).ToList();

                foreach (var item in temp)
                {                   

                     Products.Add(new ComboBoxIndexContent(0, $"{item.CurrentProductName} has been sold {item.Amount} times at {item.OrderUnitPrice}€"));
                    
                }
            }
            lbProducts.ItemsSource = Products;
        }
    }
}
