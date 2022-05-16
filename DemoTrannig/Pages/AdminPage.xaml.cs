using DemoTrannig.Classes;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DemoTrannig.Pages
{
    /// <summary>
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    /// 

    public partial class AdminPage : Page
    {

        Product _currentProduct = new Product();

        public AdminPage()
        {
            InitializeComponent();
        }

        private void EditProductButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerClass.MainFrame.Navigate(new Pages.AddEditProductPage((sender as Button).DataContext as Product));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var ProductForDelete = ProductsDG.SelectedItems.Cast<Product>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {ProductForDelete.Count()} элемент(ов)?", "Уведомление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    dfominaEntities.GetContext().Product.RemoveRange(ProductForDelete);
                    dfominaEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ProductsDG.ItemsSource = dfominaEntities.GetContext().Product.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerClass.MainFrame.Navigate(new Pages.AddEditProductPage(null));
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerClass.MainFrame.GoBack();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                dfominaEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ProductsDG.ItemsSource = dfominaEntities.GetContext().Product.ToList();
            }
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            var selectedManufacturers = dfominaEntities.GetContext().Product.ToList();
            ProductsDG.ItemsSource = selectedManufacturers.Where(x => x.ProductName.ToLower().Contains(SearchTB.Text.ToLower())).ToList();
        }
    }
}
