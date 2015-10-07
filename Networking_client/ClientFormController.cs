using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Networking_client
{
    public class ClientFormController
    {
        public ClientForm myClientForm { get; set; }
        public Client myClient { get; set; } = new Client();
        Thread clientThread;
        public ClientFormController()
        {
            myClientForm = new ClientForm(this);
            clientThread = new Thread(myClient.Start);


            clientThread.Start();
            clientThread.Join();
        }

        internal void Send(string message)
        {
            Thread senderThread = new Thread(myClient.Send);
            senderThread.Start(message);
        }
    }

    public class Client
    {
        private TcpClient client;

        public void Start()
        {
            client = new TcpClient("192.168.220.124", 5000);

            Thread listenerThread = new Thread(Listen);
            listenerThread.Start();



            //senderThread.Join();
            listenerThread.Join();
        }

        public void Listen()
        {
            string message = "";

            try
            {
                while (true)
                {
                    NetworkStream n = client.GetStream();
                    message = new BinaryReader(n).ReadString();
                    Console.WriteLine("Other: " + message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Send(object message)
        {

            try
            {
                while (!message.Equals("quit"))
                {
                    NetworkStream n = client.GetStream();
                    BinaryWriter w = new BinaryWriter(n);
                    w.Write((string)message);
                    w.Flush();
                }

                client.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

