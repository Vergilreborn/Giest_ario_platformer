using MapEditor.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Objects.MapObjects
{
    class MapObject
    {
        #region getters/setters
        public MapTypeObject Type
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

        public Rectangle DestinationBox
        {
            get
            {
                return new Rectangle((int)location.X, (int)location.Y, width, height);
            }
            set
            {
                if(location != null)
                {
                    location.X = value.X;
                    location.Y = value.Y;
                }
                else
                {
                    location = new Vector2(location.X, location.Y);
                }
                width = value.Width;
                height = value.Height;
            }
        }
        public String Data
        {
            get
            {
                return data;
            }
            set
            {
                data = value;   
            }
        }
        #endregion

        private String data;
        private MapTypeObject type;
        private Vector2 location;
        private int width;
        private int height;
        private Color drawColor;

        public MapObject()
        {
            type = MapTypeObject.Empty;
            location = Vector2.Zero;
            width = 0;
            height = 0;
        }

        public void SetType(MapTypeObject _type)
        {
            this.type = _type;
        }

        public void SetCollisionArea(Rectangle _collision)
        {
            this.DestinationBox = _collision;
        }

        public void SetDrawColor(Color _color)
        {
            this.drawColor = _color;
        }

        public Color GetDrawColor()
        {
            return drawColor;
        }
    }
}
