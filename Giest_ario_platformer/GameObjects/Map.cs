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
using Giest_ario_platformer.GameObjects.MapObjects;

namespace Giest_ario_platformer.GameObjects
{
    class Map : IGameObject
    {
        private MapInformation mapInfo;
        private int tileSize;
        private Texture2D texture;

        private Texture2D emptyBlockTexture;
        private Vector2 widthHeight;
        private Rectangle boundary;

        public Vector2 PlayerPosition
        {
            get
            {
                return mapInfo.PlayerPosition;
            }
        }
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
            
            mapInfo = FileManager<MapInformation>.LoadFile(@"Content\Map\" + _file);
            emptyBlockTexture = GameManager.Instance.CreateColorTexture(255, 255, 255, 255);
            this.tileSize = 32;
            widthHeight = new Vector2(mapInfo.Tiles.GetLength(0), mapInfo.Tiles.GetLength(1));
            boundary = new Rectangle(0, 0, (int)(widthHeight.X * tileSize), (int)(widthHeight.Y * tileSize));

            MusicManager.Instance.PlaySong(mapInfo.Music);
        }

        public Rectangle GetBoundary()
        {
            return boundary;
        }

        public void Dispose()
        {
            texture.Dispose();
            emptyBlockTexture.Dispose();
        }

        public void Init()
        {
            LoadTestMap("Testing1.gmap");
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
                    if (mapInfo.Tiles[x, y].Type != TileType.None)
                        _spriteBatch.Draw(texture, mapInfo.Tiles[x, y].Destination, mapInfo.Tiles[x, y].Source, Color.White);
                }
            }


            foreach (MapObject mapObj in mapInfo.MapObjects)
            {
                Rectangle drawRect = new Rectangle(mapObj.DestinationBox.X, mapObj.DestinationBox.Y, mapObj.DestinationBox.Width, mapObj.DestinationBox.Height);
                _spriteBatch.Draw(emptyBlockTexture, drawRect, mapObj.GetDrawColor());
            }
           
        }

        public List<MapObject> GetMapObjects()
        {
            return mapInfo.MapObjects;
        }

        public Tile GetTile(int x, int y)
        {
            return mapInfo.Tiles[x, y];
        }
    }
}
