using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    internal struct LASTINPUTINFO
    {
        public uint cbSize;
        public uint dwTime;
    }

    class Program
    {
        [DllImport("User32.dll")]
        public static extern bool LockWorkStation();
        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO Dummy);
        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        public static uint GetIdleTime()
        {
            LASTINPUTINFO LastUserAction = new LASTINPUTINFO();
            LastUserAction.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(LastUserAction);
            GetLastInputInfo(ref LastUserAction);
            return ((uint)Environment.TickCount - LastUserAction.dwTime);
        }

        public static long GetTickCount()
        {
            return Environment.TickCount;
        }

        public static long GetLastInputTime()
        {
            LASTINPUTINFO LastUserAction = new LASTINPUTINFO();
            LastUserAction.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(LastUserAction);
            if (!GetLastInputInfo(ref LastUserAction))
            {
                throw new Exception(GetLastError().ToString());
            }

            return LastUserAction.dwTime;
        }

        static void p_Exited(object sender, EventArgs e)
        {
            minerRunning = false;
        }

        static bool minerRunning = false;

        static void ShowErrorAndExit(string error)
        {
            Console.WriteLine(error);
            Console.ReadLine();
            Environment.Exit(0);
        }

        static List<string> ReadSettingsFile(string path)
        {
            if (!File.Exists(path))
            {
                ShowErrorAndExit("Settings file not found. Make sure " + path + " is in the same folder as this application");
            }

            var settings = File.ReadAllLines(path).ToList();

            if (settings.Count < 2)
            {
                ShowErrorAndExit("Settings file is empty or doesn't contain complete settings. First line should represent the idle timer and the second line should represent the miner .exe name");
            }

            return settings;
        }

        static void Main(string[] args)
        {
            var settings = ReadSettingsFile("idleMiner_settings.txt");
            int idleTimer = int.Parse(settings[0]) * 1000;
            string minerName = settings[1];

            ProcessStartInfo processStartInfo;
            Process process;

            processStartInfo = new ProcessStartInfo();
            processStartInfo.CreateNoWindow = false;
            processStartInfo.UseShellExecute = false;
            processStartInfo.FileName = minerName;

            process = new Process();
            process.StartInfo = processStartInfo;
            process.EnableRaisingEvents = true;

            process.OutputDataReceived += new DataReceivedEventHandler
            (
                delegate (object sender, DataReceivedEventArgs e)
                {
                    Console.WriteLine(e.Data);
                }
            );

            process.Exited += p_Exited;

            Console.WriteLine("Waiting for system idle...");

            while (true)
            {
                Thread.Sleep(1000);

                if (GetIdleTime() >= idleTimer && !minerRunning)
                {
                    Console.WriteLine("System is idle. Starting miner...");
                    process.Start();
                    minerRunning = true;
                }

                if (GetIdleTime() < idleTimer && minerRunning)
                {
                    Console.WriteLine("System no longer idle. MINER STOPPED.");
                    process.Kill();
                    minerRunning = false;
                }

            }

        }
    }
}
