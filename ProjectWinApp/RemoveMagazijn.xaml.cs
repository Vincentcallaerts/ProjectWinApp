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
    /// Interaction logic for RemoveMagazijn.xaml
    /// </summary>
    public partial class RemoveMagazijn : Page
    {
        private List<ComboBoxIndexContent> Magazijns;

        public RemoveMagazijn()
        {
            InitializeComponent();
            FillRoles();
        }

        private void btnRemoveMagazin_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = Convert.ToInt32(cmbMagazijns.SelectedValue);
            using (DataContext data = new DataContext())
            {
                data.OwnersMagazijn.RemoveRange(data.OwnersMagazijn.Where(o => o.MagazijnId == selectedValue));
                data.Magazijn.RemoveRange(data.Magazijn.Where(m => m.MagazijnId == selectedValue));
                data.SaveChanges();
            }
            FillRoles();
        }
        private void FillRoles()
        {
            Magazijns = new List<ComboBoxIndexContent>();
            cmbMagazijns.ItemsSource = null;

            using (DataContext data = new DataContext())
            {
                var collection = data.Magazijn.Select(m => m);
                foreach (var item in collection)
                {
                    Magazijns.Add(new ComboBoxIndexContent(item.MagazijnId, item.Adress));
                }
            }
            cmbMagazijns.ItemsSource = Magazijns;
            cmbMagazijns.SelectedIndex = 0;
        }
    }
}
