using MapEditor.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Objects.MapObjects
{
    class CollisionTypeButton
    {
        #region Getters/Setters
        public TileType Type
        {
            get
            {
                return type;
            }
        }

        public Rectangle Destination
        {
            get
            {
                return destination;
            }
        }
        #endregion

        private TileType type;
        private Rectangle destination;

        public CollisionTypeButton(Rectangle _destination, TileType _type)
        {
            this.destination = _destination;
            this.type = _type;
        }
    }
}
