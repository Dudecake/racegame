using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    public class Polygon
    {

        private List<Vector> points = new List<Vector>();
        private List<Vector> edges = new List<Vector>();

        public void BuildEdges()
        {
            Vector p1;
            Vector p2;
            edges.Clear();
            for (int i = 0; i < points.Count; i++)
            {
                p1 = points[i];
                if (i + 1 >= points.Count)
                {
                    p2 = points[0];
                }
                else
                {
                    p2 = points[i + 1];
                }
                edges.Add(p2 - p1);
            }
        }

        public List<Vector> Edges
        {
            get { return edges; }
        }

        public List<Vector> Points
        {
            get { return points; }
        }

        public Vector Center
        {
            get
            {
                double totalX = 0;
                double totalY = 0;
                for (int i = 0; i < points.Count; i++)
                {
                    totalX += points[i].X;
                    totalY += points[i].Y;
                }

                return new Vector(totalX / (double)points.Count, totalY / (double)points.Count);
            }
        }

        public void Offset(Vector v)
        {
            Offset(v.X, v.Y);
        }

        public void Offset(double x, double y)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Vector p = points[i];
                points[i] = new Vector(p.X + x, p.Y + y);
            }
        }

        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < points.Count; i++)
            {
                if (result != "") result += " ";
                result += "{" + points[i].ToString() + "}";
            }

            return result;
        }
    }
}
