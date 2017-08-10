using MapEditor.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Objects.MapObjects
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
            set
            {
                type = value;
            }
        }

        public Rectangle Source
        {
            get
            {
                return source;
            }
            set
            {
                source = value;
            }
        }

        public Rectangle Destination
        {
            get
            {
                return destination;
            }

            set
            {
                destination = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }
        #endregion

        private Rectangle source;
        private Rectangle destination;
        private TileType type;
        private Vector2 position;

        public Tile()
        {

        }

        public Tile(Rectangle _source, Rectangle _destination, TileType _type = TileType.Block)
        {
            this.position = new Vector2(_destination.X, _destination.Y);
            this.source = _source;
            this.destination = _destination;
            this.type = _type;
        }
        
        public Tile(Rectangle _destination, int _tileSizeX, int _tileSizeY)
        {
            this.position = new Vector2(_destination.X, _destination.Y);
            this.destination = _destination;
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
