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

namespace QualificationTest.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestPassedPage.xaml
    /// </summary>
    public partial class TestPassedPage : Page
    {
        public TestPassedPage()
        {
            InitializeComponent();
            string testerName = Application.Current.Properties["tester"].ToString();
            string numOfCorrectAnswers = Application.Current.Properties["corAnsCount"].ToString();

            answersCount.Text = $"{testerName} прошёл тест и набрал {numOfCorrectAnswers} правильных ответов из 10!";
        }

        private void Results_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ResultsPage());
        }
    }
}
