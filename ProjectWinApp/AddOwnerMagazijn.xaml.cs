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
    /// Interaction logic for AddOwnerMagazijn.xaml
    /// </summary>
    public partial class AddOwnerMagazijn : Page
    {
        public List<ComboBoxIndexContent> Users { get; set; }
        public List<ComboBoxIndexContent> Roles { get; set; }

        public AddOwnerMagazijn()
        {
            

            InitializeComponent();
            FillRoles();
        }

        private void btnAddOwner_Click(object sender, RoutedEventArgs e)
        {

        }
        private void FillRoles()
        {
            Roles = new List<ComboBoxIndexContent>();
            Users = new List<ComboBoxIndexContent>();

            using (DataContext data = new DataContext())
            {
                var collection = data.User.Where(u => u.UserRoleId == 1 || u.UserRoleId == 2);
                foreach (var item in collection)
                {
                    Users.Add(new ComboBoxIndexContent(item.UserId, item.FullName())); 
                }
            }
        }
    }
}
