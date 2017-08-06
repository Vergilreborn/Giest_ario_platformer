using MapEditor.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Objects
{
    class Tile 
    {
        #region Getters
        public TileType Type
        {
            get
            {
                return type; 
            }
        }

        public Rectangle Source
        {
            get
            {
                return source;
            }
        }

        public Rectangle Destination
        {
            get
            {
                return destination;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }
        #endregion

        private Rectangle source;
        private Rectangle destination;
        private TileType type;
        private int tileSizeX;
        private int tileSizeY;
        private Vector2 position;

        public Tile(Rectangle _source, Rectangle _destination, TileType _type = TileType.Block)
        {
            this.position = new Vector2(_destination.X, _destination.Y);
            this.source = _source;
            this.destination = _destination;
            this.tileSizeX = _destination.Width;
            this.tileSizeY = _destination.Height;
            this.type = _type;
        }
        
        public Tile(Rectangle _destination, int _tileSizeX, int _tileSizeY)
        {
            this.position = new Vector2(_destination.X, _destination.Y);
            this.destination = _destination;
            this.tileSizeX = _tileSizeX;
            this.tileSizeY = _tileSizeY;
            this.type = TileType.None;
        }
        
        public void SetSource(Rectangle _source)
        {
            this.source = _source;
        }
        
        public void SetTileType(TileType _type)
        {
            this.type = _type;
        }

        public void ClearSource()
        {
            source = new Rectangle();
        }
    }
}
