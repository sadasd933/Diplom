using System.ComponentModel.DataAnnotations;

namespace QualificationTest
{
    public class Result
    {
        [Key]
        public int ResultsID { get; set; }
        public string TesterName { get; set; }
        public int PercentageOfCorrectAnswers { get; set; }

        public Result() { }

        public Result(int resultsID, string testerName, int percentageOfCorrectAnswers)
        {
            ResultsID = resultsID;
            TesterName = testerName;
            PercentageOfCorrectAnswers = percentageOfCorrectAnswers;
        }
    }
}

