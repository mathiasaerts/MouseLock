using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace MouseLock
{
    static class MouseLock
    {
        [STAThread]
        static void Main()
        {
            // Check if program is already running
            bool mutexCreated = false;
            Mutex mutex = new Mutex(true, @"Local\MouseLock.exe", out mutexCreated);

            if (!mutexCreated)
            {
                MessageBox.Show(
                    "MouseLock is already running. Only one instance is allowed.",
                    "MouseLock already running",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                mutex.Close();
                return;
            }

            // Create worker
            Worker worker = new Worker();

            // Default form action
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UI());

            // Close mutex on application exit
            mutex.Close();
        }
    }
}
