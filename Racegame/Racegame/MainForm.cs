using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Input;

namespace WindowsFormsApplication1
{

    public partial class MainForm : Form
    {
        Polygon polygon = new Polygon();
        Collision collision = new Collision();
        //Vector vector = new Vector();
        Vehicle vehicle = new Vehicle();
        Vehicle2 vehicle2 = new Vehicle2();
        //RigidBody rigidBody = new RigidBody();
        Bitmap m_map = new Bitmap(Properties.Resources.design_1, 341, 256);

        //graphics
        Graphics graphics; //gdi+
        Bitmap backbuffer;
        Size buffersize;
        const float screenScale = 3.0f;
        Timer timer = new Timer();

        private int fps;
        private int fpsCounter;
        private long fpsTime;
        private Stopwatch gameTime = new Stopwatch();

        bool loaded = false;

        //keyboard controls
        bool AHeld = false, DHeld = false;
        bool WHeld = false, SHeld = false;
        bool Upheld = false, Downheld = false;
        bool Rightheld = false, Leftheld = false;

        //vehicle controls
        float steering = 0; //-1 is full left, 0 is center, 1 is full right
        float throttle = 0; //0 is coasting, 1 is full throttle
        float brakes = 0; //0 is no brakes, 1 is full brakes

        float steering2 = 0; //-1 is full left, 0 is center, 1 is full right
        float throttle2 = 0; //0 is coasting, 1 is full throttle
        float brakes2 = 0; //0 is no brakes, 1 is full brakes

        Bitmap Backbuffer;

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

            this.SetStyle(
            ControlStyles.UserPaint |
            ControlStyles.AllPaintingInWmPaint |
            ControlStyles.DoubleBuffer, true);

            System.Timers.Timer GameTimer = new System.Timers.Timer();
            GameTimer.Interval = 1;
            GameTimer.Elapsed += new ElapsedEventHandler(GameTimer_Tick);
            GameTimer.Enabled = true;

            this.ResizeEnd += new EventHandler(Form1_CreateBackBuffer);
            this.Load += new EventHandler(Form1_CreateBackBuffer);
            //this.Paint += new PaintEventHandler(Form1_Paint);
        }

        //intialize rendering
        private void Init(Size size)
        {

            //setup rendering device
            buffersize = size;
            backbuffer = new Bitmap(m_map, -171, -128);
            graphics = Graphics.FromImage(backbuffer);
            gameTime.Start();
            timer.GetETime(); //reset timer
            
            Bitmap auto = new Bitmap(Properties.Resources.Z_Type_GTA2);
            Bitmap SMiley = new Bitmap(Properties.Resources.Dementia_GTA2);
            vehicle.Setup(new Vector(7, 13) / 2.0f, 5, auto);
            vehicle.SetLocation(new Vector(210, -7), 0);
            vehicle2.Setup(new Vector(7, 13) / 2.0f, 5, SMiley);
            vehicle2.SetLocation(new Vector(190, -7), 0);
        }
        static int LoadTexture(string filename)
        {
            if (String.IsNullOrEmpty(filename))
                throw new ArgumentException(filename);

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            Bitmap bmp = new Bitmap(filename);
            BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);

            bmp.UnlockBits(bmp_data);

            // We haven't uploaded mipmaps, so disable mipmapping (otherwise the texture will not appear).
            // On newer video cards, we can use GL.GenerateMipmaps() or GL.Ext.GenerateMipmaps() to create
            // mipmaps automatically. In that case, use TextureMinFilter.LinearMipmapLinear to enable them.
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return id;
        }

        private void screen_Load(object sender, EventArgs e)
        {
            loaded = true;
            //LoadTexture("Design 1.png");
            GL.Clear(ClearBufferMask.ColorBufferBit);
            SetupViewport();
        }

        private void SetupViewport()
        {
            int w = screen.Width;
            int h = screen.Height;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, w, 0, h, -1, 1);
            GL.Viewport(0, 0, w, h);
        }

        //main rendering function
        private void Render(Graphics g)
        {
            if (!loaded) return;
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.ColorBufferBit);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            if (screen.Focused) GL.Color3(Color.Yellow);
            else GL.Color3(Color.Blue);
            GL.Begin(PrimitiveType.Quads);
            //GL.Vertex2(10, 20);
            //GL.Vertex2(100, 20);
            //GL.Vertex2(100, 50);
            GL.End();

            screen.SwapBuffers();
            
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
            vehicle.Draw(graphics, buffersize);
            vehicle2.Draw(graphics, buffersize);
        }

        //process game logic
        private void DoFrame()
        {
            //get elapsed time since last frame
            float etime = timer.GetETime();

            //process input
            ProcessInput();
            ProcessInput2();

            //apply vehicle controls
            vehicle.SetSteering(steering);
            vehicle.SetThrottle(throttle, true); //menu.Checked
            vehicle.SetBrakes(brakes);
            vehicle2.SetSteering(steering2);
            vehicle2.SetThrottle(throttle2, true); //menu.Checked
            vehicle2.SetBrakes(brakes2);

            //integrate vehicle physics
            vehicle.Update(etime);
            vehicle2.Update(etime);

            //keep the vehicle on the screen
            ConstrainVehicle();
            ConstrainVehicle2();

            CheckFps();

            //redraw our screenconstra
            screen.Invalidate();

            this.Text = String.Format("{0}FPS", fps);
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
        private void ConstrainVehicle2()
        {
            Vector position = vehicle2.GetPosition();
            Vector screenSize = new Vector(screen.Width / screenScale, screen.Height / screenScale);

            while (position.X > screenSize.X / 2.0f) { position.X -= screenSize.X; }
            while (position.Y > screenSize.Y / 2.0f) { position.Y -= screenSize.Y; }
            while (position.X < -screenSize.X / 2.0f) { position.X += screenSize.X; }
            while (position.Y < -screenSize.Y / 2.0f) { position.Y += screenSize.Y; }
        }
        private void ProcessInput2()
        {
            if (Leftheld)
                steering = -1;
            else if (Rightheld)
                steering = 1;
            else
                steering = 0;

            if (Upheld)
                throttle = 1;
            else
                throttle = 0;

            if (Downheld)
                brakes = 12;
            else
                brakes = 0.4f;
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
            if (AHeld)
                steering2 = -1;
            else if (DHeld)
                steering2 = 1;
            else
                steering2 = 0;

            if (WHeld)
                throttle2 = 1;
            else
                throttle2 = 0;

            if (SHeld)
                brakes2 = 12;
            else
                brakes2 = 0.4f;
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
            //CheckFps();
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

        private void CheckFps()
        {
            if (gameTime.ElapsedMilliseconds - fpsTime > 1000)
            {
                fpsTime = gameTime.ElapsedMilliseconds;
                fps = fpsCounter;
                fpsCounter = 0;
            }
            else
            {
                fpsCounter++;
            }
        }
        void GameTimer_Tick(object sender, EventArgs e)
        {

        }

        //our vehicle object
        class Vehicle : RigidBody
        {
            private class Wheel
            {
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
            }
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

            public void SetSteering(float steering)
            {
                const float steeringLock = 0.4f;

                //apply steering angle to front wheels
                wheels[0].SetSteeringAngle(-steering * steeringLock);
                wheels[1].SetSteeringAngle(-steering * steeringLock);
            }

            public void SetThrottle(float throttle, bool allWheel)
            {
                const float torque = 60.0f;

                //apply transmission torque to back wheels
                if (allWheel)
                {
                    wheels[0].AddTransmissionTorque(throttle * torque / 2);
                    wheels[1].AddTransmissionTorque(throttle * torque / 2);
                }

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
        }

        //our simulation object
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
            Rectangle rect = new Rectangle();
            //private Color m_color;
            private Bitmap m_bitmap = new Bitmap(Properties.Resources.Z_Type_GTA2);

            public RigidBody()
            {
                //set these defaults so we dont get divide by zeros
                m_mass = 1.0f;
                m_inertia = 1.0f;
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
        }

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

        class Vehicle2 : RigidBody2
        {
            private class Wheel
            {
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
            }
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

            public void SetSteering(float steering)
            {
                const float steeringLock = 0.4f;

                //apply steering angle to front wheels
                wheels[0].SetSteeringAngle(-steering * steeringLock);
                wheels[1].SetSteeringAngle(-steering * steeringLock);
            }

            public void SetThrottle(float throttle, bool allWheel)
            {
                const float torque = 60.0f;

                //apply transmission torque to back wheels
                if (allWheel)
                {
                    wheels[0].AddTransmissionTorque(throttle * torque / 2);
                    wheels[1].AddTransmissionTorque(throttle * torque / 2);
                }

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
        }

        //our simulation object
        class RigidBody2
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
            Rectangle rect = new Rectangle();
            //private Color m_color;
            private Bitmap m_bitmap; // = new Bitmap(Properties.Resources.Z_Type_GTA2);

            public RigidBody2()
            {
                //set these defaults so we dont get divide by zeros
                m_mass = 1.0f;
                m_inertia = 1.0f;
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

        
    }
}

