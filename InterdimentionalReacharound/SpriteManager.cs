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
        int currentFrame;
        int sheetSize;
        float timeLastFrame;
        float timeBetweenFrame;

        public SpriteManager()
        {
            currentFrame = 0;
            timeLastFrame = 0f;
            timeBetweenFrame = 0.1f;
            sheetSize = 3;
        }

        public void Update(GameTime gameTime)
        {
            timeLastFrame += (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            if (timeLastFrame >= timeBetweenFrame)
            {
                timeLastFrame = 0f;
                currentFrame = ++currentFrame % sheetSize;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Position)
        {
            var spriteX = currentFrame * 32;
            Rectangle playerFrame = new Rectangle(spriteX, 32, 32, 32);
            Rectangle destFrame = new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
            spriteBatch.Draw(SpriteSheet, destFrame, playerFrame, Color.White);
        }
    }
}
