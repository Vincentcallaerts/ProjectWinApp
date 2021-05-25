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
    /// Interaction logic for SeeCustomers.xaml
    /// </summary>
    public partial class SeeCustomers : Page
    {
        public List<ComboBoxIndexContent> Customers { get; set; }
        public SeeCustomers()
        {
            InitializeComponent();
            FillRoles();
        }
        private void FillRoles()
        {
            Customers = new List<ComboBoxIndexContent>();

            using (DataContext data = new DataContext())
            {
                var collection = data.Customer.Select(u => u);
                foreach (var item in collection)
                {
                    Customers.Add(new ComboBoxIndexContent(item.CustomerId, item.ToString()));
                }
            }
            lbCustomers.ItemsSource = Customers;
            
        }
    }
}
