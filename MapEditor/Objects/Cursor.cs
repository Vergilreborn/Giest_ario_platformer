using MapEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Manager;

namespace MapEditor.Objects
{
    class Cursor : IGameObject
    {


        private Vector2 Position;
        Texture2D texture;
        private Tile selected;

        //Rectangle selected= "";

        public Cursor()
        {

        }
        
        public void Init()
        {
            Position = Vector2.Zero;
        }

        public void Load()
        {
            texture = MapManager.Instance.Content.Load<Texture2D>("Cursor2");
        }

        public void Update(GameTime _gameTime)
        {
        
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, Position, Color.White);
          
        }

        public void SetPosition(Tile _tile, Vector2 _position)
        {
            selected = _tile;
            this.Position = _tile.Position + _position;
            
        }
    }
}
