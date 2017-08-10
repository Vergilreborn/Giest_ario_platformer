using Giest_ario_platformer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Giest_ario_platformer.Abstract;
using Giest_ario_platformer.Screens;
using Microsoft.Xna.Framework.Content;
using Giest_ario_platformer.Handlers;

namespace Giest_ario_platformer.Managers
{
    class GameManager : IGameObject
    {
        #region Static Variables
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameManager();
                return instance;
            }
        }

        private static GameManager instance;
        #endregion


        public ContentManager Content;
        public Viewport ViewPort;
        public GraphicsDevice Graphics;
      //  public SpriteFont DebugFont;
        public Camera Cam;
        public Dictionary<String, SpriteFont> Fonts
        {
            get
            {
                return fonts;
            }
        }

      
        public bool ExitGame
        {
            get
            {
                return exitGame;
            }
        }

        private Dictionary<String, SpriteFont> fonts;

        private bool exitGame;
        private AGameScreen currentScreen;
        
      

        public GameManager()
        {
            //GameScreen
            //currentScreen = new MainGameScreen();
            //StartScreen
            currentScreen = new StartScreen();
        }
       
        public void Init()
        {
            fonts = new Dictionary<string, SpriteFont>();
            exitGame = false;
            currentScreen.Init();
        }

        public void Load()
        {
            // DebugFont = Content.Load<SpriteFont>("Debug");
            fonts.Add("Debug", Content.Load<SpriteFont>("Fonts/Debug"));

            fonts.Add("XSmall", Content.Load<SpriteFont>("Fonts/GameFont_xs"));
            fonts.Add("Small", Content.Load<SpriteFont>("Fonts/GameFont_s"));
            fonts.Add("Medium", Content.Load<SpriteFont>("Fonts/GameFont_m"));
            fonts.Add("Large", Content.Load<SpriteFont>("Fonts/GameFont_l"));
            fonts.Add("XLarge", Content.Load<SpriteFont>("Fonts/GameFont_xl"));
            currentScreen.Load();
        }

        public void Update(GameTime _gameTime)
        {
            currentScreen.Update(_gameTime);
        }

        public void SetContentManager(ContentManager _content)
        {
            this.Content = _content;
        }

        public void SetGraphics(GraphicsDevice Graphics)
        {
            this.Graphics = Graphics;
        }

        public void SetViewport(Viewport _view)
        {
            this.ViewPort= _view;
            this.Cam = new Camera(GameManager.Instance.ViewPort, Vector2.Zero);
        }

        public Texture2D CreateColorTexture(int r, int g, int b, int a)
        {
            Texture2D rect = new Texture2D(this.Graphics, 1, 1);

            Color[] data = new Color[1];
            for (int i = 0; i < data.Length; ++i) data[i] = new Color(r, g, b,a);
            rect.SetData(data);

            return rect;
            
        }


        public void Draw(SpriteBatch _spriteBatch)
        { 
            currentScreen.Draw(_spriteBatch); 
        }

        public void ChangeScreen(string screenName)
        {
            currentScreen.UnLoad();
            switch (screenName)
            {
                case "MainGameScreen":
                    currentScreen = new MainGameScreen();
                    break;
                case "Exit": exitGame = true;
                    return;
            }

            currentScreen.Init();
            currentScreen.Load();
        }
    }
}
