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
        protected int[,] Map;
        protected Point MapSize;
        protected Texture2D TileSheet;
        protected int TileSize;
        protected Point TileLocation;
        protected string ContentString;

        protected Layer(string contentString)
        {
            ContentString = contentString;
            MapSize = new Point(400, 50);
            Map = new int[MapSize.X, MapSize.Y];
        }

        public abstract void LoadContent(ContentManager contentManager);

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spritebatch, Camera camera)
        {
            var position = camera.Position;

            var tilesWide = (spritebatch.GraphicsDevice.Viewport.Width / TileSize) + 3;
            var tilesHigh = (spritebatch.GraphicsDevice.Viewport.Height / TileSize) + 3;

            var XStartTile = (int)position.X / TileSize;
            var YStartTile = (int)position.Y / TileSize;

            var XOffset = (int)position.X % TileSize;
            var YOffset = (int)position.Y % TileSize;

            for (int x = 0; x < tilesWide; x++)
            {
                for (int y = 0; y < tilesHigh; y++)
                {
                    int displayX = Math.Min(XStartTile + x, 399);
                    int displayY = Math.Min(YStartTile + y, 35);
                    if (Map[displayX, displayY] > 0)
                        spritebatch.Draw(TileSheet, new Rectangle((x * TileSize) - XOffset, (y * TileSize) - YOffset, TileSize, TileSize), new Rectangle(TileLocation.X, TileLocation.Y, TileSize, TileSize), Color.White);
                }
            }
        }

        public int GetTileAtLocation(Point location)
        {
            var x = location.X / TileSize;
            var y = location.Y / TileSize;
            return Map[x, y];
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
