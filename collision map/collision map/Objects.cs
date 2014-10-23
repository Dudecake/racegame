using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace collision_map
{
    class Objects
    {
        Graphics paper;
        SolidBrush brushRoad = new SolidBrush(Color.Black);
        SolidBrush brushGrass = new SolidBrush(Color.Green);
        SolidBrush brushWall = new SolidBrush(Color.Gray);
        SolidBrush brushCheck = new SolidBrush(Color.Gold);
        SolidBrush brushStart = new SolidBrush(Color.Indigo);
        SolidBrush brushPitStop = new SolidBrush(Color.LightBlue);

        public void DrawRoad(int X, int Y, int Width, int Height)
        {
            paper.FillRectangle(brushRoad, X, Y, Width, Height);
        }

        public void DrawGrass(int X, int Y, int Width, int Height)
        {
            paper.FillRectangle(brushGrass, X, Y, Width, Height);
        }

        public void DrawWall(int X, int Y, int Width, int Height)
        {
            paper.FillRectangle(brushWall, X, Y, Width, Height);
        }

        public void DrawCheck(int X, int Y, int Width, int Height)
        {
            paper.FillRectangle(brushCheck, X, Y, Width, Height);
        }

        public void DrawStart(int X, int Y, int Width, int Height)
        {
            paper.FillRectangle(brushStart, X, Y, Width, Height);
        }

        public void DrawPitStop(int X, int Y, int Width, int Height)
        {
            paper.FillRectangle(brushPitStop, X, Y, Width, Height);
        }
    }
}
