using OutilsChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient
{
    public delegate void OnMessageReceived(GestionChat sender, OutilsChat.Message message );
    public delegate void OnClientDisconnected(GestionChat sender);

    public class GestionChat
    {
        private string address;
        private int port;
        private Stack<OutilsChat.Message> messages;
        Thread thEcouter;
        ClientReseau client;
        private String alias;

        public OnMessageReceived OnMessageReceived;
        public OnClientDisconnected OnClientDisconnected;

        public Mutex accessMessages { get; internal set; }
        public AutoResetEvent signalementMessage { get; internal set; }
        public AutoResetEvent signalementSortie { get; internal set; }

        public bool Connected
        {
            get
            {
                if ( client != null )
                {
                    return client.Connected;
                }
                return false;
            }
        }

        public GestionChat(string address, int port, String alias )
        {
            this.address = address;
            this.port = port;
            this.alias = alias;
            this.messages = new Stack<OutilsChat.Message>();
            this.accessMessages = new Mutex();
            this.signalementMessage = new AutoResetEvent(false);
            this.signalementSortie = new AutoResetEvent(false);
        }

        public void Start()
        {
            this.client = new ClientReseau(address, port);
            if (client.Connect())
            {
                client.AccessMessages = this.accessMessages;
                client.SignalementMessage = this.signalementMessage;
                client.SignalementSortie = this.signalementSortie;
                client.Messages = this.messages;
                // On prepare l'écoute du réseau
                this.miseEnPlace();
                // Démarrage du client et ouverture du Flux sous jacent
                client.Start();
                //
            }
            else
            {
                this.client = null;
            }
        }

        private void miseEnPlace()
        {

            // et le dispatch dans un autre
            ThreadStart PointEntree = new ThreadStart(this.Ecouter);
            thEcouter = new Thread(PointEntree);
            thEcouter.Start();
        }

        private void Ecouter()
        {
            WaitHandle[] attente = new WaitHandle[2];
            //
            attente[0] = this.signalementMessage;
            attente[1] = this.signalementSortie;
            while (true)
            {
                // On attend, soit le Signalement de Message, soit le Demande de Sortie
                int who = AutoResetEvent.WaitAny(attente, Timeout.Infinite);
                if (who == 1)
                {
                    // Sortie Demandée ! Ciao
                    if (this.OnClientDisconnected != null)
                        this.OnClientDisconnected(this);
                    break;
                }
                // Il y a un message en attente
                // On accède à la pile des messages
                this.accessMessages.WaitOne();
                // On lit le message en pile
                OutilsChat.Message msg = null;
                while (this.messages.Count > 0)
                {
                    msg = this.messages.Pop();
                    //
                    if ( this.OnMessageReceived != null )
                    {
                        this.OnMessageReceived(this, msg);
                    }
                }
                // On libère
                this.accessMessages.ReleaseMutex();
            }
            //
        }

        public void Ecrire( string msg )
        {
            if ( this.client != null )
            {
                OutilsChat.Message newMessage = new OutilsChat.Message( this.client.Id, msg );
                newMessage.Envoi(alias);
                //
                this.client.Ecrire( newMessage.Data);
            }
        }



        public void Stop()
        {
            if (this.client != null)
            {
                this.client.Disconnect();
            }
        }

    }
}
