using System;
using SIT323_assignment_2___crozzle;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange expected result
            string difficultyExpectedEasy = "EASY";

            // arrange test data
            string difficulty = "EASY";
            Difficulty diff;

            // act
            diff = new Difficulty(difficulty);
            string acutalDiff = diff.GetActualDifficulty();

            // asert
            Assert.AreEqual(difficultyExpectedEasy,acutalDiff);
        }

        [TestMethod]
        public void TestMethod2()
        {
            // arrange expected result
            string expectedWord = "ROBERT";

            // arrange test data
            Word word;
            string originalWord = "ROBERT";

            // act
            word = new Word(originalWord);
            string actualWord = word.GetWord();

            // assert
            Assert.AreEqual(expectedWord,actualWord);
        }

        [TestMethod]
        public void TestMethod3()
        {
            // arrange expected result
            string expectedOrientationH = "HORIZONTAL";

            // arrange test data
            string orientation = "HORIZONTAL";
            string word = "ROBERT";
            Orientation ori;

            // act
            ori = new Orientation(word, orientation);
            string acutalOrientation = ori.GetActualOrientation();

            // assert
            Assert.AreEqual(expectedOrientationH,acutalOrientation);
        }

        [TestMethod]
        public void TestMethod4()
        {
            // arrange expected result
            Char expectedLetter = 'R';
            int expectedIndex = 0;

            // arrange test data
            Letter ltr;
            char letter = 'R';
            int index = 0;


            // act
            ltr = new Letter(letter, index);
            char actualLetter = ltr.GetLetter();
            int actualIndex = ltr.GetLetterIndex();

            // assert
            Assert.AreEqual(expectedIndex, actualIndex);
            Assert.AreEqual(expectedLetter,actualLetter);
            
        }
    }
}
