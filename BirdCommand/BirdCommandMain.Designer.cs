
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BirdCommandMain));
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.maze9button = new System.Windows.Forms.PictureBox();
            this.maze3button = new System.Windows.Forms.PictureBox();
            this.maze2button = new System.Windows.Forms.PictureBox();
            this.maze1button = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.debugPanel = new System.Windows.Forms.Panel();
            this.button5 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.duplicateButton = new System.Windows.Forms.Button();
            this.copyLhsToRhsButton = new System.Windows.Forms.Button();
            this.decreaseRuleCountButton = new System.Windows.Forms.Button();
            this.increaseRuleCountButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.startOverButton = new System.Windows.Forms.PictureBox();
            this.runButton = new System.Windows.Forms.PictureBox();
            this.resetButton = new System.Windows.Forms.PictureBox();
            this.trafoRunner = new System.ComponentModel.BackgroundWorker();
            this.designer_board = new Dalssoft.DiagramNet.Designer();
            this.designer_trafo = new Dalssoft.DiagramNet.Designer();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.maze9button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maze3button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maze2button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maze1button)).BeginInit();
            this.debugPanel.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startOverButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.runButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resetButton)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.Location = new System.Drawing.Point(256, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Move Bird Down";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(263, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(88, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Move Bird Up";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.Location = new System.Drawing.Point(319, 80);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 23);
            this.button3.TabIndex = 4;
            this.button3.Text = "Move Bird Right";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.Location = new System.Drawing.Point(198, 80);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(91, 23);
            this.button4.TabIndex = 5;
            this.button4.Text = "Move Bird Left";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImage = global::BirdCommand.Properties.Resources.ChemLab_logos;
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
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1353, 91);
            this.panel2.TabIndex = 26;
            // 
            // panel6
            // 
            this.panel6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Location = new System.Drawing.Point(568, 16);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(387, 63);
            this.panel6.TabIndex = 38;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::BirdCommand.Properties.Resources.maze_selector_box;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Controls.Add(this.maze9button);
            this.panel5.Controls.Add(this.maze3button);
            this.panel5.Controls.Add(this.maze2button);
            this.panel5.Controls.Add(this.maze1button);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Location = new System.Drawing.Point(147, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(234, 54);
            this.panel5.TabIndex = 37;
            // 
            // maze9button
            // 
            this.maze9button.BackColor = System.Drawing.Color.Transparent;
            this.maze9button.BackgroundImage = global::BirdCommand.Properties.Resources.maze9;
            this.maze9button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.maze9button.Location = new System.Drawing.Point(179, 6);
            this.maze9button.Name = "maze9button";
            this.maze9button.Size = new System.Drawing.Size(48, 43);
            this.maze9button.TabIndex = 3;
            this.maze9button.TabStop = false;
            this.maze9button.Tag = "9";
            this.maze9button.Click += new System.EventHandler(this.mazeButtons_Click);
            this.maze9button.MouseEnter += new System.EventHandler(this.maze9button_MouseEnter);
            this.maze9button.MouseLeave += new System.EventHandler(this.maze9button_MouseLeave);
            // 
            // maze3button
            // 
            this.maze3button.BackColor = System.Drawing.Color.Transparent;
            this.maze3button.BackgroundImage = global::BirdCommand.Properties.Resources.maze3;
            this.maze3button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.maze3button.Location = new System.Drawing.Point(102, 6);
            this.maze3button.Name = "maze3button";
            this.maze3button.Size = new System.Drawing.Size(48, 43);
            this.maze3button.TabIndex = 2;
            this.maze3button.TabStop = false;
            this.maze3button.Tag = "3";
            this.maze3button.Click += new System.EventHandler(this.mazeButtons_Click);
            this.maze3button.MouseEnter += new System.EventHandler(this.maze3button_MouseEnter);
            this.maze3button.MouseLeave += new System.EventHandler(this.maze3button_MouseLeave);
            // 
            // maze2button
            // 
            this.maze2button.BackColor = System.Drawing.Color.Transparent;
            this.maze2button.BackgroundImage = global::BirdCommand.Properties.Resources.maze2;
            this.maze2button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.maze2button.Location = new System.Drawing.Point(55, 6);
            this.maze2button.Name = "maze2button";
            this.maze2button.Size = new System.Drawing.Size(48, 43);
            this.maze2button.TabIndex = 1;
            this.maze2button.TabStop = false;
            this.maze2button.Tag = "2";
            this.maze2button.Click += new System.EventHandler(this.mazeButtons_Click);
            this.maze2button.MouseEnter += new System.EventHandler(this.maze2button_MouseEnter);
            this.maze2button.MouseLeave += new System.EventHandler(this.maze2button_MouseLeave);
            // 
            // maze1button
            // 
            this.maze1button.BackColor = System.Drawing.Color.Transparent;
            this.maze1button.BackgroundImage = global::BirdCommand.Properties.Resources.maze1;
            this.maze1button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.maze1button.Location = new System.Drawing.Point(7, 6);
            this.maze1button.Name = "maze1button";
            this.maze1button.Size = new System.Drawing.Size(48, 43);
            this.maze1button.TabIndex = 0;
            this.maze1button.TabStop = false;
            this.maze1button.Tag = "1";
            this.maze1button.Click += new System.EventHandler(this.mazeButtons_Click);
            this.maze1button.MouseEnter += new System.EventHandler(this.maze1button_MouseEnter);
            this.maze1button.MouseLeave += new System.EventHandler(this.maze1button_MouseLeave);
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
            this.debugPanel.Controls.Add(this.button11);
            this.debugPanel.Controls.Add(this.button7);
            this.debugPanel.Controls.Add(this.label3);
            this.debugPanel.Controls.Add(this.button1);
            this.debugPanel.Controls.Add(this.button2);
            this.debugPanel.Controls.Add(this.button3);
            this.debugPanel.Controls.Add(this.button4);
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
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(479, 109);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(115, 23);
            this.button11.TabIndex = 31;
            this.button11.Text = "Small Maze Load";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button7.Location = new System.Drawing.Point(479, 50);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(139, 23);
            this.button7.TabIndex = 27;
            this.button7.Text = "Solve maze 3";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
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
            this.panel4.Controls.Add(this.duplicateButton);
            this.panel4.Controls.Add(this.copyLhsToRhsButton);
            this.panel4.Controls.Add(this.decreaseRuleCountButton);
            this.panel4.Controls.Add(this.increaseRuleCountButton);
            this.panel4.Location = new System.Drawing.Point(1137, 142);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(200, 705);
            this.panel4.TabIndex = 30;
            // 
            // duplicateButton
            // 
            this.duplicateButton.BackgroundImage = global::BirdCommand.Properties.Resources.duplicateButton;
            this.duplicateButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.duplicateButton.Location = new System.Drawing.Point(17, 259);
            this.duplicateButton.Name = "duplicateButton";
            this.duplicateButton.Size = new System.Drawing.Size(164, 91);
            this.duplicateButton.TabIndex = 11;
            this.duplicateButton.UseVisualStyleBackColor = true;
            this.duplicateButton.Visible = false;
            this.duplicateButton.Click += new System.EventHandler(this.duplicateButton_Click);
            // 
            // copyLhsToRhsButton
            // 
            this.copyLhsToRhsButton.BackgroundImage = global::BirdCommand.Properties.Resources.copyLHStoRHSbutton;
            this.copyLhsToRhsButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.copyLhsToRhsButton.Location = new System.Drawing.Point(17, 98);
            this.copyLhsToRhsButton.Name = "copyLhsToRhsButton";
            this.copyLhsToRhsButton.Size = new System.Drawing.Size(164, 91);
            this.copyLhsToRhsButton.TabIndex = 10;
            this.copyLhsToRhsButton.UseVisualStyleBackColor = true;
            this.copyLhsToRhsButton.Visible = false;
            this.copyLhsToRhsButton.Click += new System.EventHandler(this.copyLhsToRhsButton_Click_1);
            // 
            // decreaseRuleCountButton
            // 
            this.decreaseRuleCountButton.BackgroundImage = global::BirdCommand.Properties.Resources.decreaseRuleCountButton;
            this.decreaseRuleCountButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.decreaseRuleCountButton.Location = new System.Drawing.Point(108, 20);
            this.decreaseRuleCountButton.Name = "decreaseRuleCountButton";
            this.decreaseRuleCountButton.Size = new System.Drawing.Size(75, 44);
            this.decreaseRuleCountButton.TabIndex = 9;
            this.decreaseRuleCountButton.UseVisualStyleBackColor = true;
            this.decreaseRuleCountButton.Click += new System.EventHandler(this.decreaseRuleCountButton_Click_1);
            // 
            // increaseRuleCountButton
            // 
            this.increaseRuleCountButton.BackgroundImage = global::BirdCommand.Properties.Resources.increaseRuleCountButton;
            this.increaseRuleCountButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.increaseRuleCountButton.Location = new System.Drawing.Point(19, 20);
            this.increaseRuleCountButton.Name = "increaseRuleCountButton";
            this.increaseRuleCountButton.Size = new System.Drawing.Size(75, 44);
            this.increaseRuleCountButton.TabIndex = 8;
            this.increaseRuleCountButton.UseVisualStyleBackColor = true;
            this.increaseRuleCountButton.Click += new System.EventHandler(this.increaseRuleCountButton_Click_1);
            // 
            // startOverButton
            // 
            this.startOverButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startOverButton.BackColor = System.Drawing.Color.Transparent;
            this.startOverButton.BackgroundImage = global::BirdCommand.Properties.Resources.start_over_button;
            this.startOverButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.startOverButton.Location = new System.Drawing.Point(1004, 107);
            this.startOverButton.Name = "startOverButton";
            this.startOverButton.Size = new System.Drawing.Size(124, 30);
            this.startOverButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.startOverButton.TabIndex = 33;
            this.startOverButton.TabStop = false;
            this.startOverButton.Click += new System.EventHandler(this.startOverButton_Click);
            this.startOverButton.MouseEnter += new System.EventHandler(this.startOverButton_MouseEnter);
            this.startOverButton.MouseLeave += new System.EventHandler(this.startOverButton_MouseLeave);
            // 
            // runButton
            // 
            this.runButton.BackgroundImage = global::BirdCommand.Properties.Resources.run_button;
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
            // resetButton
            // 
            this.resetButton.BackgroundImage = global::BirdCommand.Properties.Resources.reset_button;
            this.resetButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.resetButton.Location = new System.Drawing.Point(146, 520);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(135, 57);
            this.resetButton.TabIndex = 35;
            this.resetButton.TabStop = false;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click_1);
            this.resetButton.MouseEnter += new System.EventHandler(this.resetButton_MouseEnter);
            this.resetButton.MouseLeave += new System.EventHandler(this.resetButton_MouseLeave);
            // 
            // trafoRunner
            // 
            this.trafoRunner.WorkerReportsProgress = true;
            this.trafoRunner.WorkerSupportsCancellation = true;
            // 
            // designer_board
            // 
            this.designer_board.AutoScroll = true;
            this.designer_board.BackColor = System.Drawing.SystemColors.Window;
            this.designer_board.Enabled = false;
            this.designer_board.Location = new System.Drawing.Point(16, 105);
            this.designer_board.Name = "designer_board";
            this.designer_board.Size = new System.Drawing.Size(401, 401);
            this.designer_board.TabIndex = 1;
            // 
            // designer_trafo
            // 
            this.designer_trafo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.designer_trafo.AutoScroll = true;
            this.designer_trafo.BackColor = System.Drawing.SystemColors.Window;
            this.designer_trafo.Location = new System.Drawing.Point(434, 142);
            this.designer_trafo.Name = "designer_trafo";
            this.designer_trafo.Size = new System.Drawing.Size(697, 705);
            this.designer_trafo.TabIndex = 11;
            // 
            // BirdCommandMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1353, 855);
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
            this.Controls.Add(this.designer_trafo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1350, 800);
            this.Name = "BirdCommandMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bird Command";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.maze9button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maze3button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maze2button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maze1button)).EndInit();
            this.debugPanel.ResumeLayout(false);
            this.debugPanel.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.startOverButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.runButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resetButton)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Dalssoft.DiagramNet.Designer designer_board;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private Dalssoft.DiagramNet.Designer designer_trafo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel debugPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button increaseRuleCountButton;
        private System.Windows.Forms.Button decreaseRuleCountButton;
        private System.Windows.Forms.Button copyLhsToRhsButton;
        private System.Windows.Forms.PictureBox startOverButton;
        private System.Windows.Forms.PictureBox runButton;
        private System.Windows.Forms.PictureBox resetButton;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.PictureBox maze1button;
        private System.Windows.Forms.PictureBox maze3button;
        private System.Windows.Forms.PictureBox maze2button;
        private System.ComponentModel.BackgroundWorker trafoRunner;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button duplicateButton;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.PictureBox maze9button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button5;
    }
}

