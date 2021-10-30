using System;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Program
    {
        private const int LnCol = 25;
        private const int lnRow = 25;
        private const uint LnMaxCnt = 300;

        static void Main(string[] args)
        {
            int Lnruns = 0;
            GameOfLife Gol = new GameOfLife(LnCol, lnRow);
            while (Lnruns++ < LnMaxCnt)
            {
                Gol.DrawNGrowGol();
                System.Threading.Thread.Sleep(100);
            }
        }
    }
    public class GameOfLife
    {
        private int LnCol;
        private int LnRow;
        private bool[,] cells;

        public GameOfLife(int LnCol, int LnRow)
        {
            this.LnCol = LnCol;
            this.LnRow = LnRow;
            cells = new bool[LnCol, LnRow];
            GenFld();
        }
        private void GenFld()
        {
            Random Lrdgen = new Random();
            int Lnnum;
            for (int i = 0; i < LnCol; i++)
            {
                for (int j = 0; j < LnRow; j++)
                {
                    Lnnum = Lrdgen.Next(2);
                    cells[i, j] = ((Lnnum == 0) ? false : true);
                }
            }
        }
        private void DrawGol()
        {
            string LnBuff = "";
            for (int i = 0; i < LnCol; i++)
            {
                for (int j = 0; j < LnRow; j++)
                {
                    LnBuff += cells[i, j] ? "*" : " ";
                }

                LnBuff += "\n";
            }
            Console.SetCursorPosition(0, Console.WindowHeight);
            Console.Write(LnBuff.TrimEnd('\n'));
            Console.Beep();

        }

        private void GrowGol()
        {
            for (int i = 0; i < LnCol; i++)
            {
                for (int j = 0; j < LnRow; j++)
                {
                    int Lnlive = GetGolNeighbors(i, j);

                    if (cells[i, j])
                    {
                        if (Lnlive < 2)
                        {
                            cells[i, j] = false;
                        }

                        if (Lnlive > 3)
                        {
                            cells[i, j] = false;
                        }
                    }
                    else
                    {
                        if (Lnlive == 3)
                        {
                            cells[i, j] = true;
                        }
                    }
                }
            }
        }
        private int GetGolNeighbors(int x, int y)
        {
            int Lnlive = 0;

            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    if (!((i < 0 || j < 0) || (i >= LnCol || j >= LnRow)))
                    {
                        if (!((i == x) && (j == y)))
                        {
                            if (cells[i, j] == true) Lnlive++;
                        }
                    }
                }
            }
            return Lnlive;
        }
        public void DrawNGrowGol()
        {
            DrawGol();
            GrowGol();
        }
    }
}
