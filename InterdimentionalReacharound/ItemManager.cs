using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterdimentionalReacharound
{
    public class ItemManager : GameControl
    {
        private IList<GameItem> _items;
        private Texture2D _displayTexture;
        private Texture2D _optionTexture;

        public ItemManager()
        {
            _items = new List<GameItem>();
        }

        public void AddItem(Vector2 position, Point size)
        {
            _items.Add(new GameItem(position, size, _displayTexture, _optionTexture));
        }

        public override void LoadContent(ContentManager content)
        {
            _displayTexture = content.Load<Texture2D>(@"Textures\SpriteSheets\Mine");
            _optionTexture = content.Load<Texture2D>(@"Buttons\xboxControllerButtonA");
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            Rectangle view = new Rectangle((int)camera.Position.X - 32, (int)camera.Position.Y - 32, camera.CameraBounds.Width, camera.CameraBounds.Height);
            foreach (var item in _items)
            {
                if (view.Intersects(item.BoundingBox))
                    item.Draw(spriteBatch, camera.Position);
            }
        }

        public void CheckItemCollisions(Rectangle playerBox)
        {
            foreach (var item in _items)
            {
                if (playerBox.Intersects(item.BoundingBox))
                    item.DisplayOptions = true;
                else
                    item.DisplayOptions = false;
            }
        }
    }
}
