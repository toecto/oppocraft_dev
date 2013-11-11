using System;
using System.Collections;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace TestServer
{
    class OppoServer
    {
        Hashtable clientsList = new Hashtable();
        TcpListener serverSocket;
        string IP;
        int Port;
        Thread sThread;

        public OppoServer(string IP, int Port)
        {
            this.IP = IP;
            this.Port = Port;
        }

        public void Start()
        {
            this.sThread = new Thread(this.ServerLoop);
            sThread.Start();
        }

        public void ServerLoop()
        {
            this.serverSocket = new TcpListener(IPAddress.Parse(this.IP), this.Port);
            TcpClient clientSocket = default(TcpClient);
            int counter = 0;

            serverSocket.Start();
            counter = 0;
            while ((true))
            {
                counter += 1;
                clientSocket = serverSocket.AcceptTcpClient();
                OppoServerClientHandler client = new OppoServerClientHandler(this, clientSocket, counter);
                clientsList.Add(counter, client);
                Console.WriteLine("Client " + counter + " joined");
            }
        }

        public void Stop()
        {
            this.sThread.Join();
            foreach (DictionaryEntry Item in clientsList)
            {
                OppoServerClientHandler client = (OppoServerClientHandler)Item.Value;
                try
                {
                    client.Stop();
                }
                catch (Exception ex)
                {}
            }
            this.serverSocket.Stop();
        }

        public void broadcast(byte[] msg, int sourceClientNo, bool toAll=true)
        {
            OppoServerClientHandler client = null;
            ArrayList toRemove = new ArrayList(2);
            foreach (DictionaryEntry Item in clientsList)
            {
                client = (OppoServerClientHandler)Item.Value;
                try
                {
                    if (toAll || sourceClientNo != (int)Item.Key)
                    {
                        client.Net.sendMessage(msg,true);
                    }
                }
                catch (Exception ex)
                {
                    client.Stop();
                    toRemove.Add(client.ID);
                }
            }

            foreach (int Item in toRemove)
            {
                clientsList.Remove(Item);
                Console.WriteLine("Client "+ Item + " is removed");
            }
            
        }  //end broadcast function
    }
}
