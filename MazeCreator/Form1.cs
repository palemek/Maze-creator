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
        List<List<P>> mazes = new List<List<P>>();
        
        public int mazeSize = 1024;

        public static double minStep = 6;

        public static double maxStep = 12;

        public static Color backgroundColor = Color.Black;

        public static Color mazeColor = Color.White;

        public static float penWidth = (float)(minStep / 2);

        public int margin = 50;

        public static Bitmap pic = null;

        public static int blurSize = 5;

        public Form1()
        {
            InitializeComponent();
        }

        void createGrid()
        {
            for (int i = 0; i * 2 * maxStep < mazeSize; i++)
            {
                P.grid.Add(new List<List<P>>());
                for (int j = 0; j * 2 * maxStep < mazeSize; j++)
                {
                    P.grid[i].Add(new List<P>());
                }
            }
        }

        void createMaze()
        {
            if (pic == null)
                return;

            mazeSize = pic.Width;

            createGrid();

            Random rnd = new Random();

            int bigPrime = 8779;
            List<int> indOfRndP = new List<int>();
            List<int> availableSubMazes = new List<int>();

            P start = new P(mazeSize / 2, mazeSize / 2);
            var temp = new List<P>() { start };

            int rand = 0;
            int currsubmaze = 0;
            indOfRndP.Add(1);

            do
            {
                while (P.goFucker(ref temp, mazeSize, minStep, maxStep, margin)) ;

                indOfRndP[currsubmaze]++;
                
                if (temp.Count > 1)
                {
                    mazes.Add(new List<P>(temp));
                    indOfRndP.Add(1);
                    availableSubMazes.Add(mazes.Count - 1);
                }

                if (indOfRndP[currsubmaze] >= mazes[currsubmaze].Count)
                    availableSubMazes.RemoveAt(rand);

                rand = rnd.Next(Math.Max(1, availableSubMazes.Count));
                if (availableSubMazes.Count == 0)
                    break;
                currsubmaze = availableSubMazes[rand];
                temp.Clear();
                start = mazes[currsubmaze][(indOfRndP[currsubmaze] * bigPrime) % mazes[currsubmaze].Count];
                temp.Add(start);

                try
                {
                    if (pictureLoad.InvokeRequired)
                    {
                        pictureLoad.Invoke(new MethodInvoker(delegate ()
                        {
                            pictureLoad.Value = Convert.ToInt32(100 * P.wholeArea / (mazeSize * mazeSize));
                        }));
                    }
                }

                catch
                {
                    Console.WriteLine("pictureBox1 refresh mistake");
                }
            } while (availableSubMazes.Count > 0);
            Console.WriteLine("maze finished");
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
            Thread.CurrentThread.Abort();
        }

        void picture()
        {
                try
                {
                if (pictureBox1.InvokeRequired)
                {
                    pictureBox1.Invoke(new MethodInvoker(delegate ()
                    {
                        pictureBox1.Width = pic.Width;
                        pictureBox1.Height = pic.Height;
                    }));
                }
            }
            catch
            {
            }
            Console.WriteLine("picture finished");
            Thread.CurrentThread.Abort();
        }

        private void maze_Paint(object sender, PaintEventArgs e)
        {
                Brush mybrush1 = new SolidBrush(backgroundColor);

                e.Graphics.FillRectangle(mybrush1, 0, 0, mazeSize, mazeSize);
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                Pen mypen1 = new Pen(mazeColor);
                mybrush1 = new SolidBrush(mazeColor);

                mypen1.Width = penWidth;

                paintLoop(mypen1, mybrush1, e.Graphics);
        }

        void paintLoop(Pen _pen, Brush _brush, Graphics _G)
        {
            for (int i = 0; i < mazes.Count; i++)
            {
                for (int j = 0; j < mazes[i].Count - 1; j++)
                {
                    P p = mazes[i][j];
                    P p1 = mazes[i][j + 1];

                    _G.FillEllipse(_brush, (float)(p.x - _pen.Width / 2), (float)(p.y - _pen.Width / 2), _pen.Width, _pen.Width);
                    _G.DrawLine(_pen, Convert.ToInt32(p.x), Convert.ToInt32(p.y), Convert.ToInt32(p1.x), Convert.ToInt32(p1.y));

                }
            }
        }

        private void StartMaze_Click(object sender, EventArgs e)
        {
            Thread mythread = new Thread(createMaze);
            mythread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pic = (Bitmap)Image.FromFile(openFileDialog1.FileName);
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
            var bmp = new Bitmap(mazeSize, mazeSize);                                           // mazes only squared
            pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, mazeSize, mazeSize));
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

        private void button4_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            backgroundColor = colorDialog1.Color;
            button4.BackColor = colorDialog1.Color;
            button4.ForeColor = Color.FromArgb(255 - colorDialog1.Color.R, 255 - colorDialog1.Color.G, 255 - colorDialog1.Color.B);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            mazeColor = colorDialog1.Color;
            button5.BackColor = colorDialog1.Color;
            button5.ForeColor = Color.FromArgb(255 - colorDialog1.Color.R, 255 - colorDialog1.Color.G, 255 - colorDialog1.Color.B);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
