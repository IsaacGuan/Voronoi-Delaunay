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

        int clickTime = 0;
        Point currentPoint;

        Pen penBlue = new Pen(Color.Blue, 6.0f);
        Pen penRed = new Pen(Color.Red, 6.0f);

        Triangle superTriangle = new Triangle();
        List<Point> points = new List<Point>();
        List<Triangle> delaunayTriangleList = new List<Triangle>();
        List<Edge> delaunayEdgeList = new List<Edge>();
        List<Edge> voronoiEdgeList = new List<Edge>();

        Dictionary<int, List<Triangle>> previousTriangleList = new Dictionary<int, List<Triangle>>();

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

            for (int i = 0; i < pointCount; i++)
            {
                PointF p = new PointF((float)(rand.NextDouble() * 1190), (float)(rand.NextDouble() * 763));
                points.Add(new Point(p.X, p.Y, 0));
            }

            for (int i = 0; i < points.Count; i++)
            {
                g.FillEllipse(Brushes.White, (float)(points[i].x - 3.0f), (float)(points[i].y - 3.0f), 6, 6);
            }

            pictureBox1.Image = backImage;
        }

        public void DelaunayTriangulate()
        {
            g.Clear(Color.Black);

            List<Triangle> tmp = new List<Triangle>();
            delaunayTriangleList.ForEach(a => tmp.Add(a));

            previousTriangleList.Add(clickTime, tmp);

            delaunayTriangleList = Delaunay.Triangulate(delaunayTriangleList, currentPoint);
            
            for (int j = 0; j < delaunayTriangleList.Count; j++)
            {
                Edge edge1 = new Edge(delaunayTriangleList[j].vertex1, delaunayTriangleList[j].vertex2);
                Edge edge2 = new Edge(delaunayTriangleList[j].vertex2, delaunayTriangleList[j].vertex3);
                Edge edge3 = new Edge(delaunayTriangleList[j].vertex3, delaunayTriangleList[j].vertex1);
                
                CSPoint p1 = new CSPoint((int)edge1.start.x, (int)edge1.start.y);
                CSPoint p2 = new CSPoint((int)edge1.end.x, (int)edge1.end.y);
                g.DrawLine(penBlue, p1.X, p1.Y, p2.X, p2.Y);
                
                CSPoint p3 = new CSPoint((int)edge2.start.x, (int)edge2.start.y);
                CSPoint p4 = new CSPoint((int)edge2.end.x, (int)edge2.end.y);
                g.DrawLine(penBlue, p3.X, p3.Y, p4.X, p4.Y);
                
                CSPoint p5 = new CSPoint((int)edge3.start.x, (int)edge3.start.y);
                CSPoint p6 = new CSPoint((int)edge3.end.x, (int)edge3.end.y);
                g.DrawLine(penBlue, p5.X, p5.Y, p6.X, p6.Y);
            }

            for (int j = 0; j < points.Count; j++)
            {
                g.FillEllipse(Brushes.White, (float)(points[j].x - 3.0f), (float)(points[j].y - 3.0f), 6, 6);
            }

            pictureBox1.Image = backImage;
        }

        public void VoronoiDiagram()
        {
            voronoiEdgeList = Voronoi.VoronoiEdges(delaunayTriangleList);
            for (int i = 0; i < voronoiEdgeList.Count; i++)
            {
                CSPoint p1 = new CSPoint((int)voronoiEdgeList[i].start.x, (int)voronoiEdgeList[i].start.y);
                CSPoint p2 = new CSPoint((int)voronoiEdgeList[i].end.x, (int)voronoiEdgeList[i].end.y);
                g.DrawLine(penRed, p1.X, p1.Y, p2.X, p2.Y);
            }

            pictureBox1.Image = backImage;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Text = e.X + ", " + e.Y;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.Black);
            points.Clear();
            delaunayTriangleList.Clear();
            delaunayEdgeList.Clear();
            voronoiEdgeList.Clear();
            previousTriangleList.Clear();
            pointCount = (int)numericUpDown1.Value;

            clickTime = 0;

            SpreadPoints();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.Black);
            points.Clear();
            delaunayTriangleList.Clear();
            delaunayEdgeList.Clear();
            voronoiEdgeList.Clear();
            previousTriangleList.Clear();

            clickTime = 0;

            pictureBox1.Image = backImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true && points.Count != 0)
            {
                if (clickTime < points.Count)
                {
                    if (clickTime == 0)
                    {
                        superTriangle = Delaunay.SuperTriangle(points);
                        delaunayTriangleList.Add(superTriangle);
                    }
                    currentPoint = points[clickTime];
                    DelaunayTriangulate();
                    clickTime++;
                }
                else if (clickTime == points.Count)
                {
                    List<Triangle> tmp = new List<Triangle>();
                    delaunayTriangleList.ForEach(a => tmp.Add(a));
                    previousTriangleList.Add(clickTime, tmp);

                    delaunayEdgeList = Delaunay.DelaunayEdges(superTriangle, delaunayTriangleList);

                    g.Clear(Color.Black);
                    for (int i = 0; i < delaunayEdgeList.Count; i++)
                    {
                        CSPoint p1 = new CSPoint((int)delaunayEdgeList[i].start.x, (int)delaunayEdgeList[i].start.y);
                        CSPoint p2 = new CSPoint((int)delaunayEdgeList[i].end.x, (int)delaunayEdgeList[i].end.y);
                        g.DrawLine(penBlue, p1.X, p1.Y, p2.X, p2.Y);
                    }

                    for (int i = 0; i < points.Count; i++)
                    {
                        g.FillEllipse(Brushes.White, (float)(points[i].x - 3.0f), (float)(points[i].y - 3.0f), 6, 6);
                    }

                    pictureBox1.Image = backImage;
                    clickTime++;
                }
            }
            if (checkBox2.Checked == true && delaunayTriangleList.Count != 0 && clickTime > points.Count)
            {
                VoronoiDiagram();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (clickTime > 1)
            {
                g.Clear(Color.Black);

                delaunayTriangleList.Clear();

                previousTriangleList[clickTime - 1].ForEach(a => delaunayTriangleList.Add(a));
                previousTriangleList.Remove(clickTime - 1);

                clickTime--;

                for (int j = 0; j < delaunayTriangleList.Count; j++)
                {
                    Edge edge1 = new Edge(delaunayTriangleList[j].vertex1, delaunayTriangleList[j].vertex2);
                    Edge edge2 = new Edge(delaunayTriangleList[j].vertex2, delaunayTriangleList[j].vertex3);
                    Edge edge3 = new Edge(delaunayTriangleList[j].vertex3, delaunayTriangleList[j].vertex1);
                    
                    CSPoint p1 = new CSPoint((int)edge1.start.x, (int)edge1.start.y);
                    CSPoint p2 = new CSPoint((int)edge1.end.x, (int)edge1.end.y);
                    g.DrawLine(penBlue, p1.X, p1.Y, p2.X, p2.Y);
                    
                    CSPoint p3 = new CSPoint((int)edge2.start.x, (int)edge2.start.y);
                    CSPoint p4 = new CSPoint((int)edge2.end.x, (int)edge2.end.y);
                    g.DrawLine(penBlue, p3.X, p3.Y, p4.X, p4.Y);
                    
                    CSPoint p5 = new CSPoint((int)edge3.start.x, (int)edge3.start.y);
                    CSPoint p6 = new CSPoint((int)edge3.end.x, (int)edge3.end.y);
                    g.DrawLine(penBlue, p5.X, p5.Y, p6.X, p6.Y);
                }

                for (int j = 0; j < points.Count; j++)
                {
                    g.FillEllipse(Brushes.White, (float)(points[j].x - 3.0f), (float)(points[j].y - 3.0f), 6, 6);
                }


                pictureBox1.Image = backImage;
            }
        }
    }
}
