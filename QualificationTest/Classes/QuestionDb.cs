using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QualificationTest
{
    class QuestionDb
    {
        private List<Question> db;


        private int index = -1;
        public Question CurrentQuestion
        {
            get
            {
                index++;
                if(index >= 5)
                {
                    return db[0];
                }
                return db[index % db.Count];
                
            }
        }

        public QuestionDb()
        {
            this.db = new List<Question>();
            this.index = 0;

            var dataFile = File.ReadAllLines(@"..\..\Images\QA.txt");

            foreach (var e in dataFile)
            {
                var args = e.Split('|');
                db.Add(new Question(args[0], args[1], args[2], args[3], args[4]));
            }
        }
    }
}
