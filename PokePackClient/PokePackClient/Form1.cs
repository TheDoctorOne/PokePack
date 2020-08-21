using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace PokePackClient
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);

        Process game;
        ServerHandler serverHandler;
        TrickHandler trickHandler;
        public Form1()
        {
            Debug.WriteLine(References.RUN_ARGS(2300, "testacc"));
            InitializeComponent();
            Application.ApplicationExit += new EventHandler(this.onAppExit);

            this.trickHandler = new TrickHandler(game);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //loginButton.Enabled = false;

            long memKb;
            GetPhysicallyInstalledSystemMemory(out memKb);
            
            if (!(memKb < 1000))
            {
                
                if(memKb / 1024 < trackBar1.Minimum)
                {
                    MessageBox.Show("You have to have at least " + trackBar1.Minimum + " MBs of RAM.");
                    trackBar1.Minimum = (int)((memKb / 1024 - 1000) < 0 ? 0 : (memKb / 1024 - 1000));
                }

                trackBar1.Maximum = (int)(memKb / 1024);
            }
            else
            {
                MessageBox.Show("Couldn't Detect RAM! Do not give out ram more than you can!\nRam : " + memKb);
            }

            trackBar1.Value = trackBar1.Minimum;
            ramLabel.Text = trackBar1.Value + " MB";



        }

        private void Form1_Shown(Object sender, EventArgs e)
        {
            loadLauncherState();
            double latest_ver = double.Parse(new WebClient().DownloadString("http://play.mahmutkocas.me/Client/launcherversion.txt"));
            if (latest_ver > References.VERSION)
            {
                MessageBox.Show("Your client is old. Update to newest version!", "Old Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Process.Start("http://play.mahmutkocas.me/#download");
                Application.Exit();
            }
            else
            {
                serverHandler = new ServerHandler(serverLabel, statusLabel, loginButton);
            }


            if (File.Exists("login.txt")) // Login through file
            {
                String logData = File.ReadAllText("login.txt");
                String[] data = logData.Split(';');
                usernameBox.Text = data[0];
                passBox.Text = data[1];
                loginButton_Click(null,null);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ramLabel.Text = trackBar1.Value + " MB";
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void loadLauncherState()
        {
            if (!Directory.Exists(References.FOLDER_PATH))
                Directory.CreateDirectory(References.FOLDER_PATH);
            if (!File.Exists(References.launcherIni))
            {
                File.Create(References.launcherIni);
                saveLauncherState();
            }
            if (File.Exists(References.launcherIni))
            {
                String[] s = File.ReadAllText(References.launcherIni).Split(';');
                if (s.Length != 2)
                    return;
                usernameBox.Text = s[0];
                trackBar1.Value = int.Parse(s[1]);
                ramLabel.Text = trackBar1.Value + " MB";
                /*if(s.Length > 2)
                {
                    javaPath = s[2];
                }*/
            }
        }

        private String javaPath = "";
        private void findJava()
        {
            bool isElevated;
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                isElevated = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            if (!isElevated)
            {
                MessageBox.Show("Run as Administrator to search for Java!", "RUN AS ADMIN!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            findJavaPath.Enabled = false;

            statusLabel.Text = "Searching for Java...";
            groupBox1.Enabled = true;

            

            new Thread(() =>
            {
                if(Environment.Is64BitOperatingSystem)
                {

                }
            string[] dirList = new string[2];
            dirList[0] = "C:/Program Files"; 
            dirList[1] = "C:/Program Files (x86)";

                foreach (string sDir in dirList)
                {
                    string[] HighLevelDirectories = null;
                    List<string> lowLevelDirectories = new List<string>();
                    HighLevelDirectories = Directory.GetDirectories(sDir, "java*", SearchOption.TopDirectoryOnly);

                    foreach(string dir in HighLevelDirectories)
                    {
                        try
                        {
                            string[] dirs = Directory.GetFiles(sDir, "*", SearchOption.AllDirectories);
                            lowLevelDirectories.AddRange(dirs);
                        }
                        catch (UnauthorizedAccessException e) { Debug.WriteLine(dir); Debug.WriteLine(e.Message); }
                    }

                    foreach (string dir in lowLevelDirectories)
                    {
                        string[] files = Directory.GetFiles(sDir, "javaw.exe", SearchOption.AllDirectories);
                        if (files != null && files.Length > 0)
                        {
                            javaPath = files[0];
                            serverHandler.updateStatus("Java Found!");
                            break;
                        }
                        if (javaPath != "")
                            break;
                    }

                }

                serverHandler.updateStatus("Java Not Found :(");
            }
            ).Start();
        }

        private void saveLauncherState()
        {
            try
            {
                if (!Directory.Exists(References.FOLDER_PATH))
                    Directory.CreateDirectory(References.FOLDER_PATH);
                String username = usernameBox.Text;
                int RamValue = trackBar1.Value;

                /*String javaPath = "";
                if (this.javaPath != "")
                    javaPath = this.javaPath;*/

                if (!File.Exists(References.launcherIni))
                    File.Create(References.launcherIni);
                try
                {
                    File.WriteAllText(References.launcherIni, username + ";" + RamValue);
                }
                catch (Exception) { }
            } catch(Exception e) { if (e is UnauthorizedAccessException) MessageBox.Show("Unauthorized Exception. Run launcher as Admin."); }
            
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (this.usernameBox.Text == "" || this.usernameBox.Text.Trim() == "")
            {
                MessageBox.Show("Username can't be empty!", "Username not valid.", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
            if (this.passBox.Text == "" || this.passBox.Text.Trim() == "")
            {
                MessageBox.Show("Password can't be empty!", "Password not valid.", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }

            int ram = trackBar1.Value;
            String username = usernameBox.Text;
            String pwd = passBox.Text;
            bool forceDownPack = forceUpdateBox.Checked;
            bool forceJava = forceDownloadJava.Checked;

            if (!serverHandler.login(username,pwd))
            {
                MessageBox.Show("Login Failed\n1. Check your password.\n2. Login servers may offline.", "LOGIN OFFLINE", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                return;
            }
                

            groupBox1.Enabled = false;
            saveLauncherState();

            Thread t = new Thread(new ThreadStart(() =>
            {
                if (serverHandler.checkPack(forceDownPack, forceJava))
                    launchGame(ram, username);
                else
                    serverHandler.updateStatus("ERROR CHECKING PACK");
            }
            ));
            t.IsBackground = true;
            t.Start();

        }

        public void launchGame(int ram, String username)
        {
            setVisible(false);

            String appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            this.game = new Process();
            ProcessStartInfo info = null;
            if(forceDownloadJava.Checked)
                if(Environment.Is64BitOperatingSystem)
                    info = new ProcessStartInfo(appdata + "/jre64/bin/javaw.exe", References.RUN_ARGS(ram, username));
                else
                    info = new ProcessStartInfo(appdata + "/jre32/bin/javaw.exe", References.RUN_ARGS(ram, username));

            if (consoleWanted.Checked)
                info = new ProcessStartInfo("java", References.RUN_ARGS(ram, username));
            else
                info = new ProcessStartInfo("javaw", References.RUN_ARGS(ram, username));
            info.WorkingDirectory = References.FOLDER_PATH;
            game.StartInfo = info;
            game.Start();
            this.trickHandler.setGameProcess(game);



            Thread t = new Thread(new ThreadStart(() =>
            {
                while (true)
                {
                    UdpClient udpClient = new UdpClient("127.0.0.1", References.localPort);
                    if (game.HasExited)
                    {
                        //MessageBox.Show("Minecraft closed!");
                        Environment.Exit(1);
                        Debug.WriteLine("Visible");
                        setEnableLoginBox(true);
                        break;
                    }
                    else
                    {
                        if(game != null) 
                        { 
                            Byte[] alive = Encoding.ASCII.GetBytes(References.packet);
                            udpClient.Send(alive,alive.Length);
                        }
                    }
                    Thread.Sleep(1000);
                }
            }));
            t.IsBackground = true;
            t.Start();
        }


        private void setVisible(bool Visible)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(setVisible), new object[] { Visible });
                return;
            }
            this.Visible = Visible;
        }

        private void setEnableLoginBox(bool enabled)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<bool>(setEnableLoginBox), new object[] { Visible });
                return;
            }
            groupBox1.Enabled = enabled;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void onAppExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void findJavaPath_Click(object sender, EventArgs e)
        {
            //findJava();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void forceDownloadJava_CheckedChanged(object sender, EventArgs e)
        {
            if (forceDownloadJava.Checked)
            {
                MessageBox.Show("Eğer oyununuz çalışıyorsa bunu işaretlemeyin!", "Dikkat!");
            }
        }
    }
}
