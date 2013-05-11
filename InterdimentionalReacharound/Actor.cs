﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InterdimentionalReacharound
{
    public abstract class Actor : GameObject
    {
        protected Vector2 Velocity;
        protected Rectangle Bounds;
        protected Layer groundLayer;
        protected SpriteState spriteState;
        protected Direction direction;
        protected float Speed;
        protected bool IsJumping;

        public SpriteManager spriteManager;

        protected Actor(Vector2 position, Rectangle bounds, Layer layer) : base(position)
        {
            spriteManager = new SpriteManager();
            Bounds = bounds;
            Speed = 5.0f;
            groundLayer = layer;
            spriteState = SpriteState.Standing;
            IsJumping = false;
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

        protected Vector2 CalculateBounds(Vector2 newPosition)
        {

            if (newPosition.X < Bounds.Left)
                newPosition.X = Bounds.Left;
            else if ((newPosition.X + 32) > Bounds.Right)
                newPosition.X = Bounds.Right - 32;

            if (newPosition.Y < Bounds.Top)
                newPosition.Y = Bounds.Top;
            else if ((newPosition.Y + 32) > Bounds.Bottom)
                newPosition.Y = Bounds.Bottom - 32;

            return newPosition;
        }


        protected bool IsGroundSolid(Vector2 newPosition)
        {
            Point worldLeftFoot = spriteManager.LeftFoot(newPosition);
            Point worldRightFoot = spriteManager.RightFoot(newPosition);

            if (groundLayer.IsLocationSolid(worldLeftFoot) && groundLayer.IsLocationSolid(worldRightFoot))
                return true;
            return false;
        }
    }
}
