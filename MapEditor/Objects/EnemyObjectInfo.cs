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

        private Rectangle source;
        private Rectangle dest;
        private EnemyType type;

        public EnemyObjectInfo()
        {

        }
    }
}
