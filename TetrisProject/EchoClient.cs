using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;

namespace TetrisProject
{
    class EchoClient
    {
        private BinaryReader br = null;
        private BinaryWriter bw = null;

        Board board;
        EnemyBoard enemyBoard;

        public EchoClient(Board b, EnemyBoard e, NetworkStream ns)
        {
            board = b;
            enemyBoard = e;
            br = new BinaryReader(ns);
            bw = new BinaryWriter(ns);
        }

        public void Process()
        {
            while (true)
            {
                // 데이터 주고 받기

                DataReceive();
                DataSend(board.Count);

            }

        }

        private int DataReceive()
        {
            // 데이터 받기
            for (int x = 0; x < 11; x++)
                for (int y = 0; y < 24; y++)
                    enemyBoard.Grid[x, y] = br.ReadBoolean();
            board.PlusLine(br.ReadInt32());
            return 0;
        }

        private void DataSend(int count)
        {
            // 데이터 보내기
            for (int x = 0; x < 11; x++)
                for (int y = 0; y < 24; y++)
                    bw.Write(board.Grid[x, y]);
            bw.Write(count);
        }
    }
}
