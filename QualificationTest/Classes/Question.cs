using System.ComponentModel.DataAnnotations;

namespace QualificationTest
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int NumOfCorrectAnswers { get; set; }
        public int TestID { get; set; }

        public Question() { }

        public Question(int questionID, string questionText, int numOfCorrectAnswers, int testID)
        {
            QuestionID = questionID;
            QuestionText = questionText;
            NumOfCorrectAnswers = numOfCorrectAnswers;
            TestID = testID;
        }
    }
}
