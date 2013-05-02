using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace InterdimentionalReacharound
{
    public class GroundLayer : Layer
    {
        public GroundLayer() : base()
        {
            tileSize = 16;
            tileLocation = new Point(0, 0);
        }

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager contentManager)
        {
            tileSheet = contentManager.Load<Texture2D>("tiles2");

            for (int x = 0; x < mapSize.X; x++)
            {
                for (int y = 0; y < mapSize.Y; y++)
                {
                    if (y > 15)
                        map[x, y] = 1;
                    else
                        map[x, y] = 0;
                }
            }
        }


    }
}
