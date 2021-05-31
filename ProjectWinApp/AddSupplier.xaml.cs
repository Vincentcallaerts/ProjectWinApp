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
    /// Interaction logic for AddSupplier.xaml
    /// </summary>
    public partial class AddSupplier : Page
    {
        public AddSupplier()
        {
            InitializeComponent();
        }

        private void btnAddSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == string.Empty || tbEmail.Text == string.Empty)
            {
                MessageBox.Show("Een van de textvelden is leeg deze moeten allemaal ingevuld worden");
            }
            else
            {
                using (DataContext data = new DataContext())
                {
                    data.Supplier.Add(new Supplier() { Name = tbName.Text, Email = tbEmail.Text });
                    data.SaveChanges();
                }

                tbName.Text = string.Empty;
                tbEmail.Text = string.Empty;
            }
        }
    }
}
