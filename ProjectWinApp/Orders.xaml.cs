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
    /// Interaction logic for Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        public User User { get; set; }
        public Orders(User user)
        {
            User = user;
            InitializeComponent();
            EnableCorrectButtons();
        }

        private void btnAddProductMagazijn_Click(object sender, RoutedEventArgs e)
        {
            fOrders.Content = new AddProductMagazijn(User);
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            fOrders.Content = new AddOrder();
        }

        private void btnSeeIncommingOrder_Click(object sender, RoutedEventArgs e)
        {
            fOrders.Content = new SeeIncommingOrder();
        }

        private void btnSeeOutgoingOrder_Click(object sender, RoutedEventArgs e)
        {
            fOrders.Content = new SeeOutgoingOrders();
        }
        private void EnableCorrectButtons()
        {
            switch (User.UserRoleId)
            {
                case 2:

                    btnAddOrder.Visibility = Visibility.Collapsed;
                    btnSeeIncommingOrder.Visibility = Visibility.Collapsed;


                    break;
                case 3:
                    btnAddProductMagazijn.Visibility = Visibility.Collapsed;
                    btnSeeOutgoingOrder.Visibility = Visibility.Collapsed;

                    break;
            }
        }
    }
}
