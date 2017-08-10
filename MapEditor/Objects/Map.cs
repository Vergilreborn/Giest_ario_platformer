﻿using MapEditor.Interfaces;
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

namespace MapEditor.Objects
{
    class Map : IGameObject
    {

    
        private Texture2D texture;
        private Texture2D emptyBlockTexture;
        private Vector2 Position;
        private Cursor cursor;
        private bool inScreen;

        //private Tile[,] tiles;
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
            FileManager<MapInformation>.SaveFile("lvl","Giestario Levels", mapInfo);
        }

        public void LoadMap()
        {
            try
            {
                mapInfo = FileManager<MapInformation>.LoadFile("map", "Giestario Map");
            }catch(NoFileSelectedException e)
            {

            }
        }

        public void Load()
        {
            cursor.Load();
            texture = MapManager.Instance.Content.Load<Texture2D>("testTiles");
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

        public void SetTile(Tile selected)
        {
            if (inScreen)
            {
                cursor.Selected.SetTileType(selected.Type);
                cursor.Selected.SetSource(selected.Source);
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
                    drawDestination.X = mapInfo.Tiles[x, y].Destination.X + (int)Position.X;
                    drawDestination.Y = mapInfo.Tiles[x, y].Destination.Y + (int)Position.Y;
                    drawDestination.Width = mapInfo.Tiles[x, y].Destination.Width;
                    drawDestination.Height = mapInfo.Tiles[x, y].Destination.Height;



                    if (mapInfo.Tiles[x, y].Type != TileType.None)
                    {
                        _spriteBatch.Draw(texture,drawDestination, mapInfo.Tiles[x, y].Source, Color.White);
                    }
                    else
                    {
                        SpriteBatchAssist.DrawBox(_spriteBatch, emptyBlockTexture, drawDestination,.33f);
                    }
                }
            }

            cursor.Draw(_spriteBatch);
            _spriteBatch.DrawString(MapManager.Instance.DebugFont, debugString, new Vector2(0, 0), Color.AliceBlue);
        }
    }
}
