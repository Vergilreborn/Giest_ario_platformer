using MapEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Enums;
using MapEditor.Helpers;

namespace MapEditor.Manager
{
    class CollisionTypeManager : IGameObject
    {

        private Texture2D emptyTexture;
       private TileType[] enumArray;
        private int tileSizeX;
        private int tileSizeY;
        private Vector2 position;
        

        public CollisionTypeManager()
        {

        }
        
        public void Init()
        {
            tileSizeX = 32;
            tileSizeY = 32;
            position = new Vector2(25, 600);
            enumArray = (TileType[])Enum.GetValues(typeof(TileType)); ;
        }

        public void Load()
        {
            emptyTexture= MapManager.Instance.CreateColorTexture(255, 255, 255, 255);
        }

        public void Update(GameTime _gameTime)
        {
            
        }
        
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(MapManager.Instance.DebugFont, "Collision Types",position - new Vector2(0,20), Color.White);
            for(var i = 0; i < enumArray.Length; i++)
            {
                Rectangle destination = new Rectangle((int)position.X, (int)position.Y
                        + (tileSizeY * i) + i, tileSizeX, tileSizeY);
                _spriteBatch.Draw(emptyTexture, destination, Constant.GetCollisionColor(enumArray[i]));
                SpriteBatchAssist.DrawBox(_spriteBatch, emptyTexture, destination);
            }
        }
    }
}
