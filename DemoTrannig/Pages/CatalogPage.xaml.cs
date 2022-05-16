using DemoTrannig.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DemoTrannig.Pages
{
    /// <summary>
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        int sorting;
        public CatalogPage()
        {
            InitializeComponent();

            if (DBServices.AuthUser != null)
            {
                NameLB.Content = $"{DBServices.AuthUser.UserSurname} {DBServices.AuthUser.UserName} {DBServices.AuthUser.UserPatronymic}";
            }

            var allTypes = dfominaEntities.GetContext().Manufacturers.ToList();
            allTypes.Insert(0, new Manufacturers
            {
                Manufacturers1 = "Все производители"
            });

            ManufatcurerCB.ItemsSource = allTypes;

            Update();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerClass.MainFrame.GoBack();

        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void ManufatcurerCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void Update()
        {
            var selectedManufacturers = dfominaEntities.GetContext().Product.ToList();

            int count = selectedManufacturers.Count;

            //if (ManufatcurerCB.SelectedIndex > 0)
            //{
            //    selectedManufacturers = selectedManufacturers.Where(x => x.Manufacturers.Manufacturers1.Contains(ManufatcurerCB.SelectedItem as Manufacturers)).ToList();
            //}

            switch (sorting)
            {
                case 1:
                    CatalogListView.ItemsSource = selectedManufacturers.Where(x => x.ProductName.ToLower().Contains(SearchTB.Text.ToLower()) ||
            x.ProductDescription.ToLower().Contains(SearchTB.Text.ToLower()) ||
            x.Providers.Provider.ToLower().Contains(SearchTB.Text.ToLower())).OrderBy(x => x.ProductCost).ToList();
                    break;

                case -1:
                    CatalogListView.ItemsSource = selectedManufacturers.Where(x => x.ProductName.ToLower().Contains(SearchTB.Text.ToLower()) ||
            x.ProductDescription.ToLower().Contains(SearchTB.Text.ToLower()) ||
            x.Providers.Provider.ToLower().Contains(SearchTB.Text.ToLower())).OrderByDescending(x => x.ProductCost).ToList();
                    break;

                default:
                    CatalogListView.ItemsSource = selectedManufacturers.Where(x => x.ProductName.ToLower().Contains(SearchTB.Text.ToLower()) ||
            x.ProductDescription.ToLower().Contains(SearchTB.Text.ToLower()) ||
            x.Providers.Provider.ToLower().Contains(SearchTB.Text.ToLower())).ToList();
                    break;
            }

            ProductQuantityLB.Content = $@"{CatalogListView.Items.Count}/{count}";

        }

        private void DesButton_Click(object sender, RoutedEventArgs e)
        {
            sorting = -1;
            Update();
        }

        private void AscButton_Click(object sender, RoutedEventArgs e)
        {
            sorting = 1;
            Update();
        }

        private void ResetSorting_Click(object sender, RoutedEventArgs e)
        {
            sorting = 0;
            Update();
        }
    }
}
