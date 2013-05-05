using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace InterdimentionalReacharound
{
    public class BackLayer : Layer
    {
        private Texture2D _mainImage;
        private Texture2D _bgLayer1;
        private Texture2D _bgLayer2;
        private float _speed;
        private float _location;

        public override void LoadContent(ContentManager contentManager)
        {
            _mainImage = contentManager.Load<Texture2D>("mainbackground");
            _bgLayer1 = contentManager.Load<Texture2D>("bgLayer1");
            _bgLayer2 = contentManager.Load<Texture2D>("bgLayer2");
            tileSize = _mainImage.Width;
            _speed = 0.2f;
            _location = 0;
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spritebatch, Camera camera)
        {
            var position = camera.Position;

            _location = (_location + _speed) % tileSize;

            var XOffset = (int)(_location + position.X) % tileSize;

            for (int i = 0; i < 3; i++)
            {
                var destination = new Vector2((i * tileSize) - XOffset, 0);
                spritebatch.Draw(_mainImage, destination, Color.White);
                spritebatch.Draw(_bgLayer1, destination, Color.White);
                spritebatch.Draw(_bgLayer2, destination, Color.White);
            }
        }
    }
}
