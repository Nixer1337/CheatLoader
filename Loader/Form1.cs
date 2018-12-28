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

        private string GetUrlData(string url)
        {
            HttpWebRequest proxy_request = (HttpWebRequest)WebRequest.Create(url);
            proxy_request.Method = "GET";
            proxy_request.ContentType = "application/x-www-form-urlencoded";
            proxy_request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US) AppleWebKit/532.5 (KHTML, like Gecko) Chrome/4.0.249.89 Safari/532.5";
            proxy_request.KeepAlive = true;
            HttpWebResponse resp = proxy_request.GetResponse() as HttpWebResponse;
            string html = "";
            using (StreamReader sr = new StreamReader(resp.GetResponseStream(), Encoding.GetEncoding(1251)))
                html = sr.ReadToEnd();
            html = html.Trim();

            return html;
        }

        public Form1()
        {
            InitializeComponent();

            if (GetUrlData("https://raw.githubusercontent.com/Nixer1337/CheatLoader/master/version") != "0.02")
            {
                MessageBox.Show("Лоадер устарел!");
                Process.Start("https://yougame.biz/threads/60719/");
                Process.GetCurrentProcess().Kill();
            }

            comboBox1.Items.Add("PPHUD");
            comboBox1.Items.Add("Crytallity.win");
            comboBox1.SelectedIndex = 0;

            var SkinManager = MaterialSkin.MaterialSkinManager.Instance;

            SkinManager.AddFormToManage(this);
            SkinManager.Theme = MaterialSkin.MaterialSkinManager.Themes.LIGHT;
            SkinManager.ColorScheme = new MaterialSkin.ColorScheme(MaterialSkin.Primary.Red500, MaterialSkin.Primary.Red600, MaterialSkin.Primary.Red500, MaterialSkin.Accent.Purple400, MaterialSkin.TextShade.WHITE);
        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            Process.Start("https://vk.com/nixer1337");
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            this.Hide();

            var dll = new WebClient().DownloadData(comboBox1.SelectedIndex == 0 ? "https://github.com/Nixer1337/CheatLoader/blob/master/Cheats/pphud.dll?raw=true" : "https://github.com/Nixer1337/CheatLoader/blob/master/Cheats/Crytallity.win.dll?raw=true");

            while (Process.GetProcessesByName("csgo").Length == 0)
            {
                Thread.Sleep(500);
            }

            Inject(dll);

            Process.GetCurrentProcess().Kill();
        }
    }
}
