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
    /// Interaction logic for DataManagement.xaml
    /// </summary>
    public partial class DataManagement : Page
    {

        public DataManagement()
        {
            InitializeComponent();

        }

        private void btnBlank_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = null;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new AdminAddUser();
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new AddCustomer();
        }

        private void btnAddMagazijn_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new AddMagazijn();
        }

        private void btnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new RemoveUser();
        }

        private void btnRemoveCustomer_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new RemoveCustomer();

        }

        private void btnRemoveMagazijn_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new RemoveMagazijn();
        }

        private void btnAddManagerMagazijn_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new AddOwnerMagazijn();
        }
    }
}
