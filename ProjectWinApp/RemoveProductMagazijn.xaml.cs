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
    /// Interaction logic for RemoveProductMagazijn.xaml
    /// </summary>
    public partial class RemoveProductMagazijn : Page
    {
        public List<ComboBoxIndexContent> Products { get; set; }
        public List<ComboBoxIndexContent> Magazijns { get; set; }

        public RemoveProductMagazijn()
        {
            InitializeComponent();
            FillRoles();
        }

        private void btnRemoveProduct_Click(object sender, RoutedEventArgs e)
        {

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
            
        }
        private void cmbRole_DropDownClosed(object sender, EventArgs e)
        {
            Update();
        }

        private void cmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
        private void Update()
        {
            lbProducten.ItemsSource = null;

            Products.Clear();

            List<ComboBoxIndexContent> temp = new List<ComboBoxIndexContent>();

            int selectedValue = Convert.ToInt32(cmbMagazijns.SelectedValue);

            using (DataContext data = new DataContext())
            {

                var collection = data.ProductsMagazijn.Join(data.Product, pm => pm.ProductId, p => p.ProductId,(pm,p) => new {} )
                foreach (var item in collection)
                {
                    Products.Add(new ComboBoxIndexContent(item.ProductId,item.Product.Name));
                }
               
            }
            lbProducten.ItemsSource = Products;
            lbProducten.SelectedIndex = 0;
        }
    }
}
