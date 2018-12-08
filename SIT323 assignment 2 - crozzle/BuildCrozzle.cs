using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIT323_assignment_2___crozzle
{
    class Crozzle
    {
        private static Log log = new Log(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString() + @"/log/Log.txt");

        private Difficulty difficulty;
        private WordList wordList = new WordList() { };
        private int rowLimit;
        private int columnLimit;
        private Configuration configuration;
      

        private const int settingIndex = 0;
        private const int wordListIndex = 1;

        private const int difficultyIndex = 0;
        private const int wordListNumberIndex = 1;
        private const int crozzleBoardRowNumberIndex = 2;
        private const int crozzleBoardColumnNumberIndex = 3;
        private List<BoardSimulation> finalChildren = new List<BoardSimulation> { };
        BoardSimulation highScoreChild;
        double highScore = 0;

        private int horizontalWordNumber = 0;
        private int verticalWordNumber = 0;

        public Crozzle(string path)
        {
            string[] file = System.IO.File.ReadAllLines(path);

            // set settings for initialize crozzle
            SetSetting(file[settingIndex]);

            // put words into list 
            wordList.SetWordList(file[wordListIndex]);         
        }

        private void SetSetting(string str)
        {
            string[] settingArray = Regex.Split(str, @",");
            difficulty = new Difficulty(settingArray[difficultyIndex]);
            try
            {
                wordList.SetSettedWordNumber(int.Parse(settingArray[wordListNumberIndex]));
            }
            catch
            {
                log.log("The setting for word list lenth is invalid.");
            }
            try
            {
                rowLimit = int.Parse(settingArray[crozzleBoardRowNumberIndex]);
            }
            catch
            {
                log.log("The setting for row number is invalid.");
            }
            try
            {
                columnLimit = int.Parse(settingArray[crozzleBoardColumnNumberIndex]);
            }
            catch
            {
                log.log("The setting for column number is invalid.");
            }
            return;
        }

        public void SetConfiguration(Configuration config)
        {
            configuration = config;
            return;
        }

        public void SetWordPoint()
        {
            foreach (Word wd in wordList.GetWordList())
            {
                foreach (Letter ltr in wd.GetLetterList())
                {
                    foreach (ConfigLetter configLtr in configuration.GetNonintersectLetterConfig())
                    {
                        if (ltr.GetLetter() == configLtr.GetLetter())
                        {
                            ltr.SetNonintersectedLetterPoint(configLtr.GetPoint());
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    foreach (ConfigLetter configLtr in configuration.GetIntersectLetterConfig())
                    {
                        if (ltr.GetLetter() == configLtr.GetLetter())
                        {
                            ltr.SetIntersectedLetterPoint(configLtr.GetPoint());
                            wd.AddWordPoint(configLtr.GetPoint());
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return;
        }

        public void RankWordByPoint()
        {
            bool flag;
            for (int i = wordList.GetWordList().Count - 1; i > 0; --i)
            {
                flag = true;
                for (int j = 0; j < i; ++j)
                {
                    if (wordList.GetWordList()[j].GetWordPoint() < wordList.GetWordList()[j + 1].GetWordPoint())
                    {  // swap
                        Word temp = wordList.GetWordList()[j];
                        wordList.GetWordList()[j] = wordList.GetWordList()[j + 1];
                        wordList.GetWordList()[j + 1] = temp;
                        flag = false;
                    }
                }
                if (flag)
                    break;
            }

            foreach (Word wd in wordList.GetWordList())
            {
                Console.WriteLine("word " + wd.GetWord() + " point: " + wd.GetWordPoint());
            }
            return;
        }

        //public void RankRandom()
        //{
        //    Random random = new Random();
        //    List<Word> randomList = new List<Word> { };
        //    foreach (Word wd in wordList.GetWordList())
        //    {
        //        randomList.Insert(random.Next(randomList.Count + 1), wd);
        //    }
        //    wordList.SetNewWordList(randomList);
        //}

        public bool StartCreateCrozzle()
        {
            BoardSimulation crozzle = new BoardSimulation(difficulty,columnLimit,rowLimit,wordList.GetWordList());
            FindFinalChild(crozzle);                     
            foreach (BoardSimulation bd in finalChildren)
            {
                if (bd.GetCurrentScore() > highScore)
                {
                    highScore = bd.GetCurrentScore();
                    highScoreChild = bd;
                }
            }
            foreach (Word wd in highScoreChild.GetWordsOnBoard())
            {
                if (wd.GetOrientation().GetActualOrientation() == wd.GetOrientation().GetExceptedOrientationH())
                {
                    horizontalWordNumber++;
                }
                else
                {
                    verticalWordNumber++;
                }
            }
            return true;    
        }

        private void FindFinalChild(BoardSimulation parent)
        {
            foreach (BoardSimulation child in parent.GetChildren())
            {
                if (child.GetHaveChildFlag())
                {
                    FindFinalChild(child);
                }
                else
                {
                    finalChildren.Add(child);
                }
            }
            Console.WriteLine("subsolutions number: " + finalChildren.Count);
            return;
        }

        public BoardSimulation GetFinalChild()
        {
            return highScoreChild;
        }

        public double GetScore()
        {
            double score;
            if (configuration.CheckConfiguration() && difficulty.CheckDifficulty() && wordList.CheckWordList())
            {
                score = highScore + highScoreChild.GetWordsOnBoard().Count * configuration.GetPointPerWord();
            }
            else
            {
                score = 0;
            }
            return score;
        }

        public int GetRowLimit()
        {
            return rowLimit;
        }

        public int GetColumnLimit()
        {
            return columnLimit;
        }

        public string GetOriginalWordList()
        {
            return wordList.GetWordsString();
        }

        public string GetCrozzleSettings()
        {
            return (difficulty.GetActualDifficulty() + "," + wordList.GetWordList().Count + "," + rowLimit + "," + columnLimit + "," + horizontalWordNumber + "," + verticalWordNumber);
        }
    }
}
