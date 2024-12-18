using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        private void InsertImageInRichTextBox(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64Image = Convert.ToBase64String(imageBytes);
                string rtfImage = @"{\pict\pngblip " + BitConverter.ToString(imageBytes).Replace("-", "") + "}";
                this.richMessages.SelectedRtf = rtfImage;
            }
        }

        public bool IsBase64String(string str)
        {
            if (str == null)
                return false;

            str = str.Trim();
            if (str.Length % 4 != 0)
                return false;

            return Regex.IsMatch(str, @"^[a-zA-Z0-9\+/]*={0,2}$");
        }

        public MainWindow()
        {
            InitializeComponent();
            comm = null;
            configFile = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "config.ini");
            int port = configFile.ReadValue("Server", "Port", 18);
            string ipAddress = configFile.ReadValue("Server", "IPAddress", "127.0.0.1");
            string alias = configFile.ReadValue("User", "Alias", "jean chaineDBeat");
            this.numericPort.Value = port;
            this.ipAddressControl1.IPAddress = ipAddress;
            this.textAlias.Text = alias;
            this.Text += " " + Constants.APP_VERSION;
            this.clients = new Dictionary<int, Color>();
        }

        public static string ImageToBase64(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                return Convert.ToBase64String(imageBytes);
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (comm == null)
            {
                configFile = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "config.ini");
                configFile.WriteValue("Server", "Port", (int)this.numericPort.Value);
                configFile.WriteValue("Server", "IPAddress", this.ipAddressControl1.IPAddress);
                configFile.WriteValue("User", "Alias", this.textAlias.Text);
                this.comm = new GestionChat(this.ipAddressControl1.IPAddress, (int)this.numericPort.Value, this.textAlias.Text);
                this.comm.OnMessageReceived += new OnMessageReceived(this.OnMessageReceived);
                this.comm.OnClientDisconnected += new OnClientDisconnected(this.OnClientDisconnected);
                this.comm.Start();

                if (this.comm.Connected)
                {
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
            this.Invoke((OnClientDisconnected)this.DoClientDisconnected, new object[] { sender });
        }

        private void DoClientDisconnected(GestionChat sender)
        {
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
            this.Invoke((OnMessageReceived)this.DoMessageReceived, new object[] { sender, message });
        }

        private void DoMessageReceived(GestionChat sender, OutilsChat.Message message)
        {
            if (IsBase64String(message.Texte))
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(message.Texte);

                    if (imageBytes.Length == 0)
                    {
                        this.AfficherErreur("Le message ne contient pas d'image valide.");
                        return;
                    }

                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        Image image = Image.FromStream(ms);

                        if (image == null)
                        {
                            this.AfficherErreur("Erreur lors de la création de l'image.");
                            return;
                        }

                        DisplayImageInPictureBox(image);
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine($"Erreur de format Base64 : {ex.Message}");
                    this.AfficherErreur("Erreur de format Base64.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la conversion Base64 en image : {ex.Message}");
                    this.AfficherErreur("Erreur de réception de l'image.");
                }
            }
            else
            {
                AjoutMessage(message, this.clients[message.Id]);
            }
        }

        private void AjoutMessage(OutilsChat.Message msg, Color clr)
        {
            int beforeAppend = this.richMessages.TextLength;
            this.richMessages.AppendText(msg.Param1 + " - " + DateTime.Now.ToString() + Environment.NewLine);
            int afterAppend = this.richMessages.TextLength;
            this.richMessages.Select(beforeAppend, afterAppend - beforeAppend);
            this.richMessages.SelectionColor = clr;
            System.Drawing.Font currentFont = richMessages.SelectionFont;
            richMessages.SelectionFont = new Font(currentFont, FontStyle.Bold);
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
            if (comm != null)
            {
                this.comm.OnClientDisconnected = null;
                this.comm.Stop();
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
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
                openFileDialog.Filter = "Images (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Image selectedImage = Image.FromFile(openFileDialog.FileName);
                    string imageBase64 = ImageToBase64(selectedImage);
                    OutilsChat.Message imageMessage = new OutilsChat.Message(0, "[Image envoyée]");
                    imageMessage.Texte = imageBase64;
                    if (comm != null)
                    {
                        comm.Ecrire(imageMessage.Texte);
                    }

                    PictureBox pictureBox = new PictureBox
                    {
                        Image = selectedImage,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Size = new Size(100, 100),
                        Location = new Point(600, richMessages.Bottom + 10)
                    };
                    this.Controls.Add(pictureBox);
                }
            }
        }

        private void DisplayImageInPictureBox(Image image)
        {
            PictureBox pictureBox = new PictureBox
            {
                Image = image,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(200, 200),
                Location = new Point(this.ClientSize.Width - 210, 10)
            };

            this.Controls.Add(pictureBox);
        }
    }
}
