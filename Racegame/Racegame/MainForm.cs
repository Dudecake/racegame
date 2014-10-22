using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Timers;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    public partial class MainForm : Form
    {
        Polygon polygon = new Polygon();
        Vector vector = new Vector();
        Collision collision = new Collision();
        Vehicle vehicle = new Vehicle();
        RigidBody rigidBody = new RigidBody();

        
        double time = 1; 

        Bitmap Backbuffer;
        
        List<Polygon> polygons = new List<Polygon>();
        Polygon player1;
        Polygon player2;

        public MainForm()
        {
            InitializeComponent();
            /*
            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.floatBuffer, true);
            */
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
            float i = 0.05 * Math.Pow(time, 2); //S = a*t^2
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
                    //float k = 5;
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
                    //e.Graphics.DrawLine(new Pen(Color.Black), p1, p2);
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
            float i = Convert.ToSingle(0.05 * Math.Pow(time, 2)); //S = a*t^2
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

            //Draw();
            
            // TODO: Add the notion of dying (disable the timer and show a message box or something)
        }

        private void Form1_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            time = 1;
        }
        
        /*
        //graphics
        Graphics graphics; //gdi+
        Bitmap backbuffer;
        Size buffersize;
        const float screenScale = 3.0f;
        Timer timer = new Timer();

        //keyboard controls
        bool leftHeld = false, rightHeld = false;
        bool upHeld = false, downHeld = false;

        //vehicle controls
        float steering = 0; //-1 is full left, 0 is center, 1 is full right
        float throttle = 0; //0 is coasting, 1 is full throttle
        float brakes = 0; //0 is no brakes, 1 is full brakes

        //game objects
        Vehicle vehicle = new Vehicle();

        public void frmMain()
        {
            InitializeComponent();
            Application.Idle += new EventHandler(ApplicationIdle);

            screen.Paint += new PaintEventHandler(screen_Paint);
            this.KeyDown += new KeyEventHandler(onKeyDown);
            this.KeyUp += new KeyEventHandler(onKeyUp);

            Init(screen.Size);
        }

        //intialize rendering
        private void Init(Size size)
        {
            //setup rendering device
            buffersize = size;
            backbuffer = new Bitmap(buffersize.Width, buffersize.Height);
            graphics = Graphics.FromImage(backbuffer);

            timer.GetETime(); //reset timer

            vehicle.Setup(new Vector(3, 8)/2.0f, 5, Color.Red);
            vehicle.SetLocation(new Vector(0, 0), 0);
        }

        //main rendering function
        private void Render(Graphics g)
        {
            //clear back buffer
            graphics.Clear(Color.Black);
            graphics.ResetTransform();
            graphics.ScaleTransform(screenScale, -screenScale);
            graphics.TranslateTransform(buffersize.Width / 2.0f / screenScale, -buffersize.Height / 2.0f / screenScale);

            //draw to back buffer
            DrawScreen();

            //present back buffer
            g.DrawImage(backbuffer, new Rectangle(0, 0, buffersize.Width, buffersize.Height), 0, 0, buffersize.Width, buffersize.Height, GraphicsUnit.Pixel);
        }

        //draw the screen
        private void DrawScreen()
        {
            vehicle.Draw(graphics, buffersize);
        }

        //process game logic
        private void DoFrame()
        {
            //get elapsed time since last frame
            float etime = timer.GetETime();

            //process input
            ProcessInput();

            //apply vehicle controls
            vehicle.SetSteering(steering);
            vehicle.SetThrottle(throttle, menu.Checked);
            vehicle.SetBrakes(brakes);

            //integrate vehicle physics
            vehicle.Update(etime);

            //keep the vehicle on the screen
            //ConstrainVehicle();

            //redraw our screen
            screen.Invalidate();
        }
        
        //keep the vehicle on the screen
        private void ConstrainVehicle()
        {
            Vector position = vehicle.GetPosition();
            Vector screenSize = new Vector(screen.Width / screenScale, screen.Height / screenScale);

            while (position.X > screenSize.X / 2.0f) { position.X -= screenSize.X; }
            while (position.Y > screenSize.Y / 2.0f) { position.Y -= screenSize.Y; }
            while (position.X < -screenSize.X / 2.0f) { position.X += screenSize.X; }
            while (position.Y < -screenSize.Y / 2.0f) { position.Y += screenSize.Y; }
        }
        
        //process keyboard input
        private void ProcessInput()
        {
            if (leftHeld)
                steering = -1;
            else if (rightHeld)
                steering = 1;
            else
                steering = 0;

            if (upHeld)
                throttle = 1;
            else
                throttle = 0;

            if (downHeld)
                brakes = 1;
            else
                brakes = 0;
        }

        private void onKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftHeld = true;
                    break;
                case Keys.Right:
                    rightHeld = true;
                    break;
                case Keys.Up:
                    upHeld = true;
                    break;
                case Keys.Down:
                    downHeld = true;
                    break;
                default: //no match found
                    return; //return so handled dosnt get set
            }

            //match found
            e.Handled = true;
        }

        private void onKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftHeld = false;
                    break;
                case Keys.Right:
                    rightHeld = false;
                    break;
                case Keys.Up:
                    upHeld = false;
                    break;
                case Keys.Down:
                    downHeld = false;
                    break;
                default: //no match found
                    return; //return so handled dosnt get set
            }

            //match found
            e.Handled = true;
        }

        //rendering - only when screen is invalidated
        private void screen_Paint(object sender, PaintEventArgs e)
        {
            Render(e.Graphics);
        }

        //when the os gives us time, run the game
        private void ApplicationIdle(object sender, EventArgs e)
        {
            // While the application is still idle, run frame routine.
            DoFrame();
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    */
    }
}
