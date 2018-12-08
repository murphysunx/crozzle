namespace SIT323_assignment_2___crozzle
{
    partial class Form1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chooseConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showScoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.buildCrozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(489, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseFileToolStripMenuItem,
            this.chooseConfigurationToolStripMenuItem,
            this.buildCrozzleToolStripMenuItem,
            this.showScoreToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.startToolStripMenuItem.Text = "File";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // chooseFileToolStripMenuItem
            // 
            this.chooseFileToolStripMenuItem.Name = "chooseFileToolStripMenuItem";
            this.chooseFileToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.chooseFileToolStripMenuItem.Text = "Choose Crozzle File";
            this.chooseFileToolStripMenuItem.Click += new System.EventHandler(this.chooseFileToolStripMenuItem_Click);
            // 
            // chooseConfigurationToolStripMenuItem
            // 
            this.chooseConfigurationToolStripMenuItem.Name = "chooseConfigurationToolStripMenuItem";
            this.chooseConfigurationToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.chooseConfigurationToolStripMenuItem.Text = "Choose Configuration";
            this.chooseConfigurationToolStripMenuItem.Click += new System.EventHandler(this.chooseConfigurationToolStripMenuItem_Click);
            // 
            // showScoreToolStripMenuItem
            // 
            this.showScoreToolStripMenuItem.Name = "showScoreToolStripMenuItem";
            this.showScoreToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.showScoreToolStripMenuItem.Text = "Show score";
            this.showScoreToolStripMenuItem.Click += new System.EventHandler(this.showScoreToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 24);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(489, 350);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // buildCrozzleToolStripMenuItem
            // 
            this.buildCrozzleToolStripMenuItem.Name = "buildCrozzleToolStripMenuItem";
            this.buildCrozzleToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.buildCrozzleToolStripMenuItem.Text = "Build Crozzle";
            this.buildCrozzleToolStripMenuItem.Click += new System.EventHandler(this.buildCrozzleToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 374);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chooseConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showScoreToolStripMenuItem;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStripMenuItem buildCrozzleToolStripMenuItem;
    }
}

