using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace Sky_ChatClient
{
    class ClientData
    {
        public TcpClient client; //tcpclient
        public string clientName; //이름
        public string ipAdress; //ip주소
        public string testType; //학생 or 감독관
        public string chattingRoom; //채팅방 이름
        public byte[] messageData; //메세지 데이터를 담을 버퍼

        //정보를 입력받습니다. 채팅방은 이후 서버에서 값을 돌려받게 됩니다.
        public ClientData()
        {
            Console.WriteLine("이름을 입력하세요");
            clientName = Console.ReadLine();
            Console.WriteLine("학생이면 1, 감독관이면 2를 입력하세요");
            testType = Console.ReadLine();
            messageData = new byte[1024];
            chattingRoom = "1";
        }
    }
}
