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

        public TileType Type
        {
            get
            {
                return type;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public Rectangle Destination;
        public Rectangle Source;

        private TileType type;

        private Vector2 position;
        private Vector2 tilePosition;

        public Tile(int _posX, int _posY, int _tileWidth, int _tileHeight)
        {
            position = new Vector2(_posX, _posY);
            Destination = new Rectangle(_posX, _posY, _tileWidth, _tileHeight);
        }

        public void SetTileType(TileType _tileType)
        {
            this.type = _tileType;
        }

        public Tile(int x, int y, int tileWidth, int tileHeight = -1, TileType tileType = TileType.Block)
        {
            int tHeight = tileHeight == -1 ? tileWidth : tileHeight;
            position = new Vector2(x * tileWidth, y * tHeight );
            tilePosition = new Vector2(x, y);

            this.type = tileType;
            Source = new Rectangle(0, 0, tileWidth, tHeight);
            Destination = new Rectangle((int)position.X, (int)position.Y, tileWidth, tHeight);

        }

        internal void SetSource(int x, int y, int moveDistanceX, int moveDistanceY)
        {
            Source = new Rectangle(x, y , moveDistanceX, moveDistanceY);
        }
    }
}
