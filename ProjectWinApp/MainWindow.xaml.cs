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
        public MainWindow(bool createData)
        {
            InitializeComponent();
            if (createData)
            {
                CreateDbInfo();
            }
        }


        private void LogIn(object sender, RoutedEventArgs e)
        {
            string encryptedPassword = md5.CreateMD5(pbPassword.Password);

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

                data.User.Add(new User() { UserRoleId = 1, FirstName = "Vincent", LastName = "Callaerts", Email = "VincentCallaerts@gmail.com", Password = md5.CreateMD5("42069") });
                data.User.Add(new User() { UserRoleId = 1, FirstName = "Kamiel", LastName = "K", Email = "KamielK@gmail.com", Password = md5.CreateMD5("42069") });

                data.User.Add(new User() { UserRoleId = 2, FirstName = "Quinten", LastName = "D", Email = "QuintenD@gmail.com", Password = md5.CreateMD5("42069") });
                data.User.Add(new User() { UserRoleId = 2, FirstName = "Ben", LastName = "Vhv", Email = "BenVhv@gmail.com", Password = md5.CreateMD5("42069") });

                data.User.Add(new User() { UserRoleId = 3, FirstName = "Michelle", LastName = "V", Email = "MichelleV@gmail.com", Password = md5.CreateMD5("42069") });

                data.Magazijn.Add(new Magazijn() { Adress = "Stuivenberg 120" });
                data.Magazijn.Add(new Magazijn() { Adress = "Nekkerspoel 43" });
                data.Magazijn.Add(new Magazijn() { Adress = "AstridLaan 120" });

                data.Customer.Add(new Customer() { Name = "Game mania", Email = "Gamemania@gmail.com" });
                data.Customer.Add(new Customer() { Name = "Delheize", Email = "Delheize@gmail.com" });
                data.Customer.Add(new Customer() { Name = "Inno", Email = "Inno@gmail.com" });
                data.Customer.Add(new Customer() { Name = "MediaMarkt", Email = "MediaMarkt@gmail.com" });
                
                data.Supplier.Add(new Supplier() { Name = "Nintendo", Email = "Nintendo@gmail.com" });
                data.Supplier.Add(new Supplier() { Name = "Heinze", Email = "Heinze@gmail.com" });
                data.Supplier.Add(new Supplier() { Name = "Amazon", Email = "Amazon@gmail.com" });
                data.Supplier.Add(new Supplier() { Name = "Sony", Email = "Sony@gmail.com" });

                data.Product.Add(new Product() { Name = "Gamecube", Price = 99.99 });
                data.Product.Add(new Product() { Name = "Switch", Price = 220.99 });
                data.Product.Add(new Product() { Name = "Mario", Price = 50 });

                data.Product.Add(new Product() { Name = "Mayonaise", Price = 2.5 });
                data.Product.Add(new Product() { Name = "Ketchup", Price = 1.5 });
                data.Product.Add(new Product() { Name = "Andaloes", Price = 0.99 });
                data.Product.Add(new Product() { Name = "Looksaus", Price = 3 });

                data.Product.Add(new Product() { Name = "Yogamat", Price = 32.99 });
                data.Product.Add(new Product() { Name = "Usb", Price = 10 });
                data.Product.Add(new Product() { Name = "Headset", Price = 60.99 });
                data.Product.Add(new Product() { Name = "Phonecase", Price = 5 });

                data.Product.Add(new Product() { Name = "Psp", Price = 199.99 });
                data.Product.Add(new Product() { Name = "Playstation 5", Price = 500 });
                data.Product.Add(new Product() { Name = "Camera", Price = 1999.50 });



                data.SaveChanges();

                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 1, SupplierId = 1 });
                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 2, SupplierId = 1 });
                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 3, SupplierId = 1 });
                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 4, SupplierId = 2 });
                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 5, SupplierId = 2 });
                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 6, SupplierId = 2 });
                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 7, SupplierId = 2 });
                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 8, SupplierId = 3 });
                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 9, SupplierId = 3 });
                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 10, SupplierId = 3 });
                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 11, SupplierId = 3 });
                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 12, SupplierId = 4 });
                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 13, SupplierId = 4 });
                data.ProductsSupplier.Add(new ProductsSupplier() { ProductId = 14, SupplierId = 4 });
                

                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 1, UserId = 1 });
                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 2, UserId = 1 });
                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 3, UserId = 1 });

                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 1, UserId = 2 });
                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 2, UserId = 2 });
                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 3, UserId = 2 });

                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 1, UserId = 3 });
                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 2, UserId = 3 });

                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 2, UserId = 4 });
                data.OwnersMagazijn.Add(new OwnersMagazijn() { MagazijnId = 3, UserId = 4 });


                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 1, MagazijnId = 1, Amount = 10 , LastAdded = DateTime.Now});
                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 2, MagazijnId = 1, Amount = 20, LastAdded = DateTime.Now });
                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 3, MagazijnId = 1, Amount = 30, LastAdded = DateTime.Now });
                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 4, MagazijnId = 2, Amount = 40, LastAdded = DateTime.Now });
                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 5, MagazijnId = 2, Amount = 50, LastAdded = DateTime.Now });
                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 6, MagazijnId = 2, Amount = 60, LastAdded = DateTime.Now });
                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 7, MagazijnId = 2, Amount = 70, LastAdded = DateTime.Now });
                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 8, MagazijnId = 2, Amount = 80, LastAdded = DateTime.Now });
                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 9, MagazijnId = 3, Amount = 90, LastAdded = DateTime.Now });
                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 10, MagazijnId = 3, Amount = 24, LastAdded = DateTime.Now });
                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 11, MagazijnId = 3, Amount = 102, LastAdded = DateTime.Now });
                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 12, MagazijnId = 1, Amount = 99, LastAdded = DateTime.Now });
                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 13, MagazijnId = 1, Amount = 200, LastAdded = DateTime.Now });
                data.ProductsMagazijn.Add(new ProductsMagazijn() { ProductId = 14, MagazijnId = 3, Amount = 96, LastAdded = DateTime.Now });


                data.SaveChanges();
            }
        }

        private void LogInQuick(object sender, RoutedEventArgs e)
        {
            using (DataContext data = new DataContext())
            {
                LoginAtempt = data.User.Where(u => u.UserRoleId == 1).FirstOrDefault();
                LoggedInOptions loginSucces = new LoggedInOptions(LoginAtempt);
                loginSucces.Show();
                this.Close();
            }
        }

        private void LogInQuickV(object sender, RoutedEventArgs e)
        {
            using (DataContext data = new DataContext())
            {
                LoginAtempt = data.User.Where(u => u.UserRoleId == 3).FirstOrDefault();
                LoggedInOptions loginSucces = new LoggedInOptions(LoginAtempt);
                loginSucces.Show();
                this.Close();
            }
        }

        private void LogInQuickM(object sender, RoutedEventArgs e)
        {
            using (DataContext data = new DataContext())
            {
                LoginAtempt = data.User.Where(u => u.UserRoleId == 2).FirstOrDefault();
                LoggedInOptions loginSucces = new LoggedInOptions(LoginAtempt);
                loginSucces.Show();
                this.Close();
            }
        }
    }
}
