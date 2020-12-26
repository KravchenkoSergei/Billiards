using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Billiards
{
    public class Ball
    {
        public Vector R { get; set; }
        public Vector V { get; set; }
        public double M { get; set; }

        public double D { get; set; }

        public void Move(double dt, Vector F)
        {
            Vector a = F / M;
            V += a * dt;
            Move(dt);
        }

        public void Move(double dt)
        {
            R += V * dt;
            V *= 0.99;
        }

        public static bool IsCollision(Ball b1, Ball b2)
        {
            double d = b1.D / 2 + b2.D / 2;
            return d * d >= (b1.R - b2.R).SquareAbs;
        }

        public bool IsCollision(Point p1, Point p2)
        {
            Vector vp1 = new Vector(p1.X, p1.Y);
            Vector vp2 = new Vector(p2.X, p2.Y);
            Vector p1p2 = vp2 - vp1;
            Vector a = R - vp1;
            Vector b = R - vp2;
            if (a * p1p2 < 0) return false;
            if (b * p1p2 > 0) return false;
            return a.Projection(p1p2.GetNorm()).SquareAbs <= (D / 2) * (D / 2);
        }


        public static void Collide(Ball b1, Ball b2)
        {
            Vector OO = b1.R - b2.R;
            Vector v0 = b1.V.Projection(OO);
            Vector u0 = b2.V.Projection(OO);
            Vector v0y = b1.V - v0;
            Vector u0y = b2.V - u0;
            Vector v = (2 * b2.M * u0 + v0 * (b1.M - b2.M)) / (b1.M + b2.M);
            Vector u = v0 + v - u0;
            b1.V = v + v0y;
            b2.V = u + u0y;
        }

        public void Collide(Point p1, Point p2)
        {
            Vector wall = new Vector(p2.X - p1.X, p2.Y - p1.Y);
            V = V.Mirror(wall);
        }
    }
}
