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
    /// Interaction logic for RemoveManagerMagazijn.xaml
    /// </summary>
    public partial class RemoveManagerMagazijn : Page
    {
        public List<ComboBoxIndexContent> Users { get; set; }
        public List<ComboBoxIndexContent> Magazijns { get; set; }

        public RemoveManagerMagazijn()
        {
            InitializeComponent();
            FillRoles();
        }

        private void btnRemoveManager_Click(object sender, RoutedEventArgs e)
        {
            int selectedMagazijn = Convert.ToInt32(cmbMagazijns.SelectedValue);
            int selectedmanager = Convert.ToInt32(lbManagers.SelectedValue);

            using (DataContext data = new DataContext())
            {
                data.OwnersMagazijn.RemoveRange(data.OwnersMagazijn.Where(om => om.MagazijnId == selectedMagazijn && om.UserId == selectedmanager));
                data.SaveChanges();
            }
            Update();
        }
        private void FillRoles()
        {
            Magazijns = new List<ComboBoxIndexContent>();
            Users = new List<ComboBoxIndexContent>();

            using (DataContext data = new DataContext())
            {
                var collectionMagazijns = data.Magazijn.Select(m => m);
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
            lbManagers.ItemsSource = null;
            Users.Clear();

            int selectedValue = Convert.ToInt32(cmbMagazijns.SelectedValue);

            using (DataContext data = new DataContext())
            {
                //ga verder hier
                var collection = data.OwnersMagazijn.Join(data.User, om => om.UserId, u => u.UserId, (om, u) => new { MagazijnId = om.MagazijnId, UserId = om.UserId, Name = u.FirstName + " " + u.LastName }).Where(pm => pm.MagazijnId == selectedValue);
                foreach (var item in collection)
                {
                    Users.Add(new ComboBoxIndexContent(item.UserId, item.Name));
                }

            }
            lbManagers.ItemsSource = Users;
            lbManagers.SelectedIndex = 0;
        }
    }
}
