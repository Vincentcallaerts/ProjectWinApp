using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for UpdateProductPrice.xaml
    /// </summary>
    public partial class UpdateProductPrice : Page
    {
        public UpdateProductPrice()
        {
            InitializeComponent();
        }

        private void btnUploadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    if (reader.ReadLine() == "Fill in the following form and send it to us by mail.")
                    {
                        reader.ReadLine();
                        reader.ReadLine();

                        string supplier = TrimLine(reader.ReadLine());
                        string product = TrimLine(reader.ReadLine());
                        double newPrice = Convert.ToDouble(TrimLine(reader.ReadLine()));


                        using (DataContext data = new DataContext())
                        {
                            Supplier supplierProduct = data.Supplier.Where(s => s.Name.ToLower() == supplier).FirstOrDefault();
                            if (supplierProduct == null)
                            {
                                MessageBox.Show("Can`t find supplier name");
                            }
                            else
                            {
                                var collection = data.ProductsSupplier.Join(data.Product, ps => ps.ProductId, p => p.ProductId, (ps, p) => new { ProductId = ps.ProductId, SupplierId = ps.SupplierId, ProductName = p.Name }).Where(p => p.SupplierId == supplierProduct.SupplierId && p.ProductName.ToLower() == product).FirstOrDefault();
                                if (collection == null)
                                {
                                    MessageBox.Show("Can`t find product name");
                                }
                                else
                                {
                                    data.Product.Where(p => p.ProductId == collection.ProductId).FirstOrDefault().Price = newPrice;
                                    data.SaveChanges();
                                }                               
                            }
                        }
                    }                  
                }
            }
        }

        private void btnCreateEmptyFile_Click(object sender, RoutedEventArgs e)
        {
            string filename = "EmptyUpdatePrice.txt";
            string path = $"C:/Users/Public/Desktop/{filename}";
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("Fill in the following form and send it to us by mail.");
                writer.WriteLine("-----------------------------------------------------------");
                writer.WriteLine("");
                writer.WriteLine("Name Supplier:");
                writer.WriteLine("Name Product:");
                writer.WriteLine("New Price:");

            }
        }
        private string TrimLine(string line)
        {
            string temp = line.Substring(line.LastIndexOf(':') + 1).ToLower();
            return temp;
        }
    }
}
