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
    /// Interaction logic for TransferProductMagazijn.xaml
    /// </summary>
    public partial class TransferProductMagazijn : Page
    {
        public List<ComboBoxIndexContent> Warehouses { get; set; }
        public List<ComboBoxIndexContent> WarehousesMinusSelected { get; set; }
        public List<ComboBoxIndexContent> Products { get; set; }


        public TransferProductMagazijn()
        {
            InitializeComponent();
            Fill();
        }
        private void btnTransfer_Click(object sender, RoutedEventArgs e)
        {

            int selectedMagazijn = Convert.ToInt32(cmbWarehouses.SelectedValue);
            int selectedProduct = Convert.ToInt32(cmbProducts.SelectedValue);
            int selectedAantal = Convert.ToInt32(iupdAantal.Text);
            int selectedMagazijnForTransfer = Convert.ToInt32(lbWarehouses.SelectedValue);

            if (selectedAantal != 0)
            {
                using (DataContext data = new DataContext())
                {
                    var productsMagazijn = data.ProductsMagazijn.Where(pm => pm.ProductId == selectedProduct && pm.MagazijnId == selectedMagazijn).FirstOrDefault();
                    if ((productsMagazijn.Amount - selectedAantal) >= 0)
                    {
                        data.ProductsMagazijn.Where(pm => pm.ProductId == selectedProduct && pm.MagazijnId == selectedMagazijn).FirstOrDefault().Amount -= selectedAantal;
                        data.ProductsMagazijn.Where(pm => pm.ProductId == selectedProduct && pm.MagazijnId == selectedMagazijnForTransfer).FirstOrDefault().Amount += selectedAantal;
                        data.SaveChanges();
                        UpdateProducts();
                    }
                    else
                    {
                        MessageBox.Show("Er zijn niet genoeg producten voor over te zetten");
                        UpdateProducts();

                    }

                }
            }
            else
            {
                MessageBox.Show("De input mag niet 0 zijn");
            }
            
        }

        private void Fill()
        {
            Warehouses = new List<ComboBoxIndexContent>();
            using (DataContext data = new DataContext())
            {
                var collection = data.Magazijn.Select(m => m);
                foreach (var item in collection)
                {
                    Warehouses.Add(new ComboBoxIndexContent(item.MagazijnId, item.Adress));
                }
            }
            cmbWarehouses.ItemsSource = Warehouses;
            cmbWarehouses.SelectedIndex = 0;
            UpdatelbWarehouse();
        }

        private void UpdatelbWarehouse()
        {
            lbWarehouses.ItemsSource = null;

            WarehousesMinusSelected = new List<ComboBoxIndexContent>();

            int selectedWarehouse = Convert.ToInt32(cmbWarehouses.SelectedValue);

            using (DataContext data = new DataContext())
            {
                var collection = data.Magazijn.Where(m => m.MagazijnId != selectedWarehouse);

                foreach (var item in collection)
                {
                    WarehousesMinusSelected.Add(new ComboBoxIndexContent(item.MagazijnId, item.Adress));
                }
            }
            lbWarehouses.ItemsSource = WarehousesMinusSelected;
            lbWarehouses.SelectedIndex = 0;
            UpdateProducts();

        }
        private void UpdateProducts()
        {
            cmbProducts.ItemsSource = null;

            Products = new List<ComboBoxIndexContent>();

            int selectedWarehouse = Convert.ToInt32(cmbWarehouses.SelectedValue);

            using (DataContext data = new DataContext())
            {
                var collection = data.Product.Join(data.ProductsMagazijn, p => p.ProductId, pm => pm.ProductId, (p, pm) => new { MagazijnId = pm.MagazijnId, ProductId = p.ProductId, ProductName = p.Name }).Where(pm => pm.MagazijnId == selectedWarehouse);

                foreach (var item in collection)
                {
                    Products.Add(new ComboBoxIndexContent(item.ProductId, item.ProductName));
                }
            }
            cmbProducts.ItemsSource = Products;
            cmbProducts.SelectedIndex = 0;
            UpdateIud();

        }

        private void cmbWarehouses_DropDownClosed(object sender, EventArgs e)
        {
            UpdatelbWarehouse();
            UpdateProducts();
            UpdateIud();
        }

        private void cmbProducts_DropDownClosed(object sender, EventArgs e)
        {
            UpdateIud();
        }

        private void UpdateIud()
        {
            int selectedMagazijn = Convert.ToInt32(cmbWarehouses.SelectedValue);
            int selectedProduct = Convert.ToInt32(cmbProducts.SelectedValue);

            using (DataContext data = new DataContext())
            {
                int amount = data.ProductsMagazijn.Where(p => p.MagazijnId == selectedMagazijn && p.ProductId == selectedProduct).FirstOrDefault().Amount;
                iupdAantal.Text = amount.ToString();
            }

        }        
    }
}
