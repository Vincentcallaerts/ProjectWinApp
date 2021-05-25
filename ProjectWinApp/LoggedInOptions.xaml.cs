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
            LoggedIn = loggedIn;

            InitializeComponent();
            SetInfo();
            UpdateTime();
            
            CompositionTarget.Rendering += CompositionTarget_Rendering;

        }

        void CompositionTarget_Rendering(object sender, System.EventArgs e)
        {
            fContent.Height = this.ActualHeight - wpButtons.ActualHeight - lbtitle.ActualHeight - sbInfo.ActualHeight;
            UpdateTime();
        }
        private void UpdateTime()
        {
            tbTime.Text = DateTime.Now.ToString("HH:mm:ss");
            SetInfo();

        }
        private void SetInfo()
        {

            using (DataContext data = new DataContext())
            {
                var checkChanges = data.User.Where(u => u.UserId == LoggedIn.UserId).FirstOrDefault();
                LoggedIn = checkChanges;
                var collection = data.UserRole.FirstOrDefault(u => u.UserRoleId == LoggedIn.UserRoleId);

                tbInfo.Text = $"Logged in as {LoggedIn.FirstName} {LoggedIn.LastName} : {collection.Description}";
                
            }
        }

        private void btnDataBeheer_Click(object sender, RoutedEventArgs e)
        {

            fContent.Content = new DataManagement(LoggedIn);
            btnDataBeheer.IsEnabled = false;
            btnBestellingen.IsEnabled = true;
            btnOverzicht.IsEnabled = true;
        }

        private void btnOverzicht_Click(object sender, RoutedEventArgs e)
        {
            fContent.Content = new Overvieuw();
            btnDataBeheer.IsEnabled = true;
            btnBestellingen.IsEnabled = true;
            btnOverzicht.IsEnabled = false;
        }

        private void btnBestellingen_Click(object sender, RoutedEventArgs e)
        {
            fContent.Content = new Orders();
            btnDataBeheer.IsEnabled = true;
            btnBestellingen.IsEnabled = false;
            btnOverzicht.IsEnabled = true;
        }
    }
}
