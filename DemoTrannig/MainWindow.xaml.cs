using DemoTrannig.Classes;
using System.Windows;

namespace DemoTrannig
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ManagerClass.MainFrame = MainFrame;
            ManagerClass.MainFrame.Navigate(new Pages.LoginPage());
            //ManagerClass.MainFrame.Navigate(new Pages.CatalogPage());
        }
    }
}
