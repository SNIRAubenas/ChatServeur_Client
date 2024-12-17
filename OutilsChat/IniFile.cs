using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace OutilsChat
{
    public class IniFile
    {
        private string path;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        /// <summary>
        /// INIFile Constructor.
        /// </summary>
        /// <param name="INIPath"></param>
        public IniFile(string iniPath)
        {
            path = iniPath;
        }

        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <param name="Section"></param>
        /// Section name
        /// <param name="Key"></param>
        /// Key Name
        /// <param name="Value"></param>
        /// Value Name
        public void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        public void WriteValue(string Section, string Key, bool Value)
        {
            string value = "false";
            if (Value)
                value = "true";
            WritePrivateProfileString(Section, Key, value, this.path);
        }

        public void WriteValue(string Section, string Key, int Value)
        {
            WritePrivateProfileString(Section, Key, Value.ToString(), this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <param name="Section"></param>
        /// <param name="Key"></param>
        /// <param name="Path"></param>
        /// <returns></returns>
        public string ReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }

        public string ReadValue(string Section, string Key, string Def)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, Def, temp, 255, this.path);
            return temp.ToString();
        }

        public bool ReadValue(string Section, string Key, bool Def)
        {
            //
            String defaut = "false";
            if (Def)
                defaut = "true";
            defaut = ReadValue(Section, Key, defaut);
            return (defaut.ToLower() == "true");
        }

        public int ReadValue(string Section, string Key, int Def)
        {
            //
            String value = Def.ToString();
            value = ReadValue(Section, Key, value);
            //
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return Def;
            }
        }

    }
}

