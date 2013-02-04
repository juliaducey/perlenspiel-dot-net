using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PerlenspielLib
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Perlenspiel _game;

        // Screen dimensions
        private const int GridWidth = 480;
        private const int GridHeight = 480;
        private const int TopMargin = 50;
        private const int BottomMargin = 50;
        private const int LeftMargin = 50;
        private const int RightMargin = 50;
        private const int StatusHeight = 20;
        private const int StatusMargin = 0;     // area between status and top of grid

        private const int ScreenWidth = LeftMargin + GridWidth + RightMargin;
        private const int ScreenHeight = TopMargin + StatusHeight + StatusMargin + GridHeight + BottomMargin;

        public Game1(Perlenspiel game)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _game = game;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // Set the size of the game window
            graphics.PreferredBackBufferWidth = ScreenWidth;
            graphics.PreferredBackBufferHeight = ScreenHeight;
            graphics.ApplyChanges();

            // Make the mouse visible
            IsMouseVisible = true;

            // Initialize managers
            Singleton<GridManager>.Instance.Initialize(GridWidth, GridHeight, LeftMargin, TopMargin+StatusHeight+StatusMargin);
            Singleton<InputManager>.Instance.Initialize(_game);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var fonts = new Dictionary<int, SpriteFont>();

            // Load all the fonts
            fonts.Add(10, Content.Load<SpriteFont>("Font10"));
            fonts.Add(14, Content.Load<SpriteFont>("Font14"));
            fonts.Add(20, Content.Load<SpriteFont>("Font20"));
            fonts.Add(30, Content.Load<SpriteFont>("Font30"));
            fonts.Add(40, Content.Load<SpriteFont>("Font40"));
            fonts.Add(50, Content.Load<SpriteFont>("Font50"));
            fonts.Add(75, Content.Load<SpriteFont>("Font75"));
            fonts.Add(125, Content.Load<SpriteFont>("Font125"));

            // Initialize GraphicsManager
            Singleton<GraphicsManager>.Instance.InitializeGraphics(graphics, spriteBatch, fonts);
            Singleton<GraphicsManager>.Instance.InitializeScreen(ScreenWidth, ScreenHeight, StatusHeight, 
                                                                 TopMargin, LeftMargin, RightMargin);

            // Game initialization must be called after content loading
            _game.Init();
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
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            Singleton<InputManager>.Instance.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            Singleton<GraphicsManager>.Instance.DrawScreen();
            
            base.Draw(gameTime);
        }
    }
}
