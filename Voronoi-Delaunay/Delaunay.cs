using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi_Delaunay
{
    class Delaunay
    {
        public static List<Edge> DelaunayEdges(Triangle superTriangle, List<Triangle> allTriangle)
        {
            List<Edge> delaunayEdgeList = new List<Edge>();

            for (int i = 0; i < allTriangle.Count; i++)
            {
                Edge edge1 = new Edge(allTriangle[i].vertex1, allTriangle[i].vertex2);
                Edge edge2 = new Edge(allTriangle[i].vertex2, allTriangle[i].vertex3);
                Edge edge3 = new Edge(allTriangle[i].vertex3, allTriangle[i].vertex1);
                if (!delaunayEdgeList.Contains(edge1) && !edge1.ContainsVertex(superTriangle.vertex1) && !edge1.ContainsVertex(superTriangle.vertex2) && !edge1.ContainsVertex(superTriangle.vertex3))
                    delaunayEdgeList.Add(edge1);
                if (!delaunayEdgeList.Contains(edge2) && !edge2.ContainsVertex(superTriangle.vertex1) && !edge2.ContainsVertex(superTriangle.vertex2) && !edge2.ContainsVertex(superTriangle.vertex3))
                    delaunayEdgeList.Add(edge2);
                if (!delaunayEdgeList.Contains(edge3) && !edge3.ContainsVertex(superTriangle.vertex1) && !edge3.ContainsVertex(superTriangle.vertex2) && !edge3.ContainsVertex(superTriangle.vertex3))
                    delaunayEdgeList.Add(edge3);
            }

            return delaunayEdgeList;
        }

        public static List<Triangle> Triangulate(Triangle superTriangle, List<Point> triangulationPoints)
        {
            triangulationPoints = triangulationPoints.OrderBy(point => point.x).ToList();
            
            List<Triangle> open = new List<Triangle>();
            List<Triangle> closed = new List<Triangle>(); 
            
            open.Add(superTriangle);

            for (int i = 0; i < triangulationPoints.Count(); i++)
            {

                List<Edge> polygon = new List<Edge>();

                for (int j = open.Count - 1; j >= 0; j--)
                {
                    double dx = triangulationPoints.ElementAt(i).x - open[j].center.x;

                    if (dx > 0.0 && dx * dx > open[j].radius * open[j].radius)
                    {
                        closed.Add(open[j]);
                        open.RemoveAt(j);
                        continue;
                    }

                    if (!open[j].ContainsInCircumcircle(triangulationPoints.ElementAt(i)))
                    {
                        continue;
                    }

                    polygon.Add(new Edge(open[j].vertex1, open[j].vertex2));
                    polygon.Add(new Edge(open[j].vertex2, open[j].vertex3));
                    polygon.Add(new Edge(open[j].vertex3, open[j].vertex1));
                    open.RemoveAt(j);
                }

                for (int j = polygon.Count - 2; j >= 0; j--)
                {
                    for (int k = polygon.Count - 1; k >= j + 1; k--)
                    {
                        if (polygon[j] == polygon[k])
                        {
                            polygon.RemoveAt(k);
                            polygon.RemoveAt(j);
                            k--;
                            continue;
                        }
                    }
                }

                for (int j = 0; j < polygon.Count; j++)
                {
                    open.Add(new Triangle(polygon[j].start, polygon[j].end, triangulationPoints.ElementAt(i)));
                }
            }

            List<Triangle> triangles = open.Concat(closed).ToList();

            return triangles;
        }

        public static Triangle SuperTriangle(List<Point> triangulationPoints)
        {
            double M = triangulationPoints[0].x;

            for (int i = 1; i < triangulationPoints.Count; i++)
            {
                double xAbs = Math.Abs(triangulationPoints[i].x);
                double yAbs = Math.Abs(triangulationPoints[i].y);
                if (xAbs > M) M = xAbs;
                if (yAbs > M) M = yAbs;
            }

            Point sp1 = new Point(100 * M, 0, 0);
            Point sp2 = new Point(0, 100 * M, 0);
            Point sp3 = new Point(-100 * M, -100 * M, 0);

            return new Triangle(sp1, sp2, sp3);
        }
    }
}
