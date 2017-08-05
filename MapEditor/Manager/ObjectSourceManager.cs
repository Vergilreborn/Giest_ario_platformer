using MapEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Objects;
using MapEditor.Helpers;

namespace MapEditor.Manager
{
    class ObjectSourceManager : IGameObject
    {

        private Texture2D texture;
        private int tileWidth;
        private int tileHeight;
        private int moveDistanceX;
        private int moveDistanceY;
    

        private Cursor cursor;
        private Tile[,] tiles;
        private Vector2 position;
        private int xTiles;
        private int yTiles;
        private int tileDistanceX;
        private int tileDistanceY;
        private int tilePadding;

        private Texture2D debugTexture;
        
        public ObjectSourceManager()
        {

        }

        public void Init()
        {
            cursor = new Cursor();
            this.tilePadding = 1;
            this.tileWidth = 32;
            this.tileHeight = 32;
            this.tileDistanceX = 33;
            this.tileDistanceY = 33;

            position = new Vector2(25, 25);
           
        }

        public void Load()
        {
            texture = MapManager.Instance.Content.Load<Texture2D>("testTiles");
            debugTexture = MapManager.Instance.CreateColorTexture(255, 0, 0, 255);
            xTiles = texture.Width / (tileWidth  + tilePadding);
            yTiles = texture.Width / (tileHeight + tilePadding);
            tiles = new Tile[xTiles, yTiles];

            for (int x = 0; x < xTiles; x++)
            {
                for (int y = 0; y < yTiles; y++)
                {
                    tiles[x, y] = new Tile(x * tileDistanceX, y * tileDistanceY, tileWidth,tileHeight);
                    tiles[x, y].SetSource(x * tileDistanceX + tilePadding, y * tileDistanceY + tilePadding, tileWidth,tileHeight);
                }
            }

            cursor.Load();
            cursor.SetPosition(tiles[0, 0],position);

        }

        public void Update(GameTime _gameTime)
        {
            
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            
            for(int x = 0; x < xTiles; x++)
            {
                for(int y = 0; y < yTiles; y++)
                {
                    Tile tile = tiles[x, y];
                    if (tile != null)
                    {
                        Vector2 tilePosition = new Vector2(position.X + tile.Destination.X, position.Y + tile.Destination.Y);
                        _spriteBatch.Draw(texture, tilePosition,tile.Source, Color.White);
                    }
                }
            }
            for (int x = 0; x < xTiles; x++)
            {
                for (int y = 0; y < yTiles; y++)
                {
                    Tile tile = tiles[x, y];
                    if (tile != null)
                    {
                        Vector2 tilePosition = new Vector2(position.X + tile.Destination.X, position.Y + tile.Destination.Y);
                        Rectangle draw = new Rectangle((int)tilePosition.X, (int)tilePosition.Y, tile.Source.Width, tile.Source.Height);
                        SpriteBatchAssist.DrawBox(_spriteBatch, debugTexture, draw);
                    }
                }
            }

            cursor.Draw(_spriteBatch);
        }
    }
}
