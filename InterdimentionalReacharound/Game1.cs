
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
        Viewport defaultView, playerOneView, playerTwoView;
        Camera CameraOne, CameraTwo;
        IList<Layer> layers;
        bool xPressed = false;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            layers.Add(new BackgroundLayer());
            layers.Add(new GroundLayer());
            
            foreach (Layer layer in layers)
            {
                layer.LoadContent(Content);
            }
            
            CameraOne = CameraTwo = new Camera(new Rectangle(0, 0, 6000, 750));

            defaultView = playerOneView = playerTwoView = GraphicsDevice.Viewport;

            playerOneView.Height = playerTwoView.Height = defaultView.Height / 2;
            playerTwoView.Y = defaultView.Height / 2;

            PlayerOne = new Player(Vector2.Zero, PlayerIndex.One, new Rectangle(0, 0, 6000, 750), layers[1]);
            PlayerTwo = new Player(Vector2.Zero, PlayerIndex.Two, new Rectangle(0, 0, 6000, 750), layers[1]);
            
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
            PlayerOne.LoadContent(Content.Load<Texture2D>("malefighter"));
            PlayerTwo.LoadContent(Content.Load<Texture2D>("malerogue"));
            // TODO: use this.Content to load your game content here
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
            // TODO: Add your update logic here
            PlayerOne.Update(gameTime);
            PlayerTwo.Update(gameTime);
            CameraOne.Update(PlayerOne.Position, playerOneView);
            CameraTwo.Update(PlayerTwo.Position, playerTwoView);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            GraphicsDevice.Viewport = playerOneView;
            DrawScene(PlayerOne, CameraOne, gameTime);

            GraphicsDevice.Viewport = playerTwoView;
            DrawScene(PlayerTwo, CameraTwo, gameTime);

            GraphicsDevice.Viewport = defaultView;
            _spriteBatch.Begin();
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawScene(Player player, Camera camera, GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            foreach (Layer layer in layers)
	        {
		        layer.Draw(_spriteBatch, camera);
	        }
            player.Draw(_spriteBatch, camera);
            
            _spriteBatch.End();
        }
    }
}
