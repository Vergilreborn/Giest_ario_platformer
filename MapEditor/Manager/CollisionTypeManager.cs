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
using MapEditor.Objects.MapObjects;

namespace MapEditor.Manager
{
    class CollisionTypeManager : IGameObject
    {

        private Texture2D emptyTexture;
        private TileType[] enumArray;
        private int tileSizeX;
        private int tileSizeY;
        private Vector2 position;
        List<CollisionTypeButton> colButtons;
        

        public CollisionTypeManager()
        {

        }
        
        public void Init()
        {
            colButtons = new List<CollisionTypeButton>();
            tileSizeX = 32;
            tileSizeY = 32;
            position = new Vector2(25, 600);
            enumArray = (TileType[])Enum.GetValues(typeof(TileType));
            for(int i = 0;i< enumArray.Length; i++)
            {
                Rectangle destination = new Rectangle((int)position.X, (int)position.Y
                      + (tileSizeY * i) + i, tileSizeX, tileSizeY);
                colButtons.Add(new CollisionTypeButton(destination, enumArray[i]));
            }
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
            foreach(CollisionTypeButton colButton in colButtons)
             {
                _spriteBatch.Draw(emptyTexture, colButton.Destination, Constant.GetCollisionColor(colButton.Type));
                _spriteBatch.DrawString(MapManager.Instance.DebugFont, colButton.Type.ToString(), new Vector2(colButton.Destination.Right + 5, colButton.Destination.Top + 5), Color.White);
                SpriteBatchAssist.DrawBox(_spriteBatch, emptyTexture, colButton.Destination);
            }
        }
    }
}
