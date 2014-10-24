using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Try_2nd
{
    class Sprite
    {
        public float X, Y;
        public int Width, Height;
        public Bitmap Texture;
        public float Friction;
        public PointF Velocity;
        public const float Gragh = 9.81f;
        public SpriteType type;
        public bool canCollide;

        public enum SpriteType { Road, Grass, Wall, CheckPoint, StartLine, PitStop };

        public Sprite(Bitmap texture, double x, double y, double width, double height, SpriteType thisType)
        {
            Bitmap b = new Bitmap((int)width,(int)height);
            using (Graphics g = Graphics.FromImage(b))
            {
                g.DrawImage(texture, 0, 0, (int)width, (int)height);
            }
            X = (int)x;
            Y = (int)y;
            Width = (int)width;
            Height = (int)height;
            Texture = b;
            type = thisType;

            switch(thisType)
            {
                case SpriteType.Road:
                    canCollide = true;
                    break;
                case SpriteType.Grass:
                    canCollide = true;
                    break;
                case SpriteType.Wall:
                    canCollide = true;
                    break;
                case SpriteType.CheckPoint:
                    canCollide = true;
                    break;
                case SpriteType.StartLine:
                    canCollide = true;
                    break;
                case SpriteType.PitStop:
                    canCollide = true;
                    break;
            }
        }

        public void Update(InputManager iManager)
        {
            Velocity.Y += Gragh;
            this.X += Velocity.X * iManager.deltaTime;
            this.Y += Velocity.Y * iManager.deltaTime;

            Collider(iManager);
        }

        private void Collider(InputManager iManager)
        {
            foreach(Sprite s in iManager.inGameSprites)
            {
                if(this.isCollidingWith(s))
                {
                    switch(this.type)
                    {
                        case SpriteType.Road:
                            break;
                        case SpriteType.Grass:
                            break;
                        case SpriteType.Wall:
                            break;
                        case SpriteType.CheckPoint:
                            break;
                        case SpriteType.StartLine:
                            break;
                        case SpriteType.PitStop:
                            break;
                    }
                }
            }
        }

        public Rectangle ToRec
        {
            get { return new Rectangle((int)X, (int)Y, Width, Height); }
        }

        public Rectangle Top
        {
            get { return new Rectangle((int)X, (int)Y, Width, Height / 4); }
        }

        public Rectangle Bottom
        {
            get { return new Rectangle((int)X, (int)Y + this.Height / 2 + this.Height / 4, Width, Height); }
        }

        public Rectangle Left
        {
            get { return new Rectangle((int)X, (int)Y, Width / 4, Height); }
        }

        public Rectangle Right
        {
            get { return new Rectangle((int)X + Width / 2 + Width / 4, (int)Y, Width / 4, Height); }
        }
        public void Draw(Spritebatch sb)
        {
            sb.Draw(this);
        }
    }

    static class SpriteHelper
    {
        public static bool isCollidingWith(this Sprite s1, Sprite s2)
        {
            if(s1.ToRec.IntersectsWith(s2.ToRec))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isTouchingLeft(Sprite s1, Sprite s2)
        {
            if (s1.Right.IntersectsWith(s2.Left))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isTouchingRight(Sprite s1, Sprite s2)
        {
            if (s1.Left.IntersectsWith(s2.Right))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isTouchingTop(Sprite s1, Sprite s2)
        {
            if (s1.Bottom.IntersectsWith(s2.Top))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool isTouchingBottom(Sprite s1, Sprite s2)
        {
            if (s1.Top.IntersectsWith(s2.Bottom))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool isOnStage(Sprite s1, Rectangle clientRect)
        {
            if (s1.ToRec.IntersectsWith(clientRect))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
