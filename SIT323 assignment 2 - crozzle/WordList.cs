using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SIT323_assignment_2___crozzle
{
    class WordList
    {
        private static Log log = new Log(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString() + @"/log/Log.txt");
        private const int minimumWordNumber = 10;
        private const int maximumWordNumber = 1000;
        private int actualWordNumber;
        private int settedWordNumber;
        private List<Word> wordList = new List<Word> { };
        private string wordsString;

        public void SetWordList(string wdlist)
        {
            wordsString = wdlist;
            // split words into an array
            string[] wds = Regex.Split(wdlist, @",");

            Console.Write("word in word list: ");

            foreach (string word in wds)
            {
                // set word list
                Word wd = new Word(word);
                wordList.Add(wd);
                Console.Write(wd.GetWord() + " ");
   
            }
            Console.WriteLine(" ");
            // set acutal word number in word list
            actualWordNumber = wordList.Count;
            return;
        }

        public bool CheckWordList()
        {
            bool flag;
            if (CheckWordListWordNumber() && CheckWordListDuplicate())
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }

        private bool CheckWordListDuplicate()
        {
            bool flag = true;
            for(int m = 0; m < wordList.Count - 1; m++)
            {
                for (int n = m + 1; n < wordList.Count; n++)
                {
                    if (wordList[m] == wordList[n])
                    {
                        flag = false;
                        log.log("The word " + wordList[m] + " is duplicated in word list.");
                    }
                }
            }
            return flag;
        }

        private bool CheckWordListWordNumber()
        {
            bool flag;
            if (actualWordNumber >= minimumWordNumber && actualWordNumber <= maximumWordNumber)
            {
                if (actualWordNumber == settedWordNumber)
                {
                    flag = true;
                }
                else
                {
                    flag = false;
                    log.log("The actual word number is " + actualWordNumber + ", but the setted word number is " + settedWordNumber + ".");
                }
            }
            else
            {
                flag = false;
                log.log("Word number should be more than 10(inclusive) and less than 1000(inclusive).");
            }
            return flag;
        }

        public void SetSettedWordNumber(int num)
        {
            // set the setted word number in file
            settedWordNumber = num;
            Console.WriteLine("The setted word number: " + settedWordNumber);
            return;
        }

        public List<Word> GetWordList()
        {
            return wordList;
        }

        public string GetWordsString()
        {
            return wordsString;
        }
    }
}
