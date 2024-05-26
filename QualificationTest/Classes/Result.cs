using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualificationTest
{
    public class Result
    {
        [Key]
        public int ResultID { get; set; }
        public int PercentageOfCorrectAnswers{ get; set; }

        public Result() { }

        public Result(int resultID, int percentageOfCorrectAnswers)
        {
            ResultID = resultID;
            PercentageOfCorrectAnswers = percentageOfCorrectAnswers;
        }
    }
}

