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
    /// Interaction logic for UpdatePassword.xaml
    /// </summary>
    public partial class UpdatePassword : Page
    {
        public User User { get; set; }
        public Encryptor md5 = new Encryptor();
        public UpdatePassword(User user)
        {
            InitializeComponent();
            User = user;
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            if (tbPassword.Password == tbPasswordCheck.Password)
            {
                using (DataContext data = new DataContext())
                {
                    data.User.Where(u => u.UserId == User.UserId).FirstOrDefault().Password = md5.CreateMD5(tbPassword.Password);
                    data.SaveChanges();
                }
                MessageBox.Show("Wachtwoord is aangepast");
            }
            else
            {
                MessageBox.Show("De opgegeven wachtwoorden kwamen niet overeen");
            }
            
        }
    }
}
