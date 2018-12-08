using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323_assignment_2___crozzle
{
    public class Difficulty
    {
        private static Log log = new Log(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString() + @"/log/Log.txt");

        // three kinds of expected difficulty word
        private const string difficultyExpectedEasy = "EASY";
        private const string difficultyExpectedMedium = "MEDIUM";
        private const string difficultyExpectedHard = "HARD";

        // the real difficulty word which load from crozzle file
        private string actualDifficulty;

        public Difficulty(string diff)
        {
            actualDifficulty = diff;
            Console.WriteLine("Current difficulty: " + actualDifficulty);
        }

        public string GetDifficultyExpectedEasy()
        {
            return difficultyExpectedEasy;
        }

        public string GetDifficultyExpectedMedium()
        {
            return difficultyExpectedMedium;
        }

        public string GetDifficultyExpectedHard()
        {
            return difficultyExpectedHard;
        }

        public string GetActualDifficulty()
        {
            return actualDifficulty;
        }

        public bool CheckDifficulty()
        {
            bool difficultyFlag;
            if (actualDifficulty == difficultyExpectedEasy || actualDifficulty == difficultyExpectedHard || actualDifficulty == difficultyExpectedMedium)
            {
                difficultyFlag = true;
            }
            else
            {
                difficultyFlag = false;
                log.log("The difficulty of crozzle should be \"EASY\" or \"MEDIUM\" or \"HARD\".");
            }
            return difficultyFlag;
        }
    }
}
