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
    public enum PlayerState
    {
        Standing,
        Falling,
        Jumping,
        Running
    }

    public class Player : Actor
    {
        public PlayerIndex _playerIndex;
        PlayerState _state;

        public Player(Vector2 position, PlayerIndex playerIndex, Rectangle bounds) : base(position, bounds)
        {
            _state = PlayerState.Standing;
            _playerIndex = playerIndex;
        }

        public override void LoadContent(Texture2D texture)
        {
            base.LoadContent(texture);
        }

        public override void Update(GameTime gameTime)
        {
            var gamePadState = GamePad.GetState(PlayerIndex.One);
            PlayerState newState = _state;

            switch (newState)
            {
                case PlayerState.Standing:
                    if (CheckIsRunning(gamePadState))
                        newState = PlayerState.Running;
                    break;
                case PlayerState.Running:
                    if (!CheckIsRunning(gamePadState))
                        newState = PlayerState.Standing;
                    break;
            }

            Vector2 newPosition = Position;

            switch (newState)
            {
                case PlayerState.Standing:
                    
                    break;
                case PlayerState.Running:
                    newPosition = Position + (new Vector2(CheckMovementVelocity(gamePadState).X,0) * Speed);
                    newPosition = OutOfBounds(newPosition);
                    break;
            }


            Position = newPosition;
            _state = newState;
            spriteManager.Update(gameTime, _state);
        }

        public static bool CheckIsRunning(GamePadState gamePadState)
        {
            if (gamePadState.ThumbSticks.Left.X != 0)
            {
                return true;
            }
            return false;
        }
        public Vector2 CheckMovementVelocity(GamePadState gamePadState)
        {
            return gamePadState.ThumbSticks.Left;
        }
        public Vector2 OutOfBounds(Vector2 newPosition)
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
