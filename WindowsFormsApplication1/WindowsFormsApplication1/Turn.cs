using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Turn
    {
        float x = 0;
        float y = 0;
        float speed = 15; // m/s;
        float heading = 120;
        float plannedHeading = 0;
        float turnRate = 1.5f; // deg /s
        public Turn()
        {
            plannedHeading = heading;
        }
        public void update(float dt)
        {
            x += dt * getVx();
            y += dt * getVy();
            if (heading != plannedHeading)
            {
                computeTurn(dt);
            }
            if (heading == plannedHeading)
            {
                Console.WriteLine(heading);
            }
        }
        void computeTurn(float dt)
        {
            float dh = plannedHeading - heading;
            if (dh < -180)
                dh += 360;
            if (dh > 180)
                dh -= 360;
            if (Math.Abs(dh) < turnRate * dt)
                heading = plannedHeading;
            else
            {
                int dir = 1;
                if (dh < 0)
                    dir = -1;
                heading += turnRate * dt * dir;
            }
        }

        public void turnTo(float newHeading)
        {
            plannedHeading = newHeading;
        }
        public float getVx()
        {
            return speed * Math.Acos(heading * Math.PI / 180);
        }
        public float getVy()
        {
            return speed * Math.Asin(heading * Math.PI / 180);
        }

    }
}
