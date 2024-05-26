using System.Linq;
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
        ApplicationContext db = new ApplicationContext();
        public TestPassedPage()
        {
            InitializeComponent();
            Result currentResult = db.Results.Where(b => b.ResultsID >= 0).FirstOrDefault();

            var name = currentResult.TesterName;
            var percentage = currentResult.PercentageOfCorrectAnswers;
            answersCount.Text = $"{name} прошёл тест и набрал {percentage}% правильных ответов";
        }

        private void Results_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ResultsPage());
        }
    }
}
