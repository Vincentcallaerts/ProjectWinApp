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
        public User User { get; set; }
        public RemoveProductMagazijn(User user)
        {
            User = user;
            
            InitializeComponent();
            FillRoles();
        }

        private void btnRemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            int selectedMagazijn = Convert.ToInt32(cmbMagazijns.SelectedValue);
            int selectedProduct = Convert.ToInt32(lbProducten.SelectedValue);
            using (DataContext data = new DataContext())
            {

                data.ProductsMagazijn.RemoveRange(data.ProductsMagazijn.Where(pm => pm.ProductId == selectedProduct && pm.MagazijnId == selectedMagazijn));
                data.SaveChanges();

            }
            Update();
        }
        private void FillRoles()
        {
            Magazijns = new List<ComboBoxIndexContent>();
            Products = new List<ComboBoxIndexContent>();

            using (DataContext data = new DataContext())
            {
                var collectionMagazijns = data.Magazijn.Join(data.OwnersMagazijn, m => m.MagazijnId, om => om.MagazijnId, (m, om) => new { MagazijnId = m.MagazijnId, Userid = om.UserId, Adress = m.Adress }).Where(m => m.Userid == User.UserId);
                foreach (var item in collectionMagazijns)
                {
                    Magazijns.Add(new ComboBoxIndexContent(item.MagazijnId, item.Adress));
                }                
            }
            cmbMagazijns.ItemsSource = Magazijns;
            cmbMagazijns.SelectedIndex = 0;                       
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
                var collection = data.ProductsMagazijn.Join(data.Product, pm => pm.ProductId, p => p.ProductId, (pm, p) => new { MagazijnId = pm.MagazijnId, ProductId = pm.ProductId, Name = p.Name }).Where(pm => pm.MagazijnId == selectedValue);
                foreach (var item in collection)
                {
                    Products.Add(new ComboBoxIndexContent(item.ProductId,item.Name));
                }
               
            }
            lbProducten.ItemsSource = Products;
            lbProducten.SelectedIndex = 0;
        }
    }
}
