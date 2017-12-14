using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi_Delaunay
{
    class Voronoi
    {
        public static List<VoronoiEdge> VoronoiEdges(List<DelaunayEdge> delaunayEdges)
        {
            List<VoronoiEdge> voronoiEdgeList = new List<VoronoiEdge>();

            for (int i = 0; i < delaunayEdges.Count; i++)
            {
                List<int> neighbours = new List<int>();
                for (int j = 0; j < Collections.allPoints[delaunayEdges[i].start].adjoinTriangles.Count; j++)
                {
                    for (int k = 0; k < Collections.allPoints[delaunayEdges[i].end].adjoinTriangles.Count; k++)
                    {
                        if (Collections.allPoints[delaunayEdges[i].start].adjoinTriangles[j] == Collections.allPoints[delaunayEdges[i].end].adjoinTriangles[k])
                        {
                            neighbours.Add(Collections.allPoints[delaunayEdges[i].start].adjoinTriangles[j]);
                        }
                    }
                }
                VoronoiEdge voronoiEdge = new VoronoiEdge(Collections.allTriangles[neighbours[0]].center, Collections.allTriangles[neighbours[1]].center);
                voronoiEdgeList.Add(voronoiEdge);
            }

            return voronoiEdgeList;
        }

        public static List<VoronoiEdge> VoronoiEdges(List<Triangle> allTriangles)
        {
            List<VoronoiEdge> voronoiEdgeList = new List<VoronoiEdge>();

            for (int i = 0; i < allTriangles.Count; i++)
            {
                for (int j = 0; j < allTriangles.Count; j++)
                {
                    if (j != i)
                    {
                        DelaunayEdge neighborEdge = allTriangles[i].FindCommonEdgeWith(allTriangles[j]);
                        if (neighborEdge != null)
                        {
                            VoronoiEdge voronoiEdge = new VoronoiEdge(allTriangles[i].center, allTriangles[j].center);
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
