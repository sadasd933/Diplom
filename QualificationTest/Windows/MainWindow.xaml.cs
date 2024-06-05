using System.Windows;

namespace QualificationTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Content = new AuthorizationPage();
        }


    }
}



//string currentQuestionText;
//string currentAnswer1;
//string currentAnswer2;
//string currentAnswer3;
//string currentAnswerCorrect;
//int correctAnswersCount = 0;
//QuestionDb data;

//public MainWindow()
//{
//    InitializeComponent();
//    data = new QuestionDb();
//    LoadNewQuestion();
//}

//void CheckAnswer(string ch)
//{

//    if (ch == currentAnswerCorrect)
//    {
//        correctAnswersCount++;
//    }

//    LoadNewQuestion();
//}
//int a = 0;
//void LoadNewQuestion()
//{
//    if (a < 5)
//    {
//        a++;

//        var q = data.CurrentQuestion;
//        currentAnswer1 = q.Answer1;
//        currentAnswer2 = q.Answer2;
//        currentAnswer3 = q.Answer3;
//        currentQuestionText = q.QuestionText;
//        currentAnswerCorrect = q.AnswerCorrect;

//        QuestionTextTextBlock.Text = currentQuestionText.ToString();
//        QuestionAnswer1.Text = currentAnswer1.ToString();
//        QuestionAnswer2.Text = currentAnswer2.ToString();
//        QuestionAnswer3.Text = currentAnswer3.ToString();
//        Progress.Text = a.ToString() + "/5";
//    }
//    else
//    {
//        QuestionTextTextBlock.Text = "GZ\n\nВы набрали: " + correctAnswersCount.ToString() + " из 5 правильных ответов!";
//        QuestionAnswer1.Text = string.Empty;
//        QuestionAnswer2.Text = string.Empty;
//        QuestionAnswer3.Text = string.Empty;
//        Answer_TextBox.Visibility = Visibility.Hidden;
//        SubmitAnswer.Visibility = Visibility.Hidden;
//    }



//}

//private void SubmitAnswer_Click(object sender, RoutedEventArgs e)
//{
//    CheckAnswer(Answer_TextBox.Text.ToString());
//}
//    }
//*