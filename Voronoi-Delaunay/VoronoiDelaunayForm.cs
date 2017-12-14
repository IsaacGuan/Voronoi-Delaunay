using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using CSPoint = System.Drawing.Point;

namespace Voronoi_Delaunay
{
    public partial class VoronoiDelaunayForm : Form
    {
        Graphics g;
        Random seeder;
        Bitmap backImage;
        int pointCount;

        Triangle superTriangle = new Triangle();

        List<Triangle> delaunayTriangleList = new List<Triangle>();
        List<DelaunayEdge> delaunayEdgeList = new List<DelaunayEdge>();
        List<VoronoiEdge> voronoiEdgeList = new List<VoronoiEdge>();

        bool pointsSpreaded = false;
        bool delaunayGenerated = false;
        bool voronoiGenerated = false;

        public VoronoiDelaunayForm()
        {
            InitializeComponent();
            seeder = new Random();
            backImage = new Bitmap(1200, 900);
            g = Graphics.FromImage(backImage);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.Clear(Color.Black);

            pictureBox1.Image = backImage;
        }

        public void SpreadPoints()
        {
            g.Clear(Color.Black);

            int seed = seeder.Next();
            Random rand = new Random(seed);

            Collections.allPoints = new List<Point>();

            for (int i = 0; i < pointCount; i++)
            {
                PointF p = new PointF((float)(rand.NextDouble() * 1190), (float)(rand.NextDouble() * 763));
                Collections.allPoints.Add(new Point(p.X, p.Y, 0));
            }

            Collections.allPoints = Collections.allPoints.OrderBy(point => point.x).ToList();

            for (int i = 0; i < Collections.allPoints.Count; i++)
            {
                g.FillEllipse(Brushes.White, (float)(Collections.allPoints[i].x - 1.5f), (float)(Collections.allPoints[i].y - 1.5f), 3, 3);
            }

            pointsSpreaded = true;
            pictureBox1.Image = backImage;
        }

        public void DelaunayTriangulate()
        {
            superTriangle = Delaunay.SuperTriangle(Collections.allPoints);
            delaunayTriangleList = Delaunay.Triangulate(superTriangle, Collections.allPoints);
            delaunayEdgeList = Delaunay.DelaunayEdges(superTriangle, delaunayTriangleList);
            for (int i = 0; i < delaunayEdgeList.Count; i++)
            {
                CSPoint p1 = new CSPoint((int)Collections.allPoints[delaunayEdgeList[i].start].x, (int)Collections.allPoints[delaunayEdgeList[i].start].y);
                CSPoint p2 = new CSPoint((int)Collections.allPoints[delaunayEdgeList[i].end].x, (int)Collections.allPoints[delaunayEdgeList[i].end].y);
                g.DrawLine(Pens.Blue, p1.X, p1.Y, p2.X, p2.Y);
            }
            for (int i = 0; i < Collections.allPoints.Count; i++)
            {
                g.FillEllipse(Brushes.White, (float)(Collections.allPoints[i].x - 1.5f), (float)(Collections.allPoints[i].y - 1.5f), 3, 3);
            }

            delaunayGenerated = true;
            pictureBox1.Image = backImage;
        }

        public void VoronoiDiagram()
        {
            voronoiEdgeList = Voronoi.VoronoiEdges(delaunayEdgeList);
            //voronoiEdgeList = Voronoi.VoronoiEdges(delaunayTriangleList);
            for (int i = 0; i < voronoiEdgeList.Count; i++)
            {
                CSPoint p1 = new CSPoint((int)voronoiEdgeList[i].start.x, (int)voronoiEdgeList[i].start.y);
                CSPoint p2 = new CSPoint((int)voronoiEdgeList[i].end.x, (int)voronoiEdgeList[i].end.y);
                g.DrawLine(Pens.Red, p1.X, p1.Y, p2.X, p2.Y);
            }

            voronoiGenerated = true;
            pictureBox1.Image = backImage;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = e.X + ", " + e.Y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.Black);

            if (pointsSpreaded)
            {
                Collections.allPoints.Clear();
                pointsSpreaded = false;
            }
            if (delaunayGenerated)
            {
                delaunayTriangleList.Clear();
                delaunayEdgeList.Clear();
                delaunayGenerated = false;
            }
            if (voronoiGenerated)
            {
                voronoiEdgeList.Clear();
                voronoiGenerated = false;
            }

            pointCount = (int)numericUpDown1.Value;

            SpreadPoints();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.Black);

            if (pointsSpreaded)
            {
                Collections.allPoints.Clear();
                pointsSpreaded = false;
            }
            if (delaunayGenerated)
            {
                delaunayTriangleList.Clear();
                delaunayEdgeList.Clear();
                delaunayGenerated = false;
            }
            if (voronoiGenerated)
            {
                voronoiEdgeList.Clear();
                voronoiGenerated = false;
            }

            pictureBox1.Image = backImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && pointsSpreaded && !delaunayGenerated)
            {
                DelaunayTriangulate();
            }
            if (checkBox2.Checked == true && delaunayGenerated && !voronoiGenerated)
            {
                VoronoiDiagram();
            }
        }
    }
}
