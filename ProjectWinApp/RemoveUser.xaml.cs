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
    /// Interaction logic for RemoveUser.xaml
    /// </summary>
    public partial class RemoveUser : Page
    {
        public List<ComboBoxIndexContent> Roles { get; set; }
        public List<ComboBoxIndexContent> Users { get; set; }


        public RemoveUser()
        {
            
            FillRoles();
            InitializeComponent();                       

        }

        private void btnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = Convert.ToInt32(lbUsers.SelectedValue);
            using (DataContext data = new DataContext())
            {
                data.User.RemoveRange(data.User.Where(u => u.UserId == selectedValue));
                data.SaveChanges();
            }
            Update();
        }

        private void FillRoles()
        {
            Users = new List<ComboBoxIndexContent>();
            Roles = new List<ComboBoxIndexContent>();

            using (DataContext data = new DataContext())
            {
                var collection = data.UserRole.Select(u => u);
                foreach (var item in collection)
                {
                    Roles.Add(new ComboBoxIndexContent(item.UserRoleId, item.Description));
                }
            }

            cmbRole.ItemsSource = Roles;
            cmbRole.SelectedIndex = 0;
        }

        private void cmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void cmbRole_DropDownClosed(object sender, EventArgs e)
        {
            Update();
        }
        private void Update()
        {
            lbUsers.ItemsSource = null;
            Users.Clear();

            int selectedValue = Convert.ToInt32(cmbRole.SelectedValue);

            using (DataContext data = new DataContext())
            {
                var collection = data.User.Where(u => u.UserRoleId == selectedValue);

                foreach (var item in collection)
                {
                    Users.Add(new ComboBoxIndexContent(item.UserId, item.ToString()));
                }
            }
            lbUsers.ItemsSource = Users;
        }
    }
}
