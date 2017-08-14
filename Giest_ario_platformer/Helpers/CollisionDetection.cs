using Giest_ario_platformer.Enums;
using Giest_ario_platformer.GameObjects;
using Giest_ario_platformer.GameObjects.MapObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giest_ario_platformer.Helpers
{
    class CollisionDetection
    {
        public static bool IsColliding(Map _map, Rectangle _collisionBox, bool _isPositive ,bool _isHor, out float _newValue)
        {
            _newValue = 0f;
            int tileSize = _map.GetTileSizes();
            
            int playerStartPosX = Math.Max((int)_collisionBox.X / tileSize, 0);
            int playerStartPosY = Math.Max((int)_collisionBox.Y / tileSize, 0);
            int playerEndPosX = Math.Min(((int)_collisionBox.X + _collisionBox.Width) / tileSize, (int)_map.GetWidthHeight().X - 1);
            int playerEndPosY = Math.Min(((int)_collisionBox.Y + _collisionBox.Height) / tileSize, (int)_map.GetWidthHeight().Y - 1);


            for (int x = playerStartPosX; x <= playerEndPosX; x++)
            {
                for (int y = playerStartPosY; y <= playerEndPosY; y++)
                {
                    Tile tile = _map.GetTile(x, y);
                    if (tile != null && tile.Type != TileType.None)
                    {
                        if (tile.Destination.Intersects(_collisionBox))
                        {
                            _newValue = _isPositive ? (_isHor ? tile.Destination.X - _collisionBox.Width : tile.Destination.Y - _collisionBox.Height) : 
                                                     (_isHor ? (tile.Destination.X + tile.Destination.Width) : 
                                                                (tile.Destination.Y + tile.Destination.Height ));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public static MapObject IsCollidingObjects(Map _map, Rectangle _collisionBox)
        {
            foreach(MapObject gObject in _map.GetMapObjects())
            {
                foreach(Tile t in gObject.Tiles)
                {
                    if (t.Destination.Intersects(_collisionBox))
                    {
                        return gObject;
                    }
                }
            }
            return null;
        }
    }
}
