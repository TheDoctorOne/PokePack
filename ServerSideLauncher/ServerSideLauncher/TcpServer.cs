using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace LauncherServer
{
    class TcpServer
    {
        private TcpListener tcpListener;
        private TcpListener tcpListener2;
        private String USER_LOG = "/userlog.log";
        private String USER_LOGIN_DATA = "/userlogin.dat";
        private String USER_CONNECTED = "/connecteduser.dat";

        public TcpServer()
        {
            String appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            USER_LOG = appdata + USER_LOG;
            USER_LOGIN_DATA = appdata + USER_LOGIN_DATA;
            USER_CONNECTED = appdata + USER_CONNECTED;

            Console.WriteLine("File creation.");
            if (!File.Exists(USER_LOG))
                File.Create(USER_LOG);

            if (!File.Exists(USER_LOGIN_DATA))
                File.Create(USER_LOGIN_DATA);

            if (!File.Exists(USER_CONNECTED))
                File.Create(USER_CONNECTED);
            Console.WriteLine("Listener Init");

            this.tcpListener = new TcpListener(IPAddress.Parse("185.254.29.188"), 2999);
            tcpListener.Start();

            Console.WriteLine("Listening... Port 2999");
            new Thread(() => {
                while (true)
                {
                    Socket client = tcpListener.AcceptSocket();
                    Console.WriteLine("Connected: " + client.RemoteEndPoint.ToString());
                    new Thread(() =>
                    {
                        listener(client);
                    }).Start();
                }
            }).Start();

            /*
            this.tcpListener2 = new TcpListener(IPAddress.Parse("193.31.118.173"), 2998);
            tcpListener.Start();

            Console.WriteLine("Listening... Port 2998");

            new Thread(() => {
                while (true)
                {
                    Socket client = tcpListener.AcceptSocket();
                    Console.WriteLine("Connected: " + client.RemoteEndPoint.ToString());
                    new Thread(() =>
                    {
                        listener(client);
                    }).Start();
                }
            }).Start();
            */
        }

        private void listener(Socket client)
        {
            try
            {
                byte[] recData = new byte[256];
                int recv = client.Receive(recData);
                List<byte> dat = new List<byte>();
                for (int i = 0; i < recv; i++)
                {
                    dat.Add(recData[i]);
                }
                bool isSuccess = processData(dat);
                locker = false;
                client.Send(Encoding.ASCII.GetBytes(isSuccess.ToString()));
                client.Close();
            } catch(Exception) { }
        }

        private bool locker = false;
        private bool processData(List<byte> recData)
        {
            bool ret = false;
            // username;ip-adress;pwd
            String[] data = Encoding.ASCII.GetString(recData.ToArray()).Split(';');
            String username = data[0];
            String ipAddress = data[1];
            String pwd = data[2];
            String pwd256 = ComputeSha256Hash(pwd);
            while (locker)
                Thread.Sleep(300);
            locker = true;
            //username exist ? check password : register
            String[] userData = File.ReadAllLines(USER_LOGIN_DATA);
            foreach (String dat in userData)
            {
                String uName = dat.Split(';')[0];
                String pwd256_ = dat.Split(';')[1];
                if (uName == username)
                {
                    if (pwd256_ == pwd256)
                    {
                        ret = true;
                        String[] connectedList = File.ReadAllLines(USER_CONNECTED);
                        int i = 0;
                        bool newEntry = true;
                        foreach (String connected in connectedList) {
                            if (connected == username + ";" + ipAddress) 
                            {
                                newEntry = false;
                                break;
                            }
                            else if (connected.Split(';')[0] == username)
                            {
                                newEntry = false;
                                connectedList[i] = username + ";" + ipAddress;
                                File.WriteAllLines(USER_CONNECTED, connectedList);
                                break;
                            }
                            i++;
                        }
                        if (newEntry)
                            File.AppendAllText(USER_CONNECTED, username + ";" + ipAddress + "\n");
                        File.AppendAllText(USER_LOG, ("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "]" + username + ";" + ipAddress + "\n"));
                        Console.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "] Logged in:" + username + ";" + ipAddress + "\n");
                    }
                    else
                    { //PASS WRONG
                        Console.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "] Passwrong:" + username + ";" + ipAddress);
                        File.AppendAllText(USER_LOG, "[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "] Passwrong:" + username + ";" + ipAddress);
                        return false;
                    }
                }
            }
            if (!ret)
            {
                File.AppendAllText(USER_LOGIN_DATA, username + ";" + pwd256 + "\n");
                Console.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "] Registered:" + username + ";" + ipAddress);
                ret = true;
            }
            return ret;
        }

        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.ASCII.GetBytes(rawData));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}
