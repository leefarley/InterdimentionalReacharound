using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InterdimentionalReacharound
{
    public class GameObject
    {
        public Vector2 Position{get; set;}
        protected Texture2D Texture;

        public GameObject(Vector2 position)
        {
            Position = position;
        }

        public virtual void LoadContent(Texture2D texture)
        {
            Texture = texture;
        }

        public virtual void Update(GameTime gameTime)
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }
    }
}
