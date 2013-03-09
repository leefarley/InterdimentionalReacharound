using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace InterdimentionalReacharound
{
    public class Player : Actor
    {
        public PlayerIndex _playerIndex;
        public Player(Vector2 position, PlayerIndex playerIndex, Rectangle bounds) : base(position, bounds)
        {
            _playerIndex = playerIndex;
        }

        public void LoadContent(Texture2D texture)
        {
            base.LoadContent(texture);
        }

        public override void Update(GameTime gameTime)
        {
            var newVelocity = CalculateMovement();

            var newPosition = Position + (newVelocity * Speed);

            newPosition = calculateBounds(newPosition);

            Position = newPosition;

            spriteManager.Update(gameTime);
            
        }

        public static Vector2 CalculateMovement()
        {
            var gamePadState = GamePad.GetState(PlayerIndex.One);

            var newVelocity = Vector2.Zero;

                if (gamePadState.ThumbSticks.Left.X != 0)
                {
                    newVelocity.X = gamePadState.ThumbSticks.Left.X;
                }

                if (gamePadState.ThumbSticks.Left.Y != 0)
                {
                    newVelocity.Y = -gamePadState.ThumbSticks.Left.Y;
                }

                return newVelocity;
        }

        public Vector2 calculateBounds(Vector2 newPosition)
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
