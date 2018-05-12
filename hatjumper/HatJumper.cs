using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace hatjumper
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class HJGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D background;
        Texture2D PlayBtnTexture => menu.buttons[0].Image;
        Texture2D ShopBtnTexture => menu.buttons[1].Image;
        Texture2D SoundBtnTexture => menu.buttons[2].Image;
        Texture2D VibrBtnTexture => menu.buttons[3].Image;
        SpriteFont testFont;
        MainMenu menu;
        public string testText = "test";
        List<Texture2D> btnTextures = new List<Texture2D>(4);
        List<Vector2> btnCoordinates = new List<Vector2>(4);

        public HJGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.Portrait;
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

            testFont = Content.Load<SpriteFont>("gameFont");

            btnTextures.Add(Content.Load<Texture2D>("Play"));
            btnTextures.Add(Content.Load<Texture2D>("Shop"));
            btnTextures.Add(Content.Load<Texture2D>("SoundOn"));
            btnTextures.Add(Content.Load<Texture2D>("VibrOn"));

            background = Content.Load<Texture2D>("Background");
            
            Viewport viewport = graphics.GraphicsDevice.Viewport;
            float viewportWidth = viewport.Width;
            float viewportHeight = viewport.Height;
            float toBorderX = 75, toBorderY = 75;
            float playBtnYPos = 600;
            // Play button position
            btnCoordinates.Add(new Vector2(viewportWidth - toBorderX - PlayBtnTexture.Width, playBtnYPos));
            // Shop button position
            btnCoordinates.Add(new Vector2(viewportWidth - toBorderX - ShopBtnTexture.Width, playBtnYPos + 150));
            // Sound button position
            btnCoordinates.Add(new Vector2(toBorderX, viewportHeight - toBorderY - SoundBtnTexture.Height));
            // Vibration button position
            btnCoordinates.Add(new Vector2(toBorderX*2 + SoundBtnTexture.Width,
                                           viewportHeight - toBorderY - VibrBtnTexture.Height));
            
            menu = new MainMenu(btnCoordinates, btnTextures);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            TouchCollection touches = TouchPanel.GetState();
            foreach (TouchLocation touch in touches)
            {
                if (touch.State != TouchLocationState.Released)
                    continue;
                menu.CheckTap(touch, ref testText);
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // TODO: Add your update logic here
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
            foreach (var butt in menu.buttons)
            {
                spriteBatch.Draw(butt.Image, btnCoordinates[menu.buttons.IndexOf(butt)], Color.White);
            }
            spriteBatch.DrawString(testFont, testText, Vector2.Zero, Color.Black);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
