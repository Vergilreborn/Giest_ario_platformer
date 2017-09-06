using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Giest_ario_platformer.Managers;

namespace Giest_ario_platformer
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        const int TargetWidth = 600;
        const int TargetHeight = 480;

        Matrix Scale;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            // Somewhere in initialisation


            graphics.PreferredBackBufferHeight = 480*3;
            graphics.PreferredBackBufferWidth= 600*3;

            //graphics.PreferredBackBufferHeight= 960;
            //graphics.PreferredBackBufferWidth = 1200;
            //graphics.PreferredBackBufferHeight = 480;
            //graphics.PreferredBackBufferWidth= 600;

            float scaleX = graphics.PreferredBackBufferWidth / TargetWidth;
            float scaleY = graphics.PreferredBackBufferHeight / TargetHeight;
            Scale = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));

            
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
            GameManager.Instance.Init();
            GameManager.Instance.SetContentManager(Content);
            GameManager.Instance.SetWidthHeight(new Vector2(600, 480));
            KeyboardManager.Instance.Init();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            GameManager.Instance.SetServices(Services);
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameManager.Instance.SetViewport(GraphicsDevice.Viewport);
            GameManager.Instance.SetGraphics(GraphicsDevice);
            GameManager.Instance.Cam.SetScale(Scale);

            GameManager.Instance.Load();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)
                    || GameManager.Instance.ExitGame)
                Exit();

            KeyboardManager.Instance.Update(gameTime);
            GameManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            //spriteBatch.Begin();
            //Scale
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, new SamplerState() { Filter = TextureFilter.Point }, null, null, null, GameManager.Instance.Cam.GetTransform());
            

            GameManager.Instance.Draw(spriteBatch);
            KeyboardManager.Instance.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
