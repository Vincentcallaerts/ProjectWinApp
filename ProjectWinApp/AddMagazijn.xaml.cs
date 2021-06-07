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
            InitializeComponent();
            FillRoles();           
        }

        private void btnAddMagazijn_Click(object sender, RoutedEventArgs e)
        {
            if (tbAdress.Text == string.Empty)
            {
                MessageBox.Show("Het textveld is leeg deze moet ingevuld worden");
            }
            else
            {
                int selectedValue = Convert.ToInt32(cmbEigenaar.SelectedValue);
                using (DataContext data = new DataContext())
                {
                    //check of adress al in database staat
                    var adressAlIngevoert = data.Magazijn.Where(m => m.Adress == tbAdress.Text).FirstOrDefault();
                    if (adressAlIngevoert == null)
                    {
                        //add magazijn en save
                        data.Magazijn.Add(new Magazijn() { Adress = tbAdress.Text });
                        data.SaveChanges();
                        //haal magazijn op en add de eerste eigenaar
                        var collection = data.Magazijn.Where(m => m.Adress == tbAdress.Text).FirstOrDefault();
                        data.OwnersMagazijn.Add(new OwnersMagazijn() { UserId = selectedValue, MagazijnId = collection.MagazijnId });
                        data.SaveChanges();

                    }
                    else
                    {
                        MessageBox.Show("Dit adress staat al in onze database.");
                    }
                }
                tbAdress.Text = string.Empty;
            }       
        }
        private void FillRoles()
        {
            Magaziniers = new List<ComboBoxIndexContent>();
            using (DataContext data = new DataContext())
            {
                var collection = data.User.Where(u => u.UserRoleId == 2 || u.UserRoleId == 1);
                foreach (var item in collection)
                {
                    Magaziniers.Add(new ComboBoxIndexContent(item.UserId,item.Email));
                }
            }
            cmbEigenaar.ItemsSource = Magaziniers;
            cmbEigenaar.SelectedIndex = 0;
        }
    }
}
