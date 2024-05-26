using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualificationTest
{
    public class Test
    {
        [Key]
        public int TestID { get; set; }
        public int NumberOfQuestions { get; set; }
        public string Difficulty { get; set; }


        public Test() { }

        public Test(int testID, int numberOfQuestions, string difficulty)
        {
            TestID = testID;
            NumberOfQuestions = numberOfQuestions;
            Difficulty = difficulty;
        }
    }
}

