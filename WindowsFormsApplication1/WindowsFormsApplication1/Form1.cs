﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
//using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        Turn turn = new Turn();
        Polygon polygon = new Polygon();
        //Vector vector = new Vector();
        Collision collision = new Collision();
        
        double time = 1; 

        Bitmap Backbuffer;
        
        List<Polygon> polygons = new List<Polygon>();
        Polygon player1;
        Polygon player2;
        /*
        const int BallAxisSpeedX = 2;
        const int BallAxisSpeedY = 2;
        Point BallSpeed = new Point(BallAxisSpeedX, BallAxisSpeedY);
        Point BallPos = new Point(30, 30);
        const int BallSize = 50;
        */
        //S = 1/2a*t^2

        public Form1()
        {
            InitializeComponent();
            
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);

            System.Timers.Timer GameTimer = new System.Timers.Timer();
            GameTimer.Interval = 10;
            GameTimer.Elapsed += new ElapsedEventHandler(GameTimer_Tick);
            GameTimer.Enabled = true;

            this.ResizeEnd += new EventHandler(Form1_CreateBackBuffer);
            this.Load += new EventHandler(Form1_CreateBackBuffer);
            this.Paint += new PaintEventHandler(Form1_Paint);

            this.KeyDown += new System.Windows.Forms.KeyEventHandler(Form1_KeyDown);

            Polygon p = new Polygon();
            p.Points.Add(new Vector(150, 100));
            p.Points.Add(new Vector(150, 150));
            p.Points.Add(new Vector(100, 150));
            p.Points.Add(new Vector(100, 100));
            polygons.Add(p);

            p = new Polygon();
            p.Points.Add(new Vector(50, 50));
            p.Points.Add(new Vector(100, 0));
            p.Points.Add(new Vector(150, 150));
            p.Offset(80, 80);

            polygons.Add(p);

            p = new Polygon();
            p.Points.Add(new Vector(0, 50));
            p.Points.Add(new Vector(50, 0));
            p.Points.Add(new Vector(150, 80));
            p.Points.Add(new Vector(160, 200));
            p.Points.Add(new Vector(-10, 190));
            p.Offset(300, 300);

            polygons.Add(p);

            foreach (Polygon polygon in polygons) polygon.BuildEdges();

            player1 = polygons[0];
            player2 = polygons[2];
        }

        void Form1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            /*
            double i = 0.05 * Math.Pow(time, 2); //S = a*t^2
            time++;
            if (i >= 20)
            {
                i = 20;
            }
            Vector velocity = new Vector();
            switch (e.KeyValue)
            {
                case 32: //SPACE

                    break;
                case 38: // UP
                    velocity = new Vector(0, -i);
                    break;
                case 40: // DOWN
                    velocity = new Vector(0, i);
                    break;
                case 39: // RIGHT
                    //double k = 5;
                    turn.turnTo(250);

                    velocity = new Vector(i, 0);
                    break;
                case 37: // LEFT
                    velocity = new Vector(-i, 0);
                    break;
            }
            Vector playerTranslation = velocity;
            foreach (Polygon polygon in polygons)
            {
                if (polygon == player1) continue;
                Collision.PolygonCollisionResult r = collision.PolygonCollision(player1, polygon, velocity);
                if (r.WillIntersect)
                {
                    playerTranslation = velocity + r.MinimumTranslationVector;
                    break;
                }
            }
            player1.Offset(playerTranslation);
            velocity = new Vector();
            /*
            int j = 0;

            if ((Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0)
            {
                j = 83;
            }
            if ((Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0)
            {
                j = 87;
            }
            if ((Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0)
            {
                j = 68;
            }
            if ((Keyboard.GetKeyStates(Key.Left) & KeyStates.Down) > 0)
            {
                j = 65;
            }

            */
            /*
            switch (e.KeyValue)
            {
                case 32: //SPACE

                    break;
                case 87: // UP
                    velocity = new Vector(0, -i);
                    break;
                case 83: // DOWN
                    velocity = new Vector(0, i);
                    break;
                case 68: // RIGHT
                    velocity = new Vector(i, 0);
                    break;
                case 65: // LEFT
                    velocity = new Vector(-i, 0);
                    break;
            }
            
            playerTranslation = velocity;
            foreach (Polygon polygon in polygons)
            {
                if (polygon == player2) continue;
                Collision.PolygonCollisionResult r = collision.PolygonCollision(player2, polygon, velocity);
                if (r.WillIntersect)
                {
                    playerTranslation = velocity + r.MinimumTranslationVector;
                    break;
                }
            }
            player2.Offset(playerTranslation);
            
            /*
            if (e.KeyCode == Keys.Left)
                BallSpeed.X = -BallAxisSpeedX;
            else if (e.KeyCode == Keys.Right)
                BallSpeed.X = BallAxisSpeedX;
            else if (e.KeyCode == Keys.Up)
                BallSpeed.Y = -BallAxisSpeedX; // Y axis is downwards so -ve is up.
            else if (e.KeyCode == Keys.Down)
                BallSpeed.Y = BallAxisSpeedX;
            */
        }
        void Form1_Paint(object sender, PaintEventArgs e)
        {
            Vector p1;
            Vector p2;
            foreach (Polygon polygon in polygons)
            {
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    p1 = polygon.Points[i];
                    if (i + 1 >= polygon.Points.Count)
                    {
                        p2 = polygon.Points[0];
                    }
                    else
                    {
                        p2 = polygon.Points[i + 1];
                    }
                    e.Graphics.DrawLine(new Pen(Color.Black), p1, p2);
                }
            }
            Invalidate();
            
            if (Backbuffer != null)
            {
                e.Graphics.DrawImageUnscaled(Backbuffer, System.Drawing.Point.Empty);
            }
            
        }
        void Form1_CreateBackBuffer(object sender, EventArgs e)
        {
            if (Backbuffer != null)
                Backbuffer.Dispose();

            Backbuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
        }
        void Draw()
        {
            if (Backbuffer != null)
            {
                using (var g = Graphics.FromImage(Backbuffer))
                {
                    g.Clear(Color.White);
                    //g.FillEllipse(Brushes.Black, BallPos.X - BallSize / 2, BallPos.Y - BallSize / 2, BallSize, BallSize);
                }
                Invalidate();
            }
        }
        void GameTimer_Tick(object sender, EventArgs e)
        {
            double k = 1000;
            turn.update(k);

            double i = 0.05 * Math.Pow(time, 2); //S = a*t^2
            time++;
            if (i >= 20)
            {
                i = 20;
            }
            Vector velocity = new Vector();

            int j = 0;

            if ((Keyboard.GetKeyStates(Key.Down) & KeyStates.Down) > 0)
            {
                j = 83;
            }
            if ((Keyboard.GetKeyStates(Key.Up) & KeyStates.Down) > 0)
            {
                j = 87;
            }
            if ((Keyboard.GetKeyStates(Key.Right) & KeyStates.Down) > 0)
            {
                j = 68;
            }
            if ((Keyboard.GetKeyStates(Key.Left) & KeyStates.Down) > 0)
            {
                j = 65;
            }
            switch (j)
            {
                case 32: //SPACE

                    break;
                case 87: // UP
                    velocity = new Vector(0, -i);
                    break;
                case 83: // DOWN
                    velocity = new Vector(0, i);
                    break;
                case 68: // RIGHT
                    velocity = new Vector(i, 0);
                    break;
                case 65: // LEFT
                    velocity = new Vector(-i, 0);
                    break;
            }
            Vector playerTranslation = velocity;
            foreach (Polygon polygon in polygons)
            {
                if (polygon == player1) continue;
                Collision.PolygonCollisionResult r = collision.PolygonCollision(player1, polygon, velocity);
                if (r.WillIntersect)
                {
                    playerTranslation = velocity + r.MinimumTranslationVector;
                    break;
                }
            }
            player1.Offset(playerTranslation);
            
            //V = a*t  a = acc in m/s^2  t = tijd
            //BallPos.X += BallSpeed.X;
            //BallPos.Y += BallSpeed.Y;
            //Draw();
            
            // TODO: Add the notion of dying (disable the timer and show a message box or something)
        }

        private void Form1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            time = 1;
        }
    }
}
