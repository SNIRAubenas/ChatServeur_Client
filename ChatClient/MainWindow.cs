using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OutilsChat;

namespace ChatClient
{
    public partial class MainWindow : Form
    {
        private GestionChat comm;
        private IniFile configFile;
        private Dictionary<int, Color> clients;
        private Button buttonsendimage;

        public MainWindow()
        {
            InitializeComponent();
            comm = null;
            //
            configFile = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "config.ini");
            int port = configFile.ReadValue("Server", "Port", 18);
            String ipAddress = configFile.ReadValue("Server", "IPAddress", "127.0.0.1");
            String alias = configFile.ReadValue("User", "Alias", "jean chaineDBeat");
            //
            this.numericPort.Value = port;
            this.ipAddressControl1.IPAddress = ipAddress;
            this.textAlias.Text = alias;
            //
            this.Text += " " + Constants.APP_VERSION;
            this.clients = new Dictionary<int, Color>();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            // Démarrage de connection
            if (comm == null)
            {
                configFile = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "config.ini");
                configFile.WriteValue("Server", "Port", (int)this.numericPort.Value);
                configFile.WriteValue("Server", "IPAddress", this.ipAddressControl1.IPAddress);
                configFile.WriteValue("User", "Alias", this.textAlias.Text);
                //
                this.comm = new GestionChat(this.ipAddressControl1.IPAddress, (int)this.numericPort.Value, this.textAlias.Text);
                this.comm.OnMessageReceived += new OnMessageReceived(this.OnMessageReceived);
                this.comm.OnClientDisconnected += new OnClientDisconnected(this.OnClientDisconnected);
                this.comm.Start();
                //
                if (this.comm.Connected)
                {
                    //
                    this.ipAddressControl1.Enabled = false;
                    this.numericPort.Enabled = false;
                    this.textAlias.Enabled = false;
                    this.buttonStart.Visible = false;
                    this.buttonStop.Visible = true;
                    this.textMessage.Enabled = true;
                    this.buttonEnvoi.Enabled = true;
                    this.AfficherInfo("Connection sur " + this.ipAddressControl1.IPAddress + ":" + this.numericPort.Value + ".");
                }
                else
                {
                    this.AfficherErreur("Erreur de connection sur " + this.ipAddressControl1.IPAddress + ":" + this.numericPort.Value + ".");
                    this.comm = null;
                }
            }
        }

        private void OnClientDisconnected(GestionChat sender)
        {
            // !!! ATTENTION !!!
            // A ce stade on est "encore" dans le contexte d'execution du Thread Réseau
            this.Invoke((OnClientDisconnected)this.DoClientDisconnected, new object[] { sender });
        }

        private void DoClientDisconnected(GestionChat sender)
        {
            // On remet tout en place
            this.ipAddressControl1.Enabled = true;
            this.numericPort.Enabled = true;
            this.textAlias.Enabled = true;
            this.buttonStart.Visible = true;
            this.buttonStop.Visible = false;
            this.textMessage.Enabled = false;
            this.buttonEnvoi.Enabled = false;
            this.comm.Stop();
            this.comm = null;
        }

        private void OnMessageReceived(GestionChat sender, OutilsChat.Message message)
        {
            // !!! ATTENTION !!!
            // A ce stade on est "encore" dans le contexte d'execution du Thread Réseau
            this.Invoke((OnMessageReceived)this.DoMessageReceived, new object[] { sender, message });
        }

        private void DoMessageReceived(GestionChat sender, OutilsChat.Message message)
        {
            // Client déjà connu ?
            if (!clients.ContainsKey(message.Id))
            {
                clients.Add(message.Id, this.RandomColor());
            }
            //
            this.AjoutMessage(message, this.clients[message.Id]);




            //cgpt

            // Si le message contient une image, affichez-la
            if (message.Image != null)
            {
                // Afficher l'image reçue
                PictureBox pictureBox = new PictureBox
                {
                    Image = message.Image,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Size = new Size(200, 200),
                    Location = new Point(10, richMessages.Bottom + 10)  // Positionner sous le message texte
                };
                this.Controls.Add(pictureBox);
            }
            else
            {
                // Si ce n'est pas une image, c'est un message texte
                this.AjoutMessage(message, Color.Black);
            }

        }

        private void AjoutMessage(OutilsChat.Message msg, Color clr)
        {
            //
            int beforeAppend = this.richMessages.TextLength;
            this.richMessages.AppendText(msg.Param1 + " - " + DateTime.Now.ToString() + Environment.NewLine);
            int afterAppend = this.richMessages.TextLength;
            this.richMessages.Select(beforeAppend, afterAppend - beforeAppend);
            this.richMessages.SelectionColor = clr;
            System.Drawing.Font currentFont = richMessages.SelectionFont;
            System.Drawing.FontStyle newFontStyle;
            newFontStyle = FontStyle.Bold;
            richMessages.SelectionFont = new Font(currentFont, newFontStyle);
            //
            beforeAppend = this.richMessages.TextLength;
            this.richMessages.AppendText(msg.Texte + Environment.NewLine);
            afterAppend = this.richMessages.TextLength;
            this.richMessages.Select(beforeAppend, afterAppend - beforeAppend);
            this.richMessages.SelectionColor = clr;
        }

        private Color RandomColor()
        {
            Random randomGen = new Random((int)DateTime.Now.Ticks);
            KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            KnownColor randomColorName = names[randomGen.Next(names.Length)];
            return Color.FromKnownColor(randomColorName);
        }

        private void AfficherErreur(string msg)
        {
            this.statusBarInfo.ForeColor = Color.Red;
            this.statusBarInfo.Text = msg;
        }

        private void AfficherInfo(string info)
        {
            this.statusBarInfo.ForeColor = Color.Black;
            this.statusBarInfo.Text = info;
        }

        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Fermeture !! Arret du client
            if (comm != null)
            {
                // Si on laisse le traitement de l'evenement, on va revenir vers la fenetre alors qu'elle n'existera plus
                this.comm.OnClientDisconnected = null;
                this.comm.Stop();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            // Fermeture !! Arret du client
            if (comm != null)
            {
                this.comm.Stop();
            }
        }

        private void buttonEnvoi_Click(object sender, EventArgs e)
        {
            if (comm != null)
            {
                this.comm.Ecrire(this.textMessage.Text);
                //
                OutilsChat.Message newMessage = new OutilsChat.Message(0, this.textMessage.Text);
                newMessage.Envoi(this.textAlias.Text);
                this.AjoutMessage(newMessage, Color.Black);
            }
        }

        private void color_btn_Click(object sender, EventArgs e)
        {
            this.colorDialog1 = new ColorDialog();
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                foreach (var client in clients.Keys.ToList())
                {
                    clients[client] = this.colorDialog1.Color;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Filtre pour les fichiers image
                openFileDialog.Filter = "Images (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Charger l'image sélectionnée
                    Image selectedImage = Image.FromFile(openFileDialog.FileName);

                    // Créer un message avec l'image
                    OutilsChat.Message imageMessage = new OutilsChat.Message(0, "[Image envoyée]");
                    imageMessage.Image = selectedImage;  // Définir l'image dans le message

                    // Envoyer via GestionChat
                    if (comm != null)
                    {
                        comm.Ecrire(imageMessage);  // Méthode qui gère l'envoi des messages
                    }

                    // Afficher l'image localement (dans l'interface)
                    PictureBox pictureBox = new PictureBox
                    {
                        Image = selectedImage,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Size = new Size(200, 200),
                        Location = new Point(10, richMessages.Bottom + 10)  // Positionner en dessous des messages
                    };
                    this.Controls.Add(pictureBox);
                }
            }
        }
    }
}
