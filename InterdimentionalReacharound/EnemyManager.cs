using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InterdimentionalReacharound
{
    public class EnemyManager
    {
        private readonly List<Enemy> _enemies;
        private readonly Layer _layer;
        private readonly Rectangle _bounds;
        private Texture2D _enemyTexture;
        public EnemyManager(Layer layer, Rectangle bounds)
        {
            _enemies = new List<Enemy>();
            _layer = layer;
            _bounds = bounds;
        }

        public void LoadContent(Texture2D texture)
        {
            _enemyTexture = texture; 
        }

        public void CreateEnemy(Vector2 position)
        {
            var enemy = new Gumba(position, _bounds, _layer);
            enemy.LoadContent(_enemyTexture);
            _enemies.Add(enemy);
        }

        public void Update(GameTime gameTime)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var enemy in _enemies)
            {
                enemy.Draw(spriteBatch);
            }
        }
    }
}