using DemoTrannig.Classes;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DemoTrannig.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void GuestEnterButton_Click(object sender, RoutedEventArgs e)
        {
            ManagerClass.MainFrame.Navigate(new Pages.CatalogPage());
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            DBServices.AuthUser = null;
            DBServices.AuthUser = dfominaEntities.GetContext().User.Where(b => b.UserLogin == LoginTextBox.Text && b.UserPassword == PasswordBox.Password).FirstOrDefault();

            if (DBServices.AuthUser != null)
            {
                switch (DBServices.AuthUser.Role.RoleName)
                {
                    case "Клиент":
                        ManagerClass.MainFrame.Navigate(new Pages.CatalogPage());
                        break;

                    case "Администратор":
                        ManagerClass.MainFrame.Navigate(new Pages.AdminPage());
                        break;

                    case "Менеджер":
                        ManagerClass.MainFrame.Navigate(new Pages.CatalogPage());
                        break;
                }

            }
            else
            {
                MessageBox.Show("Неверные данные!");
            }
        }
    }
}
