using MapEditor.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Helpers
{
    class Constant
    {
        public static Color GetCollisionColor(TileType type)
        {
            switch (type)
            {
                case TileType.Block:
                    return Color.White * .50f;
                case TileType.Damage:
                    return Color.LightPink* .50f;
                case TileType.Death:
                    return Color.Red * .50f;
                case TileType.Ice:
                    return Color.Blue * .50f;
                default:
                    return Color.White * .25f;
            }
        } 
    }
}
