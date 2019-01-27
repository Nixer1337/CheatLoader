using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using MaterialSkin.Controls;
using System.Net;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Loader
{
    public partial class Form1 : MaterialForm
    {
        [DllImport("InjectLib.dll", CallingConvention = CallingConvention.Cdecl)]
        private extern static void Inject(byte[] buf);

        public Form1()
        {
            string version = Encoding.ASCII.GetString(new WebClient().DownloadData("https://raw.githubusercontent.com/Nixer1337/CheatLoader/master/version"));

            if (version != "0.04")
            {
                MessageBox.Show("Лоадер устарел");
                Process.Start("https://yougame.biz/threads/60719/");
                Process.GetCurrentProcess().Kill();
            }

            InitializeComponent();

            comboBox1.Items.Add("PPHUD");
            comboBox1.Items.Add("Crytallity.win");
            comboBox1.Items.Add("Acedia");
            comboBox1.Items.Add("LuckyCharms");
            comboBox1.Items.Add("1tapgang.cc");

            comboBox1.SelectedIndex = 0;

            var SkinManager = MaterialSkin.MaterialSkinManager.Instance;

            SkinManager.AddFormToManage(this);
            SkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            SkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Red500, MaterialSkin.Primary.Red600, MaterialSkin.Primary.Red500, MaterialSkin.Accent.Purple400, MaterialSkin.TextShade.WHITE);
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            Process.Start("https://vk.com/nixware/");
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            this.Hide();

            byte[] dll = null;

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    dll = new WebClient().DownloadData("https://github.com/Nixer1337/CheatLoader/blob/master/Cheats/pphud.dll?raw=true");
                    break;
                case 1:
                    dll = new WebClient().DownloadData("https://github.com/Nixer1337/CheatLoader/blob/master/Cheats/Crytallity.win.dll?raw=true");
                    break;
                case 2:
                    dll = new WebClient().DownloadData("https://github.com/Nixer1337/CheatLoader/blob/master/Cheats/acedia.dll?raw=true");
                    break;
                case 3:
                    dll = new WebClient().DownloadData("https://github.com/Nixer1337/CheatLoader/blob/master/Cheats/luckycharms.dll?raw=true");
                    break; 
                case 4:
                    dll = new WebClient().DownloadData("https://github.com/Nixer1337/CheatLoader/blob/master/Cheats/onetapgang.dll?raw=true");
                    break;
            }

            while (Process.GetProcessesByName("csgo").Length == 0)
            {
                Thread.Sleep(500);
            }

            Inject(dll);

            Process.GetCurrentProcess().Kill();
        }
    }
}
