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
    public partial class manageProgramsForm : Form
    {
        public manageProgramsForm()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(manageProgramsForm_DragEnter);
            this.DragDrop += new DragEventHandler(manageProgramsForm_DragDrop);
        }

        private void manageProgramsForm_Load(object sender, EventArgs e)
        {
            initTooltips();
            updateList();
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
            toolTip.SetToolTip(this.programList, "Drag and drop items here to add them");
            toolTip.SetToolTip(this.addProgramButton, "Browse for a program to add");
            toolTip.SetToolTip(this.deleteButton, "Delete selected program(s)");
            toolTip.SetToolTip(this.closeButton, "Done managing programs");
        }

        private void updateList()
        {
            string[] listitems = Worker.getProcessList().ToArray<string>();
            this.programList.Items.Clear();
            this.programList.Items.AddRange(listitems);
            this.programList.Update();
        }

        void manageProgramsForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) 
                e.Effect = DragDropEffects.Copy;
        }

        void manageProgramsForm_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string file in files)
            {
                Worker.addProcess(file);

                // Debug output
                // Console.WriteLine("Added file:" + fileTarget);
            }

            updateList();
        }

        private void addProgramButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            // Set filter options and filter index.
            dialog.Filter = "Executable files (*.exe)|*.exe|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.Title = "Select an executable file";
            dialog.RestoreDirectory = true;
            dialog.DereferenceLinks = false;
            dialog.FileName = "";


            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Debug output
                // Console.WriteLine(dialog.FileName);

                // Add process to list
                Worker.addProcess(dialog.FileName);
                updateList();
            }
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            ListBox.SelectedIndexCollection index = this.programList.SelectedIndices;

            if (Worker.getProcessList().Count() <= 0 || index.Count <= 0)
                return;

            for (int i = index.Count - 1; i >= 0 ; i--)
            {
                // Debug output
                // Console.WriteLine("DELETING ID: " + index[i]);
                // Console.WriteLine("VALUE: " + this.programList.Items[index[i]]);
                // Console.WriteLine("VALUE2: " + Worker.getProcessList().ElementAt(index[i]));

                // Remove process from data list
                Worker.delProcess(index[i]);
            }
            updateList();
        }
    }
}
