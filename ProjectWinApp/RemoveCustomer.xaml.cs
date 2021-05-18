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
    /// Interaction logic for RemoveCustomer.xaml
    /// </summary>
    public partial class RemoveCustomer : Page
    {
        public List<ComboBoxIndexContent> Customers { get; set; }
        public RemoveCustomer()
        {
            Customers = new List<ComboBoxIndexContent>();
            InitializeComponent();
            FillRoles();

        }

        private void btnRemoveCustomer_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = Convert.ToInt32(lbCustomers.SelectedValue);
            using (DataContext data = new DataContext())
            {
                data.Customer.RemoveRange(data.Customer.Where(c => c.CustomerId == selectedValue));
                data.SaveChanges();
            }
            FillRoles();
        }
        private void FillRoles()
        {
            lbCustomers.ItemsSource = null;
            Customers.Clear();
            using (DataContext data = new DataContext())
            {
                var collection = data.Customer.Select(u => u);
                foreach (var item in collection)
                {
                    Customers.Add(new ComboBoxIndexContent(item.CustomerId, item.Name));

                }
            }
            lbCustomers.ItemsSource = Customers;
        }
    }
}
