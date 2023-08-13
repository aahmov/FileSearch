namespace FileSearchApp
{
    partial class MainForm
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
            this.driveComboBox = new System.Windows.Forms.ComboBox();
            this.resultListBox = new System.Windows.Forms.ListBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.resumeButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // driveComboBox
            // 
            this.driveComboBox.FormattingEnabled = true;
            this.driveComboBox.Location = new System.Drawing.Point(12, 344);
            this.driveComboBox.Name = "driveComboBox";
            this.driveComboBox.Size = new System.Drawing.Size(121, 21);
            this.driveComboBox.TabIndex = 0;
            // 
            // resultListBox
            // 
            this.resultListBox.FormattingEnabled = true;
            this.resultListBox.Location = new System.Drawing.Point(12, 12);
            this.resultListBox.Name = "resultListBox";
            this.resultListBox.Size = new System.Drawing.Size(776, 316);
            this.resultListBox.TabIndex = 1;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(528, 344);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // resumeButton
            // 
            this.resumeButton.Location = new System.Drawing.Point(620, 344);
            this.resumeButton.Name = "resumeButton";
            this.resumeButton.Size = new System.Drawing.Size(75, 23);
            this.resumeButton.TabIndex = 3;
            this.resumeButton.Text = "Resume";
            this.resumeButton.UseVisualStyleBackColor = true;
            // 
            // pauseButton
            // 
            this.pauseButton.Location = new System.Drawing.Point(713, 344);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(75, 23);
            this.pauseButton.TabIndex = 4;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.pauseButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 380);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.resumeButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.resultListBox);
            this.Controls.Add(this.driveComboBox);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Click += new System.EventHandler(this.resumeButton_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox driveComboBox;
        private System.Windows.Forms.ListBox resultListBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Button resumeButton;
        private System.Windows.Forms.Button pauseButton;
    }
}

