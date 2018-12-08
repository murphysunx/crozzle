using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIT323_assignment_2___crozzle
{
    class Configuration
    {
        private static Log log = new Log(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.Environment.CurrentDirectory).ToString()).ToString()).ToString() + @"/log/Log.txt");

        private List<ConfigLetter> intersectLetterConfig = new List<ConfigLetter> { };
        private List<ConfigLetter> nonintersectLetterConfig = new List<ConfigLetter> { };

        private List<ConfigLetterCheck> configLetterCheck = new List<ConfigLetterCheck> { };

        private const char A = 'A';
        private const char B = 'B';
        private const char C = 'C';
        private const char D = 'D';
        private const char E = 'E';
        private const char F = 'F';
        private const char G = 'G';
        private const char H = 'H';
        private const char I = 'I';
        private const char J = 'J';
        private const char K = 'K';
        private const char L = 'L';
        private const char M = 'M';
        private const char N = 'N';
        private const char O = 'O';
        private const char P = 'P';
        private const char Q = 'Q';
        private const char R = 'R';
        private const char S = 'S';
        private const char T = 'T';
        private const char U = 'U';
        private const char V = 'V';
        private const char W = 'W';
        private const char X = 'X';
        private const char Y = 'Y';
        private const char Z = 'Z';

        private ConfigLetterCheck CheckA;
        private ConfigLetterCheck CheckB;
        private ConfigLetterCheck CheckC;
        private ConfigLetterCheck CheckD;
        private ConfigLetterCheck CheckE;
        private ConfigLetterCheck CheckF;
        private ConfigLetterCheck CheckG;
        private ConfigLetterCheck CheckH;
        private ConfigLetterCheck CheckI;
        private ConfigLetterCheck CheckJ;
        private ConfigLetterCheck CheckK;
        private ConfigLetterCheck CheckL;
        private ConfigLetterCheck CheckM;
        private ConfigLetterCheck CheckN;
        private ConfigLetterCheck CheckO;
        private ConfigLetterCheck CheckP;
        private ConfigLetterCheck CheckQ;
        private ConfigLetterCheck CheckR;
        private ConfigLetterCheck CheckS;
        private ConfigLetterCheck CheckT;
        private ConfigLetterCheck CheckU;
        private ConfigLetterCheck CheckV;
        private ConfigLetterCheck CheckW;
        private ConfigLetterCheck CheckX;
        private ConfigLetterCheck CheckY;
        private ConfigLetterCheck CheckZ;


        private int groupsPerCrozzleLimit;
        private int pointPerWord;

        public Configuration(string path)
        {
            CheckA = new ConfigLetterCheck(A);
            CheckB = new ConfigLetterCheck(B);
            CheckC = new ConfigLetterCheck(C);
            CheckD = new ConfigLetterCheck(D);
            CheckE = new ConfigLetterCheck(E);
            CheckF = new ConfigLetterCheck(F);
            CheckG = new ConfigLetterCheck(G);
            CheckH = new ConfigLetterCheck(H);
            CheckI = new ConfigLetterCheck(I);
            CheckJ = new ConfigLetterCheck(J);
            CheckK = new ConfigLetterCheck(K);
            CheckL = new ConfigLetterCheck(L);
            CheckM = new ConfigLetterCheck(M);
            CheckN = new ConfigLetterCheck(N);
            CheckO = new ConfigLetterCheck(O);
            CheckP = new ConfigLetterCheck(P);
            CheckQ = new ConfigLetterCheck(Q);
            CheckR = new ConfigLetterCheck(R);
            CheckS = new ConfigLetterCheck(S);
            CheckT = new ConfigLetterCheck(T);
            CheckU = new ConfigLetterCheck(U);
            CheckV = new ConfigLetterCheck(V);
            CheckW = new ConfigLetterCheck(W);
            CheckX = new ConfigLetterCheck(X);
            CheckY = new ConfigLetterCheck(Y);
            CheckZ = new ConfigLetterCheck(Z);

            string[] configurationData = System.IO.File.ReadAllLines(path);
            SetConfiguration(configurationData);

            Console.WriteLine("GROUPSPERCROZZLELIMIT: " + groupsPerCrozzleLimit);
            Console.WriteLine("POINTPERWORD: " + pointPerWord);

            Console.WriteLine("Intersect Letter Configuration: ");
            foreach (ConfigLetter temp in intersectLetterConfig)
            {
                Console.WriteLine("Intersect Letter " + temp.GetLetter() + ": " + temp.GetPoint());
            }

            Console.WriteLine("Nonintersect Letter Configuration: ");
            foreach (ConfigLetter temp in nonintersectLetterConfig)
            {
                Console.WriteLine("Nonintersect Letter " + temp.GetLetter() + ": " + temp.GetPoint());
            }
            if (!CheckConfiguration())
            {
                MessageBox.Show("The configuratino file is not complete. Please check it or choose another one.", "Warning");
            }
            else
            {
                MessageBox.Show("Configuration loaded.","Notification");
            }
            
        }

        private void SetIntersectCheck(char ltr)
        {
            if (ltr == A)
            {
                CheckA.SetIntersect();
            }
            else if (ltr == B)
            {
                CheckB.SetIntersect();
            }
            else if (ltr == C)
            {
                CheckC.SetIntersect();
            }
            else if (ltr == D)
            {
                CheckD.SetIntersect();
            }
            else if (ltr == E)
            {
                CheckE.SetIntersect();
            }
            else if (ltr == F)
            {
                CheckF.SetIntersect();
            }
            else if (ltr == G)
            {
                CheckG.SetIntersect();
            }
            else if (ltr == H)
            {
                CheckH.SetIntersect();
            }
            else if (ltr == I)
            {
                CheckI.SetIntersect();
            }
            else if (ltr == J)
            {
                CheckJ.SetIntersect();
            }
            else if (ltr == K)
            {
                CheckK.SetIntersect();
            }
            else if (ltr == L)
            {
                CheckL.SetIntersect();
            }
            else if (ltr == M)
            {
                CheckM.SetIntersect();
            }
            else if (ltr == N)
            {
                CheckN.SetIntersect();
            }
            else if (ltr == O)
            {
                CheckO.SetIntersect();
            }
            else if (ltr == P)
            {
                CheckP.SetIntersect();
            }
            else if (ltr == Q)
            {
                CheckQ.SetIntersect();
            }
            else if (ltr == R)
            {
                CheckR.SetIntersect();
            }
            else if (ltr == S)
            {
                CheckS.SetIntersect();
            }
            else if (ltr == T)
            {
                CheckT.SetIntersect();
            }
            else if (ltr == U)
            {
                CheckU.SetIntersect();
            }
            else if (ltr == V)
            {
                CheckV.SetIntersect();
            }
            else if (ltr == W)
            {
                CheckW.SetIntersect();
            }
            else if (ltr == X)
            {
                CheckX.SetIntersect();
            }
            else if (ltr == Y)
            {
                CheckY.SetIntersect();
            }
            else if (ltr == Z)
            {
                CheckZ.SetIntersect();
            }
            return;
        }

        private void SetNonintersectCheck(char ltr)
        {
            if (ltr == A)
            {
                CheckA.SetNonintersect();
            }
            else if (ltr == B)
            {
                CheckB.SetNonintersect();
            }
            else if (ltr == C)
            {
                CheckC.SetNonintersect();
            }
            else if (ltr == D)
            {
                CheckD.SetNonintersect();
            }
            else if (ltr == E)
            {
                CheckE.SetNonintersect();
            }
            else if (ltr == F)
            {
                CheckF.SetNonintersect();
            }
            else if (ltr == G)
            {
                CheckG.SetNonintersect();
            }
            else if (ltr == H)
            {
                CheckH.SetNonintersect();
            }
            else if (ltr == I)
            {
                CheckI.SetNonintersect();
            }
            else if (ltr == J)
            {
                CheckJ.SetNonintersect();
            }
            else if (ltr == K)
            {
                CheckK.SetNonintersect();
            }
            else if (ltr == L)
            {
                CheckL.SetNonintersect();
            }
            else if (ltr == M)
            {
                CheckM.SetNonintersect();
            }
            else if (ltr == N)
            {
                CheckN.SetNonintersect();
            }
            else if (ltr == O)
            {
                CheckO.SetNonintersect();
            }
            else if (ltr == P)
            {
                CheckP.SetNonintersect();
            }
            else if (ltr == Q)
            {
                CheckQ.SetNonintersect();
            }
            else if (ltr == R)
            {
                CheckR.SetNonintersect();
            }
            else if (ltr == S)
            {
                CheckS.SetNonintersect();
            }
            else if (ltr == T)
            {
                CheckT.SetNonintersect();
            }
            else if (ltr == U)
            {
                CheckU.SetNonintersect();
            }
            else if (ltr == V)
            {
                CheckV.SetNonintersect();
            }
            else if (ltr == W)
            {
                CheckW.SetNonintersect();
            }
            else if (ltr == X)
            {
                CheckX.SetNonintersect();
            }
            else if (ltr == Y)
            {
                CheckY.SetNonintersect();
            }
            else if (ltr == Z)
            {
                CheckZ.SetNonintersect();
            }
            return;
        }

        private void SetConfiguration(string[] data)
        {
            foreach (string currentLine in data)
            {
                if (Regex.IsMatch(currentLine, @"^GROUPSPERCROZZLELIMIT=[0-9]+$"))
                {
                    Match num = Regex.Match(currentLine, @"[0-9]+$");
                    groupsPerCrozzleLimit = int.Parse(num.ToString());
                }
                else if (Regex.IsMatch(currentLine, @"^POINTSPERWORD=[0-9]+$"))
                {
                    Match num = Regex.Match(currentLine, @"[0-9]+$");
                    pointPerWord = int.Parse(num.ToString());
                }
                else if (Regex.IsMatch(currentLine, @"^INTERSECTING:[A-Z]{1}=[0-9]+$"))
                {
                    Match ltr = Regex.Match(currentLine, @":[A-Z]{1}=");
                    char letter = ltr.ToString().ToArray()[1];
                    Match num = Regex.Match(currentLine, @"[0-9]+$");
                    int point = int.Parse(num.ToString());

                    ConfigLetter currentConfigLetter = new ConfigLetter(letter, point);
                    intersectLetterConfig.Add(currentConfigLetter);
                    SetIntersectCheck(letter);
                }
                else if (Regex.IsMatch(currentLine, @"^NONINTERSECTING:[A-Z]{1}=[0-9]+$"))
                {
                    Match ltr = Regex.Match(currentLine, @":[A-Z]{1}=");
                    char letter = ltr.ToString().ToArray()[1];
                    Match num = Regex.Match(currentLine, @"[0-9]+$");
                    int point = int.Parse(num.ToString());

                    ConfigLetter currentConfigLetter = new ConfigLetter(letter, point);
                    nonintersectLetterConfig.Add(currentConfigLetter);
                    SetNonintersectCheck(letter);
                }
                else
                {
                    log.log(currentLine + " is an invalid input of configuration.");
                }
            }
            configLetterCheck.Add(CheckA);
            configLetterCheck.Add(CheckB);
            configLetterCheck.Add(CheckC);
            configLetterCheck.Add(CheckD);
            configLetterCheck.Add(CheckE);
            configLetterCheck.Add(CheckF);
            configLetterCheck.Add(CheckG);
            configLetterCheck.Add(CheckH);
            configLetterCheck.Add(CheckI);
            configLetterCheck.Add(CheckJ);
            configLetterCheck.Add(CheckK);
            configLetterCheck.Add(CheckL);
            configLetterCheck.Add(CheckM);
            configLetterCheck.Add(CheckN);
            configLetterCheck.Add(CheckO);
            configLetterCheck.Add(CheckP);
            configLetterCheck.Add(CheckQ);
            configLetterCheck.Add(CheckR);
            configLetterCheck.Add(CheckS);
            configLetterCheck.Add(CheckT);
            configLetterCheck.Add(CheckU);
            configLetterCheck.Add(CheckV);
            configLetterCheck.Add(CheckW);
            configLetterCheck.Add(CheckX);
            configLetterCheck.Add(CheckY);
            configLetterCheck.Add(CheckZ);
            return;
        }

        public List<ConfigLetter> GetIntersectLetterConfig()
        {
            return intersectLetterConfig;
        }

        public List<ConfigLetter> GetNonintersectLetterConfig()
        {
            return nonintersectLetterConfig;
        }

        public int GetPointPerWord()
        {
            return pointPerWord;
        }

        private bool CheckGroupPerCrozzleLimit()
        {
            bool flag;
            if (groupsPerCrozzleLimit == 0)
            {
                flag = false;
                log.log("Crozzle Group Limit can not be 0.");
            }
            else if (groupsPerCrozzleLimit == ' ')
            {
                flag = false;
                log.log("There is no setting for group per crozzle limit in configuration file.");
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        private bool CheckPointPerWord()
        {
            bool flag;
            if (pointPerWord == ' ')
            {
                flag = false;
                log.log("There is no setting for point per word in configuration file.");
            }
            else
            {
                flag = true;
            }
            return flag;
        }

        private bool CheckConfigLetterCheck()
        {
            bool flag = true;
            foreach (ConfigLetterCheck tp in configLetterCheck)
            {
                
                if (tp.CheckConfigLetterCheck())
                {
                    continue;
                }
                else
                {
                    flag = false;
                }
            }
            return flag;
        }

        public bool CheckConfiguration()
        {
            bool flag;
            if (CheckGroupPerCrozzleLimit() & CheckPointPerWord() & CheckConfigLetterCheck())
            {
                flag = true;
            }
            else
            {
                flag = false;
            }
            return flag;
        }
    }
}
