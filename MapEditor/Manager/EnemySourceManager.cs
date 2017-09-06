using MapEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Helpers;

namespace MapEditor.Manager
{
    class EnemySourceManager : IGameObject
    {

        private Texture2D texture;
        private Texture2D emptyTexture;
        private Vector2 position;
        private Rectangle sourceWindow;
        private int viewSizeX;
        private int viewSizeY;
        private int viewStartX;
        private int viewStartY;
        private int tileSizePaddingX;
        private int tileSizePaddingY;

        private int tileSizeX;
        private int tileSizeY;

        public EnemySourceManager()
        {

        }

        public void Init()
        {

            this.tileSizeY = 32;
            this.tileSizeX = 32;
            this.tileSizePaddingX = 33;
            this.tileSizePaddingY = 33;
            this.viewStartX = 0;
            this.viewStartY = 0;
            this.viewSizeX = 6;
            this.viewSizeY = 10;

            position = new Vector2(15, 400);
            sourceWindow = new Rectangle((int)position.X, (int)position.Y, tileSizePaddingX* viewSizeX, tileSizePaddingY* viewSizeY);

        }

        public void Load()
        {
            emptyTexture = MapManager.Instance.CreateColorTexture(255, 255, 255, 255);
        }

        public void Update(GameTime _gameTime)
        {
           
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(emptyTexture, sourceWindow, Color.AliceBlue * .10f);
            SpriteBatchAssist.DrawBox(_spriteBatch, emptyTexture, sourceWindow, Color.Red, 2);
            
        }
    }
}
