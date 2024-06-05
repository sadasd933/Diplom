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
            Result currentResult = db.Results.Where(r => r.ResultsID > 0).OrderByDescending(r => r.ResultsID).FirstOrDefault();

            var testerName = currentResult.TesterName;
            var percentageOfCorrectAnswers = currentResult.PercentageOfCorrectAnswers;
            var numberOfCorrectAnswers = currentResult.NumberOfCorrectAnswers;
            var numberOfQuestions = currentResult.NumberOfQuestions;
            answersCount.Text = $"{testerName} прошёл тест и набрал {percentageOfCorrectAnswers}% правильных ответов.\nЭто {numberOfCorrectAnswers}" +
                $" верных ответов на {numberOfQuestions} вопросов!";
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rsltMessageBox = MessageBox.Show("Вы хотите вернуться на окно авторизации?", "Подтверждение",
    MessageBoxButton.YesNo, MessageBoxImage.Information);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    NavigationService.Navigate(new AuthorizationPage());
                    break;
                default:
                    break;
            }
        }

        private void ResultsButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ResultsPage());
        }
    }
}
