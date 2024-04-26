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
                loginTB.Background = Brushes.DarkRed;
            }
            else if (pass.Length < 5)
            {
                passwordTB.ToolTip = "Поле введено некорректно!";
                passwordTB.Background = Brushes.DarkRed;
            }
            else
            {
                loginTB.ToolTip = "";
                loginTB.Background = Brushes.Transparent;
                passwordTB.ToolTip = "";
                passwordTB.Background = Brushes.Transparent;

                Employee authEmployee = null;
                using (ApplicationContext db = new ApplicationContext())
                {
                    authEmployee = db.Employees.Where(b => b.EmployeeLogin.ToString() == login && b.EmployeePassword.ToString() ==
                    pass).FirstOrDefault();
                }
                if (authEmployee != null)
                {
                    MessageBox.Show("Добро пожаловать!");
                    Application.Current.Properties["tester"] = authEmployee.EmployeeName.ToString();

                    NavigationService.Navigate(new MainProgram());
                    Application.Current.MainWindow.MaxHeight = 768;
                    Application.Current.MainWindow.Height = 768;
                    Application.Current.MainWindow.MaxWidth = 1024;
                    Application.Current.MainWindow.Width = 1024;

                }
                else MessageBox.Show("Вы ввели что-то некорректно!");
            }

        }
    }
}
