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
        public User User { get; set; }
        public AddProductMagazijn(User user)
        {
            User = user;

            InitializeComponent();
            FillRoles();
            Update();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            int selectedMagazijn = Convert.ToInt32(cmbMagazijns.SelectedValue);
            int selectedProduct = Convert.ToInt32(lbProducten.SelectedValue);
            int selectedAantal = Convert.ToInt32(iupdAantal.Value);

            using (DataContext data = new DataContext())
            {
                //add maybe nog messageboxes
                var productExists = data.ProductsMagazijn.Where(pm => pm.ProductId == selectedProduct && pm.MagazijnId == selectedMagazijn).FirstOrDefault();
                Product product;

                if (productExists == null)
                {
                    //voledig nieuw product
                    product = data.Product.Where(p => p.ProductId == selectedProduct).FirstOrDefault();

                    data.ProductsMagazijn.Add(new ProductsMagazijn() { MagazijnId = selectedMagazijn, ProductId = selectedProduct, Amount = selectedAantal, LastAdded = DateTime.Now});
                }
                else
                {
                    //aantal verhogen
                    product = data.Product.Where(p => p.ProductId == selectedProduct).FirstOrDefault();
                    data.ProductsMagazijn.FirstOrDefault(pm => pm.MagazijnId == selectedMagazijn && pm.ProductId == selectedProduct).Amount += selectedAantal;
                    data.ProductsMagazijn.FirstOrDefault(pm => pm.MagazijnId == selectedMagazijn && pm.ProductId == selectedProduct).LastAdded = DateTime.Now;

                }

                if (!(selectedAantal <= 0))
                {
                    data.OrderMagazijn.Add(new OrderMagazijn() { MagazijnId = selectedMagazijn, ProductName = product.Name, Amount = selectedAantal, UnitPrice = product.Price, OrderDate = DateTime.Now });
                }

                data.SaveChanges();
            }
            cmbMagazijns.SelectedIndex = 0;
            lbProducten.SelectedIndex = 0;
            iupdAantal.Value = 0;
        }
        private void FillRoles()
        {
            //dit anders doen.
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
            iupdAantal.Value = 0;
        }

        private void cmbMagazijns_DropDownClosed(object sender, EventArgs e)
        {
            Update();
        }

        private void Update()
        {
            lbProducten.ItemsSource = null;
            Products = new List<ComboBoxIndexContent>();
            int selectedMagazijn = Convert.ToInt32(cmbMagazijns.SelectedValue);
            using (DataContext data = new DataContext())
            {
                var collectionPMagazijns = data.Product.Select(p =>p);
                foreach (var item in collectionPMagazijns)
                {
                    Products.Add(new ComboBoxIndexContent(item.ProductId, item.Name));
                }

            }
            lbProducten.ItemsSource = Products;
            lbProducten.SelectedIndex = 0;
        }
    }
}
