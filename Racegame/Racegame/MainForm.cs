using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace WindowsFormsApplication1
{

    public partial class MainForm : Form
    {
        #region Variabelen

        bool running = true;

        double f1;
        double f2;

        bool veh1r11 = false;
        bool veh1r21 = false;
        bool veh1r31 = false;

        bool veh1c1 = false;
        bool veh1c2 = false;
        bool veh1c3 = false;

        bool veh2r11 = false;
        bool veh2r21 = false;
        bool veh2r31 = false;

        bool veh2c1 = false;
        bool veh2c2 = false;
        bool veh2c3 = false;

        string veh1;
        string veh2;

        Point[] startLine = new Point[2];
        Point[] checkPoint1 = new Point[2];
        Point[] checkPoint2 = new Point[2];
        Point[] checkpoint3 = new Point[2];
        Point[] outerPerimeter = new Point[7];
        Point[] outerPerimeter2 = new Point[48];
        Point[] innerPerimeterUpper = new Point[13];
        Point[] innerPerimeterLower = new Point[11];
        Point[] outerWall = new Point[5];
        Rectangle garage = new Rectangle(-47, -18, 85, 35);
        Rectangle pitsStop = new Rectangle(0, -35, 5, 5);

        Vehicle1 vehicle1 = new Vehicle1();
        Vehicle2 vehicle2 = new Vehicle2();

        Thread input = new Thread(new ThreadStart(ProcessInput));
        Thread input2 = new Thread(new ThreadStart(ProcessInput2));
        Thread constrain = new Thread(new ThreadStart(ConstrainVehicles));
        Thread collide = new Thread(new ThreadStart(Collision));
        Thread timer1 = new Thread(new ThreadStart(Timers1));
        Thread timer2 = new Thread(new ThreadStart(Timers2));

        //graphics
        Graphics graphics; //gdi+
        Bitmap backbuffer;
        Size buffersize;
        const float screenScale = 3.0f;
        Timer timer = new Timer();

        private int fps;
        private int fpsCounter;
        private Stopwatch gameTime = new Stopwatch();
        private Stopwatch raceTime = new Stopwatch();

        //keyboard controls
        bool AHeld = false, DHeld = false;
        bool WHeld = false, SHeld = false;
        bool Upheld = false, Downheld = false;
        bool Rightheld = false, Leftheld = false;
        bool EHeld = false, ShiftHeld = false;

        //vehicle controls
        float steering = 0; //-1 is full left, 0 is center, 1 is full right
        float throttle = 0; //0 is coasting, 1 is full throttle
        float brakes = 0; //0 is no brakes, 1 is full brakes

        float steering2 = 0; //-1 is full left, 0 is center, 1 is full right
        float throttle2 = 0; //0 is coasting, 1 is full throttle
        float brakes2 = 0; //0 is no brakes, 1 is full brakes

        Bitmap Backbuffer;
        Bitmap m_map = new Bitmap(Properties.Resources.design_1, 342, 257);

        Vector y1;
        Rectangle x1;
        Vector y2;
        Rectangle x2;
        #endregion

        #region initialization

        public MainForm()
        {
            InitializeComponent();
            Application.Idle += new EventHandler(ApplicationIdle);

            screen.Paint += new PaintEventHandler(screen_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(onKeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(onKeyUp);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(onKeyDown2);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(onKeyUp2);
            Init(screen.Size);


            input.Start();
            input2.Start();
            constrain.Start();
            collide.Start();
            timer1.Start();
            timer2.Start();

            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);

            this.ResizeEnd += new EventHandler(Form1_CreateBackBuffer);
            this.Load += new EventHandler(Form1_CreateBackBuffer);

            #region Lijnen
            /*
            -19, -8
            -57, -8
            4, 78
            4, 121
            124, 1
            164, 1
            4, 122
            4, -16
            */
            startLine[0] = new Point(-19, -8);
            startLine[1] = new Point(-57, -8);
            checkPoint1[0] = new Point(4, 78);
            checkPoint1[1] = new Point(4, 121);
            checkPoint2[0] = new Point(124, 1);
            checkPoint2[1] = new Point(164, 1);
            checkpoint3[0] = new Point(4, 122);
            checkpoint3[1] = new Point(4, -16);

            outerPerimeter[0] = new Point(-157, 63);
            outerPerimeter[1] = new Point(1, 123);
            outerPerimeter[2] = new Point(162, 61);
            outerPerimeter[3] = new Point(162, -60);
            outerPerimeter[4] = new Point(6, -121);
            outerPerimeter[5] = new Point(-155, -61);
            outerPerimeter[6] = new Point(-157, 63);

            outerPerimeter2[0] = new Point(-156, 128);
            outerPerimeter2[1] = new Point(-171, 113);
            outerPerimeter2[2] = new Point(-171, 98);
            outerPerimeter2[3] = new Point(-136, 128);
            outerPerimeter2[4] = new Point(-121, 128);
            outerPerimeter2[5] = new Point(-171, 83);
            outerPerimeter2[6] = new Point(-171, 68);
            outerPerimeter2[7] = new Point(-100, 128);
            outerPerimeter2[8] = new Point(-80, 128);
            outerPerimeter2[9] = new Point(-171, 53);
            outerPerimeter2[10] = new Point(-171, 43);
            outerPerimeter2[11] = new Point(-55, 128);
            outerPerimeter2[12] = new Point(-30, 128);
            outerPerimeter2[13] = new Point(-171, 35);
            outerPerimeter2[14] = new Point(-10, 128);

            outerPerimeter2[15] = new Point(156, 128);
            outerPerimeter2[16] = new Point(171, 113);
            outerPerimeter2[17] = new Point(171, 98);
            outerPerimeter2[18] = new Point(136, 128);
            outerPerimeter2[19] = new Point(121, 128);
            outerPerimeter2[20] = new Point(171, 83);
            outerPerimeter2[21] = new Point(171, 68);
            outerPerimeter2[22] = new Point(100, 128);
            outerPerimeter2[23] = new Point(80, 128);
            outerPerimeter2[24] = new Point(171, 60);
            outerPerimeter2[25] = new Point(50, 128);
            outerPerimeter2[26] = new Point(25, 128);
            outerPerimeter2[27] = new Point(171, 60);

            outerPerimeter2[28] = new Point(171, -113);
            outerPerimeter2[29] = new Point(136, -128);
            outerPerimeter2[30] = new Point(121, -128);
            outerPerimeter2[31] = new Point(171, -98);
            outerPerimeter2[32] = new Point(171, -83);
            outerPerimeter2[33] = new Point(100, -128);
            outerPerimeter2[34] = new Point(80, -128);
            outerPerimeter2[35] = new Point(171, -75);
            outerPerimeter2[36] = new Point(171, -65);
            outerPerimeter2[37] = new Point(50, -128);
            outerPerimeter2[38] = new Point(25, -128);

            outerPerimeter2[39] = new Point(-25, -128);
            outerPerimeter2[40] = new Point(-171, -70);
            outerPerimeter2[41] = new Point(-171, -85);
            outerPerimeter2[42] = new Point(-60, -128);
            outerPerimeter2[43] = new Point(-90, -128);
            outerPerimeter2[44] = new Point(-171, -100);
            outerPerimeter2[45] = new Point(-130, -128);
            outerPerimeter2[46] = new Point(-171, -110);
            outerPerimeter2[47] = new Point(-171, 32);

            innerPerimeterUpper[0] = new Point(-118, 37);
            innerPerimeterUpper[1] = new Point(3, 77);
            innerPerimeterUpper[2] = new Point(123, 40);
            innerPerimeterUpper[3] = new Point(123, -41);
            innerPerimeterUpper[4] = new Point(34, -16);
            innerPerimeterUpper[5] = new Point(-53, -18);
            innerPerimeterUpper[6] = new Point(-120, -36);
            innerPerimeterUpper[7] = new Point(-118, 37);
            innerPerimeterUpper[8] = new Point(123, 40);
            innerPerimeterUpper[9] = new Point(-120, -36);
            innerPerimeterUpper[10] = new Point(3, 77);
            innerPerimeterUpper[11] = new Point(123, -41);
            innerPerimeterUpper[12] = new Point(-118, 37);

            innerPerimeterLower[0] = new Point(2, -83);
            innerPerimeterLower[1] = new Point(-95, -47);
            innerPerimeterLower[2] = new Point(-53, -34);
            innerPerimeterLower[3] = new Point(32, -34);
            innerPerimeterLower[4] = new Point(82, -53);
            innerPerimeterLower[5] = new Point(2, -83);
            innerPerimeterLower[6] = new Point(-53, -34);
            innerPerimeterLower[7] = new Point(82, -53);
            innerPerimeterLower[8] = new Point(-93, -47);
            innerPerimeterLower[9] = new Point(32, -34);
            innerPerimeterLower[10] = new Point(2, -83);

            outerWall[0] = new Point(-171, -128);
            outerWall[1] = new Point(-171, 128);
            outerWall[2] = new Point(171, 128);
            outerWall[3] = new Point(171, -128);
            outerWall[4] = new Point(-171, -128);

            #endregion
        }

        //intialize rendering
        private void Init(Size size)
        {
            //setup rendering device
            Random rand = new Random();
            buffersize = size;
            backbuffer = new Bitmap(buffersize.Width, buffersize.Height);
            graphics = Graphics.FromImage(backbuffer);
            gameTime.Start();
            raceTime.Start();
            timer.GetETime(); //reset timer
            Bitmap[] autos = new Bitmap[2];
            #region Vehicle selection
            for (byte i = 0; i < 2; i++)
            {
                switch (rand.Next(10))
                {
                    case 0:
                        autos[i] = Properties.Resources._61px_Jefferson_GTA2;
                        break;
                    case 1:
                        autos[i] = Properties.Resources._62px_AnistonBD4_GTA2;
                        break;
                    case 2:
                        autos[i] = Properties.Resources._62px_Arachnid_GTA2;
                        break;
                    case 3:
                        autos[i] = Properties.Resources._62px_Stinger_GTA2;
                        break;
                    case 4:
                        autos[i] = Properties.Resources._63px_A_Type_GTA2;
                        break;
                    case 5:
                        autos[i] = Properties.Resources._63px_Beamer_GTA2;
                        break;
                    case 6:
                        autos[i] = Properties.Resources._63px_FuroreGT_GTA2;
                        break;
                    case 7:
                        autos[i] = Properties.Resources._63px_MichelliRoadster_GTA2;
                        break;
                    case 8:
                        autos[i] = Properties.Resources.Dementia_GTA2;
                        break;
                    case 9:
                        autos[i] = Properties.Resources.Z_Type_GTA2;
                        break;
                }
            }
            #endregion
            vehicle1.Setup(new Vector(7, 13) / 2.0f, 5, autos[0]);
            vehicle1.SetLocation(new Vector(210, -7), 0);
            vehicle2.Setup(new Vector(7, 13) / 2.0f, 5, autos[1]);
            vehicle2.SetLocation(new Vector(190, -7), 0);
        }
        #endregion

        #region Frame
        //main rendering function
        private void Render(Graphics g)
        {
            //clear back buffer
            graphics.Clear(Color.Black);
            graphics.DrawImage(m_map, -171, -128);
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
            vehicle1.Draw(graphics, buffersize);
            vehicle2.Draw(graphics, buffersize);
            Thread.Sleep(1);
        }

        //process game logic
        private void DoFrame()
        {
            //get elapsed time since last frame
            float etime = timer.GetETime();
            //process input
            //ProcessInput();
            //ProcessInput2();

            y1 = vehicle2.GetPosition();
            x1 = new Rectangle((int)y1.X - (12 / 2), (int)y1.Y - (12 / 2), 13, 13);
            y2 = vehicle1.GetPosition();
            x2 = new Rectangle((int)y2.X - (12 / 2), (int)y2.Y - (12 / 2), 13, 13);

            //CheckCollision();
            //CheckCollision2();

            //apply vehicle controls
            vehicle1.SetSteering(steering);
            vehicle1.SetThrottle(throttle); //menu.Checked
            vehicle1.SetBrakes(brakes);
            vehicle2.SetSteering(steering2);
            vehicle2.SetThrottle(throttle2); //menu.Checked
            vehicle2.SetBrakes(brakes2);

            //integrate vehicle physics
            vehicle1.Update(etime);
            vehicle2.Update(etime);

            vehicle1.UpdateFuel(etime, throttle, out f1);
            vehicle2.UpdateFuel(etime, throttle2, out f2);

            //keep the vehicle on the screen
            //ConstrainVehicle();
            //ConstrainVehicle2();

            CheckFps();

            //redraw our screenconstra
            screen.Invalidate();
            int P = Convert.ToInt32(f1);
            int Q = Convert.ToInt32(f2);
            if (P < 0) P = 0;
            if (Q < 0) Q = 0;
            this.Text = String.Format("{0}FPS {1}", fps, veh1);
            
            progressBar1.Value = Convert.ToInt32(P);
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Minimum = 0;

            progressBar2.Value = Convert.ToInt32(Q);
            progressBar2.Maximum = 100;
            progressBar2.Step = 1;
            progressBar2.Minimum = 0;
            
        }

        private void ConstrainVehicles()
        {
            while(running)
            {
                ConstrainVehicle();
                ConstrainVehicle2();
                Thread.Sleep(100);
            }
        }

        //keep the vehicle on the screen
        private void ConstrainVehicle()
        {
            Vector position = vehicle1.GetPosition();
            Vector screenSize = new Vector(screen.Width / screenScale, screen.Height / screenScale);

            while (position.X > screenSize.X / 2.0f) { position.X -= screenSize.X; }
            while (position.Y > screenSize.Y / 2.0f) { position.Y -= screenSize.Y; }
            while (position.X < -screenSize.X / 2.0f) { position.X += screenSize.X; }
            while (position.Y < -screenSize.Y / 2.0f) { position.Y += screenSize.Y; }
        }
        private void ConstrainVehicle2()
        {
            Vector position = vehicle2.GetPosition();
            Vector screenSize = new Vector(screen.Width / screenScale, screen.Height / screenScale);

            while (position.X > screenSize.X / 2.0f) { position.X -= screenSize.X; }
            while (position.Y > screenSize.Y / 2.0f) { position.Y -= screenSize.Y; }
            while (position.X < -screenSize.X / 2.0f) { position.X += screenSize.X; }
            while (position.Y < -screenSize.Y / 2.0f) { position.Y += screenSize.Y; }
        }
        #endregion

        #region Input
        private void ProcessInput2()
        {
            while(running)
            {
                if (Leftheld)
                    steering = -1;
                else if (Rightheld)
                    steering = 1;
                else
                    steering = 0;

                if (Upheld)
                    throttle = 1;// * i;
                else
                    throttle = 0;

                if (Downheld)
                    throttle = -0.35f;

                if (ShiftHeld)
                    brakes = 12;
                else
                    brakes = 0.4f;
                Thread.Sleep(5);
            }
        }

        private void onKeyDown2(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Leftheld = true;
                    break;
                case Keys.Right:
                    Rightheld = true;
                    break;
                case Keys.Up:
                    Upheld = true;
                    break;
                case Keys.Down:
                    Downheld = true;
                    break;
                case Keys.Shift:
                    ShiftHeld = true;
                    break;
                default: //no match found
                    return; //return so handled dosnt get set
            }
            //match found
            e.Handled = true;
        }

        private void onKeyUp2(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    Leftheld = false;
                    break;
                case Keys.Right:
                    Rightheld = false;
                    break;
                case Keys.Up:
                    Upheld = false;
                    break;
                case Keys.Down:
                    Downheld = false;
                    break;
                default: //no match found
                    return; //return so handled dosnt get set
            }

            //match found
            e.Handled = true;
        }


        //process keyboard input
        private void ProcessInput()
        {
            while(running)
            {
                if (AHeld)
                    steering2 = -1;
                else if (DHeld)
                    steering2 = 1;
                else
                    steering2 = 0;

                if (WHeld)
                    throttle2 = 1;// * i;
                else
                    throttle2 = 0;

                if (SHeld)
                    throttle2 = -0.35f;

                if (EHeld)
                    brakes2 = 12;
                else
                    brakes2 = 0.4f;
                Thread.Sleep(5);
            }
        }

        private void onKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    AHeld = true;
                    break;
                case Keys.D:
                    DHeld = true;
                    break;
                case Keys.W:
                    WHeld = true;
                    break;
                case Keys.S:
                    SHeld = true;
                    break;
                case Keys.E:
                    EHeld = true;
                    break;
                default: //no match found
                    return; //return so handled dosnt get set
            }

            //match found
            e.Handled = true;
        }

        private void onKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    AHeld = false;
                    break;
                case Keys.D:
                    DHeld = false;
                    break;
                case Keys.W:
                    WHeld = false;
                    break;
                case Keys.S:
                    SHeld = false;
                    break;
                case Keys.E:
                    EHeld = false;
                    break;
                default: //no match found
                    return; //return so handled dosnt get set
            }

            //match found
            e.Handled = true;
        }
        #endregion

        #region Render
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

        void Form1_CreateBackBuffer(object sender, EventArgs e)
        {
            if (Backbuffer != null)
                Backbuffer.Dispose();

            Backbuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
        }
        private void CheckFps()
        {
            if (gameTime.ElapsedMilliseconds > 1000)
            {
                fps = fpsCounter;
                fpsCounter = 0;
                gameTime.Reset();
                gameTime.Start();
            }
            else
            {
                fpsCounter++;
            }
        }
        #endregion
        
        #region Prutsterdepruts
        public static bool LineIntersectsRect(Point p1, Point p2, Rectangle r)
        {
            return LineIntersectsLine(p1, p2, new Point(r.X, r.Y), new Point(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y), new Point(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y + r.Height), new Point(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X, r.Y + r.Height), new Point(r.X, r.Y)) ||
                   (r.Contains(p1) && r.Contains(p2));
        }

        private static bool LineIntersectsLine(Point l1p1, Point l1p2, Point l2p1, Point l2p2)
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }

        private void Collision()
        {
            while (running)
            {
                CheckCollision1();
                CheckCollision2();
                Thread.Sleep(10);
            }
        }

        private void CheckCollision1()
        {
            /*
            for (int i = 0; i < outerPerimeter.Length - 1; i++)
            {
                if (LineIntersectsRect(outerPerimeter[i], outerPerimeter[i + 1], x2))
                {
                    brakes = 4;
                }
            }
            */
            for (int i = 0; i < outerPerimeter2.Length - 1; i++)
            {
                if (LineIntersectsRect(outerPerimeter2[i], outerPerimeter2[i + 1], x2))
                {
                    brakes = 4;
                }
            }
            for (int i = 0; i < innerPerimeterUpper.Length - 1; i++)
            {
                if (LineIntersectsRect(innerPerimeterUpper[i], innerPerimeterUpper[i + 1], x2))
                {
                    brakes = 4;
                }
            }
            for (int i = 0; i < innerPerimeterLower.Length - 1; i++)
            {
                if (LineIntersectsRect(innerPerimeterLower[i], innerPerimeterLower[i + 1], x2))
                {
                    brakes = 4;
                }
            }
            for (int i = 0; i < outerWall.Length - 1; i++)
            {
                if (LineIntersectsRect(outerWall[i], outerWall[i + 1], x2))
                {
                    vehicle1.SetVelocity(-0.5f);
                }
            }
            if (x2.IntersectsWith(x1))
            {
                vehicle1.SetVelocity(-0.5f);
            }
            if (x2.IntersectsWith(garage))
            {
                vehicle1.SetVelocity(-0.5f);
            }
        }

        private void CheckCollision2()
        {
            /*
            for (int i = 0; i < outerPerimeter.Length - 1; i++)
            {
                if (LineIntersectsRect(outerPerimeter[i], outerPerimeter[i + 1], x2))
                {
                    brakes2 = 4;
                }
            }
            */
            for (int i = 0; i < outerPerimeter2.Length - 1; i++)
            {
                if (LineIntersectsRect(outerPerimeter2[i], outerPerimeter2[i + 1], x1))
                {
                    brakes2 = 4;
                }
            }
            for (int i = 0; i < innerPerimeterUpper.Length - 1; i++)
            {
                if (LineIntersectsRect(innerPerimeterUpper[i], innerPerimeterUpper[i + 1], x1))
                {
                    brakes2 = 4;
                }
            }
            for (int i = 0; i < innerPerimeterLower.Length - 1; i++)
            {
                if (LineIntersectsRect(innerPerimeterLower[i], innerPerimeterLower[i + 1], x1))
                {
                    brakes2 = 4;
                }
            }
            for (int i = 0; i < outerWall.Length - 1; i++)
            {
                if (LineIntersectsRect(outerWall[i], outerWall[i + 1], x1))
                {
                    vehicle2.SetVelocity(-0.5f);
                }
            }
            if (x2.IntersectsWith(x1))
            {
                vehicle2.SetVelocity(-0.5f);
            }
            if (x1.IntersectsWith(garage))
            {
                vehicle2.SetVelocity(-0.5f);
            }
        }

        #endregion

        private void Timers1()
        {

            TimeSpan ts1;
            TimeSpan ts2;
            TimeSpan ts3;
            ts1 = raceTime.Elapsed;
            ts2 = raceTime.Elapsed;
            ts3 = raceTime.Elapsed;
            while (!veh1r11)
            {
                ts1 = raceTime.Elapsed;
                veh1 = String.Format("{0:00}:{1:00}.{2:00}",
            ts1.Minutes, ts1.Seconds,
            ts1.Milliseconds / 10);
                Thread.Sleep(10);
            }
            while (!veh1r21)
            {
                ts2 = raceTime.Elapsed;
                veh1 = String.Format("{0:00}:{1:00}.{2:00}",
            ts2.Minutes - ts1.Minutes, ts2.Seconds - ts1.Seconds,
            (ts2.Milliseconds / 10) - (ts1.Milliseconds / 10));
                Thread.Sleep(10);
            }
            while (!veh1r31)
            {
                ts3 = raceTime.Elapsed;
                veh1 = String.Format("{0:00}:{1:00}.{2:00}",
            ts3.Minutes - ts2.Minutes, ts3.Seconds - ts2.Seconds,
            (ts3.Milliseconds / 10) - (ts2.Milliseconds / 10));
                Thread.Sleep(10);
            }
        }

        private void Timers2()
        {
            TimeSpan ts1;
            TimeSpan ts2;
            TimeSpan ts3;
            ts1 = raceTime.Elapsed;
            ts2 = raceTime.Elapsed;
            ts3 = raceTime.Elapsed;
            while (!veh2r11)
            {
                ts1 = raceTime.Elapsed;
                veh2 = String.Format("{0:00}:{1:00}.{2:00}",
            ts1.Minutes, ts1.Seconds,
            ts1.Milliseconds / 10);
                Thread.Sleep(10);
            }
            while (!veh2r21)
            {
                ts2 = raceTime.Elapsed;
                veh2 = String.Format("{0:00}:{1:00}.{2:00}",
            ts2.Minutes - ts1.Minutes, ts2.Seconds - ts1.Seconds,
            (ts2.Milliseconds / 10) - (ts1.Milliseconds  / 10));
                Thread.Sleep(10);
            }
            while (!veh2r31)
            {
                ts3 = raceTime.Elapsed;
                veh2 = String.Format("{0:00}:{1:00}.{2:00}",
            ts3.Minutes - ts2.Minutes, ts3.Seconds - ts2.Minutes,
            (ts3.Milliseconds / 10) - (ts2.Milliseconds / 10));
                Thread.Sleep(10);
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            running = false;
        }


        //our vehicle object
        class Vehicle1 : RigidBody1
        {
            private class Wheel
            {
                #region Setup
                private Vector m_forwardAxis, m_sideAxis;
                private float m_wheelTorque, m_wheelSpeed, m_wheelInertia, m_wheelRadius;
                private Vector m_Position = new Vector();

                public Wheel(Vector position, float radius)
                {
                    m_Position = position;
                    SetSteeringAngle(0);
                    m_wheelSpeed = 0;
                    m_wheelRadius = radius;
                    m_wheelInertia = radius * radius; //fake value
                }
                #endregion

                #region Update
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
                    Vector responseForce = -sideVel * 2.0f;
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
                #endregion
            }
            #region Setup
            private Wheel[] wheels = new Wheel[4];

            new public void Setup(Vector halfSize, float mass, Bitmap color)
            {
                //front wheels
                wheels[0] = new Wheel(new Vector(halfSize.X, halfSize.Y), 0.5f);
                wheels[1] = new Wheel(new Vector(-halfSize.X, halfSize.Y), 0.5f);

                //rear wheels
                wheels[2] = new Wheel(new Vector(halfSize.X, -halfSize.Y), 0.5f);
                wheels[3] = new Wheel(new Vector(-halfSize.X, -halfSize.Y), 0.5f);

                base.Setup(halfSize, mass, color);
            }
            #endregion

            #region Update
            public void SetSteering(float steering)
            {
                const float steeringLock = 0.4f;

                //apply steering angle to front wheels
                wheels[0].SetSteeringAngle(-steering * steeringLock);
                wheels[1].SetSteeringAngle(-steering * steeringLock);
            }

            public void SetThrottle(float throttle)
            {
                const float torque = 60.0f;

                //apply transmission torque to back wheels
                wheels[0].AddTransmissionTorque(throttle * torque / 2);
                wheels[1].AddTransmissionTorque(throttle * torque / 2);
                wheels[2].AddTransmissionTorque(throttle * torque);
                wheels[3].AddTransmissionTorque(throttle * torque);
            }

            public void SetBrakes(float brakes)
            {
                const float brakeTorque = 0.7f;


                //apply brake torque apposing wheel vel
                foreach (Wheel wheel in wheels)
                {
                    float wheelVel = wheel.GetWheelSpeed();
                    wheel.AddTransmissionTorque(-wheelVel * brakeTorque * brakes);
                }
            }

            new public void Update(float timeStep)
            {
                foreach (Wheel wheel in wheels)
                {
                    //wheel.m_wheelSpeed = 30.0f;
                    Vector worldWheelOffset = base.RelativeToWorld(wheel.GetAttachPoint());
                    Vector worldGroundVel = base.PointVel(worldWheelOffset);
                    Vector relativeGroundSpeed = base.WorldToRelative(worldGroundVel);
                    Vector relativeResponseForce = wheel.CalculateForce(relativeGroundSpeed, timeStep);
                    Vector worldResponseForce = base.RelativeToWorld(relativeResponseForce);

                    base.AddForce(worldResponseForce, worldWheelOffset);
                }

                base.Update(timeStep);
            }
            #endregion
        }

        //our simulation object
        class RigidBody1
        {
            #region Setup
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
            Rectangle rect = new Rectangle();
            //private Color m_color;
            private Bitmap m_bitmap;// = new Bitmap(Properties.Resources.Z_Type_GTA2);
            private double m_fuel; 

            public RigidBody1()
            {
                //set these defaults so we dont get divide by zeros
                m_mass = 1.0f;
                m_inertia = 1.0f;
                m_fuel = 100;
            }

            //intialize out parameters
            public void Setup(Vector halfSize, float mass, Bitmap color)
            {
                //store physical parameters
                m_halfSize = halfSize;
                m_mass = mass;
                m_bitmap = color;
                m_inertia = (1.0f / 12.0f) * (halfSize.X * halfSize.X) * (halfSize.Y * halfSize.Y) * mass;

                //generate our viewable rectangle
                rect.X = (int)-m_halfSize.X;
                rect.Y = (int)-m_halfSize.Y;
                rect.Width = (int)(m_halfSize.X * 2.0f);
                rect.Height = (int)(m_halfSize.Y * 2.0f);
            }
            #endregion

            public Rectangle GetRect()
            {
                return rect;
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

            public void SetVelocity(float i)
            {
                m_velocity *= i;
            }

            public void UpdateFuel(float timestep, float throttle, out double fuel)
            {
                m_fuel -= 2 * timestep * Math.Abs(throttle);
                fuel = m_fuel;
            }

            #region Update
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

            public void Draw(Graphics graphics, Size buffersize)
            {
                //store transform, (like opengl's glPushMatrix())
                Matrix mat1 = graphics.Transform;

                //transform into position
                graphics.TranslateTransform(m_position.X, m_position.Y);
                graphics.RotateTransform(m_angle / (float)Math.PI * 180.0f);

                try
                {
                    //draw body
                    //graphics.DrawRectangle(new Pen(m_color), rect);
                    graphics.DrawImage(m_bitmap, rect);
                    //draw line in the "forward direction"
                    //graphics.DrawLine(new Pen(Color.Yellow), 1, 0, 1, 5);
                }
                catch (OverflowException exc)
                {
                    //physics overflow :(
                }

                //restore transform
                graphics.Transform = mat1;
            }
            #endregion

            #region Forces
            //take a relative vector and make it a world vector
            public Vector RelativeToWorld(Vector relative)
            {
                Matrix mat = new Matrix();
                PointF[] vectors = new PointF[1];

                vectors[0].X = relative.X;
                vectors[0].Y = relative.Y;

                mat.Rotate(m_angle / (float)Math.PI * 180.0f);
                mat.TransformVectors(vectors);

                return new Vector(vectors[0].X, vectors[0].Y);
            }

            //take a world vector and make it a relative vector
            public Vector WorldToRelative(Vector world)
            {
                Matrix mat = new Matrix();
                PointF[] vectors = new PointF[1];

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
            #endregion
        }



        class Vehicle2 : RigidBody2
        {
            private class Wheel
            {
                #region Setup
                private Vector m_forwardAxis, m_sideAxis;
                private float m_wheelTorque, m_wheelSpeed, m_wheelInertia, m_wheelRadius;
                private Vector m_Position = new Vector();

                public Wheel(Vector position, float radius)
                {
                    m_Position = position;
                    SetSteeringAngle(0);
                    m_wheelSpeed = 0;
                    m_wheelRadius = radius;
                    m_wheelInertia = radius * radius; //fake value
                }
                #endregion

                #region Update
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
                    Vector responseForce = -sideVel * 2.0f;
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
                #endregion
            }
            #region Update
            private Wheel[] wheels = new Wheel[4];

            new public void Setup(Vector halfSize, float mass, Bitmap color)
            {
                //front wheels
                wheels[0] = new Wheel(new Vector(halfSize.X, halfSize.Y), 0.5f);
                wheels[1] = new Wheel(new Vector(-halfSize.X, halfSize.Y), 0.5f);

                //rear wheels
                wheels[2] = new Wheel(new Vector(halfSize.X, -halfSize.Y), 0.5f);
                wheels[3] = new Wheel(new Vector(-halfSize.X, -halfSize.Y), 0.5f);

                base.Setup(halfSize, mass, color);
            }
            #endregion

            #region Update
            public void SetSteering(float steering)
            {
                const float steeringLock = 0.4f;

                //apply steering angle to front wheels
                wheels[0].SetSteeringAngle(-steering * steeringLock);
                wheels[1].SetSteeringAngle(-steering * steeringLock);
            }

            public void SetThrottle(float throttle)
            {
                const float torque = 60.0f;

                //apply transmission torque to back wheels
                wheels[0].AddTransmissionTorque(throttle * torque / 2);
                wheels[1].AddTransmissionTorque(throttle * torque / 2);
                wheels[2].AddTransmissionTorque(throttle * torque);
                wheels[3].AddTransmissionTorque(throttle * torque);
            }

            public void SetBrakes(float brakes)
            {
                const float brakeTorque = 0.7f;


                //apply brake torque apposing wheel vel
                foreach (Wheel wheel in wheels)
                {
                    float wheelVel = wheel.GetWheelSpeed();
                    wheel.AddTransmissionTorque(-wheelVel * brakeTorque * brakes);
                }
            }

            new public void Update(float timeStep)
            {
                foreach (Wheel wheel in wheels)
                {
                    //wheel.m_wheelSpeed = 30.0f;
                    Vector worldWheelOffset = base.RelativeToWorld(wheel.GetAttachPoint());
                    Vector worldGroundVel = base.PointVel(worldWheelOffset);
                    Vector relativeGroundSpeed = base.WorldToRelative(worldGroundVel);
                    Vector relativeResponseForce = wheel.CalculateForce(relativeGroundSpeed, timeStep);
                    Vector worldResponseForce = base.RelativeToWorld(relativeResponseForce);

                    base.AddForce(worldResponseForce, worldWheelOffset);
                }

                base.Update(timeStep);
            }
            #endregion
        }

        //our simulation object
        class RigidBody2
        {
            #region Setup
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
            private double m_fuel;

            //graphical properties
            private Vector m_halfSize = new Vector();
            Rectangle rect = new Rectangle();
            //private Color m_color;
            private Bitmap m_bitmap;// = new Bitmap(Properties.Resources.Z_Type_GTA2);

            public RigidBody2()
            {
                //set these defaults so we dont get divide by zeros
                m_mass = 1.0f;
                m_inertia = 1.0f;
                m_fuel = 100;
            }

            //intialize out parameters
            public void Setup(Vector halfSize, float mass, Bitmap color)
            {
                //store physical parameters
                m_halfSize = halfSize;
                m_mass = mass;
                m_bitmap = color;
                m_inertia = (1.0f / 12.0f) * (halfSize.X * halfSize.X) * (halfSize.Y * halfSize.Y) * mass;

                //generate our viewable rectangle
                rect.X = (int)-m_halfSize.X;
                rect.Y = (int)-m_halfSize.Y;
                rect.Width = (int)(m_halfSize.X * 2.0f);
                rect.Height = (int)(m_halfSize.Y * 2.0f);
            }
            #endregion

            public Rectangle GetRect()
            {
                return rect;
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

            public void SetVelocity(float i)
            {
                m_velocity *= i;
            }

            public void UpdateFuel(float timestep, float throttle, out double fuel)
            {
                m_fuel -= 2 * timestep * Math.Abs(throttle);
                fuel = m_fuel;
            }

            #region Update
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

            public void Draw(Graphics graphics, Size buffersize)
            {
                //store transform, (like opengl's glPushMatrix())
                Matrix mat1 = graphics.Transform;

                //transform into position
                graphics.TranslateTransform(m_position.X, m_position.Y);
                graphics.RotateTransform(m_angle / (float)Math.PI * 180.0f);

                try
                {
                    //draw body
                    //graphics.DrawRectangle(new Pen(m_color), rect);
                    graphics.DrawImage(m_bitmap, rect);
                    //draw line in the "forward direction"
                    //graphics.DrawLine(new Pen(Color.Yellow), 1, 0, 1, 5);
                }
                catch (OverflowException exc)
                {
                    //physics overflow :(
                }

                //restore transform
                graphics.Transform = mat1;
            }
            #endregion

            #region Forces
            //take a relative vector and make it a world vector
            public Vector RelativeToWorld(Vector relative)
            {
                Matrix mat = new Matrix();
                PointF[] vectors = new PointF[1];

                vectors[0].X = relative.X;
                vectors[0].Y = relative.Y;

                mat.Rotate(m_angle / (float)Math.PI * 180.0f);
                mat.TransformVectors(vectors);

                return new Vector(vectors[0].X, vectors[0].Y);
            }

            //take a world vector and make it a relative vector
            public Vector WorldToRelative(Vector world)
            {
                Matrix mat = new Matrix();
                PointF[] vectors = new PointF[1];

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
            #endregion
        }
        #region Vector and Timer
        //mini 2d vector :)
        class Vector
        {
            public float X, Y;

            public Vector() { X = 0; Y = 0; }
            public Vector(float x, float y) { X = x; Y = y; }

            //length property        
            public float Length
            {
                get
                {
                    return (float)Math.Sqrt((double)(X * X + Y * Y));
                }
            }

            //addition
            public static Vector operator +(Vector L, Vector R)
            {
                return new Vector(L.X + R.X, L.Y + R.Y);
            }

            //subtraction
            public static Vector operator -(Vector L, Vector R)
            {
                return new Vector(L.X - R.X, L.Y - R.Y);
            }

            //negative
            public static Vector operator -(Vector R)
            {
                Vector temp = new Vector(-R.X, -R.Y);
                return temp;
            }

            //scalar multiply
            public static Vector operator *(Vector L, float R)
            {
                return new Vector(L.X * R, L.Y * R);
            }

            //divide multiply
            public static Vector operator /(Vector L, float R)
            {
                return new Vector(L.X / R, L.Y / R);
            }

            //dot product
            public static float operator *(Vector L, Vector R)
            {
                return (L.X * R.X + L.Y * R.Y);
            }

            //cross product, in 2d this is a scalar since we know it points in the Z direction
            public static float operator %(Vector L, Vector R)
            {
                return (L.X * R.Y - L.Y * R.X);
            }

            //normalize the vector
            public void normalize()
            {
                float mag = Length;

                X /= mag;
                Y /= mag;
            }

            //project this vector on to v
            public Vector Project(Vector v)
            {
                //projected vector = (this dot v) * v;
                float thisDotV = this * v;
                return v * thisDotV;
            }

            //project this vector on to v, return signed magnatude
            public Vector Project(Vector v, out float mag)
            {
                //projected vector = (this dot v) * v;
                float thisDotV = this * v;
                mag = thisDotV;
                return v * thisDotV;
            }
        }

        //keep track of time between frames
        class Timer
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
        #endregion

        
    }
}