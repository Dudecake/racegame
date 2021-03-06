﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public class Vehicle
    {
        private class Wheel
        {
            RigidBody rigidBody = new RigidBody();
            Vector vector = new Vector();

            private Vector m_forwardAxis, m_sideAxis;
            public float m_wheelTorque, m_wheelSpeed, m_wheelInertia, m_wheelRadius;
            private Vector m_Position = new Vector();

            public Wheel(Vector position, float radius)
            {
                m_Position = position;
                SetSteeringAngle(0);
                m_wheelSpeed = 0;
                m_wheelRadius = radius;
                m_wheelInertia = radius * radius; //fake value
                Console.Write("i");
            }

            public void SetSteeringAngle(float newAngle)
            {
                Matrix mat = new Matrix();
                PointF[] vectors = new PointF[2];

                //foward vector
                vectors[0].X = 0;
                vectors[0].Y = 1;
                //side vector
                vectors[1].X = -1;
                vectors[1].Y = 0;

                mat.Rotate(newAngle / (float)Math.PI * 180.0f);
                mat.TransformVectors(vectors);

                m_forwardAxis = new Vector(vectors[0].X, vectors[0].Y);
                m_sideAxis = new Vector(vectors[1].X, vectors[1].Y);
            }

            public void AddTransmissionTorque(float newValue)
            {
                m_wheelTorque += newValue;
            }

            public float GetWheelSpeed()
            {
                return m_wheelSpeed;
            }

            public Vector GetAttachPoint()
            {
                return m_Position;
            }

            public Vector CalculateForce(Vector relativeGroundSpeed, float timeStep)
            {
                //calculate speed of tire patch at ground
                Vector patchSpeed = -m_forwardAxis * m_wheelSpeed * m_wheelRadius;

                //get velocity difference between ground and patch
                Vector velDifference = relativeGroundSpeed + patchSpeed;

                //project ground speed onto side axis
                float forwardMag = 0;
                Vector sideVel = velDifference.Project(m_sideAxis);
                Vector forwardVel = velDifference.Project(m_forwardAxis, out forwardMag);

                //calculate super fake friction forces
                //calculate response force
                Vector responseForce = -sideVel * 20.0f;
                responseForce -= forwardVel;

                //calculate torque on wheel
                m_wheelTorque += forwardMag * m_wheelRadius;

                //integrate total torque into wheel
                m_wheelSpeed += m_wheelTorque / m_wheelInertia * timeStep;

                //clear our transmission torque accumulator
                m_wheelTorque = 0;

                //return force acting on body
                return responseForce;
            }
        }
        private Wheel[] wheels = new Wheel[4];

        RigidBody rigidBody = new RigidBody();

        public void Setup(Vector halfSize, float mass, Color color)
        {
            //front wheels
            wheels[0] = new Wheel(new Vector(halfSize.X, halfSize.Y), 0.5f);
            wheels[1] = new Wheel(new Vector(-halfSize.X, halfSize.Y), 0.5f);

            //rear wheels
            wheels[2] = new Wheel(new Vector(halfSize.X, -halfSize.Y), 0.5f);
            wheels[3] = new Wheel(new Vector(-halfSize.X, -halfSize.Y), 0.5f);

            rigidBody.Setup(halfSize, mass, color);
        }

        public void SetSteering(float steering)
        {
            const float steeringLock = 0.75f;

            //apply steering angle to front wheels
            wheels[0].SetSteeringAngle(-steering * steeringLock);
            wheels[1].SetSteeringAngle(-steering * steeringLock);
        }

        public void SetThrottle(float throttle, bool allWheel)
        {
            const float torque = 80.0f;

            //apply transmission torque to back wheels
            if (allWheel)
            {
                wheels[0].AddTransmissionTorque(throttle * torque);
                wheels[1].AddTransmissionTorque(throttle * torque);
            }

            wheels[2].AddTransmissionTorque(throttle * torque);
            wheels[3].AddTransmissionTorque(throttle * torque);
        }

        public void SetBrakes(float brakes)
        {
            const float brakeTorque = 4.0f;

            //apply brake torque apposing wheel vel
            foreach (Wheel wheel in wheels)
            {
                float wheelVel = wheel.GetWheelSpeed();
                wheel.AddTransmissionTorque(-wheelVel * brakeTorque * brakes);
            }
        }

        public void Update(float timeStep)
        {
            foreach (Wheel wheel in wheels)
            {
                wheel.m_wheelSpeed = 30.0f;
                Vector worldWheelOffset = rigidBody.RelativeToWorld(wheel.GetAttachPoint());
                Vector worldGroundVel = rigidBody.PointVel(worldWheelOffset);
                Vector relativeGroundSpeed = rigidBody.WorldToRelative(worldGroundVel);
                Vector relativeResponseForce = wheel.CalculateForce(relativeGroundSpeed, timeStep);
                Vector worldResponseForce = rigidBody.RelativeToWorld(relativeResponseForce);

                rigidBody.AddForce(worldResponseForce, worldWheelOffset);
            }

            rigidBody.Update(timeStep);
        }
    }

}
