using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;



namespace MazeCreator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class P
    {
        public static List<List<List<P>>> grid = new List<List<List<P>>>();
        static Random rnd = new Random();
        public static double wholeArea = 0;

        public double x;
        public double y;
        double xd;
        double yd;
        
        public static List<double> picDers(int _i, int _j, Bitmap _pic)
        {
            int r1 = _pic.GetPixel(_i, _j).R;

            double dx = 0;
            double dy = 0;

            int blur = Form1.blurSize;

            if (_i < _pic.Width - blur && _i > blur && _j < _pic.Height - blur && _j > blur)
                for (int k = -blur; k <= blur; k++)
                {
                    for (int l = -blur; l <= blur; l++)
                    {
                        double pixcoldif = _pic.GetPixel(_i + k, _j + l).R - r1;
                        if (k != 0)
                            dx += pixcoldif / k;
                        if (l != 0)
                            dy += pixcoldif / l;
                    }
                }

            dx = dx / (2 * blur * (2 * blur + 1));
            dy = dy / (2 * blur * (2 * blur + 1));

            double ddx = 1;
            double ddy = 0;
            double length = Math.Sqrt(dx * dx + dy * dy);
            if (length != 0)
            {
                ddx = dx / length;
                ddy = dy / length;
            }

            return new List<double>() { ddx, ddy };
        }

        public static double picDensity(int _i, int _j, Bitmap _pic)
        {
            return _pic.GetPixel(_i, _j).G/(255.0);
        }

        private List<List<P>> checkMyGrid()
        {
            double step = Form1.maxStep;
            var temp = new List<List<P>>(4);
            int xloc = Convert.ToInt32(x / (2 * step));
            int yloc = Convert.ToInt32(y / (2 * step));

            double xmax = (grid.Count * 2 - 1) * step;
            double ymax = (grid[0].Count * 2 - 1) * step;

            double gCx = (xloc * 2 + 1) * step; //current grid center x coord
            double gCy = (yloc * 2 + 1) * step; //current grid center y coord

            int xlocdest = xloc + (x > gCx ? 1 : - 1);
            int ylocdest = yloc + (y > gCy ? 1 : - 1);

            bool xvalid = x > step && x < xmax;
            bool yvalid = y > step && y < ymax;

            temp.Add(grid[xloc][yloc]);

            if (xvalid)
                temp.Add(grid[xlocdest][yloc]);
            if (yvalid)
                temp.Add(grid[xloc][ylocdest]);
            if (yvalid && xvalid) 
                temp.Add(grid[xlocdest][ylocdest]);

            return temp;
        }

        public P(double _x, double _y)
        {
            x = _x;
            y = _y;

            try
            {
                var ders = picDers(Convert.ToInt32(_x), Convert.ToInt32(_y),Form1.pic);
                xd = ders[0];
                yd = ders[1];
            }
            catch
            {
                Console.WriteLine("przy tworzeniu P");
                xd = 1;
                yd = 0;
            }

            foreach(List<P> gr in checkMyGrid())
                gr.Add(this);
        }

        public bool areToClose(List<double> _xy, double _dist, Bitmap _pic)
        {
            double _x = _xy[0];
            double _y = _xy[1];

            double txd,tyd;
            try
            {
                var ders = picDers(Convert.ToInt32(_x), Convert.ToInt32(_y),_pic);
                txd = ders[0];
                tyd = ders[1];
            }
            catch
            {
                return true;
            }
            
            foreach (List<P> l in checkMyGrid())
                foreach (P p in l)
                    if (Math.Abs((_x - p.x) * txd + (_y - p.y) * tyd) < _dist * 0.9 && 
                        Math.Abs((_x - p.x) * tyd - (_y - p.y) * txd) < _dist * 0.9) 
                        return true;
            return false;
        }

        public List<List<double>> possiblePoints(double _stepLength, int _mazeXsize, int _mazeYsize, int _margin, Bitmap _pic)//robie to teraz tak na pale, dla kazdego przypadku oddzielnie
        {
            List<List<double>> temp = new List<List<double>>();

            double xdd = xd * _stepLength;
            double ydd = yd * _stepLength;

            List<List<double>> possible = new List<List<double>>();
            possible.Add(new List<double>() { x + xdd, y + ydd });
            possible.Add(new List<double>() { x - xdd, y - ydd });
            possible.Add(new List<double>() { x + ydd, y - xdd });
            possible.Add(new List<double>() { x - ydd, y + xdd });

            foreach(List<double> curr in possible)
            {
                if (curr[0] > _margin &&
                    curr[1] > _margin &&
                    curr[0] < _mazeXsize - _margin &&
                    curr[1] < _mazeYsize - _margin)
                {
                    if (!areToClose(curr, _stepLength, _pic)) //!!!!!!!!!!!!!!!
                        temp.Add(new List<double>(curr));
                }
            }
            return temp;
        }

        public static bool goFucker(ref List<P> _lista, int _mazeXsize, int _mazeYsize, double _minstep ,double _maxstep, int _margin)
        {
            P curr = _lista.Last();

            var ders = picDensity(Convert.ToInt32(curr.x), Convert.ToInt32(curr.y), Form1.pic);

            double currMargin = _minstep + (_maxstep - _minstep) * ders;

            List <List<double>> poss = curr.possiblePoints(currMargin, _mazeXsize, _mazeYsize, _margin, Form1.pic);
            int pC = poss.Count;
            if (pC == 0)
            {
                return false;
            }
            else
            {
                var r = rnd.Next(pC);
                var next = new P(poss[r][0],poss[r][1]);

                wholeArea += currMargin * currMargin;

                _lista.Add(next);
            }
            return true;
        }
    }
    //not used yet but should be
    public class Maze
    {
        Bitmap picture;
        //int mazeSize;                 //MAZESIZE

        int mazeXsize;
        int mazeYsize;

        int minStep;
        int maxStep;
        public List<List<P>> mazes = new List<List<P>>();
        int margin;
        int blurSize;

        public Maze(Bitmap _picture, int _minStep, int _maxStep, int _margin, int _blurSize)
        {
            picture = _picture;
            //mazeSize = picture.Width; // to powinno byc 2 - wymiarowe                 //MAZESIZE

            mazeXsize = picture.Width;
            mazeYsize = picture.Height;

            minStep = _minStep;
            maxStep = _maxStep;
            margin = _margin;
            blurSize = _blurSize;
        }

        private void createGrid()
        {
            P.grid = Enumerable.Range(0, Convert.ToInt16(mazeXsize / (2 * maxStep))).Select(
                x => Enumerable.Range(0, Convert.ToInt16(mazeYsize / (2 * maxStep))).Select(
                    y => new List<P>()).ToList()).ToList();
            /*
            for (int i = 0; i * 2 * maxStep < mazeSize; i++)
            {
                P.grid.Add(new List<List<P>>());
                for (int j = 0; j * 2 * maxStep < mazeSize; j++)
                {
                    P.grid[i].Add(new List<P>());
                }
            }*/
        }

        public void createMaze()
        {

            createGrid();

            Random rnd = new Random();

            int bigPrime = 8779;
            List<int> indOfRndP = new List<int>();
            List<int> availableSubMazes = new List<int>();

            P start = new P(mazeXsize / 2, mazeYsize / 2);
            var temp = new List<P>() { start };

            int rand = 0;
            int currsubmaze = 0;
            indOfRndP.Add(1);

            do
            {
                while (P.goFucker(ref temp, mazeXsize, mazeYsize, minStep, maxStep, margin)) ;

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

                //tu dzieje sie update progressbaru
                /*try
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
                }*/
            } while (availableSubMazes.Count > 0);
            Console.WriteLine("maze finished");

            // update obrazka na sam koniec
            /*
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
            }*/
            Thread.CurrentThread.Abort();
        }
    }
}

