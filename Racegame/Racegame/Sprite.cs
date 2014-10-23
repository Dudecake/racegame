using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    
    class Sprite
    {
        public float X, Y;
        public int Width, Height;
        public Bitmap Texture;
        private const float Gravity = 9.81f;
        private float Friction = 0.95f;
        public PointF Velocity;
        public SpriteType type;
        public bool hasGravity;
        public bool canCollide;
        public bool canMove;
        private float boingBoing;

        public enum SpriteType { Floor, MovingFloor, Object, Player, Enemy , View, Ball}

        public Sprite(Bitmap texture, float x, float y, int width, int height, SpriteType thisType)
        {
            Bitmap b = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawImage(texture, 0, 0, width, height);
            }
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Texture = b;
            Texture.MakeTransparent(Color.White);
            type = thisType;

            switch(thisType)
            {
                case SpriteType.Floor:
                    canCollide = true;
                    break;
                case SpriteType.MovingFloor:
                    canMove = true;
                    canCollide = true;
                    break;
                case SpriteType.Object:
                    canMove = true;
                    hasGravity = true;
                    canCollide = true;
                    break;
                case SpriteType.Player:
                    canMove = true;
                    hasGravity = true;
                    canCollide = true;
                    break;
                case SpriteType.Enemy:
                    canMove = true;
                    hasGravity = true;
                    canCollide = true;
                    break;
                case SpriteType.View:
                    canMove = true;
                    break;
                case SpriteType.Ball:
                    boingBoing = 0.85f;
                    break;
            }
        }
    }
}
