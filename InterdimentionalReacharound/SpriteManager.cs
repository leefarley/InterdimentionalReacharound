﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterdimentionalReacharound
{
    enum Direction
    {
        Left = 0,
        Right = 1
    }
    public class SpriteManager
    {
        public Texture2D SpriteSheet{ get; set; }
        int currentFrame;
        int sheetSize;
        float timeLastFrame;
        float timeBetweenFrame;
        PlayerState state;
        Direction direction;

        public SpriteManager()
        {
            currentFrame = 0;
            timeLastFrame = 0f;
            timeBetweenFrame = 0.1f;
            sheetSize = 3;
        }

        public void Update(GameTime gameTime, PlayerState newState, Vector2 velocity)
        {
            timeLastFrame += (float)gameTime.ElapsedGameTime.Milliseconds / 1000.0f;

            if (velocity.X > 0)
                direction = Direction.Right;
            else if (velocity.X < 0)
                direction = Direction.Left;

            if (newState != state)
                state = newState;

            if (timeLastFrame >= timeBetweenFrame)
            {
                timeLastFrame = 0f;
                switch(state)
                {
                    case PlayerState.Standing:
                        currentFrame = 1;
                        break;
                    case PlayerState.Running: 
                        currentFrame = ++currentFrame % sheetSize;
                        break;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 Position)
        {
            var spriteX = currentFrame * 32;
            Rectangle playerFrame = new Rectangle(spriteX, (int)direction * 32, 32, 32);
            Rectangle destFrame = new Rectangle((int)Position.X, (int)Position.Y, 32, 32);
            spriteBatch.Draw(SpriteSheet, destFrame, playerFrame, Color.White);
        }
    }
}
