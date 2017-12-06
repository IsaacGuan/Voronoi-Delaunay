using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi_Delaunay
{
    class Voronoi
    {
        public static List<Edge> VoronoiEdges(List<Triangle> allTriangles)
        {
            List<Edge> voronoiEdgeList = new List<Edge>();

            for (int i = 0; i < allTriangles.Count; i++)
            {
                for (int j = 0; j < allTriangles.Count; j++)
                {
                    if (j != i)
                    {
                        Edge neighborEdge = allTriangles[i].FindCommonEdgeWith(allTriangles[j]);
                        if (neighborEdge != null)
                        {
                            Edge voronoiEdge = new Edge(allTriangles[i].center, allTriangles[j].center);
                            if (!voronoiEdgeList.Contains(voronoiEdge))
                            {
                                voronoiEdgeList.Add(voronoiEdge);
                            }
                        }
                    }
                }
            }

            return voronoiEdgeList;
        }
    }
}
