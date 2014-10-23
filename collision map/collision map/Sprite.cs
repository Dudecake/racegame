using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace collision_map
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

        public enum SpriteType { Floor, Object, Player, View }

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

            switch (thisType)
            {
                case SpriteType.Floor:
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
                case SpriteType.View:
                    canMove = true;
                    break;
            }
        }

        public void Update(InputManager iManager)
        {
            if (hasGravity)
                Velocity.Y += Gravity;

            if (canMove)
            {
                this.X += Velocity.X * iManager.deltaTime;
                this.Y += Velocity.Y * iManager.deltaTime;
            }

            collider(iManager);
        }

        private void collider(InputManager iManager)
        {
            foreach (Sprite s in iManager.inGameSprites)
            {
                if (this.isCollidingWith(s))
                {
                    switch (this.type)
                    {
                        case SpriteType.Floor:
                            switch (s.type)
                            {
                                case SpriteType.Floor:
                                    if (s.isTouchingTop(this))
                                    {
                                        s.Y = this.Y - s.Height;
                                        s.Velocity.Y *= -1;
                                    }
                                    else if (s.isTouchingBottom(this))
                                    {
                                        s.Y = this.Y + this.Height;
                                        s.Velocity.Y *= -1;
                                    }
                                    else if (s.isTouchingLeft(this))
                                    {
                                        s.X = this.X - s.Width;
                                        s.Velocity.X *= -1;
                                    }
                                    else if (s.isTouchingRight(this))
                                    {
                                        s.X = this.X + this.Width;
                                        s.Velocity.X *= -1;
                                    }
                                    break;
                                case SpriteType.Object:
                                    if (s.isTouchingTop(this))
                                    {
                                        s.Y = this.Y - s.Height;
                                        s.X *= Friction;
                                        s.Y *= boingBoing;
                                    }
                                    else if (s.isTouchingBottom(this))
                                    {
                                        s.Y = this.Y + this.Height;
                                        s.Velocity.Y *= 0.5f;
                                        Velocity.Y *= -1;
                                    }
                                    else if (s.isTouchingLeft(this))
                                    {
                                        s.X = this.X - s.Width;
                                        s.Velocity.X *= 0.70f;
                                        s.Velocity.X *= -1;
                                    }
                                    else if (s.isTouchingRight(this))
                                    {
                                        s.X = this.X + this.Width;
                                        s.Velocity.X *= 0.70f;
                                        s.Velocity.X *= -1;
                                    }
                                    break;
                                case SpriteType.Player:
                                    if (s.isTouchingTop(this))
                                    {
                                        s.Y = this.Y - s.Height;
                                        s.Velocity.Y = 0;
                                    }
                                    else if (s.isTouchingBottom(this))
                                    {
                                        s.Y = this.Y + this.Height;
                                        s.Velocity.Y *= 0.5f;
                                        s.Velocity.Y *= -1;
                                    }
                                    else if (s.isTouchingLeft(this))
                                    {
                                        s.X = this.X - s.Width;
                                        s.Velocity.X = 0;
                                    }
                                    else if (s.isTouchingRight(this))
                                    {
                                        s.X = this.X + this.Width;
                                        s.Velocity.X = 0;
                                    }
                                    break;
                            }
                            break;
                        case SpriteType.Object:
                            break;
                        case SpriteType.Player:
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
            if (s1.ToRec.IntersectsWith(s2.ToRec))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isTouchingLeft(this Sprite s1, Sprite s2)
        {
            if (s1.Right.IntersectsWith(s2.Left))
                return true;
            else
                return false;
        }

        public static bool isTouchingRight(this Sprite s1, Sprite s2)
        {
            if (s1.Left.IntersectsWith(s2.Right))
                return true;
            else
                return false;
        }

        public static bool isTouchingTop(this Sprite s1, Sprite s2)
        {
            if (s1.Bottom.IntersectsWith(s2.Top))
                return true;
            else
                return false;
        }

        public static bool isTouchingBottom(this Sprite s1, Sprite s2)
        {
            if (s1.Top.IntersectsWith(s2.Bottom))
                return true;
            else
                return false;
        }

        public static bool isOnStage(this Sprite s1, Rectangle clientRect)
        {
            if (s1.ToRec.IntersectsWith(clientRect))
                return true;
            else
                return false;
        }
    }
}
