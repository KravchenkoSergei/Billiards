using System;
using System.Drawing;

namespace Billiards
{
    public class Vector
    {
        public Vector() : this(0, 0) { }
        public Vector(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector(Point a, Point b) : this(b.X - a.X, b.Y - a.Y) { }

        public Point a { get; set; }
        public Point b { get; set; }
        public Vector Projection(Vector onVector)
        {
            return this * onVector / onVector.SquareAbs * onVector;
        }

        public override string ToString()
        {
            return $"({X};{Y})";
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X, -v.Y);
        }

        public static Vector operator -(Vector a, Vector b)
        {
            return a + -b;
        }

        public static Vector operator *(Vector v, double n)
        {
            return new Vector(v.X * n, v.Y * n);
        }

        public static Vector operator /(Vector v, double n)
        {
            return v * (1 / n);
        }

        public static Vector operator *(double n, Vector v)
        {
            return v * n;
        }

        public static double operator *(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public double X { get;
            set; }
        public double Y { get; set; }

        public double SquareAbs 
        {
            get
            {
                return X * X + Y * Y;
            }
        }

        public double Abs
        {
            get 
            { 
                return Math.Sqrt(SquareAbs);
            }
        }

        public Vector GetNorm()
        {
            return new Vector(-Y, X);
        }

        public Vector Mirror(Vector OnVector)
        {
            return this - 2 * (this.Projection(OnVector.GetNorm()));
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Vector))
            {
                return false;
            }
            Vector v = (Vector)obj;
            return X == v.X && Y == v.Y;
        }

    }
}
