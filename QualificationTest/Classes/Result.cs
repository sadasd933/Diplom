using System.ComponentModel.DataAnnotations;

namespace QualificationTest
{
    public class Result
    {
        [Key]
        public int ResultsID { get; set; }
        public string TesterName { get; set; }
        public int PercentageOfCorrectAnswers { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfCorrectAnswers { get; set; }

        public Result() { }

        public Result(int resultsID, string testerName, int percentageOfCorrectAnswers, int numberOfQuestions, int numberOfCorrectAnswers)
        {
            ResultsID = resultsID;
            TesterName = testerName;
            PercentageOfCorrectAnswers = percentageOfCorrectAnswers;
            NumberOfQuestions = numberOfQuestions;
            NumberOfCorrectAnswers = numberOfCorrectAnswers;
        }
    }
}

