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
    /// Interaction logic for SeeMagazijns.xaml
    /// </summary>
    public partial class SeeMagazijns : Page
    {
        public List<ComboBoxIndexContent> WareHouses { get; set; }
        public User LoggedIn { get; set; }

        public SeeMagazijns(User user)
        {
            LoggedIn = user;

            InitializeComponent();
            FillWarehouses();
        }

        private void FillWarehouses()
        {
            WareHouses = new List<ComboBoxIndexContent>();

            using(DataContext data = new DataContext())
            {
                var collection = data.Magazijn.Join(data.OwnersMagazijn, m => m.MagazijnId, om => om.MagazijnId, (m,om) => new {MagazijnId = m.MagazijnId, Userid = om.UserId,Adress = m.Adress }).Where(m => m.Userid == LoggedIn.UserId);
                foreach (var item in collection)
                {
                    WareHouses.Add(new ComboBoxIndexContent(item.MagazijnId, item.Adress));
                }
            }
            cmbMagazijns.ItemsSource = WareHouses;
            cmbMagazijns.SelectedIndex = 0;
            Update();
        }
        private void cmbCustomers_DropDownClosed(object sender, EventArgs e)
        {
            Update();
        }

        private void Update()
        {
            int selectedWarehouse = Convert.ToInt32(cmbMagazijns.SelectedValue);

            lbManagers.Items.Clear();
            lbProducts.Items.Clear();

            using (DataContext data = new DataContext())
            {
                var collectionWarehouses = data.OwnersMagazijn.Join(data.User, om => om.UserId, u => u.UserId, (om, u) => new { MagazijnId = om.MagazijnId, Name = u.FirstName + " " + u.LastName +" " + u.Email}).Where(pm => pm.MagazijnId == selectedWarehouse);
                foreach (var item in collectionWarehouses)
                {
                    lbManagers.Items.Add(item.Name);
                }
                var collectionProducts = data.ProductsMagazijn.Join(data.Product, pm => pm.ProductId, p => p.ProductId, (pm, p) => new { MagazijnId = pm.MagazijnId, Name = p.Name +", "+ p.Price + "€ | Amount in stock = " + pm.Amount + "Laatst stockwijziging: " + pm.LastAdded }).Where(pm => pm.MagazijnId == selectedWarehouse);
                foreach (var item in collectionProducts)
                {
                    lbProducts.Items.Add(item.Name);
                }
            }
        }       
    }
}
