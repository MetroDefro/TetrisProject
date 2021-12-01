using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace TetrisProject
{
    class EchoServer
    {
        TcpClient tcpClient;
        private BinaryReader br = null;
        private BinaryWriter bw = null;

        Board board;
        EnemyBoard enemyBoard;

        public EchoServer(Board b, EnemyBoard e, TcpClient client)
        {
            tcpClient = client;
            board = b;
            enemyBoard = e;

        }

        public void Process()
        {
            NetworkStream ns = tcpClient.GetStream();
            br = new BinaryReader(ns);
            bw = new BinaryWriter(ns);


            while (true)
            {


                // 데이터 주고 받기
                DataSend(board.Count);
                DataReceive();
                
                
            }

        }

        private int DataReceive()
        {
            // 데이터 받기
            for (int x = 0; x < 11; x++)
                for (int y = 0; y < 24; y++)
                    enemyBoard.Grid[x, y] = br.ReadBoolean();
            board.PlusLine(br.ReadInt32());
            if (br.ReadBoolean())
                board.Victory = true;
            return 0;
        }

        private void DataSend(int count)
        {
            // 데이터 보내기
            for (int x = 0; x < 11; x++)
                for (int y = 0; y < 24; y++)
                    bw.Write(board.Grid[x, y]);
            bw.Write(count);
            if (board.Count != 0)
                board.Count = 0;
            bw.Write(board.IsGameOver());
            

        }

    }
}
