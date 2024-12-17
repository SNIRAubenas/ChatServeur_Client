using OutilsChat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatServer
{
    public partial class MainWindow : Form
    {
        IniFile configFile;
        ServeurReseau server;
        public MainWindow()
        {
            InitializeComponent();
            //
            configFile = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "config.ini");
            int port = configFile.ReadValue("Server", "Port", 18);
            //
            this.numericPort.Value = port;
            //
            this.Text += " " + Constants.APP_VERSION;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            configFile.WriteValue("Server", "Port", (int) this.numericPort.Value);
            //
            this.numericPort.Enabled = false;
            this.buttonStart.Visible = false;
            this.buttonStop.Visible = true;
            this.listBox1.Items.Clear();
            //
            server = new ServeurReseau(null, (int)this.numericPort.Value);
            server.OnClientAccept += new ClientEvent(this.OnClientConnected);
            server.OnClientClose += new ClientEvent(this.OnClientClosed);
            server.Start();
            //
        }

        private void OnClientConnected(ServeurReseau sender, ClientReseau clt)
        {
            // !!! ATTENTION !!!
            // A ce stade on est "encore" dans le contexte d'execution du Thread Réseau
            this.Invoke((ClientEvent)this.DoClientConnected, new object[] { sender, clt });
        }
        private void DoClientConnected(ServeurReseau sender, ClientReseau clt)
        {
            this.listBox1.Items.Add( ">>" + clt.Id.ToString().PadLeft(3, '0') + " : " + clt.IPAddress);
        }

        private void OnClientClosed(ServeurReseau sender, ClientReseau clt)
        {
            // !!! ATTENTION !!!
            // A ce stade on est "encore" dans le contexte d'execution du Thread Réseau
            this.Invoke((ClientEvent)this.DoClientClosed, new object[] { sender, clt });
        }
        private void DoClientClosed(ServeurReseau sender, ClientReseau clt)
        {
            this.listBox1.Items.Add( "<<" + clt.Id.ToString().PadLeft(3, '0') + " : " + clt.IPAddress);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            //
            this.numericPort.Enabled = true;
            this.buttonStart.Visible = true;
            this.buttonStop.Visible = false;
            //
            server.Stop();
            //
            server = null;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Fermeture !! Arret du serveur svp
            if ( server != null )
            {
                server.Stop();
            }
        }
    }
}
