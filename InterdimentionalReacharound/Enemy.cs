using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InterdimentionalReacharound
{
    public abstract class Enemy : Actor
    {
        public Enemy(Vector2 position, Rectangle bounds, Layer layer)
            : base(position, bounds, layer)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            spriteManager.Update(gameTime, SpriteState.Standing);
        }

        public void Draw(SpriteBatch spriteBatch, Point offset)
        {
            var newPosition = new Vector2(Position.X + offset.X, Position.Y + offset.Y);
            spriteManager.Draw(spriteBatch, newPosition);
        }
    }
}
