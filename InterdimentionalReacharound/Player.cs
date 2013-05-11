using InterdimentionalReacharound.Control;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InterdimentionalReacharound
{
    public class Player : Actor
    {
        private IControl _control;
        
        public Player(Vector2 position, Rectangle bounds, Layer layer, IControl control) : base(position, bounds, layer)
        {
            _control = control;
        }

        public override void Update(GameTime gameTime)
        {
            _control.UpdateContolState();

            var newVelocity = Velocity;
            var newState = spriteState;

            newVelocity.X = _control.GetVelocty();

            if (IsJumping == false && _control.IsJumpPressed())
            {
                newVelocity.Y = -5;
                IsJumping = true;
                newState = SpriteState.Jumping;
            }

            var newPosition = Position + (newVelocity * Speed);
            newPosition = CalculateBounds(newPosition);

            switch (newState)
            {
                case SpriteState.Standing:
                    {
                        if (newVelocity.X < 0 || newVelocity.X > 0)
                            newState = SpriteState.Running;
                        if (!IsGroundSolid(newPosition))
                        {
                            newState = SpriteState.Falling;
                            newVelocity.Y = 1;
                        }
                        break;
                    }
                case SpriteState.Running:
                    {
                        if (newVelocity.X == 0)
                            newState = SpriteState.Standing;
                        if (!IsGroundSolid(newPosition))
                        {
                            newState = SpriteState.Falling;
                            newVelocity.Y = 1;
                        }
                        break;
                    }
                case SpriteState.Falling:
                    {
                        if (IsGroundSolid(newPosition))
                        {
                            newVelocity.Y = 0;
                            newState = SpriteState.Standing;
                        }
                        else if (newVelocity.Y < 10)
                        {
                            newVelocity.Y += 0.5f;
                        }
                        break;
                    }
                case SpriteState.Jumping:
                    {
                        if (IsGroundSolid(newPosition))
                        {
                            newVelocity.Y = 0;
                            newState = SpriteState.Standing;
                            IsJumping = false;
                        }
                        else if (newVelocity.Y < 10)
                        {
                            newVelocity.Y += 0.5f;
                        }
                        break;
                    }
            }

            if (newVelocity.X > 0)
                spriteManager.ChangeSpriteDirection(Direction.Right);
            else if (newVelocity.X < 0)
                spriteManager.ChangeSpriteDirection(Direction.Left);

            newPosition = Position + (newVelocity * Speed);
            newPosition = CalculateBounds(newPosition);

            spriteState = newState;
            Velocity = newVelocity;
            Position = newPosition;

            spriteManager.Update(gameTime, spriteState);
            
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            Vector2 newPosition = new Vector2(Position.X - camera.Position.X, Position.Y - camera.Position.Y);
            spriteManager.Draw(spriteBatch, newPosition);
        }
    }
}
