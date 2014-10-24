using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace game_try
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            paper = pictureBox1.CreateGraphics();
            pictureBox1.BackColor = Color.Black;
        }

        private int interval = 1000 / 63;
        private long upTime;
        private int upCounter;
        private int Ups;
        private int previousSecond;
        private bool allowInput;
        private int fps;
        private int fpsCounter;
        private long fpsTime;
        private List<Keys> keysPressed = new List<Keys>();
        private List<Keys> keysHeld = new List<Keys>();
        private InputManager iManager = new InputManager();
        //private ScreenManager sManager = new ScreenManager();
        private Stopwatch gameTime = new Stopwatch();
        private Spritebatch spriteBatch;
        private Point mousePoint;
        private float deltaTime;
        private long lasttime;
        private Map gameMap;
        private bool Clicked;
        Graphics paper;
        

        private void LoadContent()
        {
            gameMap = new Map(ClientRectangle.Height / 13);
            gameMap.setMap(iManager);
            spriteBatch = new Spritebatch(this.ClientSize, this.CreateGraphics());
            Thread game = new Thread(GameLoop);
            game.Start(); 
        }

        private void GameLoop()
        {
            gameTime.Start();
            while (this.Created)
            {
                CheckFps();
                deltaTime = gameTime.ElapsedMilliseconds - lasttime;
                lasttime = gameTime.ElapsedMilliseconds;
                Input();
                Logic();
                Render();
            }
        }

        private void Input()
        {
            allowInput = false;   
            this.Invoke(new MethodInvoker(delegate {
                mousePoint = this.PointToClient(Cursor.Position);
                this.Text = " Fps: " + fps.ToString() + " Ups: " + Ups.ToString();
            }));
            iManager.Update(mousePoint, keysPressed.ToArray(), keysHeld.ToArray(), gameTime, deltaTime, Clicked);
            Clicked = false;
            keysPressed.Clear();
            keysHeld.Clear();
            allowInput = true;
        }

        private void Logic()
        {
            //sManager.Update(iManager);
            if(gameTime.ElapsedMilliseconds - upTime > interval)
            {
                foreach(Sprite s in iManager.inGameSprites)
                {
                    s.Update(iManager);
                }
                if(gameTime.Elapsed.Seconds != previousSecond)
                {
                    previousSecond = gameTime.Elapsed.Seconds;
                    Ups = upCounter;
                    upCounter = 0;
                }
                upTime = gameTime.ElapsedMilliseconds;
                upCounter++;
            }
        }

        private void Render()
        {
            spriteBatch.Begin();
            foreach(Sprite s in iManager.inGameSprites)
            {
                s.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        private void CheckFps()
        {
            if(gameTime.ElapsedMilliseconds - fpsTime > 1000)
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

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            System.Environment.Exit(0);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            LoadContent();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Clicked = true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if(allowInput)
            keysHeld.Add(e.KeyCode);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (allowInput)
                keysPressed.Add((Keys)e.KeyChar.ToString().ToCharArray()[0]);
        }









        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
