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

using Microsoft.Win32;
using System.Windows.Automation;

namespace MouseLock
{
    class Worker
    {
        private static readonly string fileLocation = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\MouseLock\\";
        private static readonly string filePath = fileLocation + "programs.txt";

        private static bool alwaysLock = false;
        private static string lastProcess = "";
        private static string currentProcess = "";
        private static List<string> processList;

        public Worker()
        {
            // Initialize processlist
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
                        MessageBoxIcon.Stop
                    );
                }
            }

            // Read program list from file
            string[] lines = System.IO.File.ReadAllLines(@filePath);

            // Add each line to the list
            foreach (string line in lines)
                addProcess(line, false);

            // Update file once after all processes have been added
            updateFile();

            // Listen to events
            SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;
            Automation.AddAutomationFocusChangedEventHandler(OnFocusChangedHandler);

            // Update lock
            updateLock();
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

        public static void addProcess(string newProcess, bool doFileUpdate = true)
        {
            // If shortcut, get target exe
            if (newProcess.EndsWith(".lnk"))
            {
                newProcess = Worker.GetShortcutTargetFile(newProcess);
                if (String.IsNullOrEmpty(newProcess))
                    return;
            }
            // Add if ends with .exe
            if (newProcess.EndsWith(".exe"))
            {
                if (Util.inList(newProcess, processList))
                {
                    MessageBox.Show(
                        "The selected executable is already on the list:\n" + 
                        newProcess.Truncate(50),
                        "Duplicate executable selected",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    processList.Add(newProcess);
                }
            }
            else
            {
                MessageBox.Show(
                    "Only executables can be added to the list.",
                    "Invalid executable",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }

            if (doFileUpdate) 
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

        public static void setAlwaysLock(bool value)
        {
            // Enable always lock
            alwaysLock = value;
            updateLock();
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
                    MessageBoxIcon.Exclamation
                );
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
                try
                {
                    // Get shortcut target
                    var link = (ShellLinkObject)folderItem.GetLink;
                    if(link != null)
                        return link.Path;
                }
                catch
                {
                    MessageBox.Show("Failed to get target file for this shortcut. " +
                        "This may be due to a permission error. " +
                        "Make sure the current user can access the shortcut location." +
                        "\n\n" +
                        "This occurs for shortcuts on the Windows Public Desktop, " +
                        "try adding the executable directly instead.",
                        "Unable to get shortcut target",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                
            }

            // Not found, return empty string
            return "";
        }

        public static void updateLock()
        {

            Rectangle bounds = Rectangle.Empty;
            if (alwaysLock || Util.inList(currentProcess, processList))
            {
                bounds = Screen.PrimaryScreen.Bounds;
            }
            //Console.WriteLine("Updating lock: {0}, {1} - Bounds: {2}", alwaysLock, currentProcess, bounds);

            Cursor.Clip = bounds;
        }

        private static void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("Resolution Changed!");
            updateLock();
        }

        private static void OnFocusChangedHandler(object src, AutomationFocusChangedEventArgs args)
        {
            // Reset current process
            currentProcess = "";

            //Console.WriteLine("Focus changed! ");
            AutomationElement element = src as AutomationElement;

            try
            {
                if (element != null)
                {
                    string name = element.Current.Name;
                    string id = element.Current.AutomationId;
                    int processId = element.Current.ProcessId;
                    using (Process process = Process.GetProcessById(processId))
                    {
                        currentProcess = process.MainModule.FileName;
                        //Console.WriteLine("Name: {0}, Id: {1}, Process: {2}", name, id, currentProcess);
                    }
                }
            }
            finally
            {
                // Update lock, just to be sure ;)
                updateLock();
            }
        }
    }
}
