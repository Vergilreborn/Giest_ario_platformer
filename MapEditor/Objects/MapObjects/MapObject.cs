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

        public List<Tile> Tiles
        {
            get
            {
                return tilesStored;
            }
            set
            {
                tilesStored = value;
            }
        }

        #endregion

        private String data;
        private MapTypeObject type;
        private Vector2 location;
        private int width;
        private int height;
        private List<Tile> tilesStored;

        public MapObject()
        {
            type = MapTypeObject.Empty;
            tilesStored = new List<Tile>();
            location = Vector2.Zero;
            width = 0;
            height = 0;
        }

        public void AddTile(Tile _tile)
        {
            Tile t = tilesStored.AsQueryable().Where(x => x.Position == _tile.Position).FirstOrDefault();
            if(t == null)
            {
                tilesStored.Add(_tile);
            }

            updateDestinationBox();
        }

        public void RemoveTile(Vector2 _position)
        {
            Tile t = tilesStored.AsQueryable().Where(x => x.Destination.Contains(_position)).FirstOrDefault();
            if (t == null)
            {
                tilesStored.Remove(t);
            }
            updateDestinationBox();
        }

        public void SetType(MapTypeObject _type)
        {
            this.type = _type;
        }

        public void SetCollisionArea(Rectangle _collision)
        {
            this.DestinationBox = _collision;
        }

        public Color GetDrawColor()
        {
            switch (type)
            {
                case MapTypeObject.Checkpoint:
                    return Color.AntiqueWhite * .60f;
                case MapTypeObject.EndLocation:
                    return Color.LightSeaGreen * .60f;
                case MapTypeObject.Enemy:
                    return Color.PaleVioletRed * .60f;
                case MapTypeObject.Item:
                    return Color.BlueViolet * .60f;
                case MapTypeObject.Transition:
                    return Color.LightGoldenrodYellow * .60f;
                case MapTypeObject.MoveableWall:
                    return Color.PeachPuff * .60f;
                default:
                    return Color.Magenta * .80f;
            }
        }

        private void updateDestinationBox()
        {
            int bottom = 0;
            int right = 0;
            bool firstSet = true;

            foreach (Tile t in tilesStored)
            {
                location.X = firstSet ? (int)t.Destination.X : Math.Min((int)location.X, (int)t.Destination.X);
                location.Y = firstSet ? (int)t.Destination.Y : Math.Min((int)location.Y, (int)t.Destination.Y);
                right = firstSet ? (int)t.Destination.Right : Math.Max(width, (int)t.Destination.Right);
                bottom = firstSet ? (int)t.Destination.Bottom : Math.Max(height, (int)t.Destination.Bottom);
                firstSet = false;
            }

            width = right - (int)location.X;
            height = bottom - (int)location.Y;
        }
    }
}
