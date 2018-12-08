using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323_assignment_2___crozzle
{
    class ConfigLetterCheck
    {
        private static Log log = new Log(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString() + @"/log/Log.txt");
        private char letter;
        private bool intersect;
        private bool nonintersect;

        public ConfigLetterCheck(char ltr)
        {
            letter = ltr;
            intersect = false;
            nonintersect = false;
        }

        public bool CheckConfigLetterCheck()
        {
            bool flag;
            if (intersect)
            {
                flag = true;
            }
            else
            {
                flag = false;
                log.log("There is no setting for intersect letter " + letter + " in configuration.");
            }
            if (nonintersect)
            {
                flag = true;
            }
            else
            {
                flag = false;
                log.log("There is no setting for nonintersect letter " + letter + " in configuration.");
            }
            return flag;
        }

        public void SetIntersect()
        {
            intersect = true;
            return;
        }

        public void SetNonintersect()
        {
            nonintersect = true;
            return;
        }
    }
}
