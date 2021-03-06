﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Try_2nd
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //private bool allowInput;
        //private int fps;
        //private int fpsCounter;
        //private long fpsTime;
        //private int interval = 1000 / 63;
        //private long upTime;
        //private int upCounter;
        //private int Ups;
        //private int previousSecond;
        private List<Keys> keysPressed = new List<Keys>();
        private List<Keys> keysHeld = new List<Keys>();
        private InputManager iManager = new InputManager();
        private Stopwatch gameTime = new Stopwatch();
        private Spritebatch spriteBatch;
        //private Point mousePoint;
        private float deltaTime;
        private long lasttime;
        //private Sprite s;
        private Map gameMap;

        private void LoadContent()
        {
            gameMap = new Map(ClientRectangle.Height / 18);
            gameMap.setMap(iManager);
            //s = new Sprite(Properties.Resources.Dementia_GTA2, 50, 50, 50, 30);
            spriteBatch = new Spritebatch(this.ClientSize, this.CreateGraphics());
            Thread game = new Thread(GameLoop);
            game.Start();
        }

        private void GameLoop()
        {
            gameTime.Start();
            //gameMap.setMap(iManager);
            while (this.Created)
            {
                deltaTime = gameTime.ElapsedMilliseconds - lasttime;
                lasttime = gameTime.ElapsedMilliseconds;
                //Input();
                Render();
            }
        }

        //private void Input()
        //{
        //    allowInput = false;
        //    this.Invoke(new MethodInvoker(delegate
        //    {
        //        mousePoint = this.PointToClient(Cursor.Position);
        //        this.Text = fps.ToString();
        //    }));
        //    iManager.Update(mousePoint, keysPressed.ToArray(), keysHeld.ToArray(), gameTime, deltaTime);
        //    keysPressed.Clear();
        //    keysHeld.Clear();
        //    allowInput = true;
        //}

        private void Render()
        {
            spriteBatch.Begin();
            foreach(Sprite s in iManager.inGameSprites)
            {
                s.Draw(spriteBatch);
            }
            spriteBatch.End();
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

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    base.OnKeyDown(e);
        //    if (allowInput)
        //        keysHeld.Add(e.KeyCode);
        //}

        //protected override void OnKeyPress(KeyPressEventArgs e)
        //{
        //    base.OnKeyPress(e);
        //    if (allowInput)
        //        keysPressed.Add((Keys)e.KeyChar.ToString().ToCharArray()[0]);
        //}
    }
}
