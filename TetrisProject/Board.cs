using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    class Board
    {
        private Random r = new Random(unchecked((int)DateTime.Now.Ticks));
        Pen pen = new Pen(Color.White);
        Brush brush = new SolidBrush(Color.Green);

        // 좌표
        private bool[,] grid = new bool[11, 24];
        public bool[,] Grid { get => grid; set => grid = value; }
        public bool Victory { get => victory; set => victory = value; }

        // 픽셀 크기
        public const int P_WIDTH = 20;
        public const int P_HEIGHT = 20;
        public const int PX = 11;
        public const int PY = 24;
        public const int SX = 4; // 0부터 시작
        public const int SY = 0; // 0부터 시작

        public int Count;

        public int score;

        private bool victory;


        public Board()
        {

            // grid 배열 초기화
            for (int i = 0; i < 11; i++)
                for (int j = 0; j < 24; j++)
                    grid[i, j] = false;

            score = 0;

            victory = false;

        }
        public void Reset()
        {
            for (int i = 0; i < 11; i++)
                for (int j = 0; j < 24; j++)
                    grid[i, j] = false;
            score = 0;
        }
        public void Draw(Graphics g)
        {
            // 배경
            Brush bBrush = new SolidBrush(Color.Black);
            g.DrawImage(Properties.Resources.BackGround, 0, 0, 740, 537);
            g.DrawImage(Properties.Resources.BackGorund2, 40, 40, 220, 480);
            g.DrawImage(Properties.Resources.BackGorund2, 420, 40, 220, 480);

            // 보드
            g.DrawRectangle(pen, 40, 40, 220, 480);

            for (int i = 0; i <= 200; i+= P_WIDTH)
                g.DrawLine(pen, 60 + i, 40, 60 + i, 520);

            for (int i = 0; i <= 440; i += P_HEIGHT)
                g.DrawLine(pen, 40, 60 + i, 260, 60 + i);

            // 다음 블록
            g.DrawRectangle(pen, 300, 80, 80, 80);

            g.DrawRectangle(pen, 300, 80, 80, 80);
            for (int i = 0; i <= 80; i += P_WIDTH)
                g.DrawLine(pen, 300 + i, 80, 300 + i, 160);

            for (int i = 0; i <= 80; i += P_HEIGHT)
                g.DrawLine(pen, 300, 80 + i, 380, 80 + i);

            // 블록
            for ( int x = 0; x < 11; x++)
            {
                for( int y = 0; y < 24; y++)
                {
                    if(grid[x, y])
                    {
                        g.FillRectangle(brush, 40 + x * P_WIDTH, 40 + y * P_HEIGHT,
                             P_WIDTH, P_HEIGHT);
                        g.DrawRectangle(pen, 40 + x * P_WIDTH, 40 + y * P_HEIGHT,
                            P_WIDTH, P_HEIGHT);
                    }
                }
            }
        }

        public void DeleteLine()
        {
            int sum;
            Count = 0;
            for(int y = 23; y >= 0; y--)
            {
                sum = 0;
                for(int x = 0; x < 11; x++)
                    if(grid[x, y])
                        sum++;
                if (sum == 11)
                {
                    for (int i = y; i > 0; i--)
                        for (int x = 0; x < 11; x++)
                            grid[x, i] = grid[x, i - 1];
                    score += 100;
                    Count += 1;
                }
            }
        }

        public void PlusLine(int count)
        {
            int hole;

            if(count != 0)
            {
                for (int i = 0; i < 24; i++)
                    for (int x = 0; x < 11; x++)
                    {
                        if ((i - count) >= 0)
                            grid[x, i - count] = grid[x, i];
                    }
                for (int i = 23; i > 23 - count; i--)
                {
                    hole = r.Next(11);
                    for (int x = 0; x < 11; x++)
                    {
                        if (x != hole)
                            grid[x, i] = true;
                        else
                            grid[x, i] = false;
                    }
                }
            }
        }

        public bool IsGameOver()
        {
            for (int x = 0; x < 11; x++)
                if (grid[x, 0])
                    return true;
            return false;
        }

    }
}
