
namespace ChemLab
{
    partial class ChemLabMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChemLabMain));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.maze9button = new System.Windows.Forms.PictureBox();
            this.maze3button = new System.Windows.Forms.PictureBox();
            this.maze2button = new System.Windows.Forms.PictureBox();
            this.maze1button = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.debugPanel = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.startOverButton = new System.Windows.Forms.PictureBox();
            this.resetButton = new System.Windows.Forms.PictureBox();
            this.runButton = new System.Windows.Forms.PictureBox();
            this.trafoRunner = new System.ComponentModel.BackgroundWorker();
            this.label_selectlab = new System.Windows.Forms.Label();
            this.designer_board = new Dalssoft.DiagramNet.Designer();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maze9button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maze3button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maze2button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maze1button)).BeginInit();
            this.debugPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startOverButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resetButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.runButton)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::ChemLab.Properties.Resources.ChemLab_logos;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(8, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(370, 76);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(151)))), ((int)(((byte)(102)))));
            this.panel2.Controls.Add(this.linkLabel4);
            this.panel2.Controls.Add(this.linkLabel3);
            this.panel2.Controls.Add(this.linkLabel2);
            this.panel2.Controls.Add(this.linkLabel1);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1353, 91);
            this.panel2.TabIndex = 26;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(697, 41);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(31, 13);
            this.linkLabel1.TabIndex = 24;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Tag = "1";
            this.linkLabel1.Text = "Lab1";
            this.linkLabel1.Click += new System.EventHandler(this.mazeButtons_Click);
            // 
            // maze9button
            // 
            this.maze9button.Location = new System.Drawing.Point(0, 0);
            this.maze9button.Name = "maze9button";
            this.maze9button.Size = new System.Drawing.Size(100, 50);
            this.maze9button.TabIndex = 0;
            this.maze9button.TabStop = false;
            // 
            // maze3button
            // 
            this.maze3button.Location = new System.Drawing.Point(0, 0);
            this.maze3button.Name = "maze3button";
            this.maze3button.Size = new System.Drawing.Size(100, 50);
            this.maze3button.TabIndex = 0;
            this.maze3button.TabStop = false;
            // 
            // maze2button
            // 
            this.maze2button.Location = new System.Drawing.Point(0, 0);
            this.maze2button.Name = "maze2button";
            this.maze2button.Size = new System.Drawing.Size(100, 50);
            this.maze2button.TabIndex = 0;
            this.maze2button.TabStop = false;
            // 
            // maze1button
            // 
            this.maze1button.Location = new System.Drawing.Point(0, 0);
            this.maze1button.Name = "maze1button";
            this.maze1button.Size = new System.Drawing.Size(100, 50);
            this.maze1button.TabIndex = 0;
            this.maze1button.TabStop = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(232)))), ((int)(((byte)(234)))));
            this.label6.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(150, 6);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 34);
            this.label6.TabIndex = 38;
            this.label6.Text = "...";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(101)))), ((int)(((byte)(160)))));
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(434, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 34);
            this.label1.TabIndex = 27;
            this.label1.Text = "Blocks";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(101)))), ((int)(((byte)(160)))));
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(640, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(491, 34);
            this.label2.TabIndex = 28;
            this.label2.Text = "Workspace";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // debugPanel
            // 
            this.debugPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.debugPanel.BackColor = System.Drawing.Color.Salmon;
            this.debugPanel.Controls.Add(this.button5);
            this.debugPanel.Controls.Add(this.label3);
            this.debugPanel.Location = new System.Drawing.Point(16, 693);
            this.debugPanel.Name = "debugPanel";
            this.debugPanel.Size = new System.Drawing.Size(1325, 150);
            this.debugPanel.TabIndex = 30;
            this.debugPanel.Visible = false;
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button5.Location = new System.Drawing.Point(25, 50);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(156, 23);
            this.button5.TabIndex = 32;
            this.button5.Text = "Make model interactible";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 22);
            this.label3.TabIndex = 26;
            this.label3.Text = "DEBUG PANEL";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(101)))), ((int)(((byte)(160)))));
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1137, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(200, 34);
            this.label4.TabIndex = 31;
            this.label4.Text = "Workspace Commands";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(228)))), ((int)(((byte)(228)))));
            this.panel4.Location = new System.Drawing.Point(1137, 142);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 705);
            this.panel4.TabIndex = 30;
            // 
            // startOverButton
            // 
            this.startOverButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startOverButton.BackColor = System.Drawing.Color.Transparent;
            this.startOverButton.BackgroundImage = global::ChemLab.Properties.Resources.start_over_button;
            this.startOverButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.startOverButton.Location = new System.Drawing.Point(1004, 107);
            this.startOverButton.Name = "startOverButton";
            this.startOverButton.Size = new System.Drawing.Size(124, 30);
            this.startOverButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.startOverButton.TabIndex = 33;
            this.startOverButton.TabStop = false;
            this.toolTip1.SetToolTip(this.startOverButton, "This will reset the lab to its start state and delete all the blocks you\'ve added" +
        " or changed.");
            this.startOverButton.Click += new System.EventHandler(this.startOverButton_Click);
            this.startOverButton.MouseEnter += new System.EventHandler(this.startOverButton_MouseEnter);
            this.startOverButton.MouseLeave += new System.EventHandler(this.startOverButton_MouseLeave);
            // 
            // resetButton
            // 
            this.resetButton.BackgroundImage = global::ChemLab.Properties.Resources.reset_button;
            this.resetButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.resetButton.Location = new System.Drawing.Point(146, 520);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(135, 57);
            this.resetButton.TabIndex = 35;
            this.resetButton.TabStop = false;
            this.toolTip1.SetToolTip(this.resetButton, "Reset the tranformation");
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click_1);
            this.resetButton.MouseEnter += new System.EventHandler(this.resetButton_MouseEnter);
            this.resetButton.MouseLeave += new System.EventHandler(this.resetButton_MouseLeave);
            // 
            // runButton
            // 
            this.runButton.BackgroundImage = global::ChemLab.Properties.Resources.run_button;
            this.runButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.runButton.Location = new System.Drawing.Point(16, 520);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(124, 57);
            this.runButton.TabIndex = 34;
            this.runButton.TabStop = false;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            this.runButton.MouseEnter += new System.EventHandler(this.runButton_MouseEnter);
            this.runButton.MouseLeave += new System.EventHandler(this.runButton_MouseLeave);
            // 
            // trafoRunner
            // 
            this.trafoRunner.WorkerReportsProgress = true;
            this.trafoRunner.WorkerSupportsCancellation = true;
            // 
            // label_selectlab
            // 
            this.label_selectlab.AutoSize = true;
            this.label_selectlab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(151)))), ((int)(((byte)(102)))));
            this.label_selectlab.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_selectlab.ForeColor = System.Drawing.Color.DimGray;
            this.label_selectlab.Location = new System.Drawing.Point(104, 408);
            this.label_selectlab.Name = "label_selectlab";
            this.label_selectlab.Size = new System.Drawing.Size(225, 48);
            this.label_selectlab.TabIndex = 36;
            this.label_selectlab.Text = "Select a lab environment\r\nfrom the numbers above!";
            this.label_selectlab.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // designer_board
            // 
            this.designer_board.AutoScroll = true;
            this.designer_board.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(151)))), ((int)(((byte)(102)))));
            this.designer_board.BackgroundImage = global::ChemLab.Properties.Resources.icon2;
            this.designer_board.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.designer_board.Enabled = false;
            this.designer_board.Location = new System.Drawing.Point(16, 105);
            this.designer_board.Name = "designer_board";
            this.designer_board.Size = new System.Drawing.Size(401, 401);
            this.designer_board.TabIndex = 1;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(734, 41);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(31, 13);
            this.linkLabel2.TabIndex = 25;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Tag = "2";
            this.linkLabel2.Text = "Lab2";
            this.linkLabel2.Click += new System.EventHandler(this.mazeButtons_Click);
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(771, 41);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(31, 13);
            this.linkLabel3.TabIndex = 26;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Tag = "3";
            this.linkLabel3.Text = "Lab3";
            this.linkLabel3.Click += new System.EventHandler(this.mazeButtons_Click);
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Location = new System.Drawing.Point(808, 41);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(31, 13);
            this.linkLabel4.TabIndex = 27;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Tag = "4";
            this.linkLabel4.Text = "Lab4";
            this.linkLabel4.Click += new System.EventHandler(this.mazeButtons_Click);
            // 
            // ChemLabMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1353, 855);
            this.Controls.Add(this.label_selectlab);
            this.Controls.Add(this.debugPanel);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.startOverButton);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.designer_board);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1350, 799);
            this.Name = "ChemLabMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChemLab: Chemical Reactions Made Easy";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maze9button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maze3button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maze2button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maze1button)).EndInit();
            this.debugPanel.ResumeLayout(false);
            this.debugPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startOverButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resetButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.runButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Dalssoft.DiagramNet.Designer designer_board;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel debugPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox startOverButton;
        private System.Windows.Forms.PictureBox runButton;
        private System.Windows.Forms.PictureBox resetButton;
        private System.Windows.Forms.PictureBox maze1button;
        private System.Windows.Forms.PictureBox maze3button;
        private System.Windows.Forms.PictureBox maze2button;
        private System.ComponentModel.BackgroundWorker trafoRunner;
        private System.Windows.Forms.PictureBox maze9button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label_selectlab;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.LinkLabel linkLabel2;
    }
}

