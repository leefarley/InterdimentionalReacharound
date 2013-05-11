using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InterdimentionalReacharound
{
    public class Gumba : Enemy
    {
        public Gumba(Vector2 position, Rectangle bounds, Layer layer) : base(position, bounds, layer)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            spriteManager.Update(gameTime, SpriteState.Standing);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteManager.Draw(spriteBatch, Position);
        }
    }
}
