﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            CreateDbInfo();
        }

        private void LogIn(object sender, RoutedEventArgs e)
        {
            using (DataContext data = new DataContext())
            {
                LoginAtempt = data.User.Where(u => u.Email == tbEmail.Text && u.Password == tbPassword.Text).FirstOrDefault();

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

                data.User.Add(new User() { UserRoleId = 1, FirstName = "Vincent", LastName = "Callaerts", Email = "Enail@gmail.com", Password = "42069" });
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
