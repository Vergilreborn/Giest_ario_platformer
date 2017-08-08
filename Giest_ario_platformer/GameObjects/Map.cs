using Giest_ario_platformer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Giest_ario_platformer.Managers;
using Giest_ario_platformer.Helpers;
using Giest_ario_platformer.Enums;

namespace Giest_ario_platformer.GameObjects
{
    class Map : IGameObject
    {
        private Tile[,] tiles;
        private int tileSize;
        private Texture2D texture;
        private Vector2 widthHeight;

        public int MapHeight
        {
            get
            {
                return (int)(widthHeight.Y * tileSize);
            }
        }

        public int MapWidth
        {
            get
            {
                return (int)(widthHeight.X * tileSize);
            }
        }

        public Map()
        {

        }

        public void LoadTestMap(String _file)
        {
            this.tileSize = 32;
            tiles = FileManager<Tile[,]>.LoadFile(_file);
            widthHeight = new Vector2(tiles.GetLength(0), tiles.GetLength(1));
        }

        public void LoadTestMap()
        {
            //widthHeight = new Vector2(80, 50);
            
            //tiles = new Tile[(int)widthHeight.X,(int) widthHeight.Y];
            //for(int x = 5; x < widthHeight.X; x++)
            //{
            //        tiles[x, 10] = new Tile(x, 10, tileSize);
            //}
            //for (int x = 0; x < widthHeight.X-10; x++)
            //{
            //    tiles[x, 15] = new Tile(x, 15, tileSize);
            //}

            //for (int x = 10; x < widthHeight.X - 10; x++)
            //{
            //    tiles[x, 20] = new Tile(x, 20, tileSize);
            //}

            //tiles[2, 8] = new Tile(2, 8, tileSize);
            //tiles[3, 7] = new Tile(3, 7, tileSize);
            //tiles[8, 9] = new Tile(8, 9, tileSize);
            //tiles[9, 9] = new Tile(9, 9, tileSize);
            //tiles[11, 9] = new Tile(11, 9, tileSize);
            //tiles[14, 9] = new Tile(14, 9, tileSize);
            //tiles[20, 9] = new Tile(20, 9, tileSize);
        }

        internal void Dispose()
        {
            texture.Dispose();
        }

        public void Init()
        {
            LoadTestMap("Content/Map/Testing.lvl");
        }

        internal int GetTileSizes()
        {
            return tileSize;
        }

        internal Vector2 GetWidthHeight()
        {
            return widthHeight;
        }

        public void Load()
        {
            texture = GameManager.Instance.Content.Load<Texture2D>("testTiles");
        }

        public void Update(GameTime _gameTime)
        {
            
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            for (int x = 0; x < widthHeight.X; x++)
            {
                for (int y = 0; y < widthHeight.Y; y++)
                {
                    if (tiles[x, y].Type != TileType.None)
                        _spriteBatch.Draw(texture, tiles[x, y].Destination, tiles[x, y].Source, Color.White);
                }
            }

        }

        internal Tile GetTile(int x, int y)
        {
            return tiles[x, y];
        }
    }
}
