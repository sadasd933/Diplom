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
        ApplicationContext db;
        public int currentQuestionIndex = -1;
        public int indexOfCurrentQuestion = 0;
        public string curQuestionInd = null;

        public string questionOrder = Application.Current.Properties["questionOrder"].ToString();


        public ResultsPage()
        {
            db = new ApplicationContext();

            InitializeComponent();

            LoadNewQuestion();


        }


        private void NextQuestion_Click(object sender, RoutedEventArgs e)
        {
            LoadNewQuestion();
        }

        private void LoadNewQuestion()
        {
            indexOfCurrentQuestion++;

            if (currentQuestionIndex < 8)
            {
                currentQuestionIndex++;
            }
            else if (currentQuestionIndex == 8)
            {
                currentQuestionIndex++;
                NextQuestion.Visibility = Visibility.Hidden;
                AuthorizationNavigationButton.Visibility = Visibility.Visible;
            }
            else
            {

                NavigationService.Navigate(new Authorization());
            }

            try
            {
                curQuestionInd = questionOrder[currentQuestionIndex].ToString();

            }
            catch (System.Exception)
            {
            }
            UserAnswer curUserAnsInd = db.UserAnswers.Where(b => b.UserAnswerID == indexOfCurrentQuestion).FirstOrDefault();

            var currentQuestionInd = curUserAnsInd.QuestionID;
            var correctAnswer = curUserAnsInd.CorrectAnswer;
            var userAnswer = curUserAnsInd.UsersAnswer;
            var currentQuestionText = db.Questions.Where(b => b.QuestionID == currentQuestionInd).FirstOrDefault();

            if (questionOrder[currentQuestionIndex].ToString() == "1" && questionOrder[currentQuestionIndex + 1].ToString() == "0")
            {
                try
                {
                    currentQuestionIndex += 2;

                }
                catch
                {
                    currentQuestionIndex++;

                }

                curQuestionInd = "10";
            }

            Question currQ = null;

            using (ApplicationContext db = new ApplicationContext())
            {
                currQ = db.Questions.Where(c => c.QuestionID.ToString() == curQuestionInd).FirstOrDefault();
                QuestionTextTextBlock.Text = currentQuestionText.QuestionText;

                QuestionAnswerCorrect.Text = correctAnswer.ToString();
                UserAnswer.Text = userAnswer.ToString();

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

        private void AuthorizationNavigationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Authorization());
        }
    }
}
