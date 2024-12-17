using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OutilsChat;
using System.Net;

namespace ChatServer
{
    public class ClientReseau
    {
        private TcpClient tcpClient;
        private Thread threadLecture;
        private NetworkStream clientStream;
        private bool running;

        public Mutex AccessMessages { get; internal set; }
        public AutoResetEvent SignalementMessage { get; internal set; }
        public int Id { get; internal set; }
        public Stack<OutilsChat.Message> Messages { get; internal set; }
        public ServeurReseau Serveur { get; internal set; }

        public string IPAddress
        {
            get
            {
                EndPoint ep = this.tcpClient.Client.RemoteEndPoint;
                String ip = ep.ToString();
                return ip;
            }
        }

        public ClientReseau(TcpClient client)
        {
            this.tcpClient = client;
            this.running = false;
        }

        public void Start()
        {
            //
            ThreadStart PointEntree = new ThreadStart(this.Lire);
            threadLecture = new Thread(PointEntree);
            threadLecture.Start();
            //
        }

        public void Stop()
        {
            if (running)
            {
                this.clientStream.Close();
            }
        }


        private void Lire()
        {
            this.running = true;
            // On récupère le flux avec les infos
            this.clientStream = tcpClient.GetStream();
            //
            byte[] message = new byte[4096];
            int bytesRead;
            while (true)
            {
                bytesRead = 0;
                try
                {
                    //
                    bytesRead = clientStream.Read(message, 0, 4096);
                }
                catch
                {
                    // socket error, on sort
                    break;
                }
                if (bytesRead == 0)
                {
                    // disconnected, on sort
                    break;
                }
                //message reçu
                ASCIIEncoding encoder = new ASCIIEncoding();
                String reception = encoder.GetString(message, 0, bytesRead);
                // On met le message en attente
                // On demande un accès aux Messages
                this.AccessMessages.WaitOne();
                // On reconstitue le message à partir des données brutes
                OutilsChat.Message msg = new OutilsChat.Message( reception);
                // Mais on met l'Id du client coté Server
                msg.Id = this.Id;
                // On stocke
                this.Messages.Push( msg );
                // On libère l'accès
                this.AccessMessages.ReleaseMutex();

                // Il faut signaler au Serveur qu'on a reçu un message
                // donc on leve le drapeau
                this.SignalementMessage.Set();
            }
            //
            this.running = false;
            this.Serveur.RemoveClient(this.Id);
        }

        public void Ecrire(String msg)
        {
            if (this.running)
            {
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(msg);
                this.clientStream.Write(buffer, 0, buffer.Length);
            }
        }


    }
}
