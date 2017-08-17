using MapEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Manager;
using MapEditor.Enums;
using MapEditor.Helpers;
using MapEditor.Exceptions;
using MapEditor.Objects.MapObjects;
using MapEditor.Forms;
using System.Windows.Forms;

namespace MapEditor.Objects
{
    class Map : IGameObject
    {


        private Texture2D texturePlayerPosition;

        private Texture2D texture;
        private Texture2D emptyBlockTexture;
        private Vector2 Position;
        private Cursor cursor;
        private bool inScreen;

        private MapInformation mapInfo;
       
        
        private String debugString;
        

        public Map()
        {
            
        }

        public void Init()
        {
           
            mapInfo = new MapInformation();
            mapInfo.Init();

            Position = new Vector2(200, 25);
            cursor = new Cursor();
            cursor.SetCursor("MapCursor");
        }

        public void SaveMap()
        {
            FileManager<MapInformation>.SaveFileGameObject(".gmap", "Map", mapInfo);
        }

        public void LoadMap()
        {
            try
            {
                mapInfo = FileManager<MapInformation>.LoadFileGameObject(".gmap", "Map");
            }catch(NoFileSelectedException e)
            {

            }
        }

        public void Load()
        {
            cursor.Load();
            texture = MapManager.Instance.Content.Load<Texture2D>("testTiles");
            texturePlayerPosition = MapManager.Instance.Content.Load<Texture2D>("StartPosition");

            emptyBlockTexture = MapManager.Instance.CreateColorTexture(150,150,150,255);

        }

        public void Update(GameTime _gameTime)
        {
            Point mousePosition = MouseManager.Instance.Position;
            Vector2 tilePositionCursor = mousePosition.ToVector2() - this.Position;
            int tileX = tilePositionCursor.X < 0 ? -1 : (int)(tilePositionCursor.X / mapInfo.TileWidth);
            int tileY = tilePositionCursor.Y < 0 ? -1 : (int)(tilePositionCursor.Y / mapInfo.TileHeight);

            inScreen = true;
            if (tileX >= mapInfo.DefaultWidth || tileX < 0)
                inScreen = false;
            if (tileY >= mapInfo.DefaultHeight || tileY < 0)
                inScreen = false;

            if(inScreen)
                cursor.SetPosition(mapInfo.Tiles[tileX, tileY], Position);
            debugString =$"X:{mousePosition.X} Y:{mousePosition.Y}{Environment.NewLine}Tpx:{tilePositionCursor.X} Tpy:{tilePositionCursor.Y}{Environment.NewLine}Tx:{tileX} Ty:{tileY}";

        }

        internal void ClearTile()
        {
            if (inScreen)
            {
                cursor.Selected.SetTileType(TileType.None);
                cursor.Selected.ClearSource();
            }
        }

        public void SetTile(Tile _selected)
        {
            if (inScreen)
            {
                cursor.Selected.SetSource(_selected.Source);
            }
        }

        public void SetPlayerPosition(Vector2 _position)
        {
            if (inScreen)
            {
                mapInfo.PlayerPosition = cursor.Selected.Position;
            }
        }

        public void SetTransition(Vector2 _position)
        {
            if (inScreen)
            {
                using (DropDownDialog mapSelected = new DropDownDialog(".gmap", "Map", "Selected Map:"))
                {
                    MapObject mObject = mapInfo.MapObjects.AsQueryable().Where(x => x.DestinationBox.Contains(_position - Position)).FirstOrDefault();

                    mapSelected.ShowDialog();
                    DialogResult result = mapSelected.DialogResult;
                    String results = mapSelected.GetCleanText();
                    if (result == DialogResult.OK && results != "")
                    {


                        Tile t = cursor.Selected;
                        if (mObject == null)
                        {
                            MapObject obj = new MapObject();
                            obj.AddTile(t);
                            obj.SetType(MapTypeObject.Transition);
                            obj.Data = results;
                            mapInfo.MapObjects.Add(obj);
                        }
                        else if (mObject.Type == MapTypeObject.Transition)
                        {
                            mObject.AddTile(t);
                            mObject.SetType(MapTypeObject.Transition);
                            mObject.Data = results;
                        }
                    }
                }
            }
        }

        public void SetCollisionType(TileType _type)
        {
            if (inScreen)
            {
                cursor.Selected.SetTileType(_type);
            }
        }

        public void Reset()
        {
            mapInfo.Reset();
           
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            for (int x = 0; x < mapInfo.DefaultWidth; x++)
            {
                for (int y = 0; y < mapInfo.DefaultHeight; y++)
                {
                    //Initializing an empty array of screen width and height
                    Rectangle drawDestination = new Rectangle();
                    Tile tile= mapInfo.Tiles[x, y];
                    drawDestination.X = mapInfo.Tiles[x, y].Destination.X + (int)Position.X;
                    drawDestination.Y = mapInfo.Tiles[x, y].Destination.Y + (int)Position.Y;
                    drawDestination.Width = mapInfo.Tiles[x, y].Destination.Width;
                    drawDestination.Height = mapInfo.Tiles[x, y].Destination.Height;

                    if(tile.Source != Rectangle.Empty)
                    {
                        _spriteBatch.Draw(texture, drawDestination, tile.Source, 
                                Color.White *(MapManager.Instance.ShowTypes != DrawType.Collision? 1f : .66f ));
                    }

                    SpriteBatchAssist.DrawBox(_spriteBatch, emptyBlockTexture, drawDestination, .33f);
                    if(tile.Type != TileType.None)
                        _spriteBatch.Draw(emptyBlockTexture, drawDestination, Constant.GetCollisionColor(tile.Type) * (MapManager.Instance.ShowTypes != DrawType.Tile ? 1f : .33f));
                                      
                }
            }

            foreach(MapObject mapObj in mapInfo.MapObjects){
                Rectangle drawRect = new Rectangle(mapObj.DestinationBox.X + (int)Position.X, mapObj.DestinationBox.Y + (int)Position.Y, mapObj.DestinationBox.Width, mapObj.DestinationBox.Height);
                _spriteBatch.Draw(emptyBlockTexture, drawRect, mapObj.GetDrawColor());
            }

            _spriteBatch.DrawString(MapManager.Instance.DebugFont, "Song:" + mapInfo.Music, Position - new Vector2(0, 20), Color.LightSkyBlue);


            _spriteBatch.Draw(texturePlayerPosition, Position + mapInfo.PlayerPosition, Color.White);

            cursor.Draw(_spriteBatch);
            _spriteBatch.DrawString(MapManager.Instance.DebugFont, debugString, new Vector2(0, 0), Color.AliceBlue);
        }

        internal void SetMusic()
        {
            using(DropDownDialog musicDialog = new DropDownDialog(".mp3","Music","Choose Music:"))
            {
                DialogResult result = musicDialog.ShowDialog();
                if(result == DialogResult.OK)
                {
                    mapInfo.Music = musicDialog.GetCleanText().Replace(".mp3","");
                }
            }
        }
    }
}
