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
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Page
    {
        public AddCustomer()
        {
            InitializeComponent();
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (!(tbName.Text != null && tbEmail.Text != null))
            {
                MessageBox.Show("Een van de textvelden is leeg deze moeten allemaal ingevuld worden");
            }
            else
            {
                using (DataContext data = new DataContext())
                {
                    data.Customer.Add(new Customer() { Name = tbName.Text, Email = tbEmail.Text });
                    data.SaveChanges();
                }
            }
            
        }
    }
}
