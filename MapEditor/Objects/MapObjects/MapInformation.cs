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
        #endregion

        private int defaultWidth;
        private int defaultHeight;
        private int tileWidth;
        private int tileHeight;
        private Tile[,] tiles;
        private Vector2 playerPosition;

        public MapInformation()
        {

        }

        public void Init()
        {

            defaultWidth = 30;
            defaultHeight = 26;
            tileWidth = 32;
            tileHeight = 32;
            playerPosition = Vector2.Zero;

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
        }
    }
}
