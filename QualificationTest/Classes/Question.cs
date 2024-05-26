using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace QualificationTest
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int NumOfCorrectAnswers { get; set; }
        public int TestID{ get; set; }

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
