using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_try
{
    class ScreenManager
    {
        public enum GameState { Splash, Menu, Game }
        public GameState previousState;
        public GameState currentState = GameState.Menu;
        private List<MenuItem> MenuItems = new List<MenuItem>();

        public ScreenManager()
        {
            
        }

        public void LoadContent(InputManager iManager)
        {
            switch(currentState)
            {
                case GameState.Splash:
                    break;
                case GameState.Menu:
                    MenuItems.Add(new MenuItem("Start", 100, 100, 100, 100));
                    break;
                case GameState.Game:
                    break;
            }
        }

        public void Update(InputManager iManager)
        {
            switch (currentState)
            {
                case GameState.Splash:
                    break;
                case GameState.Menu:
                    foreach(MenuItem m in MenuItems)
                    {
                        m.Update(iManager);
                    }
                    for (int i = 0; i < MenuItems.Count; i++ )
                    {
                        if(MenuItems[i].Clicked == true)
                        {
                            switch(i)
                            {
                                case 0:
                                    currentState = GameState.Game;
                                    break;
                                case 2:
                                    break;
                            }
                        }
                    }
                        break;
                case GameState.Game:
                    break;
            }
        }

        public void Draw(Spritebatch sb)
        {
            switch(currentState)
            {
                case GameState.Splash:
                    break;
                case GameState.Menu:
                    foreach(MenuItem m in MenuItems)
                    {
                        m.Draw(sb);
                    }
                    break;
                case GameState.Game:
                    break;
            }
        }

    }

    class MenuItem
    {
        public bool Clicked;
        public bool Hovered;
        public Bitmap Texture;
        public int X, Y, Width, Height;
        
        public MenuItem(Bitmap b, int x, int y, int width, int height)
        {
            Texture = b;
            X = x; Y = y;
            Width = width;
            Height = height;
        }

        public MenuItem(string s, int x, int y, int width, int height)
        {
            Texture = new Bitmap(100, 100);
            using(Graphics g = Graphics.FromImage(Texture))
            {
                g.DrawString(s, new Font(new FontFamily("Arial"), 40, FontStyle.Italic, GraphicsUnit.Pixel), Brushes.Red,Point.Empty);
            }
            X = x; Y = y;
            Width = width;
            Height = height;
        }

        private Rectangle toRec
        {
            get { return new Rectangle(X, Y, Width, Height); }
        }

        public void Update(InputManager iManager)
        {
            Hovered = false;
            Clicked = false;
            if(this.toRec.Contains(iManager.mousePoint))
            {
                Hovered = true;
                if(iManager.Clicked)
                {
                    Clicked = true;
                }
            }
        }

        public void Draw(Spritebatch spriteBatch)
        {

            spriteBatch.drawImageClipped(Texture, toRec);
            if(Hovered)
            {
                spriteBatch.drawRectrangle(new Pen(Brushes.Yellow, 5), toRec);
            }
        }
    }
}
