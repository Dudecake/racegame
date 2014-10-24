using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_try
{
    class Map
    {
        private List<int[,]> Maps = new List<int[,]>();
        public int tileSqr;
        private int mapCounter;

        int[,] Map1 = new int[,]
        {
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},            
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,1,1,1,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,2,2,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,2,1,1,1,2,2,2,2,2,2,2,2,2,2,2,1,1,1,2,2,2,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
            {2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2,2},
        };

        public Map(int sqr)
        {
            tileSqr = sqr;
            Maps.Add(Map1);
        }

        public void setMap(InputManager iManager)
        {
            int width = Maps[mapCounter].GetLength(1);
            int height = Maps[mapCounter].GetLength(0);
            for(int x = 0; x < width; x++)
            {
                for(int y = 0; y < height; y++)
                {
                    int numb = Maps[mapCounter][y, x];
                    if(numb != 0)
                    {
                        switch(numb)
                        {
                            case 1:
                                iManager.inGameSprites.Add(new Sprite(Properties.Resources.Giant_Ice_Block, x * tileSqr, y * tileSqr, tileSqr, tileSqr, Sprite.SpriteType.Road));
                                //obj.DrawRoad(tileSqr, tileSqr, tileSqr * x, tileSqr * y);
                                break;
                            case 2:
                                iManager.inGameSprites.Add(new Sprite(Properties.Resources.piq_55641, x * tileSqr, y * tileSqr, tileSqr, tileSqr, Sprite.SpriteType.Grass));
                                //obj.DrawGrass(tileSqr, tileSqr, tileSqr * x, tileSqr * y);
                                break;
                            case 3:
                                iManager.inGameSprites.Add(new Sprite(Properties.Resources.Block_2, x * tileSqr, y * tileSqr, tileSqr, tileSqr, Sprite.SpriteType.Wall));
                                //obj.DrawWall(tileSqr, tileSqr, tileSqr * x, tileSqr * y);
                                break;
                            case 4:
                                iManager.inGameSprites.Add(new Sprite(Properties.Resources.GreenQuestionBlock, x * tileSqr, y * tileSqr, tileSqr, tileSqr, Sprite.SpriteType.Check));
                                //obj.DrawCheck(tileSqr, tileSqr, tileSqr * x, tileSqr * y);
                                break;
                            case 5:
                                iManager.inGameSprites.Add(new Sprite(Properties.Resources.smw_glowing_question_block_3d_by_redyoshiu_d58qk45, x * tileSqr, y * tileSqr, tileSqr, tileSqr, Sprite.SpriteType.Start));
                                //obj.DrawStart(tileSqr, tileSqr, tileSqr * x, tileSqr * y);
                                break;
                            case 6:
                                iManager.inGameSprites.Add(new Sprite(Properties.Resources._120px_Question_Block_NSMB, x * tileSqr, y * tileSqr, tileSqr, tileSqr, Sprite.SpriteType.Pitstop));
                                //obj.DrawPitStop(tileSqr, tileSqr, tileSqr * x, tileSqr * y);
                                break;
                        }
                    }
                }
            }
        }
    }
}
