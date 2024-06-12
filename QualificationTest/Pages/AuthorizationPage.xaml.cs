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
        private readonly ApplicationContext db;

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
            string login = loginTextBox.Text.Trim();
            string password = passwordPasswordBox.Password.Trim();

            if (login.Length < 3)
            {
                loginTextBox.ToolTip = "Поле введено некорректно!";
                loginTextBox.Background = Brushes.IndianRed;
            }
            else if (password.Length < 3)
            {
                passwordPasswordBox.ToolTip = "Поле введено некорректно!";
                passwordPasswordBox.Background = Brushes.IndianRed;
            }
            else
            {
                loginTextBox.ToolTip = "";
                loginTextBox.Background = Brushes.Transparent;
                passwordPasswordBox.ToolTip = "";
                passwordPasswordBox.Background = Brushes.Transparent;

                User userToAuthorize = null;
                using (ApplicationContext db = new ApplicationContext())
                {
                    userToAuthorize = db.Users.Where(b => b.UsersLogin.ToString() == login && b.UsersPassword.ToString() == password).FirstOrDefault();
                }
                if (userToAuthorize != null)
                {
                    Application.Current.Properties["testerName"] = userToAuthorize.UsersName.ToString();
                    switch (userToAuthorize.UsersRole)
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
