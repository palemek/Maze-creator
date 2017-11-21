namespace MazeCreator
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureLoad = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.StartMaze = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button3 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.backGroundColorControl = new System.Windows.Forms.Button();
            this.mazeColorControl = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1024, 1024);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.maze_Paint);
            // 
            // pictureLoad
            // 
            this.pictureLoad.Location = new System.Drawing.Point(694, 41);
            this.pictureLoad.Name = "pictureLoad";
            this.pictureLoad.Size = new System.Drawing.Size(222, 23);
            this.pictureLoad.TabIndex = 1;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(694, 121);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(66, 20);
            this.textBox1.TabIndex = 4;
            this.textBox1.Text = "12";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(698, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Min Step Size:";
            // 
            // StartMaze
            // 
            this.StartMaze.Location = new System.Drawing.Point(694, 147);
            this.StartMaze.Name = "StartMaze";
            this.StartMaze.Size = new System.Drawing.Size(227, 34);
            this.StartMaze.TabIndex = 6;
            this.StartMaze.Text = "Start Maze";
            this.StartMaze.UseVisualStyleBackColor = true;
            this.StartMaze.Click += new System.EventHandler(this.StartMaze_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(694, 187);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(227, 34);
            this.button2.TabIndex = 7;
            this.button2.Text = "Save Maze";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(859, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Blur Size";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(855, 121);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(66, 20);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "5";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "bmp files|*.bmp";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(694, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "open file";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(694, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(227, 34);
            this.button1.TabIndex = 12;
            this.button1.Text = "DrawWhatYouGot";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(773, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Max Step Size:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(769, 121);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(66, 20);
            this.textBox3.TabIndex = 13;
            this.textBox3.Text = "12";
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // backGroundColorControl
            // 
            this.backGroundColorControl.BackColor = System.Drawing.Color.White;
            this.backGroundColorControl.Location = new System.Drawing.Point(694, 70);
            this.backGroundColorControl.Name = "backGroundColorControl";
            this.backGroundColorControl.Size = new System.Drawing.Size(102, 23);
            this.backGroundColorControl.TabIndex = 15;
            this.backGroundColorControl.Text = "background Color";
            this.backGroundColorControl.UseVisualStyleBackColor = false;
            this.backGroundColorControl.Click += new System.EventHandler(this.backgroundColoorControl_Click);
            // 
            // mazeColorControl
            // 
            this.mazeColorControl.BackColor = System.Drawing.Color.Black;
            this.mazeColorControl.ForeColor = System.Drawing.Color.White;
            this.mazeColorControl.Location = new System.Drawing.Point(814, 70);
            this.mazeColorControl.Name = "mazeColorControl";
            this.mazeColorControl.Size = new System.Drawing.Size(102, 23);
            this.mazeColorControl.TabIndex = 16;
            this.mazeColorControl.Text = "maze Color";
            this.mazeColorControl.UseVisualStyleBackColor = false;
            this.mazeColorControl.Click += new System.EventHandler(this.mazeColorControl_Click);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1294, 684);
            this.Controls.Add(this.mazeColorControl);
            this.Controls.Add(this.backGroundColorControl);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.StartMaze);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureLoad);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar pictureLoad;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button StartMaze;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button backGroundColorControl;
        private System.Windows.Forms.Button mazeColorControl;
    }
}

