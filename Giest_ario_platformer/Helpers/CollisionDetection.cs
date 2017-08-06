using Giest_ario_platformer.Enums;
using Giest_ario_platformer.GameObjects;
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
        public static bool IsColliding(Map _map, Rectangle CollisionBox, bool isPositive ,bool _isHor, out float _newValue)
        {
            _newValue = 0f;
            int tileSize = _map.GetTileSizes();
            
            int playerStartPosX = Math.Max((int)CollisionBox.X / tileSize, 0);
            int playerStartPosY = Math.Max((int)CollisionBox.Y / tileSize, 0);
            int playerEndPosX = Math.Min(((int)CollisionBox.X + CollisionBox.Width) / tileSize, (int)_map.GetWidthHeight().X - 1);
            int playerEndPosY = Math.Min(((int)CollisionBox.Y + CollisionBox.Height) / tileSize, (int)_map.GetWidthHeight().Y - 1);


            for (int x = playerStartPosX; x <= playerEndPosX; x++)
            {
                for (int y = playerStartPosY; y <= playerEndPosY; y++)
                {
                    Tile tile = _map.GetTile(x, y);
                    if (tile != null && tile.Type != TileType.None)
                    {
                        if (tile.Destination.Intersects(CollisionBox))
                        {
                            _newValue = isPositive ? (_isHor ? tile.Destination.X - CollisionBox.Width : tile.Destination.Y - CollisionBox.Height) : 
                                                     (_isHor ? (tile.Destination.X + tile.Destination.Width) : 
                                                                (tile.Destination.Y + tile.Destination.Height ));
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
