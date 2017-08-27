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
                mapObjects = value;
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

        #endregion
        private String music;
        private int defaultWidth;
        private int defaultHeight;
        private int tileWidth;
        private int tileHeight;
        private int viewSizeStartX;
        private int viewSizeStartY;
        private int viewSizeX;
        private int viewSizeY;
        private Tile[,] tiles;
        private List<MapObject> mapObjects;
        private Vector2 playerPosition;

        public MapInformation()
        {
            tiles = new Tile[0, 0];
            music = "";
            mapObjects = new List<MapObject>();
        }

        public void Init()
        {
            //The viewing window sizes
            viewSizeStartX = 0;
            viewSizeStartY = 0;
            viewSizeX = 10;
            viewSizeY = 10;
            //mapWidth and height
            defaultWidth = 30; 
            defaultHeight = 26;
            tileWidth = 32;
            tileHeight = 32;
            playerPosition = Vector2.Zero;
            mapObjects = new List<MapObject>();
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

        internal void YSizeChange(int amt)
        {
            int newHeight = defaultHeight + amt;
            if (newHeight > 1)
            {
                tiles = copyArray(tiles, defaultWidth, defaultHeight, defaultWidth, defaultHeight + amt);
                defaultHeight += amt;
            }
        }

        internal void XSizeChange(int amt)
        {
            int newWidth = defaultWidth + amt;
            if (newWidth > 1)
            {
                tiles = copyArray(tiles, defaultWidth, defaultHeight, defaultWidth + amt, defaultHeight);
                defaultWidth += amt;
            }
        }

        private Tile[,] copyArray(Tile[,] source, int currWidth, int currHeigh, int newWidth, int newHeight)
        {
            Tile[,] newArray = new Tile[newWidth, newHeight];
            for(int x = 0; x < currWidth; x++)
            {
                for(int y = 0; y < currHeigh; y++)
                {
                    if (x < newWidth && y < newHeight)
                    {
                        newArray[x, y] = source[x, y];
                    }
                }
            }
            return newArray;
        }
    }
}
