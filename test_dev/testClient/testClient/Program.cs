using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace testClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Input server (Empty for localhost): ");
            string server = Console.ReadLine();
            if (server == "") server = "127.0.0.1";
            TcpMessageClient client = new TcpMessageClient(server,8888);
            client.Connect();
            client.onMessage += readMessages;
            while (true)
            {
                Console.Write("#");
                string msg = Console.ReadLine();
                //client.sendMessage(Encoding.ASCII.GetBytes(msg));
                OppoMessage message = new OppoMessage(OppoMessageType.CreateUnit);
                message.Text["msg"] = msg;
                client.sendMessage(message.toBin());
                if (msg == "exit")
                {
                    break;
                }
            }
            Console.Write("Done");
            Console.ReadLine();
        }


        public static void readMessages(TcpMessageClient client)
        {
            if (client.Available > 0)
            { 
                Byte[] RawMessage;
                while ((RawMessage = client.getMessage()) != null)
                {
                    OppoMessage message = OppoMessage.fromBin(RawMessage);
                    Console.WriteLine(message.toString());
                }
            }

        }

    }
}
