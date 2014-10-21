using System;
using System.Collections.Generic;
using System.Text;

namespace racing_simulation_2d
{
    class Vector
    {
        public float X, Y;

        public Vector() { X = 0; Y = 0; }
        public Vector(float x, float y) { X = x; Y = y; }

        //length property        
        public float Length
        {
            get
            {
                return (float)Math.Sqrt((double)(X * X + Y * Y));
            }
        }

        //addition
        public static Vector operator +(Vector L, Vector R)
        {
            return new Vector(L.X + R.X, L.Y + R.Y);
        }

        //subtraction
        public static Vector operator -(Vector L, Vector R)
        {
            return new Vector(L.X - R.X, L.Y - R.Y);
        }

        //negative
        public static Vector operator -(Vector R)
        {
            Vector temp = new Vector(-R.X, -R.Y);
            return temp;
        }

        //scalar multiply
        public static Vector operator *(Vector L, float R)
        {
            return new Vector(L.X * R, L.Y * R);
        }

        //divide multiply
        public static Vector operator /(Vector L, float R)
        {
            return new Vector(L.X / R, L.Y / R);
        }

        //dot product
        public static float operator *(Vector L, Vector R)
        {
            return (L.X * R.X + L.Y * R.Y);
        }

        //cross product, in 2d this is a scalar since we know it points in the Z direction
        public static float operator %(Vector L, Vector R)
        {
            return (L.X * R.Y - L.Y * R.X);
        }

        //normalize the vector
        public void normalize()
        {
            float mag = Length;

            X /= mag;
            Y /= mag;
        }

        //project this vector on to v
        public Vector Project(Vector v)
        {
            //projected vector = (this dot v) * v;
            float thisDotV = this * v;
            return v * thisDotV;
        }

        //project this vector on to v, return signed magnatude
        public Vector Project(Vector v, out float mag)
        {
            //projected vector = (this dot v) * v;
            float thisDotV = this * v;
            mag = thisDotV;
            return v * thisDotV;
        }
    }
}
