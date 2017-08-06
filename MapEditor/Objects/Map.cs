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

namespace MapEditor.Objects
{
    class Map : IGameObject
    {

        private int defaultWidth;
        private int defaultHeight;
        private int tileWidth;
        private int tileHeight;
        private Texture2D texture;
        private Texture2D emptyBlockTexture;
        private Vector2 Position;
        private Cursor cursor;
        private bool inScreen;

        private Tile[,] tiles;

        private String debugString;
        

        public Map()
        {
            
        }

        public void Init()
        {
            defaultWidth = 32;
            defaultHeight = 26;
            tileWidth = 32;
            tileHeight = 32;
            Position = new Vector2(100, 25);
            tiles = new Tile[defaultWidth,defaultHeight];

            for(int x = 0; x < defaultWidth; x++)
            {
                for(int y = 0; y < defaultHeight; y++)
                {
                    //Initializing an empty array of screen width and height
                    Rectangle destination = new Rectangle(x * tileWidth,y * tileHeight, tileWidth, tileHeight);
                    tiles[x, y] = new Tile(destination, tileWidth, tileHeight);                    
                }
            }

            cursor = new Cursor();
            cursor.SetCursor("MapCursor");
        }

        public void SaveMap()
        {
            FileManager<Tile[,]>.SaveFile("Testing", tiles);
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
            int tileX = tilePositionCursor.X < 0 ? -1 : (int)(tilePositionCursor.X / tileWidth);
            int tileY = tilePositionCursor.Y < 0 ? -1 : (int)(tilePositionCursor.Y / tileHeight);

            inScreen = true;
            if (tileX >= defaultWidth || tileX < 0)
                inScreen = false;
            if (tileY >= defaultHeight || tileY < 0)
                inScreen = false;

            if(inScreen)
                cursor.SetPosition(tiles[tileX, tileY], Position);
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

        internal void SetTile(Tile selected)
        {
            if (inScreen)
            {
                cursor.Selected.SetTileType(selected.Type);
                cursor.Selected.SetSource(selected.Source);
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            for (int x = 0; x < defaultWidth; x++)
            {
                for (int y = 0; y < defaultHeight; y++)
                {
                    //Initializing an empty array of screen width and height
                    Rectangle drawDestination = new Rectangle();
                    drawDestination.X = tiles[x, y].Destination.X + (int)Position.X;
                    drawDestination.Y = tiles[x, y].Destination.Y + (int)Position.Y;
                    drawDestination.Width = tiles[x, y].Destination.Width;
                    drawDestination.Height = tiles[x, y].Destination.Height;



                    if (tiles[x, y].Type != TileType.None)
                    {
                        _spriteBatch.Draw(texture,drawDestination, tiles[x, y].Source, Color.White);
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
