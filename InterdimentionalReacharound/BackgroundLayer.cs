using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterdimentionalReacharound
{
    public class BackgroundLayer : Layer
    {
        public BackgroundLayer() : base()
        {
            tileSize = 16;
            tileLocation = new Point(64, 0);
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
