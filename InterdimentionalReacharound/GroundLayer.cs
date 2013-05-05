using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace InterdimentionalReacharound
{
    public class GroundLayer : Layer
    {
        public GroundLayer(string contentString)
            : base(contentString)
        {
            TileSize = 16;
            TileLocation = new Point(0, 0);
        }

        public override void LoadContent(ContentManager contentManager)
        {
            TileSheet = contentManager.Load<Texture2D>(ContentString);

            for (int x = 0; x < MapSize.X; x++)
            {
                for (int y = 0; y < MapSize.Y; y++)
                {
                    if (y > 20)
                        Map[x, y] = 1;
                    else
                        Map[x, y] = 0;
                }
            }
        }


    }
}
