using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace QualificationTest.Pages
{
    /// <summary>
    /// Логика взаимодействия для ResultsPage.xaml
    /// </summary>
    public partial class ResultsPage : Page
    {
        private readonly ApplicationContext db;
        private int currentQuestionIndex = -1;
        List<UserAnswer> currentUserAnswer;
        public ResultsPage()
        {
            db = new ApplicationContext();
            var lastResult = db.UserAnswers.Max(m => m.ResultID);
            InitializeComponent();
            SetPageSize();
            currentUserAnswer = db.UserAnswers.Where(r => r.ResultID == lastResult).ToList();
            currentQuestionIndex++;
            LoadNewQuestion();
        }

        private void SetPageSize()
        {
            Application.Current.MainWindow.MinHeight = 800;
            Application.Current.MainWindow.MinWidth = 1000;
            Application.Current.MainWindow.Height = 800;
            Application.Current.MainWindow.Width = 1000;
        }

        private void LoadNewQuestion()
        {
            resultsInfo.Text = $"{currentQuestionIndex + 1}/{currentUserAnswer.Count}";
            var currentQuestionID = currentUserAnswer[currentQuestionIndex].QuestionID;
            var correctAnswer = currentUserAnswer[currentQuestionIndex].CorrectAnswer;
            var usersAnswer = currentUserAnswer[currentQuestionIndex].UsersAnswer;
            var currentQuestion = db.Questions.Where(b => b.QuestionID == currentQuestionID).FirstOrDefault();

            using (ApplicationContext db = new ApplicationContext())
            {

                QuestionTextTextBlock.Text = currentQuestion.QuestionText;

                QuestionAnswerCorrect.Text = correctAnswer;
                UserAnswer.Text = usersAnswer.ToString();

                QuestionAnswerCorrect.Foreground = Brushes.Green;

                if (QuestionAnswerCorrect.Text == UserAnswer.Text)
                {
                    UserAnswer.Foreground = Brushes.Green;
                }
                else
                {
                    UserAnswer.Foreground = Brushes.Red;
                }
            }
        }
        private void NextQuestionButton_Click(object sender, RoutedEventArgs e)
        {

            if (currentQuestionIndex < currentUserAnswer.Count - 1)
            {
                currentQuestionIndex++;
                LoadNewQuestion();
            }
            else
            {
                LoadNewQuestion();
                ReturnToAuthorizationButton.Visibility = Visibility.Visible;
                NextQuestionButton.Visibility = Visibility.Hidden;
            }
        }

        private void ReturnToAuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthorizationPage());
        }
    }
}
