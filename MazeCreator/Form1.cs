using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace MazeCreator
{

    public partial class Form1 : Form
    {
        //List<List<P>> mazes = new List<List<P>>();

        //public int mazeSize = 1024;
        public static Maze maze = null;

        public static double minStep = 6;

        public static double maxStep = 12;

        public static Color backgroundColor = Color.White;

        public static Color mazeColor = Color.Black;

        public static float penWidth = (float)(minStep / 2);

        public int margin = 50;

        public static Bitmap pic = null;

        public static Bitmap picForDensity = null;

        public static int blurSize = 5;

        static bool painting = false;

        public Form1()
        {
            InitializeComponent();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1;
            timer.Elapsed += timerTick;
            timer.Start();
        }

        private void timerTick(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (maze!=null && !painting)
            {
                try
                {
                    if (pictureBox1.InvokeRequired)
                    {
                        pictureBox1.Invoke(new MethodInvoker(delegate ()
                        {
                            pictureBox1.Refresh();
                        }));
                    }
                }
                catch
                {
                    Console.WriteLine("pictureBox1 refresh mistake");
                }
            }
        }

        private void maze_Paint(object sender, PaintEventArgs e)
        {
            Brush mybrush = new SolidBrush(backgroundColor);
            if (picForDensity == null)
                return;

            e.Graphics.FillRectangle(mybrush, 0, 0, picForDensity.Width, picForDensity.Height);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Pen mypen = new Pen(mazeColor);
            mybrush = new SolidBrush(mazeColor);

            mypen.Width = penWidth;

            if(picForDensity != null)
            {
                painting = true;
                //Thread t = new Thread(() => 
                paintLoop(mypen, mybrush, e.Graphics, picForDensity);
                painting = false;
            }
        }

        void paintLoop(Pen _pen, Brush _brush, Graphics _g, Bitmap _pic)
        {
            if (maze != null)
            for (int i = 0; i < maze.mazes.Count; i++)
            {
                PointF[] mazeBranch = maze.mazes[i].ConvertAll(p => new PointF((float)p.x, (float)p.y)).ToArray();
                
                _g.DrawLines(_pen, mazeBranch);
            }
        }

        void internalCreateMaze()
        {
            maze = new Maze(pic, (int)minStep, (int)maxStep, margin, blurSize);
            maze.createMaze();
        }

        private void StartMaze_Click(object sender, EventArgs e)
        {
            Thread mythread = new Thread(internalCreateMaze);
            mythread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pic = (Bitmap)Image.FromFile(openFileDialog1.FileName);
            picForDensity = new Bitmap(pic);
            pictureBox1.Width = pic.Width;
            pictureBox1.Height = pic.Height;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            var bmp = new Bitmap(pic.Width, pic.Height);                                           // mazes only squared
            pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, pic.Width, pic.Height));
            bmp.Save(saveFileDialog1.FileName);
            
            string mydocpath =
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using (StreamWriter outputFile = new StreamWriter(Path.Combine(mydocpath, "WriteLines.svg")))
            {
                outputFile.WriteLine("<svg viewBox=\"0 0 {0} {1}\"  xmlns=\"http://www.w3.org/2000/svg\">",pic.Width, pic.Height);
                foreach (List<P> line in maze.mazes)
                {
                    outputFile.Write("<path fill=\"none\" stroke=\"black\" d = \"");
                    for (int i = 0; i < line.Count; i++)
                    {
                        if (i == 0)
                            outputFile.Write("M");
                        else if (i == 1)
                            outputFile.Write("L");

                        outputFile.Write("{0},{1} ", Math.Floor(line[i].x), Math.Floor(line[i].y));
                    }
                    outputFile.Write("\"/>");
                }
                outputFile.Write("</svg>");
            }              
        }

        // rzeczy stad powinny dziać się nie tu, ale w momencie startu labiryntu

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            minStep = Convert.ToDouble(textBox1.Text);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            blurSize = Convert.ToInt32(textBox2.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            maxStep = Convert.ToDouble(textBox3.Text);
        }

        private void backgroundColoorControl_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            backgroundColor = colorDialog1.Color;
            backGroundColorControl.BackColor = colorDialog1.Color;
            backGroundColorControl.ForeColor = Color.FromArgb(255 - colorDialog1.Color.R, 255 - colorDialog1.Color.G, 255 - colorDialog1.Color.B);
        }

        private void mazeColorControl_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            mazeColor = colorDialog1.Color;
            mazeColorControl.BackColor = colorDialog1.Color;
            mazeColorControl.ForeColor = Color.FromArgb(255 - colorDialog1.Color.R, 255 - colorDialog1.Color.G, 255 - colorDialog1.Color.B);
        }

    }
}
