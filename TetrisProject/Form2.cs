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
    public partial class Form2 : Form
    {

        Form1 form1;
        public Form2(Form1 form, string IP)
        {
            InitializeComponent();

            textBox1.Text = IP;

            form1 = form;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            form1.SetIP(textBox2.Text,true);

            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            form1.SetIP(textBox2.Text, false);

            this.Close();
        }



        private void button3_Click(object sender, EventArgs e)
        {
            /*
            while (true)
            {
                if (tcpClient.Connected)
                {
                    if (DataReceive() == -1)
                        break;
                    DataSend();
                }
                else
                {
                    AllClose();
                    break;
                }
            }
            AllClose();
            */

        }
    }
}
