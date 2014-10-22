namespace racing_simulation_2d
{
    partial class frmMain
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
            this.screen = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menu = new System.Windows.Forms.ToolStripMenuItem();
            this.allWheelDriveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.screen)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // screen
            // 
            this.screen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
<<<<<<< HEAD
            this.screen.Location = new System.Drawing.Point(24, 52);
            this.screen.Margin = new System.Windows.Forms.Padding(6);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(1726, 1611);
=======
            this.screen.Location = new System.Drawing.Point(12, 27);
            this.screen.Name = "screen";
            this.screen.Size = new System.Drawing.Size(1880, 981);
>>>>>>> FETCH_HEAD
            this.screen.TabIndex = 0;
            this.screen.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
<<<<<<< HEAD
            this.label1.Location = new System.Drawing.Point(1590, 1681);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 25);
=======
            this.label1.Location = new System.Drawing.Point(1812, 1017);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
>>>>>>> FETCH_HEAD
            this.label1.TabIndex = 2;
            this.label1.Text = "by Matt Kincaid";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
<<<<<<< HEAD
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
            this.menuStrip1.Size = new System.Drawing.Size(1774, 44);
=======
            this.menuStrip1.Size = new System.Drawing.Size(1904, 24);
>>>>>>> FETCH_HEAD
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menu
            // 
            this.menu.CheckOnClick = true;
            this.menu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allWheelDriveToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.menu.Name = "menu";
<<<<<<< HEAD
            this.menu.Size = new System.Drawing.Size(90, 36);
=======
            this.menu.Size = new System.Drawing.Size(50, 20);
>>>>>>> FETCH_HEAD
            this.menu.Text = "Menu";
            // 
            // allWheelDriveToolStripMenuItem
            // 
<<<<<<< HEAD
            this.allWheelDriveToolStripMenuItem.CheckOnClick = true;
            this.allWheelDriveToolStripMenuItem.Name = "allWheelDriveToolStripMenuItem";
            this.allWheelDriveToolStripMenuItem.Size = new System.Drawing.Size(255, 36);
=======
            this.allWheelDriveToolStripMenuItem.Checked = true;
            this.allWheelDriveToolStripMenuItem.CheckOnClick = true;
            this.allWheelDriveToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allWheelDriveToolStripMenuItem.Name = "allWheelDriveToolStripMenuItem";
            this.allWheelDriveToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
>>>>>>> FETCH_HEAD
            this.allWheelDriveToolStripMenuItem.Text = "All Wheel Drive";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
<<<<<<< HEAD
            this.toolStripSeparator1.Size = new System.Drawing.Size(252, 6);
=======
            this.toolStripSeparator1.Size = new System.Drawing.Size(151, 6);
>>>>>>> FETCH_HEAD
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
<<<<<<< HEAD
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(255, 36);
=======
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
>>>>>>> FETCH_HEAD
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.MenuExit_Click);
            // 
            // frmMain
            // 
<<<<<<< HEAD
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1774, 1729);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.screen);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(6);
=======
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1904, 1042);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.screen);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.menuStrip1;
>>>>>>> FETCH_HEAD
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.Text = "2D Racing Tutorial";
            ((System.ComponentModel.ISupportInitialize)(this.screen)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox screen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu;
        private System.Windows.Forms.ToolStripMenuItem allWheelDriveToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

