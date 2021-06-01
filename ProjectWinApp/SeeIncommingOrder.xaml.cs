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
    /// Interaction logic for SeeIncommingOrder.xaml
    /// </summary>
    public partial class SeeIncommingOrder : Page
    {
        public List<ComboBoxIndexContent> Customers { get; set; }
        public List<ComboBoxIndexContent> Orders { get; set; }

        public SeeIncommingOrder()
        {
            InitializeComponent();
            FillCustomers();
        }

        private void FillCustomers()
        {
            throw new NotImplementedException();
        }

        private void cmbLeveranciers_DropDownClosed(object sender, EventArgs e)
        {
            Update();
        }
        private void Update()
        {

        }
    }
}
