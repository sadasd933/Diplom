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
    public partial class Authorization : Page
    {
        ApplicationContext db;

        public Authorization()
        {

            InitializeComponent();
            db = new ApplicationContext();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTB.Text.Trim();
            string pass = passwordTB.Password.Trim();

            if (login.Length < 5)
            {
                loginTB.ToolTip = "Поле введено некорректно!";
                loginTB.Background = Brushes.IndianRed;
            }
            else if (pass.Length < 5)
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
                    try
                    {   
                            authUser = db.Users.Where(b => b.UsersLogin.ToString() == login && b.UsersPassword.ToString() == pass).FirstOrDefault();
                    }
                    catch
                    {
                        MessageBox.Show("Пользователя с такими данными не существует!", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }
                }
                if (authUser != null)
                {
                    switch (authUser.UsersRole)
                    {
                        case "Tester":
                            NavigationService.Navigate(new MainProgram());
                            break;
                        case "QA Engineer":
                            NavigationService.Navigate(new CreateQuestionsPage());
                            break;
                    }

                    Application.Current.Properties["testerName"] = authUser.UsersName.ToString();

                    Application.Current.MainWindow.MaxHeight = 768;
                    Application.Current.MainWindow.Height = 768;
                    Application.Current.MainWindow.MaxWidth = 1024;
                    Application.Current.MainWindow.Width = 1024;

                }
            }
        }
    }
}
