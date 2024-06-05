using QualificationTest.Pages;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace QualificationTest
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        ApplicationContext db;

        public AuthorizationPage()
        {

            InitializeComponent();
            db = new ApplicationContext();
            SetPageSize();
        }

        private void SetPageSize()
        {
            Application.Current.MainWindow.MinHeight = 450;
            Application.Current.MainWindow.MinWidth = 800;
            Application.Current.MainWindow.Height = 450;
            Application.Current.MainWindow.Width = 800;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTB.Text.Trim();
            string pass = passwordTB.Password.Trim();

            if (login.Length < 3)
            {
                loginTB.ToolTip = "Поле введено некорректно!";
                loginTB.Background = Brushes.IndianRed;
            }
            else if (pass.Length < 3)
            {
                passwordTB.ToolTip = "Поле введено некорректно!";
                passwordTB.Background = Brushes.IndianRed;
            }
            else
            {
                loginTB.ToolTip = "";
                loginTB.Background = Brushes.Transparent;
                passwordTB.ToolTip = "";
                passwordTB.Background = Brushes.Transparent;

                User authUser = null;
                using (ApplicationContext db = new ApplicationContext())
                {
                    authUser = db.Users.Where(b => b.UsersLogin.ToString() == login && b.UsersPassword.ToString() == pass).FirstOrDefault();
                }
                if (authUser != null)
                {
                    Application.Current.Properties["testerName"] = authUser.UsersName.ToString();
                    switch (authUser.UsersRole)
                    {
                        case "Tester":
                            NavigationService.Navigate(new TestSelectionPage());
                            break;
                        case "Testing Engineer":
                            NavigationService.Navigate(new TestCreationPage());
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Пользователя с такими данными не существует!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
        }
    }
}
