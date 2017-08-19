using MapEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Manager;
using MapEditor.Objects.MapObjects;
using MapEditor.Enums;

namespace MapEditor.Objects
{
    class CollisionCursor : IGameObject
    {

        public CollisionTypeButton Selected
        {
            get
            {
                return selected;
            }
        }

        private Vector2 Position;
        private Texture2D texture;
        private CollisionTypeButton selected;
        private String cursor;

        public CollisionCursor()
        {
            this.cursor = "Cursor2";
        }

        public void SetCursor(String _cursor)
        {
            this.cursor = _cursor;
        }
        
        public void Init()
        {
            Position = Vector2.Zero;
        }

        public void Load()
        {
            texture = MapManager.Instance.Content.Load<Texture2D>(cursor);
        }

        public void Update(GameTime _gameTime)
        {
           
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, Position, Color.White);
          
        }

        public void SetPosition(CollisionTypeButton _tile)
        {
            selected = _tile;
            this.Position = _tile.Destination.Location.ToVector2();
            
        }
    }
}
