using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OutilsChat;
using System.Net;

namespace ChatClient
{
    

    public class ClientReseau
    {
        private TcpClient tcpClient;
        private Thread threadLecture;
        private NetworkStream clientStream;
        private IPEndPoint ep;


        public Mutex AccessMessages { get; internal set; }
        public AutoResetEvent SignalementMessage { get; internal set; }
        public int Id { get; internal set; }
        public Stack<OutilsChat.Message> Messages { get; internal set; }
        public bool Connected { get; private set; }

        public bool Running { get; private set; }
        public AutoResetEvent SignalementSortie { get; internal set; }

        public ClientReseau()
        {
            this.Running = false;
        }

        public ClientReseau(String adresseIP, int port):this()
        {
            String[] elements = adresseIP.Split('.');
            if (elements.Length != 4)
                throw new Exception("Il faut 4 éléments dans une adresse IP");
            //
            byte[] elts = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                String element = elements[i];
                byte elt = Convert.ToByte(element);
                elts[i] = elt;
            }
            //
            IPAddress ipaddd = new IPAddress(elts);
            //
            this.ep = new IPEndPoint(ipaddd, port);
        }

        public bool Connect()
        {
            tcpClient = new TcpClient();
            try
            {
                tcpClient.Connect(this.ep);
                this.Connected = tcpClient.Connected;
                if (this.Connected)
                {
                    this.clientStream = tcpClient.GetStream();
                }
            }
            catch
            {
                this.Connected = false;
            }
            return this.Connected;
        }

        public void Disconnect()
        {
            if ( Connected)
            {
                // Fermer le flux, ferme la connection
                this.clientStream.Close();
            }
        }


        public void Start()
        {
            //
            ThreadStart PointEntree = new ThreadStart(this.Lire);
            threadLecture = new Thread(PointEntree);
            threadLecture.Start();
            //
        }


        private void Lire()
        {
            this.Running = true;
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
                // On stocke
                this.Messages.Push(new OutilsChat.Message(reception));
                // On libère l'accès
                this.AccessMessages.ReleaseMutex();

                // Il faut signaler au Serveur qu'on a reçu un message
                // donc on leve le drapeau
                this.SignalementMessage.Set();
            }
            //
            this.Running = false;
            this.SignalementSortie.Set();
        }

        public void Ecrire(String msg)
        {
            if (this.Running)
            {
                ASCIIEncoding encoder = new ASCIIEncoding();
                byte[] buffer = encoder.GetBytes(msg);
                this.clientStream.Write(buffer, 0, buffer.Length);
            }
        }

    }
}
