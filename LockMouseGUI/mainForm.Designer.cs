using System.Windows.Forms;
namespace LockMouseGUI
{
    partial class UI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UI));
            this.label1 = new System.Windows.Forms.Label();
            this.addProgramButton = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.closeButton = new System.Windows.Forms.Button();
            this.radioAlways = new System.Windows.Forms.RadioButton();
            this.radioPrograms = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.MaximumSize = new System.Drawing.Size(250, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 26);
            this.label1.TabIndex = 0;
            this.label1.Text = "This program will lock your mouse within the resolution of your primary monitor.";
            // 
            // addProgramButton
            // 
            this.addProgramButton.Location = new System.Drawing.Point(12, 93);
            this.addProgramButton.Name = "addProgramButton";
            this.addProgramButton.Size = new System.Drawing.Size(124, 23);
            this.addProgramButton.TabIndex = 3;
            this.addProgramButton.Text = "Manage programs";
            this.addProgramButton.UseVisualStyleBackColor = true;
            this.addProgramButton.Click += new System.EventHandler(this.addProgramButton_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipText = "MouseLock is now minimized.";
            this.notifyIcon.BalloonTipTitle = "MouseLock";
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "MouseLock";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(178, 93);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Close";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // radioAlways
            // 
            this.radioAlways.AutoSize = true;
            this.radioAlways.Location = new System.Drawing.Point(12, 67);
            this.radioAlways.Name = "radioAlways";
            this.radioAlways.Size = new System.Drawing.Size(81, 17);
            this.radioAlways.TabIndex = 2;
            this.radioAlways.Text = "Always lock";
            this.radioAlways.UseVisualStyleBackColor = true;
            this.radioAlways.CheckedChanged += new System.EventHandler(this.radioAlways_CheckedChanged);
            // 
            // radioPrograms
            // 
            this.radioPrograms.AutoSize = true;
            this.radioPrograms.Checked = true;
            this.radioPrograms.Location = new System.Drawing.Point(12, 44);
            this.radioPrograms.Name = "radioPrograms";
            this.radioPrograms.Size = new System.Drawing.Size(135, 17);
            this.radioPrograms.TabIndex = 1;
            this.radioPrograms.TabStop = true;
            this.radioPrograms.Text = "Selected programs only";
            this.radioPrograms.UseVisualStyleBackColor = true;
            this.radioPrograms.CheckedChanged += new System.EventHandler(this.radioPrograms_CheckedChanged);
            // 
            // UI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 127);
            this.Controls.Add(this.radioPrograms);
            this.Controls.Add(this.radioAlways);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.addProgramButton);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "UI";
            this.Text = "MouseLock";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button addProgramButton;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button closeButton;
        private RadioButton radioAlways;
        private RadioButton radioPrograms;
    }
}

