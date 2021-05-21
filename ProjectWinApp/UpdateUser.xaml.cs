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
    /// Interaction logic for UpdateUser.xaml
    /// </summary>
    public partial class UpdateUser : Page
    {
        public List<ComboBoxIndexContent> Roles { get; set; }
        public List<ComboBoxIndexContent> Users { get; set; }

        public UpdateUser()
        {
            InitializeComponent();
            FillRoles();

            
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {

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
            cmbRoleSelected.ItemsSource = Roles;
            cmbRoleSelected.SelectedIndex = 0;
            cmbUsers.ItemsSource = null;
        }

        private void cmbRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void cmbRole_DropDownClosed(object sender, EventArgs e)
        {
            UpdateUsers();
        }
        private void UpdateUsers()
        {
            cmbUsers.ItemsSource = null;
            Users.Clear();

            
            int selectedValueUserRole = Convert.ToInt32(cmbRole.SelectedValue);


            using (DataContext data = new DataContext())
            {
                var collection = data.User.Where(u => u.UserRoleId == selectedValueUserRole);

                foreach (var item in collection)
                {
                    Users.Add(new ComboBoxIndexContent(item.UserId, item.ToString()));
                }
            }
            cmbUsers.ItemsSource = Users;
            cmbUsers.SelectedIndex = 0;
        }

        private void cmbUsers_DropDownClosed(object sender, EventArgs e)
        {
            Update();
        }

        private void cmbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }
        private void Update()
        {
            //Update de Role adhv de role die vanboven selected is want als ik filter op role kunnen de users toch geen andere role dan de geselecteerde hebben.
            int selectedUser = Convert.ToInt32(cmbRole.SelectedValue);
            using (DataContext data = new DataContext())
            {
                var collection = data.User.FirstOrDefault(u => u.UserId == selectedUser);
                
                tbfirst.Text = collection.FirstName;
                tblast.Text = collection.LastName;
                tbemail.Text = collection.Email;
                tbpassword.Text = collection.Password;

            }
        }
    }
}
