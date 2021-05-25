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
    /// Interaction logic for AddProductMagazijn.xaml
    /// </summary>
    public partial class AddProductMagazijn : Page
    {
        public List<ComboBoxIndexContent> Products { get; set; }
        public List<ComboBoxIndexContent> Magazijns { get; set; }

        public AddProductMagazijn()
        {

            InitializeComponent();
            FillRoles();
            
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            int selectedMagazijn = Convert.ToInt32(cmbMagazijns.SelectedValue);
            int selectedProduct = Convert.ToInt32(lbProducten.SelectedValue);
            int selectedAantal = Convert.ToInt32(iupdAantal.Value);

            using (DataContext data = new DataContext())
            {
                //add maybe nog messagebox
                var productExists = data.ProductsMagazijn.Where(pm => pm.ProductId == selectedProduct && pm.MagazijnId == selectedMagazijn).FirstOrDefault();
                if (productExists == null)
                {
                    //voledig nieuw product
                    data.ProductsMagazijn.Add(new ProductsMagazijn() { MagazijnId = selectedMagazijn, ProductId = selectedProduct, Amount = selectedAantal });
                    
                }
                else
                {
                    //aantal verhogen
                    data.ProductsMagazijn.FirstOrDefault(pm => pm.MagazijnId == selectedMagazijn && pm.ProductId == selectedProduct).Amount += selectedAantal;
                }
                data.SaveChanges();
            }
            cmbMagazijns.SelectedIndex = 0;
            lbProducten.SelectedIndex = 0;
            iupdAantal.Value = 0;
        }
        private void FillRoles()
        {
            Magazijns = new List<ComboBoxIndexContent>();
            Products = new List<ComboBoxIndexContent>();

            using (DataContext data = new DataContext())
            {
                var collectionMagazijns = data.Magazijn.Select(m => m);
                foreach (var item in collectionMagazijns)
                {
                    Magazijns.Add(new ComboBoxIndexContent(item.MagazijnId, item.Adress));
                }
                
                var collectionProducts = data.Product.Select(p => p);
                foreach (var item in collectionProducts)
                {
                    Products.Add(new ComboBoxIndexContent(item.ProductId, item.Name));
                }
            }
            cmbMagazijns.ItemsSource = Magazijns;
            cmbMagazijns.SelectedIndex = 0;
            lbProducten.ItemsSource = Products;
            lbProducten.SelectedIndex = 0;
            iupdAantal.Value = 0;
        }       
    }
}
