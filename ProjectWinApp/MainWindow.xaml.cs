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
        private User LoginAtempt { get; set; }
        private Encryptor md5 = new Encryptor();

        public MainWindow()
        {
            InitializeComponent();
            CreateDbInfo();
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            string encryptedPassword = md5.CreateMD5(tbPassword.Text);
            using (DataContext data = new DataContext())
            {
                LoginAtempt = data.User.Where(u => u.Email == tbEmail.Text && u.Password == encryptedPassword).FirstOrDefault();

                if (LoginAtempt != null)
                {
                    //MessageBox.Show($"Logged In als {LoginAtempt.FirstName} {LoginAtempt.LastName}");
                    LoggedInOptions loginSucces = new LoggedInOptions(LoginAtempt);
                    loginSucces.Show();
                    this.Close();
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
                data.UserRole.Add(new UserRole() { UserRoleId = 1, Description = "Administrator" });
                data.UserRole.Add(new UserRole() { UserRoleId = 2, Description = "Magazijnier" });
                data.UserRole.Add(new UserRole() { UserRoleId = 3, Description = "Verkoper" });

                data.User.Add(new User() { UserRoleId = 1, FirstName = "Vincent", LastName = "Callaerts", Email = "Enail@gmail.com", Password = md5.CreateMD5("42069") });

                data.User.Add(new User() { UserRoleId = 2, FirstName = "Vincent", LastName = "Callaerts", Email = "Vincent@gmail.com", Password = "42069" });
                data.User.Add(new User() { UserRoleId = 2, FirstName = "Wincent", LastName = "Callaerts", Email = "WWincent@gmail.com", Password = "42069" });
                data.User.Add(new User() { UserRoleId = 2, FirstName = "Vincent", LastName = "Callaerts", Email = "Vincent@gmail.com", Password = "42069" });
                data.User.Add(new User() { UserRoleId = 2, FirstName = "Wincent", LastName = "Callaerts", Email = "WWincent@gmail.com", Password = "42069" });
                data.User.Add(new User() { UserRoleId = 2, FirstName = "Vincent", LastName = "Callaerts", Email = "Vincent@gmail.com", Password = "42069" });
                data.User.Add(new User() { UserRoleId = 2, FirstName = "Wincent", LastName = "Callaerts", Email = "WWincent@gmail.com", Password = "42069" });
                data.User.Add(new User() { UserRoleId = 2, FirstName = "Vincent", LastName = "Callaerts", Email = "Vincent@gmail.com", Password = "42069" });
                data.User.Add(new User() { UserRoleId = 2, FirstName = "Wincent", LastName = "Callaerts", Email = "WWincent@gmail.com", Password = "42069" }); 
                data.User.Add(new User() { UserRoleId = 2, FirstName = "Vincent", LastName = "Callaerts", Email = "Vincent@gmail.com", Password = "42069" });
                data.User.Add(new User() { UserRoleId = 2, FirstName = "Wincent", LastName = "Callaerts", Email = "WWincent@gmail.com", Password = "42069" }); 
                data.User.Add(new User() { UserRoleId = 2, FirstName = "Vincent", LastName = "Callaerts", Email = "Vincent@gmail.com", Password = "42069" });
                data.User.Add(new User() { UserRoleId = 2, FirstName = "Wincent", LastName = "Callaerts", Email = "WWincent@gmail.com", Password = "42069" });

                data.Magazijn.Add(new Magazijn() { Adress = "Testla1" });
                data.Magazijn.Add(new Magazijn() { Adress = "Testla2" });
                data.Magazijn.Add(new Magazijn() { Adress = "Testla3" });

                data.Product.Add(new Product() { Name = "Test1", Price = 20 });
                
                data.SaveChanges();

                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 1, UserId = 1 });
                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 1, UserId = 2 });
                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 1, UserId = 3 });
                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 1, UserId = 4 });
                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 1, UserId = 5 });
                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 2, UserId = 1 });
                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 3, UserId = 1 });

                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 1, MagazijnId = 1, Amount = 10 });
                data.SaveChanges();
            }
        }

        private void LogInQuick(object sender, RoutedEventArgs e)
        {
            using (DataContext data = new DataContext())
            {
                LoginAtempt = data.User.Where(u => u.UserId == 1).FirstOrDefault();
                LoggedInOptions loginSucces = new LoggedInOptions(LoginAtempt);
                loginSucces.Show();
                this.Close();
            }
        }
    }
}
