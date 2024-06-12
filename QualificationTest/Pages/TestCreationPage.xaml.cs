using QualificationTest.Classes;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace QualificationTest.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestCreationPage.xaml
    /// </summary>
    public partial class TestCreationPage : Page
    {
        private string correctAnswer;
        private Question currQ = null;
        ApplicationContext db;
        public TestCreationPage()
        {
            InitializeComponent();
            db = new ApplicationContext();
            testSelectionCB.Items.Add(1);
            testSelectionCB.Items.Add(2);
        }

        private void createQuestionButton_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult resultMessageBox = MessageBox.Show("Вы уверены, что хотите добавить вопрос?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Information);
            bool isAllAnswersEntered = firstAnswerTB != null && secondAnswerTB != null && thirdAnswerTB != null;
            bool isCorrectAnswerChecked = (isCorrect1.IsChecked != false || isCorrect2.IsChecked != false || isCorrect3.IsChecked != false);
            bool isAllEntered = questionTextTB.Text != null && testSelectionCB.SelectedItem != null && isCorrectAnswerChecked && isAllAnswersEntered;
            switch (resultMessageBox)
            {
                case MessageBoxResult.Yes:
                    if (isAllEntered)
                    {
                        SaveQuestion();
                        SaveAnswer(1);
                        SaveAnswer(2);
                        SaveAnswer(3);
                        MessageBox.Show("Вопрос успешно добавлен", "Успех!",
                MessageBoxButton.OK, MessageBoxImage.Information);
                        questionTextTB.Text = null;
                        firstAnswerTB.Text = null;
                        secondAnswerTB.Text = null;
                        thirdAnswerTB.Text = null;
                        testSelectionCB.SelectedItem = null;
                        isCorrect1.IsChecked = false;
                        isCorrect2.IsChecked = false;
                        isCorrect3.IsChecked = false;
                    }
                    else
                    {
                        MessageBox.Show("Заполнены не все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    break;
                default:
                    break;
            }

        }

        private void SaveQuestion()
        {

            Question questionsToInsert = new Question();
            PropertyInfo[] qProperties = typeof(Question).GetProperties();
            foreach (PropertyInfo property in qProperties)
            {
                switch (property.Name)
                {
                    case "QuestionText":
                        property.SetValue(questionsToInsert, questionTextTB.Text.ToString());
                        break;
                    case "TestID":
                        property.SetValue(questionsToInsert, testSelectionCB.SelectedItem);
                        break;
                    case "NumOfCorrectAnswers":
                        property.SetValue(questionsToInsert, 1);
                        break;
                }
            }


            using (var db = new ApplicationContext())
            {
                db.Questions.Add(questionsToInsert);
                db.SaveChanges();
            }
            currQ = db.Questions.Where(i => i.QuestionID > 0).OrderByDescending(q => q.QuestionID).FirstOrDefault();
        }

        private void SaveAnswer(int q)
        {
            Answer answersToInsert = new Answer();
            PropertyInfo[] aProperties = typeof(Answer).GetProperties();
            answersToInsert = switchAnswers(aProperties, answersToInsert, q);



            using (var db = new ApplicationContext())
            {
                db.Answers.Add(answersToInsert);
                db.SaveChanges();

            }
        }

        private Answer switchAnswers(PropertyInfo[] prop, Answer toAdd, int q)
        {


            if (isCorrect1.IsChecked == true)
            {
                correctAnswer = "1";
            }
            else if (isCorrect2.IsChecked == true)
            {
                correctAnswer = "2";
            }
            else if (isCorrect3.IsChecked == true)
            {
                correctAnswer = "3";
            }

            string answerToInsert;
            switch (q)
            {
                case 1:
                    answerToInsert = firstAnswerTB.Text.ToString();
                    break;
                case 2:
                    answerToInsert = secondAnswerTB.Text.ToString();
                    break;
                case 3:
                    answerToInsert = thirdAnswerTB.Text.ToString();
                    break;
                default:
                    answerToInsert = string.Empty;
                    break;
            }

            foreach (PropertyInfo property in prop)
            {
                switch (property.Name)
                {
                    case "AnswerText":
                        property.SetValue(toAdd, answerToInsert);
                        break;
                    case "QuestionID":
                        property.SetValue(toAdd, currQ.QuestionID);
                        break;
                    case "IsCorrect":
                        if (correctAnswer == q.ToString())
                        {
                            property.SetValue(toAdd, "true");
                        }
                        else
                        {
                            property.SetValue(toAdd, "false");

                        }
                        break;
                }
            }
            return toAdd;
        }


        private void returnToAuthorizationButton_Click(object sender, RoutedEventArgs e)
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
    }
}
