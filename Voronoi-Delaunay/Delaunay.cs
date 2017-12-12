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
                if (!edge1.ContainsVertex(superTriangle.vertex1) && !edge1.ContainsVertex(superTriangle.vertex2) && !edge1.ContainsVertex(superTriangle.vertex3))
                    delaunayEdgeList.Add(edge1);
                if (!edge2.ContainsVertex(superTriangle.vertex1) && !edge2.ContainsVertex(superTriangle.vertex2) && !edge2.ContainsVertex(superTriangle.vertex3))
                    delaunayEdgeList.Add(edge2);
                if (!edge3.ContainsVertex(superTriangle.vertex1) && !edge3.ContainsVertex(superTriangle.vertex2) && !edge3.ContainsVertex(superTriangle.vertex3))
                    delaunayEdgeList.Add(edge3);
            }

            return delaunayEdgeList;
        }

        public static List<Triangle> Triangulate(List<Triangle> delaunayTriangleList, Point currentPoint)
        {
            List<Edge> polygon = new List<Edge>();

            for (int i = delaunayTriangleList.Count - 1; i >= 0; i--)
            {
                if (delaunayTriangleList[i].ContainsInCircumcircle(currentPoint))
                {
                    polygon.Add(new Edge(delaunayTriangleList[i].vertex1, delaunayTriangleList[i].vertex2));
                    polygon.Add(new Edge(delaunayTriangleList[i].vertex2, delaunayTriangleList[i].vertex3));
                    polygon.Add(new Edge(delaunayTriangleList[i].vertex3, delaunayTriangleList[i].vertex1));
                    delaunayTriangleList.RemoveAt(i);
                }
            }

            for (int i = polygon.Count - 2; i >= 0; i--)
            {
                for (int j = polygon.Count - 1; j >= i + 1; j--)
                {
                    if (polygon[i] == polygon[j])
                    {
                        polygon.RemoveAt(j);
                        polygon.RemoveAt(i);
                        j--;
                        continue;
                    }
                }
            }

            for (int i = 0; i < polygon.Count; i++)
            {
                delaunayTriangleList.Add(new Triangle(polygon[i].start, polygon[i].end, currentPoint));
            }

            return delaunayTriangleList;
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
