using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIT323_assignment_2___crozzle
{
    class Crozzle_File
    {
        private static Log log = new Log(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString() + @"/log/Log.txt");

        private Difficulty difficulty;
        private WordList wordList = new WordList() { };
        private CrozzleBoard crozzleBoard;
        private List<Word> crozzleWords = new List<Word> { };
        private List<List<Word>> wordGroup = new List<List<Word>> { };


        private int groupNumber = 0;

        private const int GridSpaceLimitEasy = 1;

        private const int settingIndex = 0;
        private const int wordListIndex = 1;
        private const int crozzleWordIndex = 2;

        private const int difficultyIndex = 0;
        private const int wordListNumberIndex = 1;
        private const int crozzleBoardRowNumberIndex = 2;
        private const int crozzleBoardColumnNumberIndex = 3;
        private const int horizontalCrozzleWordNumberIndex = 4;
        private const int verticalCrozzleWordNumberIndex = 5;

        private const int minimumIntersectTimesEasy = 1;
        private const int maximumIntersectTimesEasy = 2;
        private const int minimumIntersectTimesMedium = 1;
        private const int maximumIntersectTimesMedium = 3;
        private const int minimumIntersectTimesHard = 1;

        private int settedHorizontalCrozzleWordNumber;
        private int settedVerticalCrozzleWordNumber;

        //constructor
        public Crozzle_File(string path)
        {
            //read crozzle data from file
            String[] crozzleData;
            crozzleData = System.IO.File.ReadAllLines(path);          

            //split crozzle data
            SetCrozzleData(crozzleData);
            if (!CheckCrozzleFile())
            {
                MessageBox.Show("The crozzle file is not complete. Please check it or choose another one.");
            }
            else
            {
                MessageBox.Show("Crozzle file loaded.","Notification");
            }
        }

        // classify setting and word list
        private void SetCrozzleData(string[] crozzleData)
        {
            string setting = "";

            for (int currentLine = 0; currentLine < crozzleData.Count(); currentLine++)
            {
                if (currentLine == settingIndex)
                {
                    setting = crozzleData[currentLine];
                    SetSetting(setting);
                }
                else if (currentLine == wordListIndex)
                {
                    wordList.SetWordList(crozzleData[currentLine]);
                }
                else if (currentLine >= crozzleWordIndex)
                {
                    string[] str = Regex.Split(crozzleData[currentLine], @",");
                    Word tempWord = new Word(str);
                    crozzleWords.Add(tempWord);                  
                }
            }
            // set setting 
            

            Console.WriteLine("word in crozzle:");
            foreach (Word tp in crozzleWords)
            {
                Console.WriteLine(tp.GetWord() + " " + tp.GetOrientation().GetActualOrientation() + " " + tp.GetWord_x() + " " + tp.GetWord_y());
            }

            SetCrozzleBoard(crozzleWords);

            SetWordGroup();
            Console.WriteLine("Group: " + groupNumber);

            SetCrozzleWordIntersectTimes();
            return;
        }

        // set settings
        private void SetSetting(string setting)
        {
            string[] settingArray = Regex.Split(setting, @",");

            difficulty = new Difficulty(settingArray[difficultyIndex]);
            try
            {
                wordList.SetSettedWordNumber(int.Parse(settingArray[wordListIndex]));
            }
            catch
            {
                log.log("The setting for word list lenth is invalid.");
            }           
            try
            {
                int row = int.Parse(settingArray[crozzleBoardRowNumberIndex]);
                int col = int.Parse(settingArray[crozzleBoardColumnNumberIndex]);
                crozzleBoard = new CrozzleBoard(row, col);
            }
            catch
            {
                log.log("The setting for crozzle board row and column " + settingArray[crozzleBoardRowNumberIndex] + "*" + settingArray[crozzleBoardColumnNumberIndex] + " is invalid.");
            }
            try
            {
                settedHorizontalCrozzleWordNumber = int.Parse(settingArray[horizontalCrozzleWordNumberIndex]);
            }
            catch
            {
                log.log("The setting for horizontal crozzle word number " + settingArray[horizontalCrozzleWordNumberIndex] + " is invalid.");
            }
            try
            {
                settedVerticalCrozzleWordNumber = int.Parse(settingArray[verticalCrozzleWordNumberIndex]);
            }
            catch
            {
                log.log("The setting for horizontal crozzle word number " + settingArray[verticalCrozzleWordNumberIndex] + " is invalid") ;
            }
            return;        
        }

        private bool CheckHorizontalVerticalCrozzleWordNumber()
        {
            bool flag;
            bool flagH;
            bool flagV;
            int horizontalCrozzleWordNumber = 0;
            int verticalCrozzleWordNumber = 0;
            foreach (Word wd in crozzleWords)
            {
                if (wd.GetOrientation().GetActualOrientation() == wd.GetOrientation().GetExceptedOrientationH())
                {
                    horizontalCrozzleWordNumber++;
                }
                else if (wd.GetOrientation().GetActualOrientation() == wd.GetOrientation().GetExceptedOrientationV())
                {
                    verticalCrozzleWordNumber++;
                }
            }
            if (settedHorizontalCrozzleWordNumber == horizontalCrozzleWordNumber)
            {
                flagH = true;
            }
            else
            {
                flagH = false;
                log.log("The number of horizontal word is " + horizontalCrozzleWordNumber + ", but the setted horizontal word number is " + settedHorizontalCrozzleWordNumber + ".");
            }
            if (settedVerticalCrozzleWordNumber == verticalCrozzleWordNumber)
            {
                flagV = true;
            }
            else
            {
                flagV = false;
                log.log("The number of vertical word is " + verticalCrozzleWordNumber + ", but the setted vertical word number is " + settedVerticalCrozzleWordNumber);
            }
            if (flagH && flagV)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private void SetCrozzleBoard(List<Word> wordList)
        {
            foreach (Word word in wordList)
            {
                List<Letter> currentWordLetterList = word.GetLetterList();
                foreach (Letter ltr in currentWordLetterList)
                {
                    try
                    {
                        if (crozzleBoard.GetLetterOnBoard(ltr.GetLetterX(), ltr.GetLetterY()).GetLetter() == ' ')
                        {
                            crozzleBoard.SetLetterOnBoard(ltr);
                            Console.WriteLine("The letter " + ltr.GetLetter() + " on " + ltr.GetLetterX() + ", " + ltr.GetLetterY() + " has been positioned.");
                        }
                        else
                        {
                            crozzleBoard.GetLetterOnBoard(ltr.GetLetterX(), ltr.GetLetterY()).SetIsIntersect();
                            ltr.SetIsIntersect();
                            Console.WriteLine("The letter " + ltr.GetLetter() + " is on " + ltr.GetLetterX() + ", " + ltr.GetLetterY() + " is intersected.");
                        }
                    }
                    catch
                    {
                        log.log("The letter " + ltr.GetLetter() + " is out of boundary.");
                        continue;
                    }
                    
                }
            }
            return;
        }

        public CrozzleBoard GetCrozzleBoard()
        {
            return crozzleBoard;
        }

        public List<Word> GetCrozzleWords()
        {
            return crozzleWords;
        }

        private bool CheckCrozzleWordBelongToList()
        {
            bool wordBelongToList = true;
            foreach (Word currentCrozzleWord in crozzleWords)
            {
                int count = 0;
                foreach (Word currentListWord in wordList.GetWordList())
                {
                    if (currentCrozzleWord.GetWord() == currentListWord.GetWord())
                    {
                        break;
                    }
                    else if (currentCrozzleWord.GetWord() != currentListWord.GetWord() && count ==  wordList.GetWordList().Count - 1)
                    {
                        wordBelongToList = false;
                        log.log("The word " + currentCrozzleWord.GetWord() + " does not belong to the word list.");
                    }
                    else
                    {
                        count++;
                    }
                }
            }
            return wordBelongToList;
        }

        private bool CheckCrozzleWordOrientation()
        {
            bool flag = true;
            foreach (Word wd in crozzleWords)
            {
                if (!wd.CheckWordOrientation())
                {
                    flag = false;
                }
            }
            return flag;
        }

        private void SetWordGroup()
        {
            foreach (Word temp in crozzleWords)
            {
                List<Word> group = new List<Word> { };
                group.Add(temp);
                wordGroup.Add(group);         
            }

            for (int m = 0; m < wordGroup.Count - 1; m++)
            {
                for (int n = m + 1; n < wordGroup.Count; n++)
                {
                    if (CheckWordListIntersect(wordGroup[m], wordGroup[n]))
                    {
                        for (int i = 0; i < wordGroup[m].Count; i++)
                        {
                            wordGroup[n].Add(wordGroup[m][i]);
                        }
                        wordGroup[m].Clear();
                        break;
                    }
                }
            }
            foreach (List<Word> TEMP in wordGroup)
            {
                if (TEMP.Count != 0)
                {
                    groupNumber++;             
                }
            }
            return;         
        }

        private bool CheckWordIntersect(Word a, Word b)
        {
            bool flag = false;
            foreach (Letter letterA in a.GetLetterList())
            {
                if (flag)
                {
                    break;
                }
                foreach (Letter letterB in b.GetLetterList())
                {
                    if (letterA.GetLetterX() == letterB.GetLetterX() && letterA.GetLetterY() == letterB.GetLetterY())
                    {
                        flag = true;
                        Console.WriteLine("word " + letterA.GetWord() + " intersect with word " + letterB.GetWord());
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return flag;
        }

        private bool CheckWordListIntersect(List<Word> a, List<Word> b)
        {
            bool flag = false;
            foreach (Word wordA in a)
            {
                if (flag)
                {
                    break;
                }
                foreach (Word wordB in b)
                {
                    if (CheckWordIntersect(wordA, wordB))
                    {
                        flag = true;
                        break;
                    }
                }
            }
            return flag;
        }

        public bool CheckCrozzleFile()
        {
            bool flag;
            if (difficulty.CheckDifficulty() & wordList.CheckWordList() & crozzleBoard.CheckCrozzleBoard() & CheckCrozzleWordBelongToList() & CheckCrozzleWordDuplication() & CheckCrozzleWordOrientation() & CheckHorizontalVerticalCrozzleWordNumber() & CheckCrozzleWordIntersectTimes() & CheckCrozzleWordOneGridSpace())
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private bool CheckCrozzleWordDuplication()
        {
            bool flag = true;
            for (int m = 0; m < crozzleWords.Count - 1; m++)
            {
                for (int n = m + 1; n < crozzleWords.Count; n++)
                {
                    if (crozzleWords[m].GetWord() == crozzleWords[n].GetWord())
                    {
                        flag = false;
                        log.log("The word " + crozzleWords[m].GetWord() + " is duplicated in crozzle.");
                    }
                }
            }
            return flag;
        }

        private void SetCrozzleWordIntersectTimes()
        {
            for (int m = 0; m < crozzleWords.Count - 1; m++)
            {
                for (int n = m + 1; n < crozzleWords.Count; n++)
                {
                    if (CheckWordIntersect(crozzleWords[m], crozzleWords[n]))
                    {
                        crozzleWords[m].SetIntersect();
                        crozzleWords[n].SetIntersect();
                    }
                }
            }
            return;
        }

        private bool CheckCrozzleWordIntersectTimes()
        {
            bool flag = true;
            if (difficulty.GetActualDifficulty() == difficulty.GetDifficultyExpectedEasy())
            {
                foreach (Word wd in crozzleWords)
                {
                    if (wd.GetIntersectTimes() >= minimumIntersectTimesEasy && wd.GetIntersectTimes() <= maximumIntersectTimesEasy)
                    {
                        continue;
                    }
                    else
                    {
                        flag = false;
                        log.log("The word " + wd.GetWord() + " does not match the intersec constraints.");
                    }
                }
            }
            else if (difficulty.GetActualDifficulty() == difficulty.GetDifficultyExpectedMedium())
            {
                foreach(Word wd in crozzleWords)
                {
                    if (wd.GetIntersectTimes() >= minimumIntersectTimesMedium && wd.GetIntersectTimes() <= maximumIntersectTimesMedium)
                    {
                        continue;
                    }
                    else
                    {
                        flag = false;
                        log.log("The word " + wd.GetWord() + " does not match the intersect constraints.");
                    }

                }
            }
            else if(difficulty.GetActualDifficulty() == difficulty.GetDifficultyExpectedHard())
            {
                foreach (Word wd in crozzleWords)
                {
                    if (wd.GetIntersectTimes() >= minimumIntersectTimesHard)
                    {
                        continue;
                    }
                    else
                    {
                        flag = false;
                        log.log("The word " + wd.GetWord() + " does not match the intersect constraints.");
                    }
                }
            }
            return flag;
        }

        private bool CheckCrozzleWordOneGridSpace()
        {
            bool flag = true;
            if (difficulty.GetActualDifficulty() == difficulty.GetDifficultyExpectedEasy())
            {
                for (int m = 0; m < crozzleWords.Count - 1; m++)
                {
                    for (int n = m + 1; n < crozzleWords.Count; n++)
                    {
                        if (!CheckWordInOneGridSpace(crozzleWords[m], crozzleWords[n]))
                        {
                            flag = false;
                            log.log("The word " + crozzleWords[m].GetWord() + " and " + crozzleWords[n].GetWord() + " do not match the one grid space constraints.");
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return flag;
        }

        private bool CheckWordInOneGridSpace(Word a, Word b)
        {
            bool flag = true;
            if (a.GetOrientation().GetActualOrientation() == a.GetOrientation().GetExceptedOrientationH() && b.GetOrientation().GetActualOrientation() == b.GetOrientation().GetExceptedOrientationH())
            {
                if (Math.Abs(a.GetWord_y() - b.GetWord_y()) == GridSpaceLimitEasy)
                {
                    if (a.GetWord_x() < b.GetWord_x())
                    {
                        if (a.GetWord_x() + a.GetWordLenth() >= b.GetWord_x())
                        {
                            flag = false;
                        }
                    }
                    else if (a.GetWord_x() == b.GetWord_x())
                    {
                        flag = false;
                    }
                    else if (a.GetWord_x() > b.GetWord_x())
                    {
                        if (b.GetWord_x() + b.GetWordLenth() >= a.GetWord_x())
                        {
                            flag = false;
                        }
                    }
                }
            }
            else if (a.GetOrientation().GetActualOrientation() == a.GetOrientation().GetExceptedOrientationV() && b.GetOrientation().GetActualOrientation() == b.GetOrientation().GetExceptedOrientationV())
            {
                if(Math.Abs(a.GetWord_x() - b.GetWord_x()) == GridSpaceLimitEasy)
                {
                    if (a.GetWord_y() < b.GetWord_y())
                    {
                        if (a.GetWord_y() + a.GetWordLenth() >= b.GetWord_y())
                        {
                            flag = false;
                        }
                    }
                    else if (a.GetWord_y() == b.GetWord_y())
                    {
                        flag = false;
                    }
                    else if (a.GetWord_y() > b.GetWord_y())
                    {
                        if (b.GetWord_y() + b.GetWordLenth() >= a.GetWord_y())
                        {
                            flag = false;
                        }
                    }
                }
            }
            return flag;
        }
    }
}
