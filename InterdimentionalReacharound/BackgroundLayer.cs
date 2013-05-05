using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        public BackgroundLayer(string contentString)
            : base(contentString)
        {
            TileSize = 16;
            TileLocation = new Point(64, 0);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            TileSheet = contentManager.Load<Texture2D>(ContentString);

            for (int x = 0; x < MapSize.X; x++)
            {
                for (int y = 0; y < MapSize.Y; y++)
                {
                    if (y > 37)
                        Map[x, y] = 1;
                    else
                        Map[x, y] = 0;
                }
            }
        }
    }
}
