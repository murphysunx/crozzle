using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323_assignment_2___crozzle
{
    [Serializable]
    public class Orientation 
    {
        private static Log log = new Log(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString() + @"/log/Log.txt");
        private const string ExceptedOrientationH = "HORIZONTAL";
        private const string ExceptedOrientationV = "VERTICAL";
        private string ActualOrientation;
        private string word;

        // constructor
        public Orientation(string wd, string or)
        {
            word = wd;
            ActualOrientation = or;
        }

        public Orientation(string wd)
        {
            word = wd;
            // set the default orientation
            ActualOrientation = ExceptedOrientationH;
        }

        public void SetOrientation(string or)
        {
            ActualOrientation = or;
            return;
        }

        // Get the orientation of word
        public string GetActualOrientation()
        {
            return ActualOrientation;
        }

        public string GetExceptedOrientationH()
        {
            return ExceptedOrientationH;
        }

        public string GetExceptedOrientationV()
        {
            return ExceptedOrientationV;
        }

        public bool CheckOrientation()
        {
            bool flag;
            if (ActualOrientation == ExceptedOrientationH || ActualOrientation == ExceptedOrientationV)
            {
                flag = true;
            }
            else
            {
                flag = false;
                log.log("The  Orientation of " + word + " should be \"HORIZONTAL\" or \"VERTICAL\"");
            }
            return flag;
        }
    }
}
