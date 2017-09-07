using MapEditor.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MapEditor
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        const int TargetHeight = 960;
        const int TargetWidth =1400;
        Matrix Scale;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);


            float multiplierX = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width * .9f / TargetWidth;
            float multiplierY = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height* .9f / TargetHeight;
            float multiplier = MathHelper.Min(multiplierX, multiplierY);
            graphics.PreferredBackBufferHeight = (int)(960 * multiplier);
            graphics.PreferredBackBufferWidth = (int)(1400 * multiplier);
            IsMouseVisible = true;

            float scaleX = (float)graphics.PreferredBackBufferWidth / (float)TargetWidth;
            float scaleY = (float)graphics.PreferredBackBufferHeight / (float)TargetHeight;
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

            MouseManager.Instance.Init();
            MapManager.Instance.Init();
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
            spriteBatch = new SpriteBatch(GraphicsDevice);
            MapManager.Instance.SetGraphicsDevice(GraphicsDevice);
            MapManager.Instance.SetViewport(GraphicsDevice.Viewport);
            MapManager.Instance.SetContentManager(Content);
            MapManager.Instance.SetScale(new Vector2(Scale.M11, Scale.M22));
            MapManager.Instance.Load();
            MouseManager.Instance.Load();

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
            MouseManager.Instance.Update(gameTime);
            KeyboardManager.Instance.Update(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            MapManager.Instance.SetActive(IsActive);
            MapManager.Instance.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, null, null, MapManager.Instance.Cam.GetTransform(Scale));
            MouseManager.Instance.Draw(spriteBatch);
            KeyboardManager.Instance.Draw(spriteBatch);
            MapManager.Instance.Draw(spriteBatch);


            spriteBatch.End();
            

            base.Draw(gameTime);
        }
    }
}
