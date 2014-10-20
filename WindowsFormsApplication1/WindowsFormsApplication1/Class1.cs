using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Ship
    {
        double x = 0;
        double y = 0;
        double speed = 15; // m/s;
        double heading = 120;
        double plannedHeading = 0;
        double turnRate = 3; // deg /s
        public Ship()
        {
            plannedHeading = heading;
        }
        void update(double dt)
        {
            x += dt * getVx();
            y += dt * getVy();
            if (heading != plannedHeading)
            {
                computeTurn(dt);
            }

        }
        void computeTurn(double dt)
        {
            double dh = plannedHeading - heading;
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

        public void turnTo(double newHeading)
        {
            plannedHeading = newHeading;
        }
        public double getVx()
        {
            return speed * Math.Acos(heading * Math.PI / 180);
        }
        public double getVy()
        {
            return speed * Math.Asin(heading * Math.PI / 180);
        }

    }
}
