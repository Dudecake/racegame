namespace WindowsFormsApplication1
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
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Red;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(82, 27);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 67);
            this.button1.TabIndex = 0;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Jefferson",
            "Aniston BD4",
            "Arachnidj",
            "Stinger",
            "A-Type",
            "Beamer",
            "Furore GT",
            "Michelli Roadster",
            "Benson",
            "Hachura",
            "Rumbler",
            "Trance AM",
            "Wellard",
            "Armed Land Roamer",
            "B-Type",
            "Big Bug",
            "Box Truck",
            "Bug",
            "Bulwark",
            "Bus",
            "Cop Car",
            "Dementia",
            "Dementia Limousine",
            "Eddy",
            "Fire Truck",
            "G4 Bank Van",
            "Garbage Truck",
            "GT A1",
            "Hot Dog Van",
            "Icecream Van",
            "Jagular XK",
            "Karmabus",
            "Land Roamer",
            "Maurice",
            "Medicar",
            "Meteor",
            "Miara",
            "Minx",
            "Morton",
            "Pacifier",
            "Panto",
            "Pickup Gang",
            "Pickup",
            "Romero",
            "Schmidt",
            "Shark",
            "Special Agent Car",
            "Sports Limousine",
            "Spritzer",
            "Strech Limousine",
            "Swat Van",
            "T-Rex",
            "Tank",
            "Taxi",
            "Taxi Xpress",
            "Tow Truck",
            "Truck Cab",
            "Truck Cab SX",
            "TV Van",
            "U-Jerk Truck",
            "Van",
            "Z-Type"});
            this.comboBox1.Location = new System.Drawing.Point(108, 113);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(92, 21);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Jefferson",
            "Aniston BD4",
            "Arachnidj",
            "Stinger",
            "A-Type",
            "Beamer",
            "Furore GT",
            "Michelli Roadster",
            "Benson",
            "Hachura",
            "Rumbler",
            "Trance AM",
            "Wellard",
            "Armed Land Roamer",
            "B-Type",
            "Big Bug",
            "Box Truck",
            "Bug",
            "Bulwark",
            "Bus",
            "Cop Car",
            "Dementia",
            "Dementia Limousine",
            "Eddy",
            "Fire Truck",
            "G4 Bank Van",
            "Garbage Truck",
            "GT A1",
            "Hot Dog Van",
            "Icecream Van",
            "Jagular XK",
            "Karmabus",
            "Land Roamer",
            "Maurice",
            "Medicar",
            "Meteor",
            "Miara",
            "Minx",
            "Morton",
            "Pacifier",
            "Panto",
            "Pickup Gang",
            "Pickup",
            "Romero",
            "Schmidt",
            "Shark",
            "Special Agent Car",
            "Sports Limousine",
            "Spritzer",
            "Strech Limousine",
            "Swat Van",
            "T-Rex",
            "Tank",
            "Taxi",
            "Taxi Xpress",
            "Tow Truck",
            "Truck Cab",
            "Truck Cab SX",
            "TV Van",
            "U-Jerk Truck",
            "Van",
            "Z-Type"});
            this.comboBox2.Location = new System.Drawing.Point(108, 151);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(92, 21);
            this.comboBox2.TabIndex = 2;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 113);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Player 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 157);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Player 2:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}