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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //CreateDbInfo();
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            using (DataContext data = new DataContext())
            {
                User loginAtempt = data.User.Where(u => u.Email == tbEmail.Text && u.Password == tbPassword.Text).FirstOrDefault();

                if (loginAtempt != null)
                {
                    MessageBox.Show($"Logged In als {loginAtempt.FirstName} {loginAtempt.LastName}");
                }
                else
                {
                    MessageBox.Show("De opgegeven gegevens kwamen niet overeen met onze database.");
                }

            }
        }
        public void CreateDbInfo()
        {
            using (DataContext data = new DataContext())
            {
                data.UserRole.Add(new UserRole() { Description = "Administrator" });
                data.UserRole.Add(new UserRole() { Description = "Magazijnier" });
                data.UserRole.Add(new UserRole() { Description = "Verkoper" });

                data.User.Add(new User() { RoleId = 1, FirstName = "Vincent", LastName = "Callaerts", Email = "Enail@gmail.com", Password = "42069" });
                data.SaveChanges();
            }
        }
    }
    
}
