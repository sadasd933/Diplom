using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualificationTest
{
    internal class Question
    {
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public string AnswerVariant1 { get; set; }
        public string AnswerVariant2 { get; set; }
        public string AnswerVariant3 { get; set; }
        public string AnswerImagePath { get; set; }
        public string CorrectAnswer {  get; set; }

        public Question() { }

        public Question(int questionID, string questionText, string answerVariant1, string answerVariant2, string answerVariant3, string answerImagePath, string correctAnswer)
        {
            QuestionID = questionID;
            QuestionText = questionText;
            AnswerVariant1 = answerVariant1;
            AnswerVariant2 = answerVariant2;
            AnswerVariant3 = answerVariant3;
            AnswerImagePath = answerImagePath;
            CorrectAnswer = correctAnswer;
        }
    }
}
