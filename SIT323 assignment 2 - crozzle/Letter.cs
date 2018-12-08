using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace SIT323_assignment_2___crozzle
{
    [Serializable]
    public class Letter
    {
        private string word;
        private char letter;
        private int letter_x;
        private int letter_y;
        private bool intersect;
        private int index;
        private double nonintersectedLetterPoint = 0;
        private double intersectedLetterPoint = 0;

        // constructor
        public Letter(string wd, char ltr,int idx, int x, int y)
        {
            word = wd;
            letter = ltr;
            index = idx;
            letter_x = x;
            letter_y = y;
            intersect = false;
        }

        public Letter(int x, int y)
        {
            letter = ' ';
            letter_x = x;
            letter_y = y;
            intersect = false;
        }

        public Letter(char ltr, int idx)
        {       
            intersect = false;
            letter = ltr;
            index = idx;
        }

        public int GetLetterIndex()
        {
            return index;
        }

        public void SetLetterX(int x)
        {
            letter_x = x;
            return;
        }

        public void SetLetterY(int y)
        {
            letter_y = y;
            return;
        }

        public void SetNonintersectedLetterPoint(double pt)
        {
            nonintersectedLetterPoint = pt;
            return;
        }

        public double GetLetterPoint()
        {
            double point;
            if (intersect)
                point = intersectedLetterPoint;
            else
                point = nonintersectedLetterPoint;
            return point;
        }

        public void SetIntersectedLetterPoint(double pt)
        {
            intersectedLetterPoint = pt;
            return;
        }

        public int GetLetterX()
        {
            return letter_x;
        }

        public int GetLetterY()
        {
            return letter_y;
        }

        public char GetLetter()
        {
            return letter;
        }

        public void SetIsIntersect()
        {
            intersect = true;
        }

        public bool GetIsIntersect()
        {
            return intersect;
        }

        public string GetWord()
        {
            return word;
        }
    }
}
