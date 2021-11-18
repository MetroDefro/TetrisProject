using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TetrisProject
{
    public partial class Form2 : Form
    {
        private TcpListener tcpListener = null;
        private TcpClient tcpClient = null;
        private BinaryReader br = null;
        private BinaryWriter bw = null;
        private NetworkStream ns;

        Form1 form1;
        public Form2(Form1 form)
        {
            InitializeComponent();

            form1 = form;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            tcpListener = new TcpListener(3000);
            tcpListener.Start();
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            for (int i = 0; i < host.AddressList.Length; i++)
            {
                if (host.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    textBox1.Text = host.AddressList[i].ToString();
                    break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 서버로 접속
            tcpClient = tcpListener.AcceptTcpClient();
            if (tcpClient.Connected)
            {
                textBox2.Text = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
            }

            ns = tcpClient.GetStream();
            bw = new BinaryWriter(ns);
            br = new BinaryReader(ns);

            // 이후는 클라이언트가 들어오면 실행되게 하고싶음
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

            form1.SetIP(textBox1.Text, textBox2.Text);

            this.Close();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 클라이언트로 접속
            tcpClient = new TcpClient(textBox1.Text, 3000);
            if (tcpClient.Connected)
            {
                ns = tcpClient.GetStream();
                br = new BinaryReader(ns);
                bw = new BinaryWriter(ns);
                MessageBox.Show("서버 접속 성공");
            }
            else
            {
                MessageBox.Show("서버 접속 실패");
            }

            this.Close();
        }

        private int DataReceive()
        {
            // 데이터 받기
            return 0;
        }

        private void DataSend()
        {
            // 데이터 보내기
        }

        private void AllClose()
        {
            if (bw != null)
            {
                bw.Close(); bw = null;
            }
            if (br != null)
            {
                br.Close(); br = null;
            }
            if (ns != null)
            {
                ns.Close(); ns = null;
            }
            if (tcpClient != null)
            {
                tcpClient.Close(); tcpClient = null;
            }
        }
    }
}
