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
        public DateTime Time { get; set; }
        public LoggedInOptions(User loggedIn)
        {
            InitializeComponent();
            UpdateTime();
            CompositionTarget.Rendering += CompositionTarget_Rendering;

            LoggedIn = loggedIn;

            using (DataContext data = new DataContext())
            {
                //maak hier join van voor snelheid
                var collection = data.Role.Where(r => r.UserId == LoggedIn.UserId).FirstOrDefault();
                var collection2 = data.UserRole.Where(r => r.UserRoleId == collection.UserRoleId).FirstOrDefault();

                tbInfo.Text = $"Logged in as {loggedIn.FirstName} {loggedIn.LastName} : {collection2.Description}";
            }
        }

        void CompositionTarget_Rendering(object sender, System.EventArgs e)
        {
            UpdateTime();
        }
        private void UpdateTime()
        {
            tbTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
        }
    }
}
