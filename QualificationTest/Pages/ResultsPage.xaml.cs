using QualificationTest.Classes;
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

        public string corAnswers = Application.Current.Properties["corAnswers"].ToString();
        public string userAnswers = Application.Current.Properties["userAnswers"].ToString();
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

            if (currentQuestionIndex < 8)
            {
                currentQuestionIndex++;
            }
            else if (currentQuestionIndex == 8)
            {
                currentQuestionIndex++;
                NextQuestion.Content = "Перейти к авторизации";
            }
            else
            {


                NavigationService.Navigate(new Authorization());
            }


            string curQuestionInd = questionOrder[currentQuestionIndex].ToString();
            string curUserAnsInd = userAnswers[currentQuestionIndex].ToString();
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

            int currentQuestionInd = int.Parse(curQuestionInd);
            int currentUserAnswerInd = int.Parse(curUserAnsInd);

            Question currQ = null;
            Answer currAns1, currAns2, currAnsCorrect = null;

            using (ApplicationContext db = new ApplicationContext())
            {
                currQ = db.Questions.Where(c => c.QuestionID.ToString() == curQuestionInd).FirstOrDefault();
                currAns1 = db.Answers.Where(c => c.QuestionID == currQ.QuestionID).FirstOrDefault();
                currAns2 = db.Answers.Where(c => c.QuestionID == currQ.QuestionID&&c.AnswerID != currAns1.AnswerID).FirstOrDefault();
                currAnsCorrect = db.Answers.Where(c => c.QuestionID == currQ.QuestionID && c.IsCorrect == "True").FirstOrDefault();

                QuestionTextTextBlock.Text = currQ.QuestionText.ToString();

                string userAnswerDB = db.Answers.Where(c => c.QuestionID.ToString() == curQuestionInd && c.AnswerID % 3 == currentUserAnswerInd).FirstOrDefault().ToString(); ;

                switch (curUserAnsInd)
                {
                    case "1":
                        if (corAnswers[currentQuestionInd] == curUserAnsInd[currentQuestionInd])
                        {
                            UserAnswer.Foreground = Brushes.Green;
                            UserAnswer.Text = currAnsCorrect.AnswerText;
                        }
                        else
                        {
                            UserAnswer.Foreground = Brushes.Red;
                            UserAnswer.Text = userAnswerDB;
                        }
                        QuestionAnswerCorrect.Foreground = Brushes.Green;
                        QuestionAnswerCorrect.Text = currAns1.AnswerText;
                        break;
                    case "2":
                        if (corAnswers[currentQuestionInd] == curUserAnsInd[currentQuestionInd])
                        {
                            UserAnswer.Foreground = Brushes.Green;
                            UserAnswer.Text = currAnsCorrect.AnswerText;
                        }
                        else
                        {
                            UserAnswer.Foreground = Brushes.Red;
                            UserAnswer.Text = userAnswerDB;
                        }
                        QuestionAnswerCorrect.Foreground = Brushes.Green;
                        QuestionAnswerCorrect.Text = currAns1.AnswerText;
                        break;
                    case "3":
                        if (corAnswers[currentQuestionInd] == curUserAnsInd[currentQuestionInd])
                        {
                            UserAnswer.Foreground = Brushes.Green;
                            UserAnswer.Text = currAnsCorrect.AnswerText;
                        }
                        else
                        {
                            UserAnswer.Foreground = Brushes.Red;
                            UserAnswer.Text = userAnswerDB;
                        }
                        QuestionAnswerCorrect.Foreground = Brushes.Green;
                        QuestionAnswerCorrect.Text = currAns1.AnswerText;
                        break;


                    default:
                        break;
                }


                //    switch (currQ.CorrectAnswer)
                //    {
                //        case "1":
                //            if (curUserAnsInd == "1")
                //            {
                //                UserAnswer.Foreground = Brushes.Green;
                //                UserAnswer.Text = currQ.AnswerVariant1;
                //            }
                //            if (curUserAnsInd == "2")
                //            {
                //                UserAnswer.Foreground = Brushes.Red;
                //                UserAnswer.Text = currQ.AnswerVariant2;
                //            }
                //            if (curUserAnsInd == "3")
                //            {
                //                UserAnswer.Foreground = Brushes.Red;
                //                UserAnswer.Text = currQ.AnswerVariant3;
                //            }
                //            QuestionAnswerCorrect.Foreground = Brushes.Green;
                //            QuestionAnswerCorrect.Text = currQ.AnswerVariant1;
                //            break;
                //        case "2":
                //            if (curUserAnsInd == "1")
                //            {
                //                UserAnswer.Foreground = Brushes.Red;
                //                UserAnswer.Text = currQ.AnswerVariant1;
                //            }
                //            if (curUserAnsInd == "2")
                //            {
                //                UserAnswer.Foreground = Brushes.Green;
                //                UserAnswer.Text = currQ.AnswerVariant2;
                //            }
                //            if (curUserAnsInd == "3")
                //            {
                //                UserAnswer.Foreground = Brushes.Red;
                //                UserAnswer.Text = currQ.AnswerVariant3;
                //            }
                //            QuestionAnswerCorrect.Foreground = Brushes.Green;
                //            QuestionAnswerCorrect.Text = currQ.AnswerVariant2;
                //            break;
                //        case "3":
                //            if (curUserAnsInd == "1")
                //            {
                //                UserAnswer.Foreground = Brushes.Red;
                //                UserAnswer.Text = currQ.AnswerVariant1;
                //            }
                //            if (curUserAnsInd == "2")
                //            {
                //                UserAnswer.Foreground = Brushes.Red;
                //                UserAnswer.Text = currQ.AnswerVariant2;
                //            }
                //            if (curUserAnsInd == "3")
                //            {
                //                UserAnswer.Foreground = Brushes.Green;
                //                UserAnswer.Text = currQ.AnswerVariant3;
                //            }
                //            QuestionAnswerCorrect.Foreground = Brushes.Green;
                //            QuestionAnswerCorrect.Text = currQ.AnswerVariant3;
                //            break;
                //    }
                //}
            }
        }

        private void ChechAnswers()
        {

        }
    }
}
