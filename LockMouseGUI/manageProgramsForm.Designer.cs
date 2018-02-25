namespace MouseLock
{
    partial class manageProgramsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(manageProgramsForm));
            this.programList = new System.Windows.Forms.ListBox();
            this.addProgramButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // programList
            // 
            this.programList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.programList.FormattingEnabled = true;
            this.programList.HorizontalScrollbar = true;
            this.programList.Location = new System.Drawing.Point(12, 29);
            this.programList.Name = "programList";
            this.programList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.programList.Size = new System.Drawing.Size(686, 171);
            this.programList.TabIndex = 0;
            // 
            // addProgramButton
            // 
            this.addProgramButton.Location = new System.Drawing.Point(12, 221);
            this.addProgramButton.Name = "addProgramButton";
            this.addProgramButton.Size = new System.Drawing.Size(87, 23);
            this.addProgramButton.TabIndex = 2;
            this.addProgramButton.Text = "Add program";
            this.addProgramButton.UseVisualStyleBackColor = true;
            this.addProgramButton.Click += new System.EventHandler(this.addProgramButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(105, 221);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(101, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "Delete selected";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Location = new System.Drawing.Point(623, 221);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(75, 23);
            this.closeButton.TabIndex = 4;
            this.closeButton.Text = "Done";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Gray;
            this.label1.Location = new System.Drawing.Point(12, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(546, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Add programs by dragging and dropping shortcuts or executables from Windows Explo" +
    "rer or click the button below.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(252, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Your mouse will be locked in the following programs:";
            // 
            // manageProgramsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(710, 252);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.addProgramButton);
            this.Controls.Add(this.programList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "manageProgramsForm";
            this.Text = "Manage programs";
            this.Load += new System.EventHandler(this.manageProgramsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox programList;
        private System.Windows.Forms.Button addProgramButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}