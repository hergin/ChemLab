
namespace BirdCommand
{
    partial class BirdCommandMain
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
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.designer_board = new Dalssoft.DiagramNet.Designer();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(621, 332);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Move Bird Down";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(621, 220);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(153, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Move Bird Up";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(732, 272);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(153, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Move Bird Right";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(512, 272);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(153, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Move Bird Left";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(732, 54);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(77, 69);
            this.button5.TabIndex = 6;
            this.button5.Text = "Turn Right";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(575, 54);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(77, 69);
            this.button6.TabIndex = 7;
            this.button6.Text = "Turn Left";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // designer_board
            // 
            this.designer_board.AutoScroll = true;
            this.designer_board.BackColor = System.Drawing.SystemColors.Window;
            this.designer_board.Enabled = false;
            this.designer_board.Location = new System.Drawing.Point(12, 12);
            this.designer_board.Name = "designer_board";
            this.designer_board.Size = new System.Drawing.Size(401, 401);
            this.designer_board.TabIndex = 1;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(481, 387);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(42, 13);
            this.linkLabel1.TabIndex = 8;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Level 1";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(545, 387);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(42, 13);
            this.linkLabel2.TabIndex = 9;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Level 2";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(610, 387);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(42, 13);
            this.linkLabel3.TabIndex = 10;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "Level 3";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // BirdCommandMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 430);
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.designer_board);
            this.Name = "BirdCommandMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bird Command";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Dalssoft.DiagramNet.Designer designer_board;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel3;
    }
}

