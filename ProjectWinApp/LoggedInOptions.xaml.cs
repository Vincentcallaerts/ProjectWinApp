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
using System.Windows.Shapes;

namespace ProjectWinApp
{
    /// <summary>
    /// Interaction logic for LoggedInOptions.xaml
    /// </summary>
    public partial class LoggedInOptions : Window
    {
        private User LoggedIn { get; set; }
        public LoggedInOptions(User loggedIn)
        {
            InitializeComponent();

            LoggedIn = loggedIn;
            string role = String.Empty;

            using (DataContext data = new DataContext())
            {
                var collection = data.Role.Where(r => r.UserId == LoggedIn.UserId).FirstOrDefault();
                var collection2 = data.UserRole.Where(r => r.UserRoleId == collection.UserRoleId).FirstOrDefault();

                tbInfo.Text = $"Logged in as {loggedIn.FirstName} {loggedIn.LastName} : {collection2.Description}";
            }          
        }
    }
}
