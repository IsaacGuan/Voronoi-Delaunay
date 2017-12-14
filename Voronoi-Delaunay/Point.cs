using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi_Delaunay
{
    class Point
    {
        public double x, y, z;
        public List<int> adjoinTriangles;

        public Point()
        {
            this.x = 0;
            this.y = 0;
            this.z = 0;
            adjoinTriangles = new List<int>();
        }

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.z = 0;
            adjoinTriangles = new List<int>();
        }

        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            adjoinTriangles = new List<int>();
        }

        public Point(Point point)
        {
            this.x = point.x;
            this.y = point.y;
            this.z = point.z;
            adjoinTriangles = new List<int>();
        }

        public static Point operator + (Point a, Point b)
        {
            Point result = new Point(a.x + b.x, a.y + b.y, a.z + b.z);
            return result;
        }

        public static Point operator - (Point a, Point b)
        {
            Point result = new Point(a.x - b.x, a.y - b.y, a.z - b.z);
            return result;
        }

        public static Point operator * (double s, Point a)
        {
            Point result = new Point(s * a.x, s * a.y, s * a.z);
            return result;
        }

        public static Point operator * (Point a, double s)
        {
            return s * a;
        }

        public double dot (Point a, Point b)
        {
            return (a.x * b.x + a.y * b.y + a.z * b.z);
        }
        
        public Point cross (Point a, Point b)
        {
            Point result = new Point(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
            return result;
        }

        public override bool Equals(object obj)
        {
            return this == (Point)obj;
        }

        public static bool operator == (Point a, Point b)
        {
            if (((object)a) == ((object)b))
            {
                return true;
            }

            if ((((object)a) == null) || (((object)b) == null))
            {
                return false;
            }
            
            if (a.x != b.x) return false;
            if (a.y != b.y) return false;

            return true;
        }

        public static bool operator != (Point a, Point b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            int xHc = this.x.ToString().GetHashCode();
            int yHc = this.y.ToString().GetHashCode();
            int zHc = this.z.ToString().GetHashCode();

            return (xHc * 1) ^ (yHc * 2) ^ (zHc * 3);
        }
    }
}
