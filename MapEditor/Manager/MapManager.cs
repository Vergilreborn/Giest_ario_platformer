using MapEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MapEditor.Handlers;
using MapEditor.Objects;

namespace MapEditor.Manager
{
    class MapManager : IGameObject
    {

        public static MapManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MapManager();
                return instance;
            }
        }
        
        private static MapManager instance;
        public ContentManager Content;
        public GraphicsDevice Graphics;
        public Viewport Viewport;
        public Camera Cam;
        private ObjectSourceManager objectSourceManager;
        private Map map;

        public MapManager()
        {

        }

        public void Init()
        {
            map = new Map();
            objectSourceManager = new ObjectSourceManager();
            objectSourceManager.Init();
        }

        public void Load()
        {
            map.Load();
            objectSourceManager.Load();
        }

        public void Update(GameTime _gameTime)
        {
            map.Update(_gameTime);
            objectSourceManager.Update(_gameTime);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            
            objectSourceManager.Draw(_spriteBatch);
            map.Draw(_spriteBatch);
        }

        public void SetGraphicsDevice(GraphicsDevice _graphicsDevice)
        {
            this.Graphics = _graphicsDevice;
            
        }

        public void SetViewport(Viewport _viewport)
        {
            this.Viewport = _viewport;
            Cam = new Camera(_viewport);
        }

        public void SetContentManager(ContentManager _content)
        {
            this.Content = _content;
        }


        public Texture2D CreateColorTexture(int r, int g, int b, int a)
        {
            Texture2D rect = new Texture2D(this.Graphics, 1, 1);

            Color[] data = new Color[1];
            for (int i = 0; i < data.Length; ++i) data[i] = new Color(r, g, b, a);
            rect.SetData(data);

            return rect;

        }
    }
}
