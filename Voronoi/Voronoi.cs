using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi
{
    class Voronoi
    {
        public static List<Edge> VoronoiEdges(List<Triangle> allTriangles)
        {
            List<Edge> voronoiEdgeList = new List<Edge>();
            List<Edge> voronoiRayEdgeList = new List<Edge>();

            for (int i = 0; i < allTriangles.Count; i++)
            {
                //List<Edge> neighborEdgeList = new List<Edge>();

                for (int j = 0; j < allTriangles.Count; j++)
                {
                    if (j != i)
                    {
                        Edge neighborEdge = allTriangles[i].FindCommonEdgeWith(allTriangles[j]);
                        if (neighborEdge != null)
                        {
                            //neighborEdgeList.Add(neighborEdge);
                            Edge voronoiEdge = new Edge(allTriangles[i].center, allTriangles[j].center);
                            if (!voronoiEdgeList.Contains(voronoiEdge))
                            {
                                voronoiEdgeList.Add(voronoiEdge);
                            }
                        }
                    }
                }

                /*
                if (neighborEdgeList.Count == 2)
                {
                    Point midPoint;
                    Edge rayEdge;
                    if (neighborEdgeList[0].ContainsVertex(allTriangles[i].vertex1) && neighborEdgeList[1].ContainsVertex(allTriangles[i].vertex1))
                    {
                        midPoint = FindMidPoint(allTriangles[i].vertex2, allTriangles[i].vertex3);
                        rayEdge = RayEdge(allTriangles[i].center, midPoint);
                        voronoiRayEdgeList.Add(rayEdge);
                    }
                    if (neighborEdgeList[0].ContainsVertex(allTriangles[i].vertex2) && neighborEdgeList[1].ContainsVertex(allTriangles[i].vertex2))
                    {
                        midPoint = FindMidPoint(allTriangles[i].vertex1, allTriangles[i].vertex3);
                        rayEdge = RayEdge(allTriangles[i].center, midPoint);
                        voronoiRayEdgeList.Add(rayEdge);
                    }
                    if (neighborEdgeList[0].ContainsVertex(allTriangles[i].vertex3) && neighborEdgeList[1].ContainsVertex(allTriangles[i].vertex3))
                    {
                        midPoint = FindMidPoint(allTriangles[i].vertex1, allTriangles[i].vertex2);
                        rayEdge = RayEdge(allTriangles[i].center, midPoint);
                        voronoiRayEdgeList.Add(rayEdge);
                    }
                }
                */
            }

            return voronoiEdgeList;
        }

        public static Point FindMidPoint(Point a, Point b)
        {
            Point midPoint = new Point((a.x + b.x) / 2.0, (a.y + b.y) / 2.0);
            return midPoint;
        }

        public static Edge RayEdge(Point start, Point direction)
        {
            Point end = new Point();
            Edge rayEdge;
            end.x = 1000 * (direction.x - start.x) + start.x;
            end.y = (direction.y - start.y) * (end.x - start.x) / (direction.x - start.x) + start.y;
            rayEdge = new Edge(start, end);

            return rayEdge;
        }
    }
}
