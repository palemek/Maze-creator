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

        //void createGrid()
        //{
        //    P.grid = Enumerable.Range(0, Convert.ToInt16(mazeSize / (2 * maxStep))).Select(
        //        x => Enumerable.Range(0, Convert.ToInt16(mazeSize / (2 * maxStep))).Select(
        //            y => new List<P>()).ToList()).ToList();
        //    /*
        //    for (int i = 0; i * 2 * maxStep < mazeSize; i++)
        //    {
        //        P.grid.Add(new List<List<P>>());
        //        for (int j = 0; j * 2 * maxStep < mazeSize; j++)
        //        {
        //            P.grid[i].Add(new List<P>());
        //        }
        //    }*/
        //}
        //
        //List<P> createMazeStart()
        //{
        //    List<P> starts = new List<P>();
        //
        //    int iterations = (int)Math.Floor(4 * Math.PI * maxStep / minStep + 1);
        //
        //    for (int i = 0; i < iterations;i++)
        //    {
        //        starts.Add(new P(mazeSize/2 + maxStep * Math.Sin(0.5f * i / Math.PI), mazeSize / 2 +  maxStep * Math.Cos(0.5f * i / Math.PI)));
        //    }
        //
        //    starts.Add(new P(mazeSize / 2, mazeSize / 2 + maxStep * 2));
        //
        //    return starts;
        //}
        //
        //void createMaze()
        //{
        //    if (pic == null)
        //        return;
        //
        //    mazeSize = pic.Width;
        //
        //    createGrid();
        //
        //    Random rnd = new Random();
        //
        //    int bigPrime = 8779;
        //    List<int> indOfRndP = new List<int>();
        //    List<int> availableSubMazes = new List<int>();
        //
        //    var starts = createMazeStart();
        //    P start = new P(mazeSize / 2, mazeSize / 2);
        //    var temp = starts;
        //
        //    int rand = 0;
        //    int currsubmaze = 0;
        //    indOfRndP.Add(1);
        //
        //    do
        //    {
        //        while (P.goFucker(ref temp, mazeSize, minStep, maxStep, margin)) ;
        //
        //        indOfRndP[currsubmaze]++;
        //        
        //        if (temp.Count > 1)
        //        {
        //            mazes.Add(new List<P>(temp));
        //            indOfRndP.Add(1);
        //            availableSubMazes.Add(mazes.Count - 1);
        //        }
        //
        //        if (indOfRndP[currsubmaze] >= mazes[currsubmaze].Count)
        //            availableSubMazes.RemoveAt(rand);
        //
        //        rand = rnd.Next(Math.Max(1, availableSubMazes.Count));
        //        if (availableSubMazes.Count == 0)
        //            break;
        //        currsubmaze = availableSubMazes[rand];
        //        temp.Clear();
        //        start = mazes[currsubmaze][(indOfRndP[currsubmaze] * bigPrime) % mazes[currsubmaze].Count];
        //        temp.Add(start);
        //
        //        try
        //        {
        //            if (pictureLoad.InvokeRequired)
        //            {
        //                pictureLoad.Invoke(new MethodInvoker(delegate ()
        //                {
        //                    pictureLoad.Value = Convert.ToInt32(100 * P.wholeArea / (mazeSize * mazeSize));
        //                }));
        //            }
        //        }
        //
        //        catch
        //        {
        //            Console.WriteLine("pictureBox1 refresh mistake");
        //        }
        //    } while (availableSubMazes.Count > 0);
        //    Console.WriteLine("maze finished");
        //    try
        //    {
        //        if (pictureBox1.InvokeRequired)
        //        {
        //            pictureBox1.Invoke(new MethodInvoker(delegate ()
        //            {
        //                pictureBox1.Refresh();
        //            }));
        //        }
        //    }
        //
        //    catch
        //    {
        //        Console.WriteLine("pictureBox1 refresh mistake");
        //    }
        //    Thread.CurrentThread.Abort();
        //}
        //
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
