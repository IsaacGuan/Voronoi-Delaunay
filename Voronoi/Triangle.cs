using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Voronoi
{
    class Triangle
    {
        public Point vertex1, vertex2, vertex3;
        public Point center;
        public double radius;

        public Triangle(Point vertex1, Point vertex2, Point vertex3)
        {
            double x1, x2, x3, y1, y2, y3;
            double x, y;

            this.vertex1 = vertex1;
            this.vertex2 = vertex2;
            this.vertex3 = vertex3;

            x1 = vertex1.x;
            x2 = vertex2.x;
            x3 = vertex3.x;
            y1 = vertex1.y;
            y2 = vertex2.y;
            y3 = vertex3.y;

            x = ((y2 - y1) * (y3 * y3 - y1 * y1 + x3 * x3 - x1 * x1) - (y3 - y1) * (y2 * y2 - y1 * y1 + x2 * x2 - x1 * x1)) / (2 * (x3 - x1) * (y2 - y1) - 2 * ((x2 - x1) * (y3 - y1)));
            y = ((x2 - x1) * (x3 * x3 - x1 * x1 + y3 * y3 - y1 * y1) - (x3 - x1) * (x2 * x2 - x1 * x1 + y2 * y2 - y1 * y1)) / (2 * (y3 - y1) * (x2 - x1) - 2 * ((y2 - y1) * (x3 - x1)));

            this.center = new Point(x, y);
            this.radius = Math.Sqrt(Math.Abs(vertex1.x - x) * Math.Abs(vertex1.x - x) + Math.Abs(vertex1.y - y) * Math.Abs(vertex1.y - y));
        }

        public double ContainsInCircumcircle(Point point)
        {
            double ax = this.vertex1.x - point.x;
            double ay = this.vertex1.y - point.y;
            double bx = this.vertex2.x - point.x;
            double by = this.vertex2.y - point.y;
            double cx = this.vertex3.x - point.x;
            double cy = this.vertex3.y - point.y;

            double det_ab = ax * by - bx * ay;
            double det_bc = bx * cy - cx * by;
            double det_ca = cx * ay - ax * cy;

            double a_squared = ax * ax + ay * ay;
            double b_squared = bx * bx + by * by;
            double c_squared = cx * cx + cy * cy;

            return a_squared * det_bc + b_squared * det_ca + c_squared * det_ab;
        }

        public bool SharesVertexWith(Triangle triangle)
        {
            if (this.vertex1.x == triangle.vertex1.x && this.vertex1.y == triangle.vertex1.y) return true;
            if (this.vertex1.x == triangle.vertex2.x && this.vertex1.y == triangle.vertex2.y) return true;
            if (this.vertex1.x == triangle.vertex3.x && this.vertex1.y == triangle.vertex3.y) return true;

            if (this.vertex2.x == triangle.vertex1.x && this.vertex2.y == triangle.vertex1.y) return true;
            if (this.vertex2.x == triangle.vertex2.x && this.vertex2.y == triangle.vertex2.y) return true;
            if (this.vertex2.x == triangle.vertex3.x && this.vertex2.y == triangle.vertex3.y) return true;

            if (this.vertex3.x == triangle.vertex1.x && this.vertex3.y == triangle.vertex1.y) return true;
            if (this.vertex3.x == triangle.vertex2.x && this.vertex3.y == triangle.vertex2.y) return true;
            if (this.vertex3.x == triangle.vertex3.x && this.vertex3.y == triangle.vertex3.y) return true;

            return false;
        }

        public Edge FindCommonEdgeWith(Triangle triangle)
        {
            Edge commonEdge;
            List<Point> commonVertices = new List<Point>();

            if (this.vertex1 == triangle.vertex1 || this.vertex1 == triangle.vertex2 || this.vertex1 == triangle.vertex3) commonVertices.Add(this.vertex1);
            if (this.vertex2 == triangle.vertex1 || this.vertex2 == triangle.vertex2 || this.vertex2 == triangle.vertex3) commonVertices.Add(this.vertex2);
            if (this.vertex3 == triangle.vertex1 || this.vertex3 == triangle.vertex2 || this.vertex3 == triangle.vertex3) commonVertices.Add(this.vertex3);

            if (commonVertices.Count == 2)
            {
                commonEdge = new Edge(commonVertices[0], commonVertices[1]);
                return commonEdge;
            }
            
            return null;
        }
    }
}
