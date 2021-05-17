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
    /// Interaction logic for AddMagazijn.xaml
    /// </summary>
    public partial class AddMagazijn : Page
    {
        public List<ComboBoxIndexContent> Magaziniers { get; set; }

        public AddMagazijn()
        {
            Magaziniers = new List<ComboBoxIndexContent>();
            
            FillRoles();
            InitializeComponent();

            cmbEigenaar.ItemsSource = Magaziniers;
            cmbEigenaar.SelectedIndex = 0;
        }

        private void btnAddMagazijn_Click(object sender, RoutedEventArgs e)
        {
            using (DataContext data = new DataContext())
            {
                data.Magazijn.Add(new Magazijn() { UserId = Convert.ToInt32(cmbEigenaar.SelectedValue), Adress = tbAdress.Text });
                data.SaveChanges();
            }
        }
        private void FillRoles()
        {
            using (DataContext data = new DataContext())
            {
                var collection = data.User.Where(u => u.UserRoleId == 2);
                foreach (var item in collection)
                {
                    Magaziniers.Add(new ComboBoxIndexContent(item.UserId,item.Email));
                }
            }
        }
    }
}
