using Giest_ario_platformer.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giest_ario_platformer.GameObjects
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

        public Rectangle Destination;
        public Rectangle Source;

        private TileType type;

        private Vector2 position;
        private Vector2 tilePosition;


        public Tile(int x, int y, int tileWidth, int tileHeight = -1, TileType tileType = TileType.Block)
        {
            int tHeight = tileHeight == -1 ? tileWidth : tileHeight;
            position = new Vector2(x * tileWidth, y * tHeight );
            tilePosition = new Vector2(x, y);

            this.type = tileType;
            Source = new Rectangle(0, 0, tileWidth, tHeight);
            Destination = new Rectangle((int)position.X, (int)position.Y, tileWidth, tHeight);

        }
        
    }
}
