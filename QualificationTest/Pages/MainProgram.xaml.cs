using QualificationTest.Pages;
using System;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace QualificationTest
{
    /// <summary>
    /// Логика взаимодействия для MainProgram.xaml
    /// </summary>
    public partial class MainProgram : Page
    {
        ApplicationContext db;
        Random rnd = new Random();
        public int questionIndex = -1;
        public string correctAnswer;
        public int numOfCorrectAnswers = 0;
        public int currentQuestionIndex = -1;

        public int[] answers = new int[10];
        public int[] corAnswers = new int[10];


        int[] questionOrder = new int[10];


        public MainProgram()
        {

            InitializeComponent();
            db = new ApplicationContext();


            for (int i = 0; i < questionOrder.Length; i++)
            {
                questionIndex = rnd.Next(1, 11);
                if (questionOrder.Contains(questionIndex))
                {
                    i--;
                }
                else
                {
                    questionOrder[i] = questionIndex;
                }
            }
            LoadQuestion();
            testerName.Text = (string)System.Windows.Application.Current.Properties["test"];
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            switch (correctAnswer)
            {
                case "1":
                    if (Ans1.IsChecked == true)
                    {
                        answers[currentQuestionIndex] = 1;
                        numOfCorrectAnswers++;
                        corAnswers[currentQuestionIndex] = 1;
                    }
                    if (Ans2.IsChecked == true)
                    {
                        answers[currentQuestionIndex] = 2;
                        corAnswers[currentQuestionIndex] = 1;
                    }
                    if (Ans3.IsChecked == true)
                    {
                        answers[currentQuestionIndex] = 3;
                        corAnswers[currentQuestionIndex] = 1;
                    }
                    break;

                case "2":
                    if (Ans2.IsChecked == true)
                    {
                        answers[currentQuestionIndex] = 2;
                        numOfCorrectAnswers++;
                        corAnswers[currentQuestionIndex] = 2;
                    }
                    if (Ans1.IsChecked == true)
                    {
                        answers[currentQuestionIndex] = 1;
                        corAnswers[currentQuestionIndex] = 2;
                    }
                    if (Ans3.IsChecked == true)
                    {
                        answers[currentQuestionIndex] = 3;
                        corAnswers[currentQuestionIndex] = 2;
                    }
                    break;
                case "3":
                    if (Ans3.IsChecked == true)
                    {
                        answers[currentQuestionIndex] = 3;
                        numOfCorrectAnswers++;
                        corAnswers[currentQuestionIndex] = 3;
                    }
                    if (Ans1.IsChecked == true)
                    {
                        answers[currentQuestionIndex] = 1;
                        corAnswers[currentQuestionIndex] = 3;
                    }
                    if (Ans2.IsChecked == true)
                    {
                        answers[currentQuestionIndex] = 2;
                        corAnswers[currentQuestionIndex] = 3;
                    }
                    break;

            }
            LoadQuestion();
        }

        public void LoadQuestion()
        {
            if (currentQuestionIndex < 8)
            {
                currentQuestionIndex++;
            }
            else if (currentQuestionIndex == 8)
            {
                currentQuestionIndex++;
                SubmitAnswer.Content = "Завершить тестирование";
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    System.Windows.Application.Current.Properties["userAnswers"] += answers[i].ToString();
                    System.Windows.Application.Current.Properties["corAnswers"] += corAnswers[i].ToString();
                    System.Windows.Application.Current.Properties["questionOrder"] += questionOrder[i].ToString();
                }
                System.Windows.Application.Current.Properties["corAnsCount"] = numOfCorrectAnswers;

               

                NavigationService.Navigate(new TestPassedPage());
            }

            var curAnsInd = questionOrder[currentQuestionIndex];

            Question currQ = null;
            using (ApplicationContext db = new ApplicationContext())
            {

                currQ = db.Questions.Where(b => b.QuestionID == curAnsInd).FirstOrDefault();
                QuestionTextTextBlock.Text = currQ.QuestionText.ToString();
                QuestionAnswer1.Text = currQ.AnswerVariant1.ToString();
                QuestionAnswer2.Text = currQ.AnswerVariant2.ToString();
                QuestionAnswer3.Text = currQ.AnswerVariant3.ToString();
                if (currQ.AnswerImagePath != null)
                {
                    QuestionImage.Source = new BitmapImage(new Uri(currQ.AnswerImagePath.ToString()));
                }
                else
                {
                    QuestionImage.Source = null;
                }

                correctAnswer = currQ.CorrectAnswer.ToString();

            }
        }
    }
}
