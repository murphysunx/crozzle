using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIT323_assignment_2___crozzle
{
    [Serializable]
    public class Word : ICloneable
    {

        private string word;
        private int word_x = 0;
        private int word_y = 0;
        private Orientation orientation;
        private double wordPoint = 0;
     
        private int wordLenth;
        private List<Letter> letterList = new List<Letter> { };
        private int intersect;

        private const int orientationIndex = 0;
        private const int wordYIndex = 1;
        private const int wordXIndex = 2;
        private const int wordIndex = 3;

        public Word(string[] str)
        {       // constructor for completed crozzle file      
            word = str[wordIndex];
            orientation = new Orientation(word, str[orientationIndex]);
            word_x = int.Parse(str[wordXIndex]) - 1;
            word_y = int.Parse(str[wordYIndex]) - 1;
            intersect = 0;

            SetWordLetterList(word);
        }

        public Word(string str)
        {   // constructor for incompleted crozzle file
            word = str;
            intersect = 0;
            orientation = new Orientation(word);
            word_x = 0;
            word_y = 0;
            SetLetterList(word);
        }

        public void AddWordPoint(double pt)
        {
            wordPoint += pt;
        }

        public double GetWordPoint()
        {
            return wordPoint;
        }

        public void SetWordX(int x)
        {
            word_x = x;
            if (orientation.GetActualOrientation() == orientation.GetExceptedOrientationH())
            {
                int count = 0;
                foreach (Letter ltr in letterList)
                {
                    ltr.SetLetterX(x + count);
                    count++;
                }
            }
            else
            {
                foreach (Letter ltr in letterList)
                {
                    ltr.SetLetterX(x);
                }
            }
            return;
        }

        public void SetWordY(int y)
        {
            word_y = y;
            if (orientation.GetActualOrientation() == orientation.GetExceptedOrientationH())
            {
                foreach (Letter ltr in letterList)
                {
                    ltr.SetLetterY(y);
                }
            }
            else
            {
                int count = 0;
                foreach (Letter ltr in letterList)
                {
                    ltr.SetLetterY(y + count);
                    count++;
                }
            }
            return;
        }

        public int GetWordLenth()
        {
            return wordLenth;
        }

        public void SetIntersect()
        {
            intersect++;
            return;
        }

        public int GetIntersectTimes()
        {
            return intersect;
        }

        // set Letter List of every word
        private void SetWordLetterList(string wd)
        {
            char[] letterArray = wd.ToArray();
            wordLenth = letterArray.Count();
            int positionCount = 0;

            foreach (char ltr in letterArray)
            {
                if (orientation.GetActualOrientation() == orientation.GetExceptedOrientationH())
                {
                    Letter currentLetter = new Letter(wd, ltr, positionCount, word_x + positionCount, word_y);
                    letterList.Add(currentLetter);
                }
                else if (orientation.GetActualOrientation() == orientation.GetExceptedOrientationV())
                {
                    Letter currentLetter = new Letter(wd, ltr, positionCount,word_x, word_y + positionCount);
                    letterList.Add(currentLetter);
                }
                positionCount++;
            }
            return;
        }

        private void SetLetterList(string wd)
        {
            char[] letterArray = wd.ToArray();
            wordLenth = letterArray.Count();
            int positionCount = 0;

            foreach (char ltr in letterArray)
            {
                Letter currentLetter = new Letter(ltr, positionCount);
                letterList.Add(currentLetter);
                
                positionCount++;
            }
            return;
        }

        public string GetWord()
        {
            return word;
        }

        public Orientation GetOrientation()
        {
            return orientation;
        }

        public List<Letter> GetLetterList()
        {
            return letterList;
        }

        public int GetWord_x()
        {
            return word_x;
        }

        public int GetWord_y()
        {
            return word_y;
        }

        public bool CheckWordOrientation()
        {
            return orientation.CheckOrientation();
        }

        public Word DeepClone()
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as Word;
            }
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

    }
}
