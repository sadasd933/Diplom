using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

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

        public MainProgram()
        {
            InitializeComponent();
            db = new ApplicationContext();
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
                        numOfCorrectAnswers++;
                    }
                    break;

                case "2":
                    if (Ans2.IsChecked == true)
                    {
                        numOfCorrectAnswers++;
                    }
                    break;
                case "3":
                    if (Ans3.IsChecked == true)
                    {
                        numOfCorrectAnswers++;
                    }
                    break;

            }
            LoadQuestion();
        }

        public void LoadQuestion()
        {
            
            int[] arr = new int[10];
            for(int i = 0; i<arr.Length; i++)
            {
                questionIndex = rnd.Next(1, 11);
                if (arr.Contains(questionIndex))
                {
                    i--;
                }
                else
                {
                    arr[i] = questionIndex;
                }
            }


            Question currQ = null;
            using (ApplicationContext db = new ApplicationContext())
            {
                
                currQ = db.Questions.Where(b => b.QuestionID == questionIndex).FirstOrDefault();
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
