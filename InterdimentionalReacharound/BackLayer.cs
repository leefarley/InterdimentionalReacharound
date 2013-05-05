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
        private Texture2D _background;
        private float _location;
        private readonly float _speed;


        public BackLayer(float speed, string contentString) : base(contentString)
        {
            _speed = speed;
        }
        public override void LoadContent(ContentManager contentManager)
        {
            _background = contentManager.Load<Texture2D>(ContentString);
            TileSize = _background.Width;
            _location = 0;
        }

        public override void Update(GameTime gameTime)
        {
            _location = (_location + _speed) % TileSize;
        }

        public override void Draw(SpriteBatch spritebatch, Camera camera)
        {
            var position = camera.Position;


            var offset = (int)(_location + position.X) % TileSize;

            for (int i = 0; i < 4; i++)
            {
                var destination = new Vector2((i * TileSize) - offset, 0);
                spritebatch.Draw(_background, destination, Color.White);
            }
        }
    }
}
