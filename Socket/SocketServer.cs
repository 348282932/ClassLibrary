using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketServer
{
    class Program
    {
        private static Socket serverSocket;

        private static Byte[] buffer = new Byte[2048];

        static void Main(String[] args)
        {
            IPAddress ip = IPAddress.Parse("10.0.0.46");

            Int32 myProt = 8081;

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(new IPEndPoint(ip, myProt));

            serverSocket.Listen(10);

            Console.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());

            new Thread(ListenClientConnect).Start();
        }

        /// <summary>  
        /// 监听客户端连接  
        /// </summary> 
        private static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket =  serverSocket.Accept();

                new Thread(ReceiveMessage).Start(clientSocket);

                new Thread(SendMessage).Start(clientSocket);
            }
        }

        /// <summary>
        /// 向客户端发送消息
        /// </summary>
        /// <param name="clientSocket">对应的客户端标识</param>
        private static void SendMessage(Object clientSocket)
        {
            Socket serverSocket = (Socket)clientSocket;

            while (true)
            {
                String str = Console.ReadLine();

                try
                {
                    serverSocket.Send(Encoding.UTF8.GetBytes(str));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("向客户端发出的消息失败，原因：" + ex.Message);

                    serverSocket.Shutdown(SocketShutdown.Both);

                    serverSocket.Close();
                }
            }
        }

        /// <summary>
        /// 接收客户端推送过来的消息
        /// </summary>
        /// <param name="clientSocket">对应的客户端标识</param>
        private static void ReceiveMessage(Object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;

            while (true)
            {
                try
                {
                    Int32 receiveNumber = myClientSocket.Receive(buffer);

                    if (receiveNumber != 0)
                    {
                        Console.WriteLine("收到客户端{0}消息:{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.UTF8.GetString(buffer, 0, receiveNumber));
                    }
                    else
                    {
                        myClientSocket.Shutdown(SocketShutdown.Both);

                        myClientSocket.Close();

                        return;
                    }

                }
                catch (Exception)
                {
                    if (myClientSocket != null)
                    {
                        try
                        {
                            myClientSocket.Shutdown(SocketShutdown.Both);
                        }
                        catch (Exception) { }

                        myClientSocket.Close();
                    }
                    return;
                }
            }
        }
    }
}
