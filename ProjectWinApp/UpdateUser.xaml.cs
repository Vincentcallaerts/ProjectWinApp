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
        public User User { get; set; }

        public Encryptor md5 = new Encryptor();
        public UpdateUser(User user)
        {
            User = user;
            InitializeComponent();
            FillRoles();            
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {
            int selectedUser = Convert.ToInt32(cmbUsers.SelectedValue);
            int selectedRole = Convert.ToInt32(cmbRoleSelected);
            using (DataContext data = new DataContext())
            {
                data.User.Where(u => u.UserId == selectedUser).FirstOrDefault().FirstName = tbFirst.Text;
                data.User.Where(u => u.UserId == selectedUser).FirstOrDefault().LastName = tbLast.Text;
                data.User.Where(u => u.UserId == selectedUser).FirstOrDefault().Email = tbEmail.Text;
                data.User.Where(u => u.UserId == selectedUser).FirstOrDefault().UserRoleId = selectedRole;
                data.SaveChanges();
            }
            cmbRole.SelectedIndex = 0;
        }
        private void btpassword_Click(object sender, RoutedEventArgs e)
        {
            int selectedUser = Convert.ToInt32(cmbUsers.SelectedValue);
            using (DataContext data = new DataContext())
            {
                data.User.Where(u => u.UserId == selectedUser).FirstOrDefault().Password = md5.CreateMD5("12345!");
                data.SaveChanges();
            }
            MessageBox.Show("Passwoord is aangepast");
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
            //de 2de box met rolles maar aangezien deze altijd hetzelfde is doe ik dit met dezelfde data
            cmbRoleSelected.ItemsSource = Roles;
            cmbRoleSelected.SelectedIndex = 0;
            UpdateUsers();
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
                var collection = data.User.Where(u => u.UserRoleId == selectedValueUserRole && u.UserId != User.UserId);

                foreach (var item in collection)
                {
                    Users.Add(new ComboBoxIndexContent(item.UserId, item.ToString()));
                }
            }
            cmbUsers.ItemsSource = Users;
            cmbUsers.SelectedIndex = 0;
            Update();
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
            cmbRoleSelected.SelectedIndex = cmbRole.SelectedIndex;
            if (cmbUsers.SelectedItem != null)
            {
                int selectedUser = Convert.ToInt32(cmbUsers.SelectedValue);
                using (DataContext data = new DataContext())
                {
                    var collection = data.User.FirstOrDefault(u => u.UserId == selectedUser);

                    tbFirst.Text = collection.FirstName;
                    tbLast.Text = collection.LastName;
                    tbEmail.Text = collection.Email;

                }
            }
            else
            {
                tbFirst.Text = string.Empty;
                tbLast.Text = string.Empty;
                tbEmail.Text = string.Empty;
            }
            
        }        
    }
}
