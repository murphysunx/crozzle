using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace SIT323_assignment_2___crozzle
{
    public partial class Form1 : Form
    {
        private Crozzle_File crozzleFile;
        private Configuration configuration;
        private Crozzle buildCrozzle;
        private WebBrowserDisplay webBrowserDisplay = new WebBrowserDisplay { };

        private System.Timers.Timer fiveMinuteTimer;
        private System.Timers.Timer progressTimer;


        public Form1()
        {
            InitializeComponent();

            // Setup 5 minute timer
            fiveMinuteTimer = new System.Timers.Timer(1000*60*5);     // 10 second limit
            fiveMinuteTimer.Elapsed += fiveMinuteTimer_Elapsed;

            // Setup 5 second progress timer
            progressTimer = new System.Timers.Timer(1000*60*5);        //  1 second progress limit
            progressTimer.Elapsed += progressTimer_Elapsed;
        }

        private void fiveMinuteTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            fiveMinuteTimer.Stop();
        }

        private void progressTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            progressTimer.Stop();
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            progressTimer.Stop();
            fiveMinuteTimer.Stop();
        }

        private void chooseFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
               crozzleFile = new Crozzle_File(theDialog.FileName.ToString());
            }
        }

        private void chooseConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.InitialDirectory = @"C:\";
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                configuration = new Configuration(theDialog.FileName.ToString());
            }
        }

        private void showScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (crozzleFile == null)
            {
                MessageBox.Show("Please choose crozzle file.", "Warning");
            }
            else if (configuration == null)
            {
                MessageBox.Show("Please choose configuration file.", "Warning");
            }
            else
            {
                double score = ComputeScore();
                Console.WriteLine("Score: " + score);
                webBrowser1.DocumentText = webBrowserDisplay.GetContent();
                webBrowserDisplay.ClearContent();
                // reset configuration
                configuration = null;
                // reset crozzle file
                crozzleFile = null;
            }
            return;
        }

        private double ComputeScore()
        {
            double score = 0;
            
            List<Word> wordList = crozzleFile.GetCrozzleWords();
            foreach (Word currentWord in wordList)
            {
                score += configuration.GetPointPerWord();
            }

            List<ConfigLetter> intersectLetterConfig = configuration.GetIntersectLetterConfig();
            List<ConfigLetter> nonintersectLetterConfig = configuration.GetNonintersectLetterConfig();

            List<List<Letter>> letterOnBoard = crozzleFile.GetCrozzleBoard().GetCrozzleBoard();
            foreach (List<Letter> currentLetterList in letterOnBoard)
            {
                webBrowserDisplay.AddContent("<tr>");

                foreach (Letter currentLetter in currentLetterList)
                {
                    webBrowserDisplay.AddContent("<td>" + currentLetter.GetLetter() + "</td>");    

                    if (currentLetter.GetLetter() == ' ')
                    {
                        continue;
                    }
                    else if (currentLetter.GetIsIntersect())   //add 
                    {
                        foreach (ConfigLetter currentIntersectConfigLetter in intersectLetterConfig)
                        {
                            if (currentIntersectConfigLetter.GetLetter() == currentLetter.GetLetter())
                            {
                                score += currentIntersectConfigLetter.GetPoint();
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    else if (!currentLetter.GetIsIntersect())  
                    {
                        foreach (ConfigLetter currentNonintersecConfigLetter in nonintersectLetterConfig)
                        {
                            if (currentNonintersecConfigLetter.GetLetter() == currentLetter.GetLetter())
                            {
                                score += currentNonintersecConfigLetter.GetPoint();
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }

                webBrowserDisplay.AddContent("</tr>");
            }
            if (!crozzleFile.CheckCrozzleFile() | !configuration.CheckConfiguration())
            {
                score = 0;
            }

            webBrowserDisplay.AddContent("<br>Score: " + score);
            webBrowserDisplay.AddContent("</table></body></html>");


            return score;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void buildCrozzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (configuration == null)
            {
                MessageBox.Show("Please choose configuration file first.", "Warning");
            }
            else
            {
                OpenFileDialog theDialog = new OpenFileDialog();
                theDialog.Title = "Open Text File";
                theDialog.Filter = "TXT files|*.txt";
                theDialog.InitialDirectory = @"C:\";
                if (theDialog.ShowDialog() == DialogResult.OK)
                {
                    fiveMinuteTimer.Start();
                    string filename = Path.GetFileNameWithoutExtension(theDialog.FileName.ToString());
                    // output txt file
                    Log log = new Log(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString() + @"/crozzle/" + filename + ".txt");
                    while (fiveMinuteTimer.Enabled)
                    {
                        progressTimer.Start();
                        buildCrozzle = new Crozzle(theDialog.FileName.ToString());
                        // get the opened file name without extension
                        
                        //Console.WriteLine(filename);
                        buildCrozzle.SetConfiguration(configuration);
                        buildCrozzle.SetWordPoint();
                        buildCrozzle.RankWordByPoint();

                        if (buildCrozzle.StartCreateCrozzle())
                        {
                            Console.WriteLine("current score: " + buildCrozzle.GetScore());
                            MessageBox.Show("Crozzle has been built.","Congratuation");
                            break;
                        }                            
                    }
                    BoardSimulation finalchild = buildCrozzle.GetFinalChild();
                    List<Word> crozzleWords = finalchild.GetWordsOnBoard();
                    if (finalchild.GetMinimumX() < 0)
                    {
                        foreach (Word wd in finalchild.GetWordsOnBoard())
                        {
                            wd.SetWordX(wd.GetWord_x() - finalchild.GetMinimumX());
                        }
                    }
                    else if (finalchild.GetMinimumX() > 0)
                    {
                        foreach (Word wd in finalchild.GetWordsOnBoard())
                        {
                            wd.SetWordX(finalchild.GetMinimumX() - wd.GetWord_x());
                        }
                    }
                    if (finalchild.GetMinimumY() < 0)
                    {
                        foreach (Word wd in finalchild.GetWordsOnBoard())
                        {
                            wd.SetWordY(wd.GetWord_y() - finalchild.GetMinimumY());
                        }
                    }
                    else if (finalchild.GetMinimumY() > 0)
                    {
                        foreach (Word wd in finalchild.GetWordsOnBoard())
                        {
                            wd.SetWordY(finalchild.GetMinimumY() - wd.GetWord_y());
                        }
                    }

                    CrozzleBoard board = new CrozzleBoard(buildCrozzle.GetRowLimit(), buildCrozzle.GetColumnLimit());

                    
                    // output crozzle settings
                    log.record(buildCrozzle.GetCrozzleSettings());
                    // output word list
                    log.record(buildCrozzle.GetOriginalWordList());
                    foreach (Word wd in finalchild.GetWordsOnBoard())
                    {
                        // crozzle words 
                        if (wd.GetOrientation().GetActualOrientation() == wd.GetOrientation().GetExceptedOrientationH())
                        {
                            // output horizontal words info
                            log.record(wd.GetOrientation().GetActualOrientation() + "," + (wd.GetWord_y() + 1) + "," + (wd.GetWord_x() + 1) + "," + wd.GetWord());
                        }
                        foreach (Letter ltr in wd.GetLetterList())
                        {
                            board.SetLetterOnBoard(ltr);
                        }
                    }
                    foreach (Word wd in finalchild.GetWordsOnBoard())
                    {
                        if (wd.GetOrientation().GetActualOrientation() == wd.GetOrientation().GetExceptedOrientationV())
                        {
                            // output vertical word
                            log.record(wd.GetOrientation().GetActualOrientation() + "," + (wd.GetWord_y() + 1) + "," + (wd.GetWord_x() + 1) + "," + wd.GetWord());
                        }
                    }
                    List<List<Letter>> current = board.GetCrozzleBoard();
                    
                    foreach (List<Letter> a in current)
                    {
                        webBrowserDisplay.AddContent("<tr>");
                        foreach (Letter b in a)
                        {
                            webBrowserDisplay.AddContent("<td>" + b.GetLetter() + "</td>");
                        }
                        webBrowserDisplay.AddContent("</tr>");
                    }
                    Console.WriteLine("score: " + buildCrozzle.GetScore());
                    webBrowserDisplay.AddContent("<br>Score: " + buildCrozzle.GetScore());
                    webBrowserDisplay.AddContent("</table></body></html>");
                    webBrowser1.DocumentText = webBrowserDisplay.GetContent();
                    webBrowserDisplay.ClearContent();
                }
            }
            // reset configuration
            configuration = null;
            return;
        }
    }
}
