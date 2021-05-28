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
    /// Interaction logic for UpdateUserSelf.xaml
    /// </summary>
    public partial class UpdateUserSelf : Page
    {
        public User User { get; set; }
        public UpdateUserSelf(User user)
        {
            InitializeComponent();
            User = user;
            tbFirst.Text = User.FirstName;
            tbLast.Text = User.LastName;
            tbEmail.Text = User.Email;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (tbFirst.Text != null && tbLast.Text != null && tbEmail.Text != null)
            {
                using (DataContext data = new DataContext())
                {
                    data.User.Where(u => u.UserId == User.UserId).FirstOrDefault().FirstName = tbFirst.Text;
                    data.User.Where(u => u.UserId == User.UserId).FirstOrDefault().LastName = tbLast.Text;
                    data.User.Where(u => u.UserId == User.UserId).FirstOrDefault().Email = tbEmail.Text;
                    data.SaveChanges();

                }
                User.FirstName = tbFirst.Text;
                User.LastName = tbLast.Text;
                User.Email = tbEmail.Text;
            }
            else
            {
                MessageBox.Show("Een van de velden is leeg deze moeten allemaal ingevuld worden");
            }                      
        }
    }
}
