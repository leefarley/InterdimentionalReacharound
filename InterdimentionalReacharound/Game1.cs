﻿
using System.Linq;
using InterdimentionalReacharound.Control;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace InterdimentionalReacharound
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager _graphics;
        SpriteBatch _spriteBatch;

        
        Player PlayerOne, PlayerTwo;
        EnemyManager playerOneEnemies, playerTwoEnemies;
        ItemManager itemManagerOne, itemManagerTwo;
        CollisionManager collisionManagerOne, collisionManagerTwo;
        Viewport defaultView, playerOneView, playerTwoView;
        Camera CameraOne, CameraTwo;
        IList<Layer> layers;
        bool xPressed = false;
        float timeLastFrame;
        readonly float timeBetweenFrame;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            timeLastFrame = 0f;
            timeBetweenFrame = 0.05f;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            layers = new List<Layer>();
            layers.Add(new BackLayer(0.2f, @"Textures\Environment\mainbackground"));
            layers.Add(new BackLayer(0.3f, @"Textures\Environment\bgLayer1"));
            layers.Add(new BackLayer(0.4f, @"Textures\Environment\bgLayer2"));
            layers.Add(new BackgroundLayer(@"Textures\Environment\tiles2"));
            layers.Add(new GroundLayer(@"Textures\Environment\tiles2"));
            
            foreach (Layer layer in layers)
            {
                layer.LoadContent(Content);
            }

            CameraOne = new Camera(new Rectangle(0, 0, 6000, 560));
            CameraTwo = new Camera(new Rectangle(0, 0, 6000, 560));

            defaultView = GraphicsDevice.Viewport;
            playerOneView = GraphicsDevice.Viewport;
            playerTwoView = GraphicsDevice.Viewport;

            playerOneView.Height = defaultView.Height / 2;
            playerTwoView.Height = defaultView.Height / 2;
            playerTwoView.Y = defaultView.Height / 2;

            PlayerOne = new Player(Vector2.Zero, new Rectangle(0, 0, 6000, 560), layers.Last(), new Controller(PlayerIndex.One));
            PlayerTwo = new Player(Vector2.Zero, new Rectangle(0, 0, 6000, 560), layers.Last(), new KeyboardControl(PlayerIndex.Two));

            playerOneEnemies = new EnemyManager(layers.Last(), new Rectangle(0, 0, 6000, 560));
            playerTwoEnemies = new EnemyManager(layers.Last(), new Rectangle(0, 0, 6000, 560));

            itemManagerOne = new ItemManager();
            itemManagerTwo = new ItemManager();

            collisionManagerOne = new CollisionManager(PlayerOne, itemManagerOne, playerOneEnemies);
            collisionManagerTwo = new CollisionManager(PlayerTwo, itemManagerTwo, playerTwoEnemies);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            PlayerOne.LoadContent(Content.Load<Texture2D>(@"Textures\SpriteSheets\PlayerOne"));
            PlayerTwo.LoadContent(Content.Load<Texture2D>(@"Textures\SpriteSheets\PlayerTwo"));
            playerOneEnemies.LoadContent(Content.Load<Texture2D>(@"Textures\SpriteSheets\Gumba"));
            playerTwoEnemies.LoadContent(Content.Load<Texture2D>(@"Textures\SpriteSheets\Gumba2"));
            itemManagerOne.LoadContent(Content);
            itemManagerTwo.LoadContent(Content);

            playerOneEnemies.CreateEnemy(Vector2.Zero);
            playerTwoEnemies.CreateEnemy(new Vector2(200, 0));

            itemManagerOne.AddItem(new Vector2(400, 270), new Point(44, 44));
            itemManagerTwo.AddItem(new Vector2(400, 270), new Point(44, 44));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed)
            {
                xPressed = true;

            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Released && xPressed)
            {
                Player temp = PlayerTwo;
                PlayerTwo = PlayerOne;
                PlayerOne = temp;
                xPressed = false;
            }
            
            //timeLastFrame += gameTime.ElapsedGameTime.Milliseconds / 1000.0f;
            //if (timeLastFrame >= timeBetweenFrame)
            //{
            //    timeLastFrame = 0f;
                foreach (Layer layer in layers)
                {
                    layer.Update(gameTime);
                }
                PlayerOne.Update(gameTime);
                PlayerTwo.Update(gameTime);
                playerOneEnemies.Update(gameTime);
                playerTwoEnemies.Update(gameTime);
                CameraOne.Update(PlayerOne.Position, playerOneView);
                CameraTwo.Update(PlayerTwo.Position, playerTwoView);

                collisionManagerOne.Update(gameTime);
                collisionManagerTwo.Update(gameTime);
                
                base.Update(gameTime);
            //}
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GraphicsDevice.Viewport = playerOneView;
            DrawScene(PlayerOne, CameraOne, gameTime, playerOneEnemies, itemManagerOne);

            GraphicsDevice.Viewport = playerTwoView;
            DrawScene(PlayerTwo, CameraTwo, gameTime, playerTwoEnemies, itemManagerTwo);

            
            _spriteBatch.Begin();
            // draw stuff over both viewports
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawScene(Player player, Camera camera, GameTime gameTime, EnemyManager enemyManager, ItemManager itemManager)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            foreach (Layer layer in layers)
	        {
		        layer.Draw(_spriteBatch, camera);
	        }

            itemManager.Draw(_spriteBatch, camera);
            enemyManager.Draw(_spriteBatch, camera);
            player.Draw(_spriteBatch, camera.Position);

            _spriteBatch.End();
        }
    }

    public class CollisionManager : GameControl
    {
        private Player _player;
        private ItemManager _items;
        private EnemyManager _enemies;

        public CollisionManager(Player player, ItemManager items, EnemyManager enemies)
        {
            _player = player;
            _items = items;
            _enemies = enemies;
        }

        public override void LoadContent(ContentManager content)
        {
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerBox = _player.BoundingBox;
            _items.CheckItemCollisions(playerBox);
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
        }
    }
}
