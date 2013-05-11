﻿using InterdimentionalReacharound.Control;
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
            newPosition = calculateBounds(newPosition);

            switch (newState)
            {
                case SpriteState.Standing:
                    {
                        if (newVelocity.X < 0 || newVelocity.X > 0)
                            newState = SpriteState.Running;
                        if (!IsGroundSolid(newPosition))
                        {
                            newState = SpriteState.Falling;
                            newVelocity.Y = 2;
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
                            newVelocity.Y = 2;
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
            newPosition = calculateBounds(newPosition);

            spriteState = newState;
            Velocity = newVelocity;
            Position = newPosition;

            spriteManager.Update(gameTime, spriteState);
            
        }

        private bool IsGroundSolid(Vector2 newPosition)
        {
            Point worldLeftFoot  = spriteManager.LeftFoot(newPosition);
            Point worldRightFoot = spriteManager.RightFoot(newPosition);

            if (groundLayer.IsLocationSolid(worldLeftFoot) && groundLayer.IsLocationSolid(worldRightFoot))
                return true;
            return false;
        }

        private Vector2 calculateBounds(Vector2 newPosition)
        {

            if (newPosition.X < Bounds.Left)
                newPosition.X = Bounds.Left;
            else if((newPosition.X + 32) > Bounds.Right)
                newPosition.X = Bounds.Right - 32;

            if (newPosition.Y < Bounds.Top)
                newPosition.Y = Bounds.Top;
            else if ((newPosition.Y + 32) > Bounds.Bottom)
                newPosition.Y = Bounds.Bottom - 32;

            return newPosition;
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            Vector2 newPosition = new Vector2(Position.X - camera.Position.X, Position.Y - camera.Position.Y);
            spriteManager.Draw(spriteBatch, newPosition);
        }
    }
}
