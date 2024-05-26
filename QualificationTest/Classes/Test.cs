using System.ComponentModel.DataAnnotations;

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

