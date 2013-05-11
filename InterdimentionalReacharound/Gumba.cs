using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InterdimentionalReacharound
{
    public class Gumba : Enemy
    {
        public Gumba(Vector2 position, Rectangle bounds, Layer layer) : base(position, bounds, layer)
        {
            Velocity = new Vector2(2, 0);
            direction = Direction.Right;
            spriteState = SpriteState.Running;
        }

        public override void Update(GameTime gameTime)
        {
            var newVelocity = Velocity;
            var newPosition = Position + (newVelocity * Speed);
            var newState = spriteState;

            switch (newState)
            {
                case SpriteState.Running:
                    {
                        if (!IsGroundSolid(newPosition))
                        {
                            newState = SpriteState.Falling;
                            newVelocity.X = 0;
                            newVelocity.Y = 1;
                        }
                        else if (HitBounds(newPosition))
                        {
                            newVelocity *= -1;
                        }
                        break;
                    }
                case SpriteState.Falling:
                    {
                        if (IsGroundSolid(newPosition))
                        {
                            newVelocity.Y = 0;
                            newVelocity.X = 1;
                            newState = SpriteState.Running;
                        }
                        else if (newVelocity.Y < 5)
                        {
                            newVelocity.Y += 0.5f;
                        }
                        break;
                    }
            }

            newPosition = Position + (newVelocity * Speed);
            newPosition = CalculateBounds(newPosition);

            if (newVelocity.X > 0)
                spriteManager.ChangeSpriteDirection(Direction.Right);
            else if (newVelocity.X < 0)
                spriteManager.ChangeSpriteDirection(Direction.Left);

            spriteState = newState;
            Velocity = newVelocity;
            Position = newPosition;

            spriteManager.Update(gameTime, spriteState);
        }

        private bool HitBounds(Vector2 newPosition)
        {
            if (newPosition.X < Bounds.Left)
                return true;
            if ((newPosition.X + 32) > Bounds.Right)
                return true;
            return false;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            var newPosition = Position - offset;
            spriteManager.Draw(spriteBatch, newPosition);
        }

    }
}
