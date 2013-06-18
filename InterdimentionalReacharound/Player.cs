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
                newVelocity.Y = -4;
                IsJumping = true;
                newState = SpriteState.Jumping;
            }

            var newPosition = Position + (newVelocity * Speed);

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

                        if (newVelocity.X > 0 && IsRightWallSolid(newPosition))
                        {
                            newPosition.X -= newPosition.X % 16;
                            newVelocity.X = 0;
                        }
                        else if (newVelocity.X < 0 && IsLeftWallSolid(newPosition))
                        {
                            newPosition.X += (16 - newPosition.X % 16);
                            newVelocity.X = 0;
                        }
                        break;
                    }
                case SpriteState.Falling:
                    {
                        if (newVelocity.X > 0 && IsRightWallSolid(newPosition))
                        {
                            newPosition.X -= newPosition.X % 16;
                            newVelocity.X = 0;
                        }
                        else if (newVelocity.X < 0 && IsLeftWallSolid(newPosition))
                        {
                            newPosition.X += (16 - newPosition.X % 16);
                            newVelocity.X = 0;
                        }

                        if (IsGroundSolid(newPosition))
                        {
                            newVelocity.Y = 0;
                            newPosition.Y -= newPosition.Y  % 16;
                            newState = SpriteState.Standing;
                        }
                        else if (newVelocity.Y < 5)
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
                            newPosition.Y -= newPosition.Y  % 16;
                            IsJumping = false;
                        }
                        else if (newVelocity.Y < 10)
                        {
                            newVelocity.Y += 0.5f;
                        }

                        if (newVelocity.X > 0 && IsRightWallSolid(newPosition))
                        {
                            newPosition.X -= newPosition.X % 16;
                            newVelocity.X = 0;
                        }
                        else if (newVelocity.X < 0 && IsLeftWallSolid(newPosition))
                        {
                            newPosition.X += (16 - newPosition.X % 16);
                            newVelocity.X = 0;
                        }
                        break;
                    }
            }

            if (newVelocity.X > 0)
                spriteManager.ChangeSpriteDirection(Direction.Right);
            else if (newVelocity.X < 0)
                spriteManager.ChangeSpriteDirection(Direction.Left);

            newPosition = CalculateBounds(newPosition);

            spriteState = newState;
            Velocity = newVelocity;
            Position = newPosition;

            spriteManager.Update(gameTime, spriteState);
            
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            Vector2 newPosition = Position - offset;
            spriteManager.Draw(spriteBatch, newPosition);
        }
    }
}
