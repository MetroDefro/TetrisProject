using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    class EnemyBoard
    {
        Pen pen = new Pen(Color.White);
        Brush brush = new SolidBrush(Color.Green);

        // 좌표
        private bool[,] grid = new bool[11, 24];
        public bool[,] Grid { get => grid; set => grid = value; }

        // 픽셀 크기
        public const int P_WIDTH = 20;
        public const int P_HEIGHT = 20;
        public const int PX = 11;
        public const int PY = 24;
        public const int SX = 4; // 0부터 시작
        public const int SY = 0; // 0부터 시작


        public int score;

        public EnemyBoard()
        {
            // grid 배열 초기화
            for (int i = 0; i < 11; i++)
                for (int j = 0; j < 24; j++)
                    grid[i, j] = false;

            score = 0;

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
            // 보드
            g.DrawRectangle(pen, 420, 40, 220, 480);

            for (int i = 0; i <= 200; i += P_WIDTH)
                g.DrawLine(pen, 420 + i, 40, 420 + i, 520);

            for (int i = 0; i <= 440; i += P_HEIGHT)
                g.DrawLine(pen, 420, 60 + i, 640, 60 + i);

            // 블록
            for (int x = 0; x < 11; x++)
            {
                for (int y = 0; y < 24; y++)
                {
                    if (grid[x, y])
                    {
                        g.FillRectangle(brush, 420 + x * P_WIDTH, 40 + y * P_HEIGHT,
                             P_WIDTH, P_HEIGHT);
                        g.DrawRectangle(pen, 420 + x * P_WIDTH, 40 + y * P_HEIGHT,
                            P_WIDTH, P_HEIGHT);
                    }
                }
            }

        }

    }
}
