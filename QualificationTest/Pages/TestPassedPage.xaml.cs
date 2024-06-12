using System;
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
            SetPageSize();
            Result currentResult = db.Results.Where(r => r.ResultsID > 0).OrderByDescending(r => r.ResultsID).FirstOrDefault();

            var testerName = currentResult.TesterName;
            var percentageOfCorrectAnswers = currentResult.PercentageOfCorrectAnswers;
            var numberOfCorrectAnswers = currentResult.NumberOfCorrectAnswers;
            var numberOfQuestions = currentResult.NumberOfQuestions;
            answersCount.Text = $"{testerName} прошёл тест и набрал {percentageOfCorrectAnswers}% правильных ответов.\nЭто {numberOfCorrectAnswers}" +
                $" верных ответов на {numberOfQuestions} вопросов!";
        }

        private void SetPageSize()
        {
            Application.Current.MainWindow.MinHeight = 450;
            Application.Current.MainWindow.MinWidth = 800;
            Application.Current.MainWindow.Height = 450;
            Application.Current.MainWindow.Width = 800;
        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultMessageBox = MessageBox.Show("Вы хотите вернуться на окно авторизации?", "Подтверждение",
    MessageBoxButton.YesNo, MessageBoxImage.Information);

            switch (resultMessageBox)
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
