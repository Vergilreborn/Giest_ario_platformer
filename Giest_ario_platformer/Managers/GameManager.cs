﻿using Giest_ario_platformer.Interfaces;
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
        public SpriteFont DebugFont;

        private AGameScreen currentScreen;
        public Camera Cam;
      

        public GameManager()
        {
            currentScreen = new MainGameScreen();
        }
       
        public void Init()
        {
            currentScreen.Init();
           
           
        }

        public void Load()
        {
            DebugFont = Content.Load<SpriteFont>("Debug");
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
           // throw new NotImplementedException();
        }
    }
}