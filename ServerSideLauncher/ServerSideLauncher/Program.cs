using LauncherServer;
using System;

namespace ServerSideLauncher
{
    class Program
    {
        static void Main(string[] args)
        {
            new TcpServer();
            Console.ReadLine();
        }
    }
}
