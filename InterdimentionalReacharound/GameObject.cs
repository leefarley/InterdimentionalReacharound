using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InterdimentionalReacharound
{
    public abstract class GameObject
    {
        public Vector2 Position{get; set;}
        protected Texture2D Texture;
        protected Point Size;

        public GameObject(Vector2 position, Point size)
        {
            Position = position;
            Size = size;
        }
        public GameObject(Vector2 position, Point size, Texture2D texture)
        {
            Position = position;
            Size = size;
            Texture = texture;
        }

        public virtual void LoadContent(Texture2D texture)
        {
            Texture = texture;
        }
        public virtual Rectangle BoundingBox { get { return new Rectangle((int)Position.X, (int)Position.Y, Size.X, Size.Y); } }
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, Vector2 offset);
    }
}
