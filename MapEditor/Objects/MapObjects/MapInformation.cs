using MapEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Objects.MapObjects
{
    class MapInformation
    {
    #region Getters/Setters
        public int DefaultWidth
        {
            get
            {
                return defaultWidth;
            }
            set
            {
                defaultWidth = value;
            }
        }

        public int DefaultHeight
        {
            get
            {
                return defaultHeight;
            }
            set
            {
                defaultHeight = value;
            }
        }

        public int TileWidth
        {
            get
            {
                return tileWidth;
            }
            set
            {
                tileWidth = value;
            }
        } 

        public int TileHeight
        {
            get
            {
                return tileHeight;
            }
            set
            {
                tileHeight = value;
            }
        }

        public Tile[,] Tiles
        {
            get
            {
                return tiles;
            }
            set
            {
                tiles = value;
            }
        }

        public Vector2 PlayerPosition
        {
            get
            {
                return playerPosition;
            }
            set
            {
                playerPosition = value;
            }
        }

        public List<MapObject> MapObjects
        {
            get
            {
                return mapObjects;
            }
            set
            {
                if (value == null)
                    mapObjects = new List<MapObject>();
                
            }
        }

        public string Music
        {
            get
            {
                return music;
            }
            set
            {
                music = value;
            }
        }

        public List<EnemyObjectInfo> EnemyObjects
        {
            get
            {
                return enemyObjects;
            }
            set
            {
                if(value == null)
                
                    enemyObjects = new List<EnemyObjectInfo>();
            }
        }

        #endregion
        private String music;
        private int defaultWidth;
        private int defaultHeight;
        private int tileWidth;
        private int tileHeight;

        private Tile[,] tiles;
        private List<MapObject> mapObjects;
        private List<EnemyObjectInfo> enemyObjects;

        private Vector2 playerPosition;

        public MapInformation()
        {
            tiles = new Tile[0, 0];
            music = "";
            mapObjects = new List<MapObject>();
            enemyObjects = new List<EnemyObjectInfo>();
        }

        public void Init()
        {
            
            //mapWidth and height
            defaultWidth = 30; 
            defaultHeight = 26;
            tileWidth = 32;
            tileHeight = 32;
            playerPosition = Vector2.Zero;
            mapObjects = new List<MapObject>();
            enemyObjects = new List<EnemyObjectInfo>();
            tiles = new Tile[defaultWidth, defaultHeight];

            for (int x = 0; x < defaultWidth; x++)
            {
                for (int y = 0; y < defaultHeight; y++)
                {
                    //Initializing an empty array of screen width and height
                    Rectangle destination = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                    tiles[x, y] = new Tile(destination, tileWidth, tileHeight);
                }
            }

        }
        
        public void Reset()
        {
            tiles = new Tile[defaultWidth, defaultHeight];

            for (int x = 0; x < defaultWidth; x++)
            {
                for (int y = 0; y < defaultHeight; y++)
                {
                    //Initializing an empty array of screen width and height
                    Rectangle destination = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                    tiles[x, y] = new Tile(destination, tileWidth, tileHeight);
                }
            }
            mapObjects.Clear();
            playerPosition = tiles[0, 0].Position;
        }

        internal void YSizeChange(int _amt)
        {
            int newHeight = defaultHeight + _amt;
            if (newHeight > 1)
            {
                tiles = copyArray(tiles, defaultWidth, defaultHeight, defaultWidth, defaultHeight + _amt);
                defaultHeight += _amt;
            }
        }

        internal void XSizeChange(int _amt)
        {
            int newWidth = defaultWidth + _amt;
            if (newWidth > 1)
            {
                tiles = copyArray(tiles, defaultWidth, defaultHeight, defaultWidth + _amt, defaultHeight);
                defaultWidth += _amt;
            }
        }

        private Tile[,] copyArray(Tile[,] _source, int _currWidth, int _currHeigh, int _newWidth, int _newHeight)
        {
            Tile[,] newArray = new Tile[_newWidth, _newHeight];
            for(int x = 0; x < _newWidth; x++)
            {
                for(int y = 0; y < _newHeight; y++)
                {
                    if (x < _currWidth && y < _currHeigh)
                    {
                        newArray[x, y] = _source[x, y];
                    }
                    else
                    {
                        Rectangle destination = new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
                        newArray[x, y] = new Tile(destination, tileWidth, tileHeight);
                    }
                }
            }
            return newArray;
        }

        internal void AddEnemy(EnemyObjectInfo _enemyObjectInfo)
        {
            enemyObjects.Add(_enemyObjectInfo);
        }
    }
}
