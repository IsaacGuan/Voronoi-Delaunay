using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi_Delaunay
{
    class DelaunayEdge
    {
        public int start, end;

        public DelaunayEdge(int start, int end)
        {
            this.start = start;
            this.end = end;
        }

        public bool ContainsVertex(int point)
        {
            if (start == point || end == point) return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            return this == (DelaunayEdge)obj;
        }

        public static bool operator == (DelaunayEdge a, DelaunayEdge b)
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

        public static bool operator != (DelaunayEdge a, DelaunayEdge b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return this.start.GetHashCode() ^ this.end.GetHashCode();
        }
    }
}
