using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace HitmanChat
{
    class Program
    {
        private static readonly int HITMAN_PORT = 50001;

        private static Socket CONN;

        private static String CURRENT_USER = Environment.UserName;

        private static Thread[] TASKS = new Thread[2];
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome " + CURRENT_USER + " to HitmanChat! What do you want to do?");
            int choice;

            while (true)
            {
                ShowMenu();
                choice = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Cyan;
                if (choice == 1)
                {
                    IPAddress address = IPAddress.Parse("0.0.0.0");
                    TcpListener listener = new TcpListener(address, HITMAN_PORT);
                    Console.WriteLine(string.Format("Listening for connections on port {0} on all interfaces ...", HITMAN_PORT));
                    listener.Start(1);
                    CONN = listener.AcceptSocket();
                    Console.WriteLine(string.Format("Received connection request from {0}", CONN.RemoteEndPoint.ToString()));
                    TASKS[0] = new Thread(ListenForMessages);
                    TASKS[1] = new Thread(ReadAndSendUserInput);
                    break;
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Enter IP address to connect to ...");
                    IPAddress address = IPAddress.Parse(Console.ReadLine());
                    TcpClient client = new TcpClient();
                    client.Connect(address.ToString(), HITMAN_PORT);
                    Console.WriteLine(string.Format("Connection to {0} succeeded", address.ToString()));
                    CONN = client.Client;
                    TASKS[0] = new Thread(ListenForMessages);
                    TASKS[1] = new Thread(ReadAndSendUserInput);
                    break;
                }
                else if (choice == 3) 
                {
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Try again.\n");
                }
            }
            TASKS[0].Start();
            TASKS[1].Start();
            TASKS[1].Join();
            TASKS[2].Join();
        }

        static void ShowMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Main Menu");
            Console.WriteLine("---------");
            Console.WriteLine("1. Wait for connection from others");
            Console.WriteLine("2. Connect to some IP address");
            Console.WriteLine("3. Exit");
            Console.Write("Enter choice: ");
        }

        static void ListenForMessages()
        {
            while (true)
            {
                byte[] data = new byte[1024];
                int size = CONN.Receive(data);
                string message = System.Text.Encoding.UTF8.GetString(data);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message);
            }         
        }

        static void ReadAndSendUserInput()
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(CURRENT_USER + ": ");
                String message = Console.ReadLine();
                if (message.Equals(":q"))
                {
                    CONN.Close();
                    TASKS[0].Abort();
                    break;
                }
                byte[] dataToSend = System.Text.Encoding.UTF8.GetBytes(CURRENT_USER + ": " + message);
                CONN.Send(dataToSend);
            }         
        }

    }
}
