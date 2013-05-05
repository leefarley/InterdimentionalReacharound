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
    public abstract class Layer
    {
        protected int[,] map;
        protected Point mapSize;
        protected Texture2D tileSheet;
        protected int tileSize;
        protected Point tileLocation;

        protected Layer()
        {
            mapSize = new Point(400, 50);
            map = new int[mapSize.X, mapSize.Y];
        }

        public abstract void LoadContent(ContentManager contentManager);

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spritebatch, Camera camera)
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
                        spritebatch.Draw(tileSheet, new Rectangle((x * tileSize) - XOffset, (y * tileSize) - YOffset, tileSize, tileSize), new Rectangle(tileLocation.X, tileLocation.Y, tileSize, tileSize), Color.White);
                }
            }
        }

        public int GetTileAtLocation(Point location)
        {
            var x = location.X / tileSize;
            var y = location.Y / tileSize;
            return map[x, y];
        }

        public bool IsLocationSolid(Point location)
        {
            int tile = GetTileAtLocation(location);
            if (tile == 1)
            {
                return true;
            }
            return false;
        }
    }
}
