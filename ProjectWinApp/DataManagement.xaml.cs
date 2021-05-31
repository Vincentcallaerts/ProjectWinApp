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
        public User User { get; set; }
        public DataManagement(User user)
        {
            User = user;
            InitializeComponent();

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

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new AddProduct();
        }
       

        private void btnRemoveProductsMagazijn_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new RemoveProductMagazijn();
        }

        private void btnRemoveManagerMagazijn_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new RemoveManagerMagazijn();
        }
        private void btnUpdateUserClick(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new UpdateUser();
        }

        private void btnUpdateCustomerClick(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new UpdateCustomer();
        }

        private void btnUpdateProductClick(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new UpdateProduct();
        }

        private void btnUpdateUserSelfClick(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new UpdateUserSelf(User);
        }

        private void btnUpdateUserSelfPasswordClick(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new UpdatePassword(User);
        }

        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new AddSupplier();
        }

        private void btnTransferProductMagazijnClick(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new TransferProductMagazijn();
        }
    }
}
