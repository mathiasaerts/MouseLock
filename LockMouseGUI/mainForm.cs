using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LockMouseGUI
{
    public partial class UI : Form
    {
        private manageProgramsForm progForm;
        private ContextMenu contextMenu;

        public UI()
        {
            // Default initialize of form items
            InitializeComponent();
        }

        private void initOtherForms()
        {
            // Initialize program form
            this.progForm = new manageProgramsForm();
            this.progForm.FormClosing += new FormClosingEventHandler(progForm_FormClosing);
            this.progForm.Hide();
        }

        private void initContextMenu()
        {
            // Initialize context menu
            MenuItem conOpen = new MenuItem("&Open MouseLock", conOpen_click);
            MenuItem conProg = new MenuItem("&Manage Programs", conProg_click);
            MenuItem conExit = new MenuItem("E&xit", conExit_click);

            this.contextMenu = new ContextMenu();
            this.contextMenu.MenuItems.Add(conOpen);
            this.contextMenu.MenuItems.Add(conProg);
            this.contextMenu.MenuItems.Add("-");
            this.contextMenu.MenuItems.Add(conExit);

            // Add context menu to notify icon
            this.notifyIcon.ContextMenu = this.contextMenu;
        }

        private void initTooltips()
        {
            // Initialize tooltips
            ToolTip toolTip = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip.ShowAlways = true;

            // Define tooltip text
            toolTip.SetToolTip(this.radioPrograms, "Lock your mouse only in selected programs");
            toolTip.SetToolTip(this.radioAlways, "Always lock your mouse (or use in 64bit program)");
            toolTip.SetToolTip(this.addProgramButton, "Add or remove programs where your mouse should be locked");
            toolTip.SetToolTip(this.closeButton, "Close MouseLock");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialize other form items
            initOtherForms();
            initContextMenu();
            initTooltips();

            // Set as foreground window
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Show notify icon if minimized
            if (this.WindowState == FormWindowState.Minimized)
            {
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(500);
                this.Hide();
            }
            // Hide notify icon when window is restored
            else if (this.WindowState == FormWindowState.Normal)
            {
                notifyIcon.Visible = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If user asked for close (not Windows shutdown/taskmanager/..)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Confirm user wants to close
                DialogResult res = MessageBox.Show(this,
                    "Are you sure you want to close MouseLock?",
                    "Closing MouseLock",
                    MessageBoxButtons.YesNo);

                // If NO - cancel close
                if (res == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            // Stop worker thread/loop
            Worker.stop();
        }

        private void progForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // If user asked for close (not Windows shutdown/taskmanager/..)
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Restore main form
                restoreForm1();

                // Cancel close
                e.Cancel = true;
            }
        }

        private void restoreForm1()
        {
            // Make sure program form is hidden
            progForm.Hide();

            // Restore main form
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.BringToFront();

            // Hide notify icon
            notifyIcon.Visible = false;
        }

        private void restoreForm2()
        {
            // Make sure main form is hidden
            this.Hide();

            // Restore program form
            progForm.Show();
            progForm.WindowState = FormWindowState.Normal;
            progForm.BringToFront();

            // Hide notify icon
            notifyIcon.Visible = false;
        }

        private void addProgramButton_Click(object sender, EventArgs e)
        {
            restoreForm2();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            restoreForm1();
        }

        private void conOpen_click(object sender, EventArgs e)
        {
            restoreForm1();
        }

        private void conProg_click(object sender, EventArgs e)
        {
            restoreForm2();
        }

        private void conExit_click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioPrograms_CheckedChanged(object sender, EventArgs e)
        {
            if(radioPrograms.Checked)
                Worker.disableAlwaysLock();
        }

        private void radioAlways_CheckedChanged(object sender, EventArgs e)
        {
            if(radioAlways.Checked)
                Worker.enableAlwaysLock();
        }
    }
}
