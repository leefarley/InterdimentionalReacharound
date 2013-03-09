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
    public class Layer
    {
        int[,] map;
        Point mapSize;
        Texture2D tileSheet;
        int tileSize;
        public Layer()
        {
            mapSize = new Point(400, 50);
            map = new int[mapSize.X, mapSize.Y];
            tileSize = 16;
        }

        public void LoadContent(ContentManager contentManager)
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

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spritebatch, Camera camera)
        {
            var position = camera.Position;

            var tilesWide = (spritebatch.GraphicsDevice.Viewport.Width / tileSize) + 3;
            var tilesHigh = (spritebatch.GraphicsDevice.Viewport.Height / tileSize) + 3;

            var XStartTile = (int)position.X / tileSize;
            var YStartTile = (int)position.Y / tileSize;

            var XOffset = (int)position.X % tileSize;
            var YOffset = (int)position.Y % tileSize;

            for (int x = 0; x < tilesWide; x++)
            {
                for (int y = 0; y < tilesHigh; y++)
                {
                    int displayX = XStartTile + x;
                    int displayY = YStartTile + y;
                    if (map[displayX, displayY] > 0)
                        spritebatch.Draw(tileSheet, new Rectangle((x * 16) - XOffset, (y * 16) - YOffset, 16, 16), new Rectangle(0, 0, 16, 16), Color.White);
                }
            }
        }
    }
}
