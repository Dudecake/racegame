using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsApplication1
{
    public class Timer
    {
        //store last time sample
        private int lastTime = Environment.TickCount;
        private float etime;

        //calculate and return elapsed time since last call
        public float GetETime()
        {
            etime = (Environment.TickCount - lastTime) / 1000.0f;
            lastTime = Environment.TickCount;

            return etime;
        }
    }
}
