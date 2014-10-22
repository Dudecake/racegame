using System;
using System.Collections.Generic;
using System.Text;

namespace racing_simulation_2d
{

    //handles 3-4 component vector math, though we only need 2d for this project, the 3d vector will work
    class Vector
    {
        //member variables
        private float[] xyzw = new float[4] { 0, 0, 0, 1 };

        //properties
        public float x
        {
            get
            {
                return xyzw[0];
            }
            set
            {
                xyzw[0] = value;
            }
        }
        public float y
        {
            get
            {
                return xyzw[1];
            }
            set
            {
                xyzw[1] = value;
            }
        }
        public float z
        {
            get
            {
                return xyzw[2];
            }
            set
            {
                xyzw[2] = value;
            }
        }
        public float w
        {
            get
            {
                return xyzw[3];
            }
            set
            {
                xyzw[3] = value;
            }
        }
        public float length
        {
            get
            {
                return (float)Math.Sqrt((double)(x * x + y * y + z * z));
            }
        }
        public float length2
        {
            get
            {
                return (x * x + y * y + z * z);
            }
        }

        //constructors
        public Vector()
        {

        }
        public Vector(float x, float y, float z)
        {
            xyzw[0] = x;
            xyzw[1] = y;
            xyzw[2] = z;
        }
        public Vector(float x, float y, float z, float w)
        {
            xyzw[0] = x;
            xyzw[1] = y;
            xyzw[2] = z;
            xyzw[3] = w;
        }
        public Vector(float[] xyz)
        {
            x = xyz[0];
            y = xyz[1];
            z = xyz[2];
            w = 1;
        }
        public Vector(Vector vec)
        {
            x = vec.x;
            y = vec.y;
            z = vec.z;
            w = vec.w;
        }

        public void Set(float x, float y, float z)
        {
            xyzw[0] = x;
            xyzw[1] = y;
            xyzw[2] = z;
        }
        public void Set(Vector vec)
        {
            x = vec.x;
            y = vec.y;
            z = vec.z;
            w = vec.w;
        }

        //operators
        //subtraction
        public static Vector operator -(Vector L, Vector R)
        {
            Vector temp = new Vector(L.x - R.x, L.y - R.y, L.z - R.z);
            return temp;
        }

        //negative
        public static Vector operator -(Vector R)
        {
            Vector temp = new Vector(-R.x, -R.y, -R.z);
            return temp;
        }

        //addition
        public static Vector operator +(Vector L, Vector R)
        {
            Vector temp = new Vector(L.x + R.x, L.y + R.y, L.z + R.z);
            return temp;
        }

        //dot product
        public static float operator *(Vector L, Vector R)
        {
            return (L.x * R.x + L.y * R.y + L.z * R.z);
        }

        //cross product
        public static Vector operator %(Vector L, Vector R)
        {
            Vector temp = new Vector(L.y * R.z - L.z * R.y, L.z * R.x - L.x * R.z, L.x * R.y - L.y * R.x);
            return temp;
        }

        //transform by matrix
        public static Vector operator *(Vector L, Matrix R)
        {
            Vector temp = new Vector();

            temp.x = L.x * R.m11 + L.y * R.m12 + L.z * R.m13 + R.m14;
            temp.y = L.x * R.m21 + L.y * R.m22 + L.z * R.m23 + R.m24;
            temp.z = L.x * R.m31 + L.y * R.m32 + L.z * R.m33 + R.m34;
            temp.w = L.x * R.m41 + L.y * R.m42 + L.z * R.m43 + R.m44;

            return temp;
        }

        //multiply by scalar
        public static Vector operator *(Vector L, float R)
        {
            Vector temp = new Vector();

            temp.x = L.x * R;
            temp.y = L.y * R;
            temp.z = L.z * R;

            return temp;
        }

        //divide by scalar
        public static Vector operator /(Vector L, float R)
        {
            Vector temp = new Vector();

            temp.x = L.x / R;
            temp.y = L.y / R;
            temp.z = L.z / R;

            return temp;
        }

        //normalize the vector
        public void normalize()
        {
            float mag = length;

            x /= mag;
            y /= mag;
            z /= mag;
        }

        //reflect this vector over n
        public Vector Reflect(Vector n)
        {
            float angle;
            Vector nTemp = new Vector(n);
            Vector vTemp = this;

            angle = (vTemp * n) * 2; //v dot n * 2
            nTemp *= angle;

            return (vTemp - nTemp);
        }

        //component wise multiplication
        public Vector CompMult(Vector v)
        {
            Vector temp = new Vector();

            temp.x = x * v.x;
            temp.y = y * v.y;
            temp.z = z * v.z;
            temp.w = w * v.w;

            return temp;
        }

        //project this vector on to v
        public Vector Project(Vector v)
        {
            //projected vector = (this dot v) * v;
            float thisDotV = this * v;
            return v * thisDotV;
        }
    }

}
