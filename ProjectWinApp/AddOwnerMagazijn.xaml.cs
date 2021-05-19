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
    /// Interaction logic for AddOwnerMagazijn.xaml
    /// </summary>
    public partial class AddOwnerMagazijn : Page
    {
        public List<ComboBoxIndexContent> Users { get; set; }
        public List<ComboBoxIndexContent> Magazijns { get; set; }

        public AddOwnerMagazijn()
        {
            
            InitializeComponent();
            FillRoles();
        }

        private void btnAddOwner_Click(object sender, RoutedEventArgs e)
        {
            int selectedMagazijn = Convert.ToInt32(cmbMagazijns.SelectedValue);
            int selectedUser = Convert.ToInt32(lbUsers.SelectedValue);

            using (DataContext data = new DataContext())
            {
                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = selectedMagazijn, UserId = selectedUser });
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
                var collection = data.Magazijn.Select(m => m);
                foreach (var item in collection)
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
            lbUsers.ItemsSource = null;

            Users.Clear();

            List<ComboBoxIndexContent> temp = new List<ComboBoxIndexContent>();

            int selectedValue = Convert.ToInt32(cmbMagazijns.SelectedValue);

            using (DataContext data = new DataContext())
            {
                var collection = data.User.Where(u => u.UserRoleId == 1 || u.UserRoleId == 2);

                foreach (var item in collection)
                {
                    temp.Add(new ComboBoxIndexContent(item.UserId, item.ToString()));
                }

                //remove users die daar al owner van zijn 
                List<OwnersMagazijn> owners = data.OwnersMagazijn.Where(o => o.MagazijnId == selectedValue).ToList();
                
                foreach (var item in temp)
                {
                    if (!IsNotOwner(owners, item))
                    {                     
                        Users.Add(item);
                    }
                                  
                }
            }
            lbUsers.ItemsSource = Users;
            lbUsers.SelectedIndex = 0;
        }
        private bool IsNotOwner(List<OwnersMagazijn> owners, ComboBoxIndexContent item)
        {
            int count = 0;
            for (int i = 0; i < owners.Count(); i++)
            {
                if (owners[i].UserId == item.Id)
                {
                    count++;
                }
            }
            if (count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
