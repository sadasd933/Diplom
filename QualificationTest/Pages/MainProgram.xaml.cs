using QualificationTest.Classes;
using QualificationTest.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace QualificationTest
{
    /// <summary>
    /// Логика взаимодействия для MainProgram.xaml
    /// </summary>
    public partial class MainProgram : Page
    {
        int numOfCorrectAnswers, indexOfAnswers = 0;
        private Question currentQuestion;
        private readonly int tmpToRandomizeQuestions, questionCount;
        private readonly string testID = Application.Current.Properties["testID"].ToString();
        private readonly ApplicationContext db;
        private readonly Random rnd = new Random();
        private readonly List<int> questionOrder;
        private readonly List<string> userAnswers;
        private readonly List<string> correctAnswers;
        int currentQuestionIndex = -1;

        public MainProgram()
        {

            InitializeComponent();
            SetPageSize();
            db = new ApplicationContext();
            var questions = db.Questions.ToList();
            questionCount = rnd.Next(5, questions.Count);


            questionOrder = new List<int>(questionCount);
            userAnswers = new List<string>(questionCount);
            correctAnswers = new List<string>(questionCount);

            for (int i = 0; i < questionOrder.Capacity; i++)
            {
                tmpToRandomizeQuestions = rnd.Next(1, questionCount + 1);
                if (questionOrder.Contains(tmpToRandomizeQuestions))
                {
                    i--;
                }
                else
                {
                    questionOrder.Add(tmpToRandomizeQuestions);
                }
            }
            currentQuestionIndex++;
            LoadQuestion();
        }
        public void LoadQuestion()
        {
            testInfoTextBlock.Text = $"{currentQuestionIndex + 1}/{questionCount}";

            Answer currentFirstAnswer, currentSecondAnswer, currentThirdAnswer, correctAnswer = null;

            int testIDToInt = int.Parse(testID);
            var currentAnswerIndex = questionOrder.ElementAt(currentQuestionIndex);

            using (ApplicationContext db = new ApplicationContext())
            {
                currentQuestion = db.Questions.Where(b => b.QuestionID == currentAnswerIndex).FirstOrDefault();

                currentFirstAnswer = db.Answers.Where(b => b.QuestionID == currentAnswerIndex).FirstOrDefault();
                currentSecondAnswer = db.Answers.Where(b => b.QuestionID == currentAnswerIndex && b.AnswerID != currentFirstAnswer.AnswerID).FirstOrDefault();
                currentThirdAnswer = db.Answers.Where(b => b.QuestionID == currentAnswerIndex && b.AnswerID != currentFirstAnswer.AnswerID && b.AnswerID != currentSecondAnswer.AnswerID).FirstOrDefault();
                correctAnswer = db.Answers.Where(b => b.IsCorrect == "true" && b.QuestionID == currentAnswerIndex).FirstOrDefault();
                indexOfAnswers = correctAnswer.AnswerID % 3; //индексация ответов от 0 до 2


                QuestionTextTextBlock.Text = currentQuestion.QuestionText.ToString();
                FirstAnswerTextBlock.Text = currentFirstAnswer.AnswerText.ToString();
                SecondAnswerTextBlock.Text = currentSecondAnswer.AnswerText.ToString();
                ThirdAnswerTextBlock.Text = currentThirdAnswer.AnswerText.ToString();

            }
        }
        private void AddUserAnswers()
        {
            UserAnswer answersToInsert = new UserAnswer();
            PropertyInfo[] properties = typeof(UserAnswer).GetProperties();
            User userToID = new User();
            string testerName = Application.Current.Properties["testerName"].ToString();
            userToID = db.Users.Where(u => u.UsersName == testerName).FirstOrDefault();

            Result currentResult = db.Results.Where(r => r.ResultsID > 0).OrderByDescending(c => c.ResultsID).FirstOrDefault();

            for (int i = 0; i < questionCount; i++)
            {
                foreach (PropertyInfo property in properties)
                {
                    switch (property.Name)
                    {
                        case "CorrectAnswer":
                            property.SetValue(answersToInsert, correctAnswers[i].ToString());
                            break;
                        case "UsersAnswer":
                            property.SetValue(answersToInsert, userAnswers[i].ToString());
                            break;
                        case "UserID":
                            property.SetValue(answersToInsert, userToID.UsersID);
                            break;
                        case "QuestionID":
                            property.SetValue(answersToInsert, questionOrder[i]);
                            break;
                        case "ResultID":
                            property.SetValue(answersToInsert, currentResult.ResultsID);
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
        private void SaveResults()
        {
            Result resultsToInsert = new Result();
            PropertyInfo[] properties = typeof(Result).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                switch (property.Name)
                {
                    case "TesterName":
                        property.SetValue(resultsToInsert, Application.Current.Properties["testerName"].ToString());
                        break;
                    case "PercentageOfCorrectAnswers":
                        property.SetValue(resultsToInsert, numOfCorrectAnswers * 100 / questionOrder.Capacity);
                        break;
                    case "NumberOfQuestions":
                        property.SetValue(resultsToInsert, questionCount);
                        break;
                    case "NumberOfCorrectAnswers":
                        property.SetValue(resultsToInsert, numOfCorrectAnswers);
                        break;
                    default:
                        break;
                }
            }
            using (var db = new ApplicationContext())
            {
                db.Results.Add(resultsToInsert);
                db.SaveChanges();
            }

        }
        private void SubmitAnswerButton_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswers();

            if (currentQuestionIndex < questionOrder.Count - 1)
            {
                currentQuestionIndex++;
                LoadQuestion();
            }
            else
            {
                SubmitAnswerButton.Visibility = Visibility.Hidden;
                EndTestButton.Visibility = Visibility.Visible;
            }
        }

        private void CheckAnswers()
        {
            switch (indexOfAnswers)
            {
                case 1:
                    if (FirstAnswerRadioButton.IsChecked == true)
                    {
                        AddAnswers(1, 1);
                    }
                    else if (SecondAnswerRadioButton.IsChecked == true)
                    {
                        AddAnswers(1, 2);
                    }
                    else
                    {
                        AddAnswers(1, 3);
                    }
                    break;

                case 2:
                    if (FirstAnswerRadioButton.IsChecked == true)
                    {
                        AddAnswers(2, 1);
                    }
                    else if (SecondAnswerRadioButton.IsChecked == true)
                    {
                        AddAnswers(2, 2);
                    }
                    else
                    {
                        AddAnswers(2, 3);
                    }
                    break;
                case 0:
                    if (FirstAnswerRadioButton.IsChecked == true)
                    {
                        AddAnswers(3, 1);
                    }
                    else if (SecondAnswerRadioButton.IsChecked == true)
                    {
                        AddAnswers(3, 2);

                    }
                    else
                    {
                        AddAnswers(3, 3);

                    }
                    break;
            }
        }

        private void AddAnswers(int correctAnswer, int userAnswer)
        {
            switch (correctAnswer)
            {
                case 1:
                    switch (userAnswer)
                    {
                        case 1:
                            userAnswers.Add(FirstAnswerTextBlock.Text.ToString());
                            correctAnswers.Add(FirstAnswerTextBlock.Text.ToString());
                            numOfCorrectAnswers++;
                            break;
                        case 2:
                            userAnswers.Add(SecondAnswerTextBlock.Text.ToString());
                            correctAnswers.Add(FirstAnswerTextBlock.Text.ToString());
                            break;
                        default:
                            userAnswers.Add(ThirdAnswerTextBlock.Text.ToString());
                            correctAnswers.Add(FirstAnswerTextBlock.Text.ToString());
                            break;
                    }
                    break;
                case 2:
                    switch (userAnswer)
                    {
                        case 1:
                            userAnswers.Add(FirstAnswerTextBlock.Text.ToString());
                            correctAnswers.Add(SecondAnswerTextBlock.Text.ToString());

                            break;
                        case 2:
                            userAnswers.Add(SecondAnswerTextBlock.Text.ToString());
                            correctAnswers.Add(SecondAnswerTextBlock.Text.ToString());
                            numOfCorrectAnswers++;
                            break;
                        default:
                            userAnswers.Add(ThirdAnswerTextBlock.Text.ToString());
                            correctAnswers.Add(SecondAnswerTextBlock.Text.ToString());
                            break;
                    }
                    break;
                case 3:
                    switch (userAnswer)
                    {
                        case 1:
                            userAnswers.Add(FirstAnswerTextBlock.Text.ToString());
                            correctAnswers.Add(ThirdAnswerTextBlock.Text.ToString());
                            break;
                        case 2:
                            userAnswers.Add(SecondAnswerTextBlock.Text.ToString());
                            correctAnswers.Add(ThirdAnswerTextBlock.Text.ToString());
                            break;
                        default:
                            userAnswers.Add(ThirdAnswerTextBlock.Text.ToString());
                            correctAnswers.Add(ThirdAnswerTextBlock.Text.ToString());
                            numOfCorrectAnswers++;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
        private void EndTestButton_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswers();
            SaveResults();
            AddUserAnswers();
            NavigationService.Navigate(new TestPassedPage());
        }
        private void SetPageSize()
        {
            Application.Current.MainWindow.MinHeight = 800;
            Application.Current.MainWindow.MinWidth = 1000;
            Application.Current.MainWindow.Height = 800;
            Application.Current.MainWindow.Width = 1000;
        }
    }
}
