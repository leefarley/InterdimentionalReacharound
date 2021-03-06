﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace InterdimentionalReacharound
{
    public class EnemyManager
    {
        private readonly List<Gumba> _enemies;
        private readonly Layer _layer;
        private readonly Rectangle _bounds;
        private Texture2D _enemyTexture;
        public EnemyManager(Layer layer, Rectangle bounds)
        {
            _enemies = new List<Gumba>();
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

        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            Rectangle view = new Rectangle((int)camera.Position.X - 32, (int)camera.Position.Y - 32, camera.CameraBounds.Width, camera.CameraBounds.Height);
            foreach (var enemy in _enemies)
            {
                if (view.Contains((int) enemy.Position.X, (int) enemy.Position.Y))
                    enemy.Draw(spriteBatch, camera.Position);
            }
        }
    }
}