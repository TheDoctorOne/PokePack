using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Compression;

namespace PokePackClient
{
    class ServerHandler
    {
        public bool server_pending { get; } = true;
        public bool server_online { get; } = false;
        public bool isPackDown = false;

        private Label status;
        private Button button;

        private TcpClient tcpClient;

        private int port = 25565;
        public ServerHandler(Label serverStat, Label status, Button button)
        {
            this.button = button;
            this.status = status;
            if (server_pending)
                serverStat.Text = "Checking Server Status";
            server_online = isServerAvailable(References.url, port);
            if (server_online)
            {
                serverStat.Text = "SERVER ONLINE!";
                serverStat.ForeColor = Color.GreenYellow;
            }
            else
            {
                serverStat.Text = "SERVER OFFLINE";
                serverStat.ForeColor = Color.Red;
            }
            server_pending = false;


            //checksPackDown(status);
        }

        private bool isServerAvailable(string url, int port)
        {
            try
            {
                using (var client = new TcpClient(url, port))
                    return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }

        private void checksPackDown(Label status)
        {
            String path = References.FOLDER_PATH;
            try
            {
                status.ForeColor = Color.BlueViolet;
                status.Text = "Pack Status: Getting folders";
                String dir = new WebClient().DownloadString(References.dir_url);
                status.Text = "Pack Status: Generating folders";
                foreach (String folder in dir.Split('\n')) 
                {
                    if(!Directory.Exists(path + folder))
                    {
                        Directory.CreateDirectory(path+folder);
                    }
                }
                status.Text = "Pack Status: Folders done.";
                Thread t = new Thread(new ThreadStart(() =>
                {
                    
                        //checkPack();
                    
                }));
                t.IsBackground = true;
                t.Start();
            }
            catch (Exception)
            {
                status.Text = "Pack Status: ERROR!";
                status.ForeColor = Color.Red;
            }
        }

        public bool checkPack(bool forceDownPack, bool forceJava)
        {
            try
            {
                String appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                String path = References.FOLDER_PATH;


                updateStatus("Getting folders");
                String dir = new WebClient().DownloadString(References.dir_url);
                updateStatus("Generating folders");
                foreach (String folder in dir.Split('\n'))
                {
                    if (!Directory.Exists(path + folder))
                    {
                        Directory.CreateDirectory(path + folder);
                    }
                }

                /*String jredir = new WebClient().DownloadString(References.jreDirs_url);
                foreach (String folder in jredir.Split('\n'))
                {
                    if (!Directory.Exists(appdata + folder))
                    {
                        Directory.CreateDirectory(appdata + folder);
                    }
                }*/
                updateStatus("Folders done.");


                String files = new WebClient().DownloadString(References.files_url);
                String jrefiles = new WebClient().DownloadString(References.jreZips_url);

                String rm_files = new WebClient().DownloadString(References.rm_files_url);
                updateStatus("Downloading pack...");
                foreach (String file in rm_files.Split('\n'))
                {
                    String dfile = file.Replace("\r", "");
                    String filename = dfile.Split('/')[dfile.Split('/').Length - 1];
                    updateStatus("Deleting " + filename);
                    if (File.Exists(path + dfile))
                        File.Delete(path + dfile);
                }
                if (forceJava)
                {
                    download(jrefiles, appdata, forceJava);
                    foreach (String zip in jrefiles.Split('\n'))
                    {
                        String file = zip.Replace(".zip", "");
                        file = file.Replace("\r", "");
                        if (Environment.Is64BitOperatingSystem)
                            if (file.Contains("32"))
                                continue;
                        if (Directory.Exists(appdata + file) && !forceJava)
                            continue;
                        updateStatus("Unzipping Java");
                        try
                        {
                            ZipFile.ExtractToDirectory(appdata + (zip.Replace("\r", "")), appdata);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("DELETE THE " + file + " FOLDER!", "CRITICAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            MessageBox.Show("-> " + file + " ADLI KLASÖRÜ SİL! TEKRAR DENE! Klasörü açılan yerden sil.", "CRITICAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            OpenFolder(appdata);
                        }
                    }
                }

                download(files, path, forceDownPack);


                updateStatus("DONE! PLAY!");
                isPackDown = true;
                button.Invoke(new Action(() => button.Enabled = true));
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                MessageBox.Show("CRITICAL ERROR WHILE CHECKING FILES. IS SERVER ONLINE?\nError Log:" + e.StackTrace, "CRITICAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                updateStatus("Pack Status: ERROR!");
                Environment.Exit(-1);
            }
            return false;

        }

        private void download(String files, String path, bool force)
        {
            foreach (String file in files.Split('\n'))
            {
                String dfile = file.Replace("\r", "");
                String filename = dfile.Split('/')[dfile.Split('/').Length - 1];
                if (dfile.Trim() == "") { }
                else if (File.Exists(path + dfile) && !force)
                {
                    updateStatus("Checking " + filename);
                    long exist = new FileInfo(path + dfile).Length;
                    long remote = GetFileSize(References.files_data_url + dfile);
                    if (exist == remote && !force)
                    {
                        updateStatus("Checked " + filename);
                    }
                    else
                    {
                        updateStatus("Downloading " + filename + "...");
                        new WebClient().DownloadFile(References.files_data_url + dfile, path + dfile);
                    }
                }
                else
                {
                    updateStatus("Downloading " + filename + "...");
                    new WebClient().DownloadFile(References.files_data_url + dfile, path + dfile);
                }
            }
        }

        public void updateStatus(String update)
        {
            status.Invoke(new Action(() => status.Text = "Pack Status: " + update));
        }

        private long GetFileSize(String uriPath)
        {
            var webRequest = HttpWebRequest.Create(uriPath);
            webRequest.Method = "HEAD";

            using (var webResponse = webRequest.GetResponse())
            {
                String s = webResponse.Headers.Get("Content-Length");
                return long.Parse(s == null ? "0" : s);
            }
        }

        public bool login(String username, String pwd)
        {
            try
            {
                //tcpClient = new TcpClient("play.mahmutkocas.me", 2999);
                tcpClient = new TcpClient(References.IPAdress, 2999);
                byte[] user = Encoding.ASCII.GetBytes(username + ";");
                byte[] ipAdd = Encoding.ASCII.GetBytes(GetIPAddress() + ";");
                byte[] pass = Encoding.ASCII.GetBytes(pwd);
                List<byte> send = new List<byte>();
                send.AddRange(user);
                send.AddRange(ipAdd);
                send.AddRange(pass);
                tcpClient.Client.Send(send.ToArray());
                byte[] ret = new byte[16];
                updateStatus("Checking login information");
                tcpClient.Client.ReceiveTimeout = 5000;
                int recieve = tcpClient.Client.Receive(ret);
                tcpClient.Close();
                List<byte> recieveList = new List<byte>();
                for (int i=0; i<recieve ;i++)
                {
                    recieveList.Add(ret[i]);
                }
                String retStr = Encoding.ASCII.GetString(recieveList.ToArray());
                bool retBool = bool.Parse(retStr);
                return retBool;
            }
            catch(Exception)
            {
                return false;
            }
        }

        private string GetIPAddress()
        {
            String address = "";
            WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
            using (WebResponse response = request.GetResponse())
            using (StreamReader stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }

            int first = address.IndexOf("Address: ") + 9;
            int last = address.LastIndexOf("</body>");
            address = address.Substring(first, last - first);

            return address;
        }

        private void OpenFolder(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    Arguments = folderPath,
                    FileName = "explorer.exe"
                };

                Process.Start(startInfo);
            }
        }

        
    }
}
