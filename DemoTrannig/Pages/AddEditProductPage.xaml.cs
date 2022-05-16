using DemoTrannig.Classes;
using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DemoTrannig.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        private Product _currentproduct = new Product();
        public AddEditProductPage(Product product)
        {

            InitializeComponent();

            if (product != null)
            {
                _currentproduct = product;
                ArticleTB.IsEnabled = false;
            }
            DataContext = _currentproduct;
            CategoryCB.ItemsSource = dfominaEntities.GetContext().Category.ToList();
            ManufaturerCB.ItemsSource = dfominaEntities.GetContext().Manufacturers.ToList();
            ProviderCB.ItemsSource = dfominaEntities.GetContext().Providers.ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerClass.MainFrame.GoBack();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentproduct.ProductName))
            {
                errors.AppendLine("Укажите имя продукта");
            }
            if (_currentproduct.Manufacturers == null)
            {
                errors.AppendLine("Выберите производителя");
            }
            if (_currentproduct.ProductDiscountAmount < 0)
            {
                errors.AppendLine("Отрицательное значение скидки");
            }
            if (string.IsNullOrWhiteSpace(_currentproduct.ProductDescription))
            {
                errors.AppendLine("Укажите описание");
            }
            if (_currentproduct.ProductQuantityInStock < 0)
            {
                errors.AppendLine("Отрицательное значение товара в наличии");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                if (_currentproduct.ProductArticleNumber != dfominaEntities.GetContext().Product.Where(b => b.ProductArticleNumber == ArticleTB.Text).FirstOrDefault().ProductArticleNumber)
                {
                    dfominaEntities.GetContext().Product.Add(_currentproduct);
                }

                dfominaEntities.GetContext().SaveChanges();
                MessageBox.Show("Сохранено!");
                ManagerClass.MainFrame.GoBack();
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                MessageBox.Show("Товар с таким же артикулом уже существует");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
