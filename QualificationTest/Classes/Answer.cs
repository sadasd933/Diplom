using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualificationTest.Classes
{
    public class Answer
    {
        [Key]
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public string IsCorrect{ get; set; }
        public int QuestionID { get; set; }

        public Answer()
        {
        }
        public Answer (int answerID, string answerText, string isCorrect, int questionID)
        {
            AnswerID = answerID;
            AnswerText = answerText;
            AnswerID = answerID;
            AnswerText = answerText;
        }
    }
}
