using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterdimentionalReacharound
{
    public class GameItem : GameObject
    {
        public bool DisplayOptions { get; set; }
        private Texture2D _optionTexture;
        public GameItem(Vector2 position, Point size, Texture2D displayTexture, Texture2D optionTexture)
            : base(position, size, displayTexture)
        {
            _optionTexture = optionTexture;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            var newPosition = Position - offset;
            spriteBatch.Draw(Texture, newPosition, Color.White);
            if (DisplayOptions)
                spriteBatch.Draw(_optionTexture, new Rectangle((int)newPosition.X + 30, (int)newPosition.Y - 35, 30, 30), Color.White);

        }

    }
}
