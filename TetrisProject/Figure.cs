using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisProject
{
    class Figure
    {
        private int x; //x 좌표
        private int y; //y 좌표
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }

        private Rectangle rect;
        public Rectangle Rect { get => rect; set => rect = value;  }

        private Pen BlockPen = new Pen(Color.White);
        private Brush BlockBrush = new SolidBrush(Color.Red);

        private bool right;
        private bool left;
        public bool Right { get => right; set => right = value; }
        public bool Left { get => left; set => left = value; }

        private int speed;
        private int count;
        public int Speed { get => speed; set => speed = value; }

        private int blockNum;
        private int turnNum;

        private int memory;

        private bool fix;

        public Figure(int bN, int tN, Board board)
        {
            // 40, 40
            // 여기에서 보드 가져오는 걸로 해서 스피드 제어하자
            if (board.score >= 3000)
                speed = 4;
            else if (board.score >= 2000)
                speed = 3;
            else if (board.score >= 1000)
                speed = 3;
            else
                speed = 1;
            count = 1;
            x = Board.SX;
            y = Board.SY;

            blockNum = bN;
            turnNum = tN;

            memory = 0;

            fix = false;
        }

        public void Draw(Graphics g)
        {
            for(int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (Block.value[blockNum, turnNum, i, j] == 1)
                    {
                        rect = new Rectangle(40 + (x + j) * Board.P_WIDTH, 40 + (y + i) * Board.P_HEIGHT,
                            Board.P_WIDTH, Board.P_HEIGHT);
                        g.FillRectangle(BlockBrush, rect);
                        g.DrawRectangle(BlockPen, rect);
                    }
        }

        public void viewer(Graphics g)
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Block.value[blockNum, turnNum, i, j] == 1)
                    {
                        rect = new Rectangle(300 + (j * Board.P_WIDTH), 80 + (i * Board.P_HEIGHT),
                            Board.P_WIDTH, Board.P_HEIGHT);
                        g.FillRectangle(BlockBrush, rect);
                        g.DrawRectangle(BlockPen, rect);

                    }
                }
            }
        }

        public void SetGrid(Board board)
        {
            for (int i = 0; i < 4; i++)
                for (int j = 0; j < 4; j++)
                    if (Block.value[blockNum, turnNum, i, j] == 1)
                        board.Grid[x + j, y + i] = true;
        }

        public bool CheckGrid(Board board, int Direction)
        {
            // 이동하려는 그리드가 비어있지 않다면 리턴
            // 0 아래 1 우측 2 좌측
            switch (Direction)
            {
                case 0:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (Block.value[blockNum, turnNum, i, j] == 1)
                                if (board.Grid[x + j, y + i + 1] == true)
                                    return true;
                        }
                    }
                    break;
                case 1:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (Block.value[blockNum, turnNum, i, j] == 1)
                            {
                                if (x + j + 1 > 10)
                                    return false;
                                if (board.Grid[x + j + 1, y + i] == true)
                                    return true;
                            }
                        }
                    }
                    break;
                case 2:
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j < 4; j++)
                        {
                            if (Block.value[blockNum, turnNum, i, j] == 1)
                            {
                                if (x + j - 1 < 0)
                                    return false;
                                if (board.Grid[x + j - 1, y + i] == true)
                                    return true;
                            }
                        }
                    }
                    break;
                case 3:
                    for (int k = 1; k < (24 - y); k++)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                if (Block.value[blockNum, turnNum, i, j] == 1)
                                {
                                    if (y + i + k > 23)
                                        return false;
                                    if (board.Grid[x + j, y + i + k] == true)
                                    {
                                        y = y + k - 1;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
            return false;

        }

        public bool MoveDown(Board board)
        {
            int maxY = 2;

            for (int i = 0; i < 4; i++)
            {
                if (Block.value[blockNum, turnNum, 3, i] == 1)
                {
                    maxY = 4;
                }
                if (Block.value[blockNum, turnNum, 2, i] == 1)
                {
                    if (maxY < 4)
                        maxY = 3;
                }
                if (Block.value[blockNum, turnNum, 1, i] == 1)
                {
                    if (maxY < 3)
                        maxY = 2;
                }
            }
            if (maxY == 4)
            {
                if (y == 20)
                    return true;
            }
            else if (maxY == 3)
            {
                if (y == 21)
                    return true;
            }
            else if (maxY == 2)
            {
                if (y == 22)
                    return true;
            }

            if (CheckGrid(board, 0))
                return true;

            if (count == 5)
            {
                y ++;
                count = speed;
            }
            else
            {
                count++;
            }
            
            return false;
        }

        public void MoveBottom(Board board)
        {

            if (CheckGrid(board, 3))
                return;

            int maxY = 2;

            for (int i = 0; i < 4; i++)
            {
                if (Block.value[blockNum, turnNum, 3, i] == 1)
                {
                    maxY = 4;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (Block.value[blockNum, turnNum, 2, i] == 1)
                {
                    if (maxY < 4)
                        maxY = 3;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (Block.value[blockNum, turnNum, 1, i] == 1)
                {
                    if (maxY < 3)
                        maxY = 2;
                }
            }

            if (maxY == 4)
            {
                y = 20;
            }
            else if (maxY == 3)
            {
                y = 21;
            }
            else if (maxY == 2)
            {
                y = 22;
            }

            fix = true;

            
        }

        public void Move(Board board)
        {
            if (fix)
                return;

            int maxX = 2;
            if (right)
            {
                if (CheckGrid(board, 1))
                    return;
                x++;
                for (int i = 0; i < 4; i++)
                {
                    if (Block.value[blockNum, turnNum, i, 3] == 1)
                    {
                        maxX = 4;
                    }
                    if (Block.value[blockNum, turnNum, i, 2] == 1)
                    {
                        if (maxX < 4)
                            maxX = 3;
                    }
                    if (Block.value[blockNum, turnNum, i, 1] == 1)
                    {
                        if (maxX < 3)
                            maxX = 2;
                    }
                }

                if (maxX == 4)
                {
                    if (x > 7)
                    {
                        x = 7;
                        return;
                    }
                }
                else if (maxX == 3)
                {
                    if (x > 8)
                    {
                        x = 8;
                        return;
                    }
                }
                else if (maxX == 2)
                {
                    if (x > 9)
                    {
                        x = 9;
                        return;
                    }
                }
            }
            else if (left)
            {
                if (CheckGrid(board, 2))
                    return;
                x--;
                for (int i = 0; i < 4; i++)
                {
                    if (Block.value[blockNum, turnNum, i, 0] == 1)
                    {
                        maxX = 4;
                    }
                    if (Block.value[blockNum, turnNum, i, 1] == 1)
                    {
                        if (maxX < 4)
                            maxX = 3;
                    }
                    if (Block.value[blockNum, turnNum, i, 2] == 1)
                    {
                        if (maxX < 3)
                            maxX = 2;
                    }
                }

                if (maxX == 4)
                {
                    if (x < 0)
                    {
                        x = 0;
                        return;
                    }
                }
                else if (maxX == 3)
                {
                    if (x < -1)
                    {
                        x = -1;
                        return;
                    }
                }
                else if (maxX == 2)
                {
                    if (x < -2)
                    {
                        x = -2;
                        return;
                    }
                }

            }
        }

        public void Turn()
        {
            x -= memory;
            memory = 0;
            if (turnNum == 3)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (Block.value[blockNum, 0, i, j] == 1)
                        {
                            if (x + j >= 11)
                            {
                                int result = x + j - 10;
                                x += -result;
                                memory = -result;
                            }
                            else if (x + j < 0)
                            {
                                int result = 0 - (x - j);
                                x += result;
                                memory = result;
                            }
                        }
                    }
                }
                turnNum = 0;
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (Block.value[blockNum, turnNum + 1, i, j] == 1)
                        {
                            if (x + j >= 11)
                            {
                                int result = x + j - 10;
                                x += -result;
                                memory = -result;
                            }
                            else if (x + j < 0)
                            {
                                int result = 0 - (x + j);
                                x += result;
                                memory = result;
                            }
                        }
                    }
                }
                turnNum++;
            }
        }
    }
}