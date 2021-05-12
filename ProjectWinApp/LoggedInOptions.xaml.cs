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
                var collection = data.UserRole.FirstOrDefault(u => u.UserRoleId == LoggedIn.UserRoleId);

                tbInfo.Text = $"Logged in as {LoggedIn.FirstName} {LoggedIn.LastName} : {collection.Description}";
            }
            fContent.Content = new DataManagement();

        }

        void CompositionTarget_Rendering(object sender, System.EventArgs e)
        {
            fContent.Height = this.ActualHeight - wpButtons.ActualHeight - lbtitle.ActualHeight - sbInfo.ActualHeight;
            UpdateTime();
        }
        private void UpdateTime()
        {
            tbTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }
    }
}
