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
            string headerText = "$$#$$Header$$#$$" + clientData.clientName + "$$#$$" + clientData.testType + "$$#$$Header$$#$$";
            byte[] header = new byte[headerText.Length];
            client.GetStream().Write(header, 0, header.Length);

        }

        private void DisconnectServer()
        {
            string headerText = "$$#$$DisConnect$$#$$" + clientData.clientName + "$$#$$" + clientData.testType + "$$#$$DisConnect$$#$$";
            byte[] header = new byte[headerText.Length];
            client.GetStream().Write(header, 0, header.Length);
        }

        //메세지 전송
        private void SendMessage(string text)
        {
            string message = "";
            byte[] byteData = new byte[message.Length];
            byteData = Encoding.UTF8.GetBytes(message);
            client.GetStream().Write(byteData, 0, byteData.Length);
        }

        //메세지를 실시간으로 받아오는 메서드
        private void ReceiveMessage()
        {
         
        }
    }
}
