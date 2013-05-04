using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterdimentionalReacharound
{
    public class SpriteManager
    {
        public Texture2D SpriteSheet{ get; set; }
        Point currentFrame;
        int sheetSize;
        float timeLastFrame;
        float timeBetweenFrame;
        Point spriteSize;
        int currentDirection;

        public SpriteManager()
        {
            currentFrame = Point.Zero;
            timeLastFrame = 0f;
            timeBetweenFrame = 0.1f;
            sheetSize = 3;
            spriteSize = new Point(32, 32);
        }

        public void Update(GameTime gameTime, SpriteState spriteState)
        {
            if (spriteState == SpriteState.Running)
            {
                timeLastFrame += (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
                if (timeLastFrame >= timeBetweenFrame)
                {
                    timeLastFrame = 0f;
                    currentFrame.X = ++currentFrame.X % sheetSize;
                }
            }
        }

        public void ChangeSpriteDirection(Direction direction)
        {
            if (direction == Direction.Left)
                currentDirection = 0;
            else
                currentDirection = 1;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Position)
        {
            var spriteX = currentFrame.X * spriteSize.X;
            var spriteY = currentDirection * spriteSize.Y;
            Rectangle playerFrame = new Rectangle(spriteX, spriteY, spriteSize.X, spriteSize.Y);
            Rectangle destFrame = new Rectangle((int)Position.X, (int)Position.Y, spriteSize.Y, spriteSize.Y);
            spriteBatch.Draw(SpriteSheet, destFrame, playerFrame, Color.White);
        }

        public Point LeftFoot(Vector2 position)
        {
            return new Point((int)(position.X + 5), (int)(position.Y + spriteSize.Y));
        }

        public Point RightFoot(Vector2 position)
        {
            return new Point((int)position.X + (spriteSize.X - 5), (int)(position.Y + spriteSize.Y));
        }
    }
}
