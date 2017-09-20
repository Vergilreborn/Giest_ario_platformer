using MapEditor.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Objects
{
    class EnemyObjectInfo
    {
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
                return dest;
            }
            set
            {
                dest = value;
            }
        }

        public EnemyType Type
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

        public string Attr {
            get
            {
                return attr;
            }
            set
            {
                attr = value;
            }
        }


        private String attr;
        private Rectangle source;
        private Rectangle dest;
        private EnemyType type;

        public EnemyObjectInfo()
        {

        }

        internal EnemyObjectInfo Clone()
        {
            EnemyObjectInfo newObject = new EnemyObjectInfo();
            newObject.source = new Rectangle(this.source.X, this.source.Y, this.source.Width, this.Source.Height);
            newObject.Type = type;
            newObject.attr = attr;
            return newObject;
        }
    }
}
