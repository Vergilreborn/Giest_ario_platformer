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
        private Texture2D enemyTexture;
        private Texture2D emptyBlockTexture;
        private Vector2 Position;
        private Cursor cursor;
        private bool inScreen;


        private int viewSizeStartX;
        private int viewSizeStartY;
        private int viewSizeX;
        private int viewSizeY;
        private float keyboardHoldTimer = 0f;
        private float changeTimer = 20f;
        private Rectangle mapBorder;

        private MapInformation mapInfo;
       
        
        private String debugString;
        

        public Map()
        {
            
        }

        public void Init()
        {
            //The viewing window sizes
            viewSizeStartX = 0;
            viewSizeStartY = 0;
            viewSizeX = 30;
            viewSizeY = 26;
            
            mapInfo = new MapInformation();
            mapInfo.Init();

            Position = new Vector2(240, 25);
            mapBorder = new Rectangle((int)Position.X, (int)Position.Y, viewSizeX * 32, viewSizeY * 32);
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
                MessageBox.Show("Alert:" + e.Message);
            }
        }

        public void Load()
        {
            cursor.Load();
            texture = MapManager.Instance.Content.Load<Texture2D>("testTiles");
            enemyTexture = MapManager.Instance.Content.Load<Texture2D>("enemySpriteSheet");
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

            keyboardHoldTimer += _gameTime.ElapsedGameTime.Milliseconds;
            bool checkDown= false;
            if (keyboardHoldTimer > changeTimer)
            {
                keyboardHoldTimer %= changeTimer;
                checkDown = true;
            }

            if (checkDown)
            {
                if (KeyboardManager.Instance.IsKeyActivity(Keys.A.ToString(), KeyActivity.Down))
                {
                    viewSizeStartX = Math.Max(viewSizeStartX - 1, 0);
                }
                if (KeyboardManager.Instance.IsKeyActivity(Keys.D.ToString(), KeyActivity.Down))
                {
                    viewSizeStartX = Math.Min(viewSizeStartX + 1, mapInfo.DefaultWidth - viewSizeX);
                    viewSizeStartX = Math.Max(viewSizeStartX , 0);
                }
                if (KeyboardManager.Instance.IsKeyActivity(Keys.W.ToString(), KeyActivity.Down))
                {
                    viewSizeStartY = Math.Max(viewSizeStartY - 1, 0);
                }
                if (KeyboardManager.Instance.IsKeyActivity(Keys.S.ToString(), KeyActivity.Down))
                {
                    viewSizeStartY = Math.Min(viewSizeStartY + 1, mapInfo.DefaultHeight - viewSizeY);
                    viewSizeStartY = Math.Max(viewSizeStartY, 0);
                }
            }

            if (tileX >= viewSizeX || tileX >= mapInfo.DefaultWidth || tileX < 0)
                inScreen = false;
            if (tileY >= viewSizeY || tileY >= mapInfo.DefaultHeight || tileY < 0)
                inScreen = false;

            if (inScreen)
                cursor.SetPosition(mapInfo.Tiles[viewSizeStartX +  tileX, viewSizeStartY + tileY],new Vector2(tileX * 32, tileY * 32), Position);
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

            _spriteBatch.Draw(emptyBlockTexture, mapBorder, Color.AliceBlue * .2f);
            SpriteBatchAssist.DrawBox(_spriteBatch, emptyBlockTexture, mapBorder,Color.Yellow);

            int showX = Math.Min(mapInfo.DefaultWidth, viewSizeStartX + viewSizeX);
            int showY = Math.Min(mapInfo.DefaultHeight, viewSizeStartY + viewSizeY);


            for (int x = viewSizeStartX; x < showX; x++)
            {
                for (int y = viewSizeStartY; y < showY; y++)
                {
                    //Initializing an empty array of screen width and height
                    Rectangle drawDestination = new Rectangle();
                    Tile tile= mapInfo.Tiles[x, y];

                    drawDestination.X = ((x-viewSizeStartX) * 32) + (int)Position.X;
                    drawDestination.Y = ((y - viewSizeStartY) * 32) + (int)Position.Y;
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
                int calcTileX = mapObj.DestinationBox.X / 32;
                int calcTileY = mapObj.DestinationBox.Y / 32;
                bool withinX = calcTileX > viewSizeStartX && calcTileX < showX;
                bool withinY = calcTileY > viewSizeStartY && calcTileY < showY;
                if (withinX && withinY)
                {
                    Rectangle drawRect = new Rectangle(mapObj.DestinationBox.X + (int)Position.X - (viewSizeStartX * 32), mapObj.DestinationBox.Y + (int)Position.Y - (viewSizeStartY * 32), mapObj.DestinationBox.Width, mapObj.DestinationBox.Height);
                    _spriteBatch.Draw(emptyBlockTexture, drawRect, mapObj.GetDrawColor());
                }
            }

            foreach(EnemyObjectInfo enm in mapInfo.EnemyObjects)
            {
                int calcTileX = enm.Destination.X / 32;
                int calcTileY = enm.Destination.Y / 32;
                bool withinX = calcTileX >= viewSizeStartX && calcTileX < showX;
                bool withinY = calcTileY >= viewSizeStartY && calcTileY < showY;
                if (withinX && withinY)
                {
                    Rectangle drawRect = new Rectangle(enm.Destination.X + (int)Position.X - (viewSizeStartX * 32), enm.Destination.Y + (int)Position.Y - (viewSizeStartY * 32), enm.Destination.Width, enm.Destination.Height);
                    _spriteBatch.Draw(enemyTexture, drawRect, enm.Source, Color.White);
                }
            }

            _spriteBatch.DrawString(MapManager.Instance.DebugFont, $"Tiles(x,y):{showX},{showY} out of (x,y):{mapInfo.DefaultWidth},{mapInfo.DefaultHeight}", new Vector2(750, 2), Color.Yellow);
            _spriteBatch.DrawString(MapManager.Instance.DebugFont, "Song:" + mapInfo.Music, Position - new Vector2(0, 20), Color.LightSkyBlue);

            Rectangle startPosition = mapInfo.Tiles[viewSizeStartX, viewSizeStartY].Destination;
            Rectangle endPosition = mapInfo.Tiles[showX-1, showY-1].Destination;

            if (new Rectangle(startPosition.X, startPosition.Y, endPosition.X + endPosition.Width, endPosition.Y + endPosition.Height).Contains(mapInfo.PlayerPosition))
            {
                _spriteBatch.Draw(texturePlayerPosition, Position + mapInfo.PlayerPosition - new Vector2(viewSizeStartX * 32, viewSizeStartY * 32), Color.White);
            }

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

        internal void SetEnemy(EnemyObjectInfo _enemyObjectInfo)
        {
            if (inScreen)
            {
                Point mousePosition = MouseManager.Instance.Position;

                Vector2 tilePositionCursor = mousePosition.ToVector2() - this.Position;
                int tileX = tilePositionCursor.X < 0 ? -1 : (int)(tilePositionCursor.X / mapInfo.TileWidth);
                int tileY = tilePositionCursor.Y < 0 ? -1 : (int)(tilePositionCursor.Y / mapInfo.TileHeight);

                Rectangle destination = new Rectangle((int)(tileX * mapInfo.TileWidth), (int)(tileY * mapInfo.TileHeight), _enemyObjectInfo.Source.Width, _enemyObjectInfo.Source.Height);
                _enemyObjectInfo.Destination = destination;
                mapInfo.AddEnemy(_enemyObjectInfo);
            }
        }

        internal void YSizeChange(int _amt)
        {

            mapInfo.YSizeChange(_amt);
            FixMapAdjustment();
        }

        internal void XSizeChange(int _amt)
        {
            mapInfo.XSizeChange(_amt);
            FixMapAdjustment();
        }

        private void FixMapAdjustment()
        {
            //viewSizeStartX = Math.Min(mapInfo.DefaultWidth - viewSizeX, viewSizeStartX );
            //viewSizeStartY = Math.Min(mapInfo.DefaultHeight - viewSizeY, viewSizeStartY );

            if(viewSizeStartX + viewSizeX > mapInfo.DefaultWidth)
            {
                viewSizeStartX = mapInfo.DefaultWidth - viewSizeX;
            }
            viewSizeStartX = Math.Max(0, viewSizeStartX);


            if (viewSizeStartY + viewSizeY > mapInfo.DefaultHeight)
            {
                viewSizeStartY = mapInfo.DefaultHeight - viewSizeY;
            }

            viewSizeStartY = Math.Max(0, viewSizeStartY);
        }
    }
}
