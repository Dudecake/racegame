using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ballon
{
    class Balloon
    {
        private int x = 50;
        private int y = 50;
        private int diameter = 20;
        private Pen pen = new Pen(Color.Black);
        private SolidBrush brush;

        public Balloon()
        {
            brush = new SolidBrush(Color.YellowGreen);
        }

        public void MoveRight(int xStep)
        {
            x += xStep;
        }

        public void MoveLeft(int xStep)
        {
            x -= xStep;
        }

        public void MoveUp(int yStep)
        {
            y -= yStep;
        }

        public void MoveDown(int yStep)
        {
            y += yStep;
        }

        public void ChangeSize(int change)
        {
            diameter += change;
        }

        public void Display(Graphics drawArea)
        {
            drawArea.DrawEllipse(pen, x, y, diameter, diameter);
            drawArea.FillEllipse(brush, x, y, diameter, diameter);
        }
    }
}
