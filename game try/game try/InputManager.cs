﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace game_try
{
    class InputManager
    {
        public Point mousePoint;
        public Keys[] keysPressed;
        public Keys[] keysHeld;
        public Stopwatch gameTime;
        public float deltaTime;

        public void Update(Point mp, Keys[] kp, Keys[] kh, Stopwatch gt,float dt)
        {
            mousePoint = mp;
            keysPressed = kp;
            keysHeld = kh;
            gameTime = gt;
            deltaTime = dt;
        }


    }
}