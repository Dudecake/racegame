using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class RigidBody
    {
        //linear properties
        private Vector m_position = new Vector();
        private Vector m_velocity = new Vector();
        private Vector m_forces = new Vector();
        private float m_mass;

        //angular properties
        private float m_angle;
        private float m_angularVelocity;
        private float m_torque;
        private float m_inertia;

        //graphical properties
        private Vector m_halfSize = new Vector();
        System.Drawing.Rectangle rect = new System.Drawing.Rectangle();
        private System.Drawing.Color m_color;

        public RigidBody()
        {
            //set these defaults so we dont get divide by zeros
            m_mass = 1.0f;
            m_inertia = 1.0f;
        }

        //intialize out parameters
        public void Setup(Vector halfSize, float mass, System.Drawing.Color color)
        {
            //store physical parameters
            m_halfSize = halfSize;
            m_mass = mass;
            m_color = color;
            m_inertia = (1.0f / 12.0f) * (halfSize.X * halfSize.X) * (halfSize.Y * halfSize.Y) * mass;

            //generate our viewable rectangle
            rect.X = (int)-m_halfSize.X;
            rect.Y = (int)-m_halfSize.Y;
            rect.Width = (int)(m_halfSize.X * 2.0f);
            rect.Height = (int)(m_halfSize.Y * 2.0f);
        }

        public void SetLocation(Vector position, float angle)
        {
            m_position = position;
            m_angle = angle;
        }

        public Vector GetPosition()
        {
            return m_position;
        }

        public void Update(float timeStep)
        {
            //integrate physics
            //linear
            Vector acceleration = m_forces / m_mass;
            m_velocity += acceleration * timeStep;
            m_position += m_velocity * timeStep;
            m_forces = new Vector(0, 0); //clear forces

            //angular
            float angAcc = m_torque / m_inertia;
            m_angularVelocity += angAcc * timeStep;
            m_angle += m_angularVelocity * timeStep;
            m_torque = 0; //clear torque
        }

        public void Draw(System.Drawing.Graphics graphics, System.Drawing.Size buffersize)
        {
            //store transform, (like opengl's glPushMatrix())
            System.Drawing.Drawing2D.Matrix mat1 = graphics.Transform;

            //transform into position
            graphics.TranslateTransform(m_position.X, m_position.Y);
            graphics.RotateTransform(m_angle / (float)Math.PI * 180.0f);

            try
            {
                //draw body
                graphics.DrawRectangle(new System.Drawing.Pen(m_color), rect);

                //draw line in the "forward direction"
                graphics.DrawLine(new System.Drawing.Pen(System.Drawing.Color.Yellow), 1, 0, 1, 5);
            }
            catch (OverflowException exc)
            {
                //physics overflow :(
            }

            //restore transform
            graphics.Transform = mat1;
        }

        //take a relative vector and make it a world vector
        public Vector RelativeToWorld(Vector relative)
        {
            System.Drawing.Drawing2D.Matrix mat = new System.Drawing.Drawing2D.Matrix();
            System.Drawing.PointF[] vectors = new System.Drawing.PointF[1];

            vectors[0].X = relative.X;
            vectors[0].Y = relative.Y;

            mat.Rotate(m_angle / (float)Math.PI * 180.0f);
            mat.TransformVectors(vectors);

            return new Vector(vectors[0].X, vectors[0].Y);
        }

        //take a world vector and make it a relative vector
        public Vector WorldToRelative(Vector world)
        {
            System.Drawing.Drawing2D.Matrix mat = new System.Drawing.Drawing2D.Matrix();
            System.Drawing.PointF[] vectors = new System.Drawing.PointF[1];

            vectors[0].X = world.X;
            vectors[0].Y = world.Y;

            mat.Rotate(-m_angle / (float)Math.PI * 180.0f);
            mat.TransformVectors(vectors);

            return new Vector(vectors[0].X, vectors[0].Y);
        }

        //velocity of a point on body
        public Vector PointVel(Vector worldOffset)
        {
            Vector tangent = new Vector(-worldOffset.Y, worldOffset.X);
            return tangent * m_angularVelocity + m_velocity;
        }

        public void AddForce(Vector worldForce, Vector worldOffset)
        {
            //add linar force
            m_forces += worldForce;
            //and it's associated torque
            m_torque += worldOffset % worldForce;
        }
    }
}
