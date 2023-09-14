using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation; 

namespace BFC_Wifi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;
            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }
            return pingable;
        }

        static async Task Main(string[] args)
        {
            try
            {
                await ScanWifiNetworksAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba: " + ex.Message);
            }
        }

        static async Task ScanWifiNetworksAsync(out string wifi )
        {
            try
            {
                // Spustit nástroj netsh pro zjištění dostupných Wi-Fi sítí
                Process process = new Process();
                process.StartInfo.FileName = "netsh";
                process.StartInfo.Arguments = "wlan show networks mode=Bssid";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.Start();

                // Asynchronní čekání na ukončení procesu
                await Task.Run(() => process.WaitForExit());

                // Přečíst výstup nástroje netsh
                string output = process.StandardOutput.ReadToEnd();

                // Výstup obsahuje informace o dostupných Wi-Fi sítích
                wifi
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba: " + ex.Message);
            }
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            if (PingHost("8.8.8.8"))
            {

            }
            else
            {
                label2.Text = "Not connected";
                label2.Visible = true;
            }
        }
    }
}
