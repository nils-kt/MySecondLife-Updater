using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;

namespace Updater
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();

            Console.Title = "MySecondLife Updater";
            Console.WriteLine("Lade Update herunter ...");
            Console.WriteLine("");


            Process[] ProcessName = Process.GetProcessesByName("MySecondLife - Modpack-Loader");

            while(ProcessName.Length != 0)
            {
                Console.Write("\rWarte auf Beendigung vom Mod-Launcher ...");
                ProcessName = Process.GetProcessesByName("MySecondLife - Modpack-Loader");
                Thread.Sleep(100);
            }

            Console.Clear();

            if (File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\MySecondLife - Modpack-Loader.exe"))
            {
                File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\MySecondLife - Modpack-Loader.exe");
            }


            using (WebClient Client = new WebClient())
            {
                Client.DownloadFileAsync(new Uri("https://mods.my2.life/update.exe"), Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\MySecondLife - Modpack-Loader.exe");
                Client.DownloadProgressChanged += OnDownloadProgressChanged;
                Client.DownloadFileCompleted += OnDownloadFileCompleted;
            }

            Console.Read();

        }

        private static void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\update.bat"))
            {
                File.Delete(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\update.bat");
            }
            Process.Start(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\MySecondLife - Modpack-Loader.exe");
            Environment.Exit(0);
        }

        private static void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Console.Write("\rFortschritt: {0}%", e.ProgressPercentage);
        }
    }
}
