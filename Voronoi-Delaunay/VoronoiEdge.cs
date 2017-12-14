using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi_Delaunay
{
    class VoronoiEdge
    {
        public Point start, end;

        public VoronoiEdge(Point start, Point end)
        {
            this.start = start;
            this.end = end;
        }

        public bool ContainsVertex(Point point)
        {
            if (start == point || end == point) return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            return this == (VoronoiEdge)obj;
        }

        public static bool operator ==(VoronoiEdge a, VoronoiEdge b)
        {
            if (((object)a) == ((object)b))
            {
                return true;
            }

            if ((((object)a) == null) || (((object)b) == null))
            {
                return false;
            }

            return ((a.start == b.start && a.end == b.end) ||
                     (a.start == b.end && a.end == b.start));
        }

        public static bool operator !=(VoronoiEdge a, VoronoiEdge b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return this.start.GetHashCode() ^ this.end.GetHashCode();
        }
    }
}
