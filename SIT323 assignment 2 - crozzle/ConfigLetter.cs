using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323_assignment_2___crozzle
{
    class ConfigLetter
    {
        private char letter;
        private double point;

        public ConfigLetter(char ltr, int pt)
        {
            letter = ltr;
            point = pt;
        }

        public char GetLetter()
        {
            return letter;
        }

        public double GetPoint()
        {
            return point;
        }
    }
}
