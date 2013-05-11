using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InterdimentionalReacharound
{
    public abstract class Enemy : Actor
    {
        protected Enemy(Vector2 position, Rectangle bounds, Layer layer)
            : base(position, bounds, layer)
        {
            
        }

        public override abstract void Update(GameTime gameTime);
    }
}
