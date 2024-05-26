using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualificationTest
{
    public class UserAnswer
    {
        [Key]
        public int UserAnswerID { get; set; }
        public string CorrectAnswer{ get; set; }
        public string UsersAnswer { get; set; }
        public int UserID{ get; set; }
        public int QuestionID { get; set; }
        public int ResultID { get; set; }


        public UserAnswer() { }

        public UserAnswer(int userAnswerID, string correctAnswer, string usersAnswer,
            int userID, int questionID, int resultID)
        {
            UserAnswerID = userAnswerID;
            CorrectAnswer = correctAnswer;
            UsersAnswer = usersAnswer;
            UserID = userID;
            QuestionID = questionID;
            ResultID = resultID;
        }
    }
}

