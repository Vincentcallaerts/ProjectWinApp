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
    /// Interaction logic for AdminAddUser.xaml
    /// </summary>
    public partial class AdminAddUser : Page
    {
        public List<ComboBoxIndexContent> Roles { get; set; }
        private Encryptor md5 = new Encryptor();

        public AdminAddUser()
        {
            InitializeComponent();
            Roles = new List<ComboBoxIndexContent>();
            FillRoles();
            cmbRole.ItemsSource = Roles;
            cmbRole.SelectedIndex = 0;
            
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            if (!(tbfirst.Text != null && tblast.Text != null && tbpassword.Text != null && tbemail.Text != null))
            {
                MessageBox.Show("Een van de textvelden is leeg deze moeten allemaal ingevuld worden");
            }
            else
            {
                string encryptedPassword = md5.CreateMD5(tbpassword.Text);
                using (DataContext data = new DataContext())
                {                   
                    //vergeet encritption niet te added later
                    data.User.Add(new User() { UserRoleId = Convert.ToInt32(cmbRole.SelectedValue), FirstName = tbfirst.Text, LastName = tblast.Text, Email = tbemail.Text, Password = encryptedPassword});
                    data.SaveChanges();
                }

                tbemail.Text = string.Empty;
                tbfirst.Text = string.Empty;
                tblast.Text = string.Empty;
                tbpassword.Text = string.Empty;

                MessageBox.Show("De user is succesvol aangemaakt.");
            }
        }
        private void FillRoles()
        {
            using (DataContext data = new DataContext())
            {
                var collection = data.UserRole.Select(u => u);
                foreach (var item in collection)
                {
                    Roles.Add(new ComboBoxIndexContent(item.UserRoleId, item.Description));
                }
            }
        }
    }
}
