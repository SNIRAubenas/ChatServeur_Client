using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutilsChat
{
    /// <summary>
    /// Un message c'est un ensemble d'information, qui sont stockées dans RawData :
    ///  0 : Id 
    ///  1 : Commande
    ///  2 : Param 1
    ///  3 : Param 2
    ///  4 : Texte du message
    /// </summary>
    public class Message
    {
        private string separator;
        private String[] rawData;

        public int Id
        {
            get
            {
                int result = 0;
                String tmp =this.rawData[0];
                int.TryParse(tmp, out result);
                return result;
            }

            set
            {
                this.rawData[0] = value.ToString();
            }
        }

        public string Texte
        {
            get
            {
                return this.rawData[4];
            }

            set
            {
                this.rawData[4] = value;
            }
        }

        public String Data
        {
            get
            {
                StringBuilder concat = new StringBuilder();
                for (int i = 0; i < rawData.Length; i++)
                {
                    concat.Append(this.rawData[i]);
                    if (i < rawData.Length - 1)
                        concat.Append(this.separator);
                }
                return concat.ToString();
            }

            set
            {
                ClearData();
                // Id, Cmd, Param1, Param2, Data
                String[] tmpData = value.Split(new String[] { this.separator }, 5, StringSplitOptions.None);
                for (int i = 0; i < tmpData.Length; i++)
                {
                    this.rawData[i] = tmpData[i];
                }
            }
        }

        public Message( string RawData )
        {
            this.separator = Constants.CMD_SEPARATOR;
            this.Data = RawData;
        }

        public void ClearData()
        {
            this.rawData = new string[] { "", "", "", "", "" };
        }

        public Message(int id, String texte ):this("")
        {
            this.Id = id;
            this.Texte = texte;
        }

        public String Command
        {
            get
            {
                return this.rawData[1];
            }
            set
            {
                this.rawData[1] = value; ;
            }
        }

        public String Param1
        {
            get
            {
                return this.rawData[2];
            }
            set
            {
                this.rawData[2] = value; ;
            }
        }

        public String Param2
        {
            get
            {
                return this.rawData[3];
            }
            set
            {
                this.rawData[3] = value; ;
            }
        }

        public String this[ int num ]
        {
            get
            {
                return this.rawData[num];
            }
        }

        public void Envoi(string alias)
        {
            this.Command = Constants.CMD_MSG;
            this.Param1 = alias;
        }
    }
}
