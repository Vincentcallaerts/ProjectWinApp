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
            EnableCorrectButtons();

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
            adminDataBeheer.Content = new AddOwnerMagazijn(User);
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new AddProduct();
        }
       

        private void btnRemoveProductsMagazijn_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new RemoveProductMagazijn(User);
        }

        private void btnRemoveManagerMagazijn_Click(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new RemoveManagerMagazijn(User);
        }
        private void btnUpdateUserClick(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new UpdateUser(User);
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

        private void btnUpdateProductPriceClick(object sender, RoutedEventArgs e)
        {
            adminDataBeheer.Content = new UpdateProductPrice();

           
        }

        private void btnUpdate(object sender, RoutedEventArgs e)
        {
            spAdd.Visibility = Visibility.Collapsed;
            spRemove.Visibility = Visibility.Collapsed;
            spUpdate.Visibility = Visibility.Visible;

            btnAddContent.IsEnabled = true;
            btnRemoveContent.IsEnabled = true;
            btnUpdateContent.IsEnabled = false;
        }     

        private void btnRemove(object sender, RoutedEventArgs e)
        {
            spAdd.Visibility = Visibility.Collapsed;
            spRemove.Visibility = Visibility.Visible;
            spUpdate.Visibility = Visibility.Collapsed;

            btnAddContent.IsEnabled = true;
            btnRemoveContent.IsEnabled = false;
            btnUpdateContent.IsEnabled = true;
        }

        private void btnAdd(object sender, RoutedEventArgs e)
        {

            spAdd.Visibility = Visibility.Visible;
            spRemove.Visibility = Visibility.Collapsed;
            spUpdate.Visibility = Visibility.Collapsed;

            btnAddContent.IsEnabled = false;
            btnRemoveContent.IsEnabled = true;
            btnUpdateContent.IsEnabled = true;
           
        }
        private void EnableCorrectButtons()
        {
            switch (User.UserRoleId)
            {
                case 2:

                    btnRemoveUser.Visibility = Visibility.Collapsed;
                    btnRemoveCustomer.Visibility = Visibility.Collapsed;
                    btnUpdateUser.Visibility = Visibility.Collapsed;
                    btnRemoveMagazijn.Visibility = Visibility.Collapsed;
                    btnAddCustomer.Visibility = Visibility.Collapsed;
                    btnAddMagazijn.Visibility = Visibility.Collapsed;
                    btnAddUser.Visibility = Visibility.Collapsed;
                    btnUpdateCustomer.Visibility = Visibility.Collapsed;

                    break;
                case 3:

                    btnRemoveUser.Visibility = Visibility.Collapsed;
                    btnUpdateUser.Visibility = Visibility.Collapsed;
                    btnRemoveMagazijn.Visibility = Visibility.Collapsed;
                    btnAddCustomer.Visibility = Visibility.Collapsed;
                    btnAddMagazijn.Visibility = Visibility.Collapsed;
                    btnAddUser.Visibility = Visibility.Collapsed;
                    btnAddManagerMagazijn.Visibility = Visibility.Collapsed;
                    btnRemoveManagerMagazijn.Visibility = Visibility.Collapsed;
                    btnRemoveProductMagazijn.Visibility = Visibility.Collapsed;
                    btnTransferProductMagazijn.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
