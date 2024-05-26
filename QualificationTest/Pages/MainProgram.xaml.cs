using QualificationTest.Classes;
using QualificationTest.Pages;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

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
        public int numOfCorrectAnswers = 0;
        public int currentQuestionIndex = -1;
        public Answer currentUserAnswer;
        public Answer correctAnswer;
        int indexOfQuestion = -1;

        public List<string> userAnswers = new List<string>(10);
        public List<string> correctAnswers = new List<string>(10);

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
        }

        private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
        {
            var tmp = correctAnswer.AnswerID % 3;
            switch (tmp)
            {
                case 1:
                    if (Ans1.IsChecked == true)
                    {
                        AddAnswers(1, 1);
                    }
                    else if (Ans2.IsChecked == true)
                    {
                        AddAnswers(1, 2);
                    }
                    else
                    {
                        AddAnswers(1, 3);
                    }
                    break;

                case 2:
                    if (Ans2.IsChecked == true)
                    {
                        AddAnswers(2, 1);
                    }
                    else if(Ans1.IsChecked == true)
                    {
                        AddAnswers(2, 2);
                    }
                    else
                    {
                        AddAnswers(2, 3);
                    }
                    break;
                case 0:
                    if (Ans1.IsChecked == true)
                    {
                        AddAnswers(3, 1);
                    }
                    else if(Ans2.IsChecked == true)
                    {
                        AddAnswers(3, 2);

                    }
                    else
                    {
                        AddAnswers(3, 3);

                    }
                    break;

            }

            AddUserAnswerCommandExecute();
            LoadQuestion();
        }

        public void LoadQuestion()
        {

            User currentTester;
            Question currQ = null;
            Answer currAnswers1, currAnswers2, currAnswers3 = null;

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
                    System.Windows.Application.Current.Properties["questionOrder"] += questionOrder[i].ToString();
                }
                System.Windows.Application.Current.Properties["corAnsCount"] = numOfCorrectAnswers;

                using (ApplicationContext db = new ApplicationContext())
                {

                    string currentTesterName = Application.Current.Properties["testerName"].ToString();

                    currentTester = db.Users.Where(u => u.UsersName == currentTesterName).FirstOrDefault();

                }
                NavigationService.Navigate(new TestPassedPage());
            }

            var curAnsInd = questionOrder[currentQuestionIndex];


            using (ApplicationContext db = new ApplicationContext())
            {
                currQ = db.Questions.Where(b => b.QuestionID == curAnsInd).FirstOrDefault();
                currAnswers1 = db.Answers.Where(b => b.QuestionID == curAnsInd).FirstOrDefault();
                currAnswers2 = db.Answers.Where(b => b.QuestionID == curAnsInd && b.AnswerID != currAnswers1.AnswerID).FirstOrDefault();
                currAnswers3 = db.Answers.Where(b => b.QuestionID == curAnsInd && b.AnswerID != currAnswers1.AnswerID && b.AnswerID != currAnswers2.AnswerID).FirstOrDefault();
                QuestionTextTextBlock.Text = currQ.QuestionText.ToString();



                QuestionAnswer1.Text = currAnswers1.AnswerText.ToString();
                QuestionAnswer2.Text = currAnswers2.AnswerText.ToString();
                QuestionAnswer3.Text = currAnswers3.AnswerText.ToString();

                correctAnswer = db.Answers.Where(b => b.IsCorrect == "true" && b.QuestionID == curAnsInd).FirstOrDefault();

            }
        }

        private void AddAnswers(int corrAns, int userAns)
        {
            switch (corrAns)
            {
                case 1:
                    switch (userAns)
                    {
                        case 1:
                            userAnswers.Add(QuestionAnswer1.Text.ToString());
                            correctAnswers.Add(QuestionAnswer1.Text.ToString());
                            numOfCorrectAnswers++;
                            break;
                        case 2:
                            userAnswers.Add(QuestionAnswer2.Text.ToString());
                            correctAnswers.Add(QuestionAnswer1.Text.ToString());
                            break;
                        default:
                            userAnswers.Add(QuestionAnswer3.Text.ToString());
                            correctAnswers.Add(QuestionAnswer1.Text.ToString());
                            break;
                    }
                    break;
                case 2:
                    switch (userAns)
                    {
                        case 1:
                            userAnswers.Add(QuestionAnswer1.Text.ToString());
                            correctAnswers.Add(QuestionAnswer2.Text.ToString());
                            
                            break;
                        case 2:
                            userAnswers.Add(QuestionAnswer2.Text.ToString());
                            correctAnswers.Add(QuestionAnswer2.Text.ToString());
                            numOfCorrectAnswers++;
                            break;
                        default:
                            userAnswers.Add(QuestionAnswer3.Text.ToString());
                            correctAnswers.Add(QuestionAnswer2.Text.ToString());
                            break;
                    }
                    break;
                case 3:
                    switch (userAns)
                    {
                        case 1:
                            userAnswers.Add(QuestionAnswer1.Text.ToString());
                            correctAnswers.Add(QuestionAnswer3.Text.ToString());
                            break;
                        case 2:
                            userAnswers.Add(QuestionAnswer2.Text.ToString());
                            correctAnswers.Add(QuestionAnswer3.Text.ToString());
                            break;
                        default:
                            userAnswers.Add(QuestionAnswer3.Text.ToString());
                            correctAnswers.Add(QuestionAnswer3.Text.ToString());
                            numOfCorrectAnswers++;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }




        private void AddUserAnswerCommandExecute()
        {
            indexOfQuestion++;
            var curAnsInd = questionOrder[currentQuestionIndex];

            UserAnswer answersToInsert = new UserAnswer();
            PropertyInfo[] properties = typeof(UserAnswer).GetProperties();
            User userToID = new User();
            string userFio = Application.Current.Properties["testerName"].ToString();
            userToID = db.Users.Where(u=>u.UsersName == userFio).FirstOrDefault();

            foreach (PropertyInfo property in properties)
            {
                switch (property.Name)
                {
                    case "CorrectAnswer":
                        property.SetValue(answersToInsert, correctAnswer.AnswerText.ToString());
                        break;
                    case "UsersAnswer":
                        property.SetValue(answersToInsert, userAnswers[indexOfQuestion].ToString());
                        break;
                    case "UserID":
                        property.SetValue(answersToInsert, userToID.UsersID);
                        break;
                    case "QuestionID":
                        property.SetValue(answersToInsert, curAnsInd);
                        break;
                    case "ResultID":
                        property.SetValue(answersToInsert, 1);
                        break;
                    default:
                        break;
                }
            }
            using (var db = new ApplicationContext())
            {
                db.UserAnswers.Add(answersToInsert);
                db.SaveChanges();
            }
        }

    }
}
