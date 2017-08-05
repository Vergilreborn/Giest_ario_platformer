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
    class Map : IGameObject
    {

        private int defaultWidth;
        private int defaultHeight;
        private int tileWidth;
        private int tileHeight;
        private Texture2D texture;
        Tile[,] tiles;

        public Map()
        {
            
        }

        public void Init()
        {
            defaultWidth = 40;
            defaultHeight = 30;
            tileWidth = 32;
            tileHeight = 32;
            tiles = new Tile[20,30];
        }

        public void Load()
        {
            texture = MapManager.Instance.Content.Load<Texture2D>("testTiles");
        }

        public void Update(GameTime _gameTime)
        {
           
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
           
        }
    }
}
