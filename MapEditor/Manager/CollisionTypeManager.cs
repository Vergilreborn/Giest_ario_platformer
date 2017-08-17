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
using MapEditor.Objects;

namespace MapEditor.Manager
{
    class CollisionTypeManager : IGameObject
    {

        public CollisionCursor Cursor
        {
            get
            {
                return cursor;
            }
        }

        private Texture2D emptyTexture;
        private TileType[] enumArray;
        private int tileSizeX;
        private int tileSizeY;
        private Vector2 position;
        private List<CollisionTypeButton> colButtons;
        private CollisionCursor cursor;
        

        public CollisionTypeManager()
        {

        }
        
        public void Init()
        {
            cursor = new CollisionCursor();
            cursor.Init();
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
            cursor.SetPosition(colButtons[0]);
        }

        public void Load()
        {
            cursor.Load();
            emptyTexture= MapManager.Instance.CreateColorTexture(255, 255, 255, 255);
        }

        public void Update(GameTime _gameTime)
        {
            if (MouseManager.Instance.IsKeyActivity(true, KeyActivity.Pressed))
            {
                Vector2 mousePosition = MouseManager.Instance.Position.ToVector2();
                foreach (CollisionTypeButton colButton in colButtons)
                {
                    if (colButton.Destination.Contains(mousePosition))
                        cursor.SetPosition(colButton);
                }
            }
        }
        
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(MapManager.Instance.DebugFont, "Collision Types",position - new Vector2(0,20), Color.White);
            foreach(CollisionTypeButton colButton in colButtons)
             {
                _spriteBatch.Draw(emptyTexture, colButton.Destination, Constant.GetCollisionColor(colButton.Type));
                _spriteBatch.DrawString(MapManager.Instance.DebugFont, colButton.Type.ToString(), new Vector2(colButton.Destination.Right + 5, colButton.Destination.Top + 5), Color.White);
                cursor.Draw(_spriteBatch);
                SpriteBatchAssist.DrawBox(_spriteBatch, emptyTexture, colButton.Destination);
            }
        }

        internal Texture2D getTexture()
        {
            return emptyTexture;
        }
    }
}
