using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class Spritebatch
    {
        private Graphics Gfx;
        private BufferedGraphics bfgfx;
        private BufferedGraphicsContext cntxt = BufferedGraphicsManager.Current;

        public Spritebatch(Size clientsize, Graphics gfx)
        {
            cntxt.MaximumBuffer = new Size(clientsize.Width + 1, clientsize.Height + 1);
            bfgfx = cntxt.Allocate(gfx, new Rectangle(Point.Empty, clientsize));
            Gfx = gfx;
        }

        public void Begin()
        {
            bfgfx.Graphics.Clear(Color.Purple);
        }

        public void Draw(Sprite s)
        {
            bfgfx.Graphics.DrawImageUnscaled(s.Texture, s.ToRec);
        }

        public void drawImage(Bitmap b, Rectangle rec)
        {
            bfgfx.Graphics.DrawImageUnscaled(b, rec);
        }

        public void End()
        {
            bfgfx.Render(Gfx);
        }
    }
}
