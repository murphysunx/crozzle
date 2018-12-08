using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323_assignment_2___crozzle
{
    class BoardSimulation
    {
        private Difficulty difficulty;
        private int maxLenth; // to limit the board length(column number);
        private int maxWidth; // to limit the board width(row number);
        private int minimumX;
        private int maximumX;
        private int minimumY;
        private int maximumY;
        private double currentScore; // for clearing lower score BoardSimulation
        private const string expectedOrientationH = "HORIZONTAL";
        private const string expectedOrientationV = "VERTICAL";
        private List<Word> wordOnBoard;
        private List<BoardSimulation> childrenList = new List<BoardSimulation> { };
        private List<Word> wordList;
        private bool haveChild;
        private List<Word> accidentIntersectWords = new List<Word> { };
        private const int easyMaxIntersectTimes = 2;
        private const int mediumMaxIntersectTimes = 3;

        
        public BoardSimulation(Difficulty df, int lenLimt, int wdtLimt, List<Word> wdList)
        { // constructor from outside
            difficulty = df;
            maxLenth = lenLimt;
            maxWidth = wdtLimt;
            minimumX = 0;
            maximumX = 0;
            minimumY = 0;
            maximumY = 0;
            currentScore = 0;
            wordOnBoard = new List<Word> { };
            wordList = wdList;            
            haveChild = false;
            Initial();
        }

        public BoardSimulation(Difficulty df,int lenLimt, int wdtLimt, int miniX, int maxX, int miniY, int maxY, double currentScr, List<Word> wdList, List<Word> wdOnBoard)
        { // constructor from inside
            difficulty = df;
            maxLenth = lenLimt;
            maxWidth = wdtLimt;
            minimumX = miniX;
            maximumX = maxX;
            minimumY = miniY;
            maximumY = maxY;
            currentScore = currentScr;
            wordList = wdList;
            //iterateDataList = new List<IterateData> { };
            wordOnBoard = wdOnBoard;
            haveChild = false;           
            AddWord();
        }

        private void Initial()
        {
            // find the highest score word in word list
            currentScore = wordList.First().GetWordPoint();
            Word wd = wordList.First();

            Console.WriteLine("high score word : " + wd.GetWordPoint());
            Console.WriteLine("first word: " + wd.GetWord());
            // set temps for making a child
            // clone temp word
            Word tempWord = wd.DeepClone();
            // set coordination for temp word
            tempWord.SetWordX(0);
            tempWord.SetWordY(0);

            // set temp word on board
            List<Word> tempWordOnBoard = new List<Word> { };
            // add temp word into word on board list
            tempWordOnBoard.Add(tempWord);

            // set temp word list
            List<Word> tempWordList = new List<Word> { };
            foreach (Word tpwd in wordList)
            {
                if (tpwd.GetWord() == tempWord.GetWord())
                { // if current word is equal to temp word
                    continue;
                }
                else
                {
                    // clone word in word list 
                    Word temp = tpwd.DeepClone();
                    // put clone word into temp word list
                    tempWordList.Add(temp);
                }// end if
            }

            // set temp current minimum and maximu x,y
            int tempMinimumX = tempWord.GetWord_x();
            int tempMinimumY = tempWord.GetWord_y();
            int tempMaximumX = tempWord.GetWord_x() + tempWord.GetWordLenth();
            int tempMaximumY = tempWord.GetWord_y();

            // set temp current score
            double tempCurrentScore = 0;
            foreach (Letter ltr in tempWord.GetLetterList())
            {
                tempCurrentScore += ltr.GetLetterPoint();
            }

            // making a child
            BoardSimulation child = new BoardSimulation(difficulty, maxLenth, maxWidth, tempMinimumX, tempMaximumX, tempMinimumY, tempMaximumY, tempCurrentScore, tempWordList, tempWordOnBoard);
            // note that current node have child
            haveChild = true;
            // put the child into children list
            childrenList.Add(child);
            return;
        }

        private void AddWord()
        {
            bool flag = false;
            foreach (Word wdinlist in wordList)
            {            
                foreach (Word wdonboard in wordOnBoard)
                {
                    if (difficulty.GetActualDifficulty() == difficulty.GetDifficultyExpectedEasy())
                    {
                        if (wdonboard.GetIntersectTimes() >= easyMaxIntersectTimes)
                        {
                            continue;
                        }
                        else
                        {
                            if (CheckWordIntersect(wdonboard, wdinlist))
                            {
                                flag = true;
                                //break;
                            }
                        }
                    }
                    else if (difficulty.GetActualDifficulty() == difficulty.GetDifficultyExpectedMedium())
                    {
                        if (wdonboard.GetIntersectTimes() >= mediumMaxIntersectTimes)
                        {
                            continue;
                        }
                        else
                        {
                            if (CheckWordIntersect(wdonboard, wdinlist))
                            {
                                flag = true;
                                //break;
                            }
                        }
                    }
                    else
                    {
                        if (CheckWordIntersect(wdonboard, wdinlist))
                        {
                            flag = true;
                            //break;
                        }
                    }

                }
                if (flag)
                {
                    break;
                }
            }
            return;
        }

        private bool CheckWordIntersect(Word wdonboard, Word wordinlist)
        { // check if words can be intersected. if yes, make a child
            bool flag = false;            
            for(int i = 0; i < wdonboard.GetLetterList().Count; i++)
            {
                if (wdonboard.GetLetterList()[i].GetIsIntersect())
                { // if letter has been intersected
                    continue;
                }
                if (i == 0)
                {
                    if (wdonboard.GetLetterList()[i + 1].GetIsIntersect())
                    {
                        continue;
                    }
                }
                else if (i == wdonboard.GetLetterList().Count - 1)
                {
                    if (wdonboard.GetLetterList()[i - 1].GetIsIntersect())
                    {
                        continue;
                    }
                }
                else
                {
                    if (wdonboard.GetLetterList()[i + 1].GetIsIntersect() || wdonboard.GetLetterList()[i - 1].GetIsIntersect())
                    {
                        continue;
                    }
                }
                foreach (Letter lb in wordinlist.GetLetterList())
                {                   
                    if (wdonboard.GetLetterList()[i].GetLetter() == lb.GetLetter())
                    { // two word have the same letter
                        if (CheckBoundary(wdonboard.GetWord_x(), wdonboard.GetWord_y(), lb.GetLetterIndex(), wordinlist.GetWordLenth(), wdonboard.GetOrientation().GetActualOrientation()) && CheckGridAvailability(wdonboard.GetLetterList()[i],wdonboard,lb.GetLetterIndex(),wordinlist,wordOnBoard))
                        { // if this inserted word will not be out of boundary, then set temp for making a child
                          //clone the inserted word

                            Word tempWord = wordinlist.DeepClone();                           
                            // set insert word coordination
                            if (wdonboard.GetOrientation().GetActualOrientation() == expectedOrientationH)
                            { // if the word on board is horizontal
                                // set ori
                                tempWord.GetOrientation().SetOrientation(expectedOrientationV);
                                // set X
                                tempWord.SetWordX(wdonboard.GetWord_x() + i);
                                // set Y
                                tempWord.SetWordY(wdonboard.GetWord_y() - lb.GetLetterIndex());
                                                             
                            } // end if
                            else
                            {
                                // set ori
                                tempWord.GetOrientation().SetOrientation(expectedOrientationH);
                                // set X
                                tempWord.SetWordX(wdonboard.GetWord_x() - lb.GetLetterIndex());
                                // set Y
                                tempWord.SetWordY(wdonboard.GetWord_y() + i);

                            }
                            // set temp word intersect
                            tempWord.SetIntersect();

                            // set updated word on board list
                            List<Word> tempWordOnBoard = new List<Word> { };
                            // add the original words on board

                            foreach (Word wd in wordOnBoard)
                            {
                                bool haveAccident = false;
                                foreach (Word ac in accidentIntersectWords)
                                {
                                    if (ac.GetWord() == wd.GetWord())
                                    {
                                        Word currentWord = wd.DeepClone();
                                        currentWord.SetIntersect();
                                        tempWord.SetIntersect();
                                        tempWordOnBoard.Add(currentWord);
                                        haveAccident = true;
                                        break;
                                    }
                                }
                                if (haveAccident)
                                {
                                    continue;
                                }
                                if (wd.GetWord() == wdonboard.GetWord())
                                { // set word intersected
                                    Word currentWord = wd.DeepClone();
                                    currentWord.SetIntersect();
                                    tempWordOnBoard.Add(currentWord);
                                } // end if
                                else
                                {
                                    Word currentWord = wd.DeepClone();
                                    tempWordOnBoard.Add(currentWord);
                                }
                                
                                                               
                            }
                            // clear haveaccident list
                            accidentIntersectWords.Clear();

                            // add the new word
                            tempWordOnBoard.Add(tempWord);
                            Console.WriteLine("word " + tempWord.GetWord() + "added.");

                            // set temp word list
                            List<Word> tempWordList = new List<Word> { };
                            foreach (Word wd in wordList)
                            {
                                if (wd.GetWord() == tempWord.GetWord())
                                {
                                    continue;
                                }
                                else
                                {
                                    Word temp = wd.DeepClone();
                                    tempWordList.Add(temp);
                                }
                            }

                            // set temp current score
                            double tempCurrentScore = currentScore - wdonboard.GetLetterList()[i].GetLetterPoint();
                            tempWord.GetLetterList()[lb.GetLetterIndex()].SetIsIntersect();
                            foreach (Letter ltr in tempWord.GetLetterList())
                            {
                                tempCurrentScore += ltr.GetLetterPoint();
                            }

                            // set temp current minimum X,Y
                            int tempMinimumX = minimumX;
                            int tempMinimumY = minimumY;
                            int tempMaximumX = maximumX;
                            int tempMaximumY = maximumY;
                            if (tempWord.GetOrientation().GetActualOrientation() == expectedOrientationH)
                            {
                                int firstX = wdonboard.GetWord_x() - lb.GetLetterIndex();
                                int lastX = wdonboard.GetWord_x() + wordinlist.GetWordLenth() - lb.GetLetterIndex() - 1;
                                if (firstX < minimumX)
                                {
                                    //Console.WriteLine(tempWord.GetWord() + " " + firstX + " " + minimumX);
                                    tempMinimumX = firstX;
                                }
                                if (lastX > maximumX)
                                {
                                    //Console.WriteLine(tempWord.GetWord() + " " + firstX + " " + maximumX);
                                    tempMaximumX = lastX;
                                }
                            }
                            else
                            {
                                int firstY = wdonboard.GetWord_y() - lb.GetLetterIndex();
                                int lastY = wdonboard.GetWord_y() + wordinlist.GetWordLenth() - lb.GetLetterIndex() - 1;
                                if (firstY < minimumY)
                                {
                                    tempMinimumY = firstY;
                                }
                                if (lastY > maximumY)
                                {
                                    tempMaximumY = lastY;
                                }
                            }
                            flag = true;
                            BoardSimulation child = new BoardSimulation(difficulty, maxLenth, maxWidth, tempMinimumX, tempMaximumX, tempMinimumY, tempMaximumY, tempCurrentScore, tempWordList, tempWordOnBoard);
                            haveChild = true;
                            childrenList.Add(child);
                        }
                    }                
                }               
            }
            return flag;
        }

        private bool CheckBoundary(int wdonboardX, int wdonboardY, int insertltrIndex, int insertwordLenth, string or)
        {
            bool flag = true;
            if (or == expectedOrientationV)
            {
                int firstX = wdonboardX - insertltrIndex;
                int lastX = wdonboardX + insertwordLenth - insertltrIndex - 1;
                if (firstX < minimumX && lastX <= maximumX)
                {
                    if (maximumX - firstX + 1 > maxLenth)
                    {
                        flag = false;
                    }
                }
                else if (firstX < minimumX && lastX > maximumX)
                {
                    if (lastX - firstX + 1 > maxLenth)
                    {
                        flag = false;
                    }
                }
                else if (firstX >= minimumX && lastX > maximumX)
                {
                    if (lastX - minimumX + 1 > maxLenth)
                    {
                        flag = false;
                    }
                }
                else if (firstX > maximumX)
                {
                    if (lastX - minimumX + 1 > maxLenth)
                    {
                        flag = false;
                    }
                }
                else if (lastX < minimumX)
                {
                    if (maximumX - firstX + 1 > maxLenth)
                    {
                        flag = false;
                    }
                }
            }
            else
            {
                int firstY = wdonboardY - insertltrIndex;
                int lastY = wdonboardY + insertwordLenth - insertltrIndex - 1;
                if (firstY < minimumY && lastY <= maximumY)
                {
                    if (maximumY - firstY + 1 > maxWidth)
                    {
                        flag = false;
                    }
                }
                else if (firstY < minimumY && lastY > maximumY)
                {
                    if (lastY - firstY + 1 > maxWidth)
                    {
                        flag = false;
                    }
                }
                else if (firstY >= minimumY && lastY > maximumY)
                {
                    if (lastY - minimumY + 1 > maxWidth)
                    {
                        flag = false;
                    }
                }
                else if (firstY > maximumY)
                {
                    if (lastY - minimumY + 1 > maxWidth)
                    {
                        flag = false;
                    }
                }
                else if (lastY < minimumY)
                {
                    if (maximumY - firstY + 1 > maxWidth)
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        private bool CheckGridAvailability(Letter ltronboard, Word wdonboard, int insertltrIndex, Word insertword, List<Word> wordsonboard)
        {
            bool flag = true;            
            if (wdonboard.GetOrientation().GetActualOrientation() == expectedOrientationV)
            {
                int firstX = wdonboard.GetWord_x() - insertltrIndex;
                int lastX = wdonboard.GetWord_x() + insertword.GetWordLenth() - insertltrIndex - 1;
                int Y = ltronboard.GetLetterY();
                foreach (Word wd in wordsonboard)
                {
                    if (wd.GetWord() == wdonboard.GetWord())
                    {
                        continue;
                    }
                    if (wd.GetOrientation().GetActualOrientation() == expectedOrientationH)
                    { // same orientation
                        if (wd.GetWord_y() == Y - 1 || wd.GetWord_y() == Y + 1)
                        {
                            foreach (Letter ltr in wd.GetLetterList())
                            {
                                if (difficulty.GetActualDifficulty() == difficulty.GetDifficultyExpectedEasy())
                                {
                                    if (ltr.GetLetterX() >= firstX - 1 && ltr.GetLetterX() <= lastX + 1)
                                    {
                                        flag = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (ltr.GetLetterX() >= firstX && ltr.GetLetterX() <= lastX)
                                    {
                                        flag = false;
                                        break;
                                    }
                                }

                            }
                        }
                        else if (wd.GetWord_y() == Y)
                        {
                            foreach (Letter ltr in wd.GetLetterList())
                            {
                                if (ltr.GetLetterX() >= firstX - 1 && ltr.GetLetterX() <= lastX + 1)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    { // different orientation
                        if (wd.GetWord_x() == firstX - 1 || wd.GetWord_x() == lastX + 1)
                        {
                            foreach (Letter ltr in wd.GetLetterList())
                            {
                                if (ltr.GetLetterY() == Y)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        else if (wd.GetWord_x() >= firstX && wd.GetWord_x() <= lastX)
                        { // word between head and tail
                            foreach (Letter ltr in wd.GetLetterList())
                            {
                                if (ltr.GetLetterY() == Y - 1 && ltr.GetLetterIndex() == wd.GetWordLenth() - 1)
                                { // it is the last letter and next to the insert word
                                    flag = false;
                                    break;
                                }
                                else if (ltr.GetLetterY() == Y + 1 && ltr.GetLetterIndex() == 0)
                                {
                                    flag = false;
                                    break;
                                }
                                else if (ltr.GetLetterY() == Y)
                                {
                                    if (insertword.GetLetterList()[ltr.GetLetterX() - firstX].GetLetter() != ltr.GetLetter())
                                    {
                                        flag = false;
                                        break;
                                    }
                                    else
                                    { // same letter
                                        if (difficulty.GetActualDifficulty() == difficulty.GetDifficultyExpectedEasy())
                                        {
                                            if (wd.GetIntersectTimes() >= easyMaxIntersectTimes)
                                            { // EASY intersect constraints
                                                flag = false;
                                                break;
                                            }
                                            else
                                            {
                                                accidentIntersectWords.Add(wd);
                                            }
                                        }
                                        else if (difficulty.GetActualDifficulty() == difficulty.GetDifficultyExpectedMedium())
                                        {
                                            if (wd.GetIntersectTimes() >= mediumMaxIntersectTimes)
                                            { // EASY intersect constraints
                                                flag = false;
                                                break;
                                            }
                                            else
                                            {
                                                accidentIntersectWords.Add(wd);
                                            }
                                        }
                                        else
                                        {
                                            accidentIntersectWords.Add(wd);
                                        }                                       
                                    }
                                }
                            }
                        }
                    }                 
                    if (!flag)
                    {
                        break;
                    }
                }
            }
            else
            { // insert word is vertical
                int X = ltronboard.GetLetterX();
                int firstY = wdonboard.GetWord_y() - insertltrIndex;
                int lastY = wdonboard.GetWord_y() + insertword.GetWordLenth() - insertltrIndex - 1;
                foreach (Word wd in wordsonboard)
                {
                    if (wd.GetWord() == wdonboard.GetWord())
                    {
                        continue;
                    }
                    if (wd.GetOrientation().GetActualOrientation() == expectedOrientationV)
                    {
                        if (wd.GetWord_x() == X - 1 || wd.GetWord_x() == X + 1)
                        {
                            foreach (Letter ltr in wd.GetLetterList())
                            {
                                if (difficulty.GetActualDifficulty() == difficulty.GetDifficultyExpectedEasy())
                                {
                                    if (ltr.GetLetterY() >= firstY - 1 && ltr.GetLetterY() <= lastY + 1)
                                    {
                                        flag = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    if (ltr.GetLetterY() >= firstY && ltr.GetLetterY() <= lastY)
                                    {
                                        flag = false;
                                        break;
                                    }
                                }

                            }
                        }
                        else if (wd.GetWord_x() == X)
                        {
                            foreach (Letter ltr in wd.GetLetterList())
                            {
                                if (ltr.GetLetterY() >= firstY - 1 && ltr.GetLetterY() <= lastY + 1)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    { // word can be intersect with inserted word
                        if (wd.GetWord_y() == firstY - 1 || wd.GetWord_y() == lastY + 1)
                        {
                            foreach (Letter ltr in wd.GetLetterList())
                            {
                                if (ltr.GetLetterX() == X)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                        }
                        else if (wd.GetWord_y() >= firstY && wd.GetWord_y() <= lastY)
                        {
                            foreach (Letter ltr in wd.GetLetterList())
                            {
                                if (ltr.GetLetterX() == X - 1 && ltr.GetLetterIndex() == wd.GetWordLenth() - 1)
                                {
                                    flag = false;
                                    break;
                                }
                                else if (ltr.GetLetterX() == X + 1 && ltr.GetLetterIndex() == 0)
                                {
                                    flag = false;
                                    break;
                                }
                                else if (ltr.GetLetterX() == X)
                                {
                                    if (ltr.GetLetter() != insertword.GetLetterList()[ltr.GetLetterY() - firstY].GetLetter())
                                    {
                                        flag = false;
                                        break;
                                    }
                                    else
                                    {
                                        if (difficulty.GetActualDifficulty() == difficulty.GetDifficultyExpectedEasy())
                                        {
                                            if (wd.GetIntersectTimes() == easyMaxIntersectTimes)
                                            {
                                                flag = false;
                                                break;
                                            }
                                            else
                                            {
                                                accidentIntersectWords.Add(wd);
                                            }
                                        }
                                        else if (difficulty.GetActualDifficulty() == difficulty.GetDifficultyExpectedMedium())
                                        {
                                            if (wd.GetIntersectTimes() == mediumMaxIntersectTimes)
                                            {
                                                flag = false;
                                                break;
                                            }
                                            else
                                            {
                                                
                                            }
                                        }
                                        else
                                        {
                                            accidentIntersectWords.Add(wd);
                                        }
                                        
                                        
                                    }
                                } 
                            }
                        }
                    }
                    if (!flag)
                    {
                        break;
                    }
                }
            }
            return flag;
        }

        public bool GetHaveChildFlag()
        {
            return haveChild;
        }

        public List<BoardSimulation> GetChildren()
        {
            return childrenList;
        }

        public double GetCurrentScore()
        {
            return currentScore;
        }

        public List<Word> GetWordsOnBoard()
        {
            return wordOnBoard;
        }

        public int GetMinimumX()
        {
            return minimumX;
        }

        public int GetMinimumY()
        {
            return minimumY;
        }
    }


}
