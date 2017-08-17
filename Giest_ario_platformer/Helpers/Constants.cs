using Giest_ario_platformer.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giest_ario_platformer.Helpers
{
    class Constants
    {
        public const float DEBUG_OPACITY = 0.33f;

        public static Color GetCollisionColor(TileType type)
        {
            switch (type)
            {
                case TileType.Block:
                    return Color.White * .50f;
                case TileType.Damage:
                    return Color.LightPink * .50f;
                case TileType.Death:
                    return Color.Red * .50f;
                case TileType.Water:
                    return Color.MediumAquamarine * .50f;
                case TileType.Ice:
                    return Color.Blue * .50f;
                default:
                    return Color.White * .25f;
            }
        }
    }
}
