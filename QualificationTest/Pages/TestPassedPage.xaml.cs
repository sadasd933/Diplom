using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace QualificationTest.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestPassedPage.xaml
    /// </summary>
    public partial class TestPassedPage : Page
    {
        public string msg;
        public TestPassedPage()
        {
            InitializeComponent();
            string testerName = Application.Current.Properties["testerName"].ToString();
            string numOfCorrectAnswers = Application.Current.Properties["corAnsCount"].ToString();

            msg = $"{testerName} прошёл тест и набрал {numOfCorrectAnswers} правильных ответов из 10!";
            answersCount.Text = msg;
        }

        private void Results_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ResultsPage());
        }
    }
}
