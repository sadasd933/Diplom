using System.ComponentModel.DataAnnotations;

namespace QualificationTest.Classes
{
    public class Answer
    {
        [Key]
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public string IsCorrect { get; set; }
        public int QuestionID { get; set; }

        public Answer()
        {
        }
        public Answer(int answerID, string answerText, string isCorrect, int questionID)
        {
            AnswerID = answerID;
            AnswerText = answerText;
            AnswerID = answerID;
            AnswerText = answerText;
        }
    }
}
