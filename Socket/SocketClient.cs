using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;

namespace SocketClient
{
    class Program
    {
        private static Socket clientSocket;

        static void Main(string[] args)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                IPAddress ip = IPAddress.Parse("10.0.0.46");

                Int32 port = 8081;

                clientSocket.Connect(new IPEndPoint(ip, port));

                Console.WriteLine("连接服务器成功"); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("服务器链接失败，原因：" + ex.Message);

                return;
            }

            new Thread(ReceiveMessage).Start(clientSocket);

            new Thread(SendMessage).Start();

        }
        
        /// <summary>
        /// 接收服务器推送过来的信息
        /// </summary>
        /// <param name="clientSocket"></param>
        private static void ReceiveMessage(Object clientSocket)
        {
            Byte[] buffer = new Byte[1024];

            Int32 clientNumber;

            Socket serverSocket = (Socket)clientSocket;

            while (true)
            {
                clientNumber = serverSocket.Receive(buffer);

                if (clientNumber != 0)
                {
                    Console.WriteLine("收到服务器消息：" + Encoding.UTF8.GetString(buffer, 0, clientNumber));

                    Array.Clear(buffer, 0, clientNumber);
                }
            }
        }

        /// <summary>
        /// 向服务器发送信息
        /// </summary>
        private static void SendMessage()
        {
            while (true)
            {
                String str = Console.ReadLine();

                try
                {
                    clientSocket.Send(Encoding.UTF8.GetBytes(str));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("向服务器发出的消息失败，原因：" + ex.Message);

                    clientSocket.Shutdown(SocketShutdown.Both);

                    clientSocket.Close();
                }
            }
        }
    }
}
