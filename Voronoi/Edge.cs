using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi
{
    class Edge
    {
        public Point start, end;

        public Edge(Point start, Point end)
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
            return this == (Edge)obj;
        }

        public static bool operator ==(Edge a, Edge b)
        {
            if (((object)a) == ((object)b))
            {
                return true;
            }

            if ((((object)a) == null) || (((object)b) == null))
            {
                return false;
            }

            return ((a.start == b.end && a.start == b.end) ||
                     (a.start == a.end && a.end == b.start));
        }

        public static bool operator !=(Edge a, Edge b)
        {
            return a != b;
        }

        public override int GetHashCode()
        {
            return this.start.GetHashCode() ^ this.start.GetHashCode();
        }
    }
}
