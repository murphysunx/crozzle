using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323_assignment_2___crozzle
{
    class CrozzleBoard
    {
        private static Log log = new Log(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString() + @"/log/Log.txt");

        private const int minimumRowNumber = 4;
        private const int maxiumRowNumber = 400;
        private const int minimumColumnNumber = 8;
        private const int maximumColumnNumber = 800;
        private List<List<Letter>> crozzleBoard = new List<List<Letter>> { };

        private int row;
        private int col;

        // constructor
        public CrozzleBoard(int r, int c)
        {
            row = r;
            col = c;
            for (int rowCount = 0; rowCount < row; rowCount++)
            {
                List<Letter> currentList = new List<Letter> { };
                for (int colCount = 0; colCount < col; colCount++)
                {
                    Letter currentLetter = new Letter(rowCount,colCount);
                    currentList.Add(currentLetter);
                }
                crozzleBoard.Add(currentList);
            }
        }

        public void SetLetterOnBoard(Letter ltr)
        {
            try
            {
                crozzleBoard[ltr.GetLetterY()][ltr.GetLetterX()] = ltr;
            }
            catch
            {
                log.log("The Letter " + ltr.GetLetter() + " which belong to the word " + ltr.GetWord() + " is out of board.");
            }
            return;
        }

        public Letter GetLetterOnBoard(int x, int y)
        {
                return crozzleBoard[y][x];      
        }

        public List<List<Letter>> GetCrozzleBoard()
        {
            return crozzleBoard;
        }

        public bool CheckCrozzleBoard()
        {
            bool crozzleBoardSizeFlag;
            if (row >= minimumRowNumber && row <= maxiumRowNumber && col >= minimumColumnNumber && col <= maximumColumnNumber)
            {
                crozzleBoardSizeFlag = true;
            }
            else
            {
                crozzleBoardSizeFlag = false;
                log.log("The size of crozzle should be " + minimumRowNumber + "*" + minimumColumnNumber + " at least and " + maxiumRowNumber + "*" + maximumColumnNumber + " at most.");
            }
            return crozzleBoardSizeFlag;
        }
    }
}
