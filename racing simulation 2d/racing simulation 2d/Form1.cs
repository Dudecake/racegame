using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace racing_simulation_2d
{
    //our main application form
    public partial class frmMain : Form
    {
        Vehicle vehicle = new Vehicle();
        RigidBody rigidBody = new RigidBody();
        Vector vector = new Vector();
        Timer timers = new Timer();
        
        //graphics
        Graphics graphics; //gdi+
        Bitmap backbuffer;
        Size buffersize;
        const float screenScale = 2.0f;
        Timer timer = new Timer();

        //keyboard controls
        bool leftHeld = false, rightHeld = false;
        bool upHeld = false, downHeld = false;

        //vehicle controls
        float steering = 0; //-1 is full left, 0 is center, 1 is full right
        float throttle = 0; //0 is coasting, 1 is full throttle
        float brakes = 0; //0 is no brakes, 1 is full brakes

        //game objects
        //Vehicle vehicle = new Vehicle();

        public frmMain()
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
            ConstrainVehicle();

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
}