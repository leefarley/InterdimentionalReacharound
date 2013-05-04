using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InterdimentionalReacharound
{
    public class Actor : GameObject
    {
        protected Vector2 Velocity;
        protected Rectangle Bounds;
        protected Layer groundLayer;
        protected SpriteState spriteState;
        protected Direction direction;
        protected float Speed;

        public SpriteManager spriteManager;

        public Actor(Vector2 position, Rectangle bounds, Layer layer) : base(position)
        {
            spriteManager = new SpriteManager();
            Bounds = bounds;
            Speed = 5.0f;
            groundLayer = layer;
            spriteState = SpriteState.Standing;
        }

        public override void LoadContent(Texture2D texture)
        {
            spriteManager.SpriteSheet = texture;

            base.LoadContent(texture);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteManager.Draw(spriteBatch, Position);
        }
    }
}
