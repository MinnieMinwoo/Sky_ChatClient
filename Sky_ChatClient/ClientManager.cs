using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;

namespace Sky_ChatClient
{
    class ClientManager
    {
        public TcpClient client { get; set; } //tcp client, 이를 활용해 서버에 접속
        public ClientData clientData { get; set; } //채팅 서버에 접속한 사람 정보

        public ClientManager()
        {
            clientData = new ClientData();
            ConnectServer();
            SendHeader();
            Thread sendMessageThread = new Thread(new ThreadStart(SendMessage));
            sendMessageThread.Start();
            //ReadLineAsync();
            Thread readMessageThread = new Thread(new ThreadStart(ReceiveMesasage));
            readMessageThread.Start();
            //readMessageThread.Join();
            //client.GetStream().BeginRead(clientData.messageData, 0, clientData.messageData.Length, new AsyncCallback(ReceiveMesasage), clientData);
        }

        //서버 연결
        private void ConnectServer()
        {
            client = new TcpClient();
            try
            {
                client.Connect("127.0.0.1", 9999);
            }
            catch (Exception e)
            {
                Console.WriteLine("Server connection failed..");
                Thread.Sleep(1000);
                ConnectServer();
            }

        }

        //서버에 자신의 정보 헤더파일 전송
        private void SendHeader()
        {
            string headerText = "$$#$$UserInfo$$#$$" + clientData.clientName + "$$#$$" + clientData.testType + "$$#$$" + clientData.chattingRoom + "$$#$$UserInfo$$#$$";
            byte[] header = new byte[headerText.Length];
            header = Encoding.UTF8.GetBytes(headerText);
            client.GetStream().Write(header, 0, header.Length);
        }

        //서버에 자신이 나간다는 신호를 전송
        private void DisconnectServer()
        {
            string headerText = "$$#$$DisConnect$$#$$" + clientData.clientName + "$$#$$" + clientData.testType + "$$#$$DisConnect$$#$$";
            byte[] header = new byte[headerText.Length];
            header = Encoding.UTF8.GetBytes(headerText);
            client.GetStream().Write(header, 0, header.Length);
        }

        //메세지 전송
        private void SendMessage()
        {
                string message = "$$#$$Message$$#$$" + clientData.chattingRoom + "$$#$$" + "All" +  "$$#$$" + clientData.clientName + "$$#$$" + Console.ReadLine() + "$$#$$Message$$#$$";
                byte[] byteData = new byte[message.Length];
                byteData = Encoding.UTF8.GetBytes(message);
                client.GetStream().Write(byteData, 0, byteData.Length);
        }

        //메세지를 받아 헤더에 맞는 작업을 진행합니다.
        private void ReceiveMesasage()
        {
            while (true)
            {
                byte[] messagePacket = new byte[1024];
                client.GetStream().Read(messagePacket, 0, messagePacket.Length);
                string readString = Encoding.UTF8.GetString(messagePacket);
                string[] messageData = readString.Split("$$#$$");

                if (messageData[1] == "Message")
                {
                    Console.WriteLine("{0} : {1}", messageData[2], messageData[3]);
                }

            }
        }
    }
}
