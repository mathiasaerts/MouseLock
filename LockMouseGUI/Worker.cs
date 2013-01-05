using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.IO;
using Shell32;


namespace LockMouseGUI
{
    class Worker
    {
        private static readonly string fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MouseLock\\";
        private static readonly string filePath = fileLocation + "programs.txt";

        private static string lastProcess = "";
        private static string currentProcess = "";
        private static List<string> processList;

        private static bool allowRun = false;
        private static bool alwaysLock = false;

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        /* Add console for debugging
        [DllImport("kernel32.dll")]
        static extern bool AttachConsole(int dwProcessId);
        private const int ATTACH_PARENT_PROCESS = -1;
         * */

        public Worker()
        {
            /*
            // Redirect console output to parent process
            AttachConsole(ATTACH_PARENT_PROCESS);
             * */

            processList = new List<string>();

            // Check if file exists
            if (!File.Exists(@filePath))
            {
                // Create file
                try
                {
                    if (!Directory.Exists(fileLocation))
                        Directory.CreateDirectory(fileLocation);

                    FileStream fs = File.Create(@filePath);
                    fs.Close();
                }
                catch
                {
                    MessageBox.Show(
                    "Mouse Lock was unable to write the program list. Please make sure the " + filePath + " file is not read-only. The program will now exit.",
                    "Unable to write to file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                }
            }

            // Read program list from file
            string[] lines = System.IO.File.ReadAllLines(@filePath);

            // Add each line to the list
            foreach (string line in lines)
            {
                processList.Add(line);
            }

            // Allow running worker
            allowRun = true;
        }

        #region Getters, setters and other methods for processList

        public static List<string> getProcessList()
        {
            return processList;
        }

        public static void setProcessList(List<string> newList)
        {
            processList = newList;
        }

        public static void addProcess(string newProcess)
        {
            // If shortcut, get target exe
            if (newProcess.EndsWith(".lnk"))
                newProcess = Worker.GetShortcutTargetFile(newProcess);
            // Add if ends with .exe
            if (newProcess.EndsWith(".exe"))
            {
                if (inList(newProcess, processList))
                {
                    MessageBox.Show(
                    "The selected executable is already on the list.",
                    "Already in list",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
                else
                    processList.Add(newProcess);
            }
            else
            {
                MessageBox.Show(
                    "Only executables can be added to the list.",
                    "Invalid executable",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            updateFile();
        }

        public static void delProcess(string procName)
        {
            processList.Remove(procName);
            updateFile();
        }

        public static void delProcess(int procID)
        {
            processList.Remove(processList.ElementAt(procID));
            updateFile();
        }

        public static void enableAlwaysLock()
        {
            // Enable always lock
            alwaysLock = true;
            // Reset last process to force update on lock
            lastProcess = "";
        }

        public static void disableAlwaysLock()
        {
            // Disable always lock
            alwaysLock = false;

            // Disable current lock
            Cursor.Clip = Rectangle.Empty;

            // Reset last process to force update on lock
            lastProcess = "";
        }

        public static void updateFile()
        {
            try
            {
                File.WriteAllLines(@filePath, processList.ToArray());
            }
            catch
            {
                MessageBox.Show(
                    "Mouse Lock was unable to write the program list. Please make sure the " + filePath + " file is not read-only.",
                    "Unable to write to file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        #endregion

        public static string GetShortcutTargetFile(string shortcutFilename)
        {
            // Get directory
            string pathOnly = Path.GetDirectoryName(shortcutFilename);
            // Get filename
            string filenameOnly = Path.GetFileName(shortcutFilename);

            Shell shell = new Shell();
            Folder folder = shell.NameSpace(pathOnly);
            FolderItem folderItem = folder.ParseName(filenameOnly);

            if (folderItem != null)
            {
                // Get shortcut target
                ShellLinkObject link = (ShellLinkObject) folderItem.GetLink;
                return link.Path;
            }

            // Not found, return empty string
            return "";
        }

        public static bool inList(string checkString, List<string> list)
        {
            // To lowercase
            checkString = checkString.ToLower();
            foreach (string item in list)
            {
                if (checkString.Equals(item.ToLower()))
                    return true;
            }
            return false;
        }

        public static string GetActiveProcessFileName()
        {
            try
            {
                uint pid;
                string retVal;

                // Get foreground window handle
                IntPtr hwnd = GetForegroundWindow();
                // Get Process ID
                GetWindowThreadProcessId(hwnd, out pid);

                // Get process from PID
                Process p = Process.GetProcessById((int) pid);
                retVal = p.MainModule.FileName;
                p.Close();
                return retVal;
            }
            catch
            {
                return "Error/x64";
            }
        }

        public void run()
        {
            ThreadStart updateCurrentProcess = delegate
            {
                while (allowRun)
                {
                    // Get active process
                    currentProcess = GetActiveProcessFileName();

                    // Debug output
                    //Console.WriteLine(currentProcess);

                    // If current process changed and is in our list
                    if (alwaysLock || (!currentProcess.Equals(lastProcess) && inList(currentProcess, processList)))
                    {
                        // Get screen resolution and set size
                        Cursor.Clip = Screen.PrimaryScreen.Bounds;
                    }
                    // Update last process
                    lastProcess = currentProcess;

                    Thread.Sleep(500);
                }

            };
            Thread t1 = new Thread(updateCurrentProcess);
            t1.IsBackground = true;
            t1.Start();
        }

        public static void stop()
        {
            allowRun = false;
        }
    }
}
