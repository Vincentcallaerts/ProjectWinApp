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
    /// Interaction logic for UpdateCustomer.xaml
    /// </summary>
    public partial class UpdateCustomer : Page
    {
        public List<ComboBoxIndexContent> Customers { get; set; }
        public UpdateCustomer()
        {
            InitializeComponent();
            FillCustomers();
        }

        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            int selectedCustomer = Convert.ToInt32(cmbCustomers.SelectedValue);
            using (DataContext data = new DataContext())
            {
                data.Customer.Where(c => c.CustomerId == selectedCustomer).FirstOrDefault().Name = tbName.Text;
                data.Customer.Where(c => c.CustomerId == selectedCustomer).FirstOrDefault().Email = tbEmail.Text;
                data.SaveChanges();

                cmbCustomers.ItemsSource = null;
                Customers.Clear();
                var collectionCustomers = data.Customer.Select(m => m);
                foreach (var item in collectionCustomers)
                {
                    Customers.Add(new ComboBoxIndexContent(item.CustomerId, item.ToString()));
                }
                cmbCustomers.ItemsSource = Customers;
                cmbCustomers.SelectedIndex = 0;
                Update();
            }

        }

        private void FillCustomers()
        {

            Customers = new List<ComboBoxIndexContent>();

            using (DataContext data = new DataContext())
            {
                var collectionCustomers = data.Customer.Select(m => m);
                foreach (var item in collectionCustomers)
                {
                    Customers.Add(new ComboBoxIndexContent(item.CustomerId, item.ToString()));
                }            
            }
            cmbCustomers.ItemsSource = Customers;
            cmbCustomers.SelectedIndex = 0;
            Update();
            
        }
        private void cmbCustomers_DropDownClosed(object sender, EventArgs e)
        {
            Update();
        }       

        private void Update()
        {
            int selectedCustomer = Convert.ToInt32(cmbCustomers.SelectedValue);
            using (DataContext data = new DataContext())
            {
                tbName.Text = data.Customer.Where(c => c.CustomerId == selectedCustomer).FirstOrDefault().Name;
                tbEmail.Text = data.Customer.Where(c => c.CustomerId == selectedCustomer).FirstOrDefault().Email;

            }
        }       
    }
}
