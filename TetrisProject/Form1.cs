using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TetrisProject
{
    public partial class Form1 : Form
    {
        private Figure figure;
        private Board board;
        private Figure nextFigure;

        // 멀티
        private EnemyBoard enemyBoard;

        private Random r = new Random(unchecked((int)DateTime.Now.Ticks));


        // 네트워크 관련
        private string myIP;
        private string yourIP;


        public Form1()
        {
            board = new Board();
            figure = new Figure(r.Next(7), r.Next(4), board);
            nextFigure = new Figure(r.Next(7), r.Next(4), board);
            enemyBoard = new EnemyBoard();
            InitializeComponent();
            DoubleBuffered = true;
            GameOverL.Visible = false;
            score.BackColor = Color.Transparent;
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            GameOverL.BackColor = Color.Transparent;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            board.Draw(e.Graphics);
            figure.Draw(e.Graphics);
            nextFigure.viewer(e.Graphics);
            enemyBoard.Draw(e.Graphics);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            board.DeleteLine();
            figure.Move(board);
            if (figure.MoveDown(board))
            {
                figure.SetGrid(board);
                figure = nextFigure;
                nextFigure = new Figure(r.Next(7), r.Next(4), board);
                
            }
            score.Text = board.score.ToString(); ;
            if (board.IsGameOver())
            {
                GameOverL.Visible = true;
                timer1.Stop();
                board.Reset();
                GameOverL.Visible = false;
                timer1.Start();
            }

            panel1.Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Form2 f2 = new Form2(this);
            f2.Show();

            timer1.Interval = 100;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    figure.Right = true;
                    break;

                case Keys.Left:
                    figure.Left = true;
                    break;
                case Keys.Space:
                    figure.MoveBottom(board);
                    break;
                case Keys.Down:
                    figure.Turn();
                    break;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    figure.Right = false;
                    break;

                case Keys.Left:
                    figure.Left = false;
                    break;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
        
        // ip 받아오기
        public void SetIP(string ip1, string ip2)
        {
            myIP = ip1;
            yourIP = ip2;
            timer1.Start();
        }
    }
}