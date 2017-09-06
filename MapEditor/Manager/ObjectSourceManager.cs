using MapEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Objects;
using MapEditor.Helpers;
using MapEditor.Enums;
using MapEditor.Objects.MapObjects;
using Microsoft.Xna.Framework.Input;

namespace MapEditor.Manager
{
    class ObjectSourceManager : IGameObject
    {

        public Cursor Cursor
        {
            get
            {
                return cursor;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }


        private Texture2D emptyTexture;
        private Texture2D texture;
        private Cursor cursor;
        private Tile[,] tiles;
        private Vector2 position;

        private int xTiles;
        private int yTiles;

        private int tileWidth;
        private int tileHeight;
        private int tileDistanceX;
        private int tileDistanceY;
        private int tilePadding;

        private int viewStartX;
        private int viewStartY;
        private int viewSizeX;
        private int viewSizeY;

        private Texture2D debugTexture;
        private Rectangle sourceManagerWindow;
        
        public ObjectSourceManager()
        {

        }

        public void Init()
        {
            cursor = new Cursor();

            this.tilePadding = 1;
            this.tileWidth = 32;
            this.tileHeight = 32;
            this.tileDistanceX = 33;
            this.tileDistanceY = 33;
            this.viewStartX = 0;
            this.viewStartY = 0;
            this.viewSizeX = 6;
            this.viewSizeY = 10;

            position = new Vector2(15, 25);

            sourceManagerWindow = new Rectangle((int)position.X, (int)position.Y, viewSizeX * tileDistanceX, viewSizeY * tileDistanceY);
        }

        public void Load()
        {
            emptyTexture = MapManager.Instance.CreateColorTexture(255, 255, 255, 255);
            texture = MapManager.Instance.Content.Load<Texture2D>("testTiles");
            debugTexture = MapManager.Instance.CreateColorTexture(255, 0, 0, 255);
            xTiles = texture.Width / (tileWidth  + tilePadding);
            yTiles = texture.Width / (tileHeight + tilePadding);
            tiles = new Tile[xTiles, yTiles];

            for (int x = 0; x < xTiles; x++)
            {
                for (int y = 0; y < yTiles; y++)
                {
                    Rectangle source = new Rectangle((x * tileDistanceX) + tilePadding, (y * tileDistanceY )+ tilePadding, tileWidth, tileHeight);
                    Rectangle destination= new Rectangle(x * tileDistanceX + tilePadding, y * tileDistanceY + tilePadding, tileWidth, tileHeight);
                    tiles[x, y] = new Tile(source, destination);
                }
            }

            cursor.Load();
            cursor.SetPosition(tiles[0, 0],position);

        }

        public void Update(GameTime _gameTime)
        {

            Point mousePosition = MouseManager.Instance.Position;
            if (MouseManager.Instance.IsKeyActivity(true, KeyActivity.Pressed))
            {
                Vector2 tilePositionCursor = mousePosition.ToVector2() - Position;

                int tileX = tilePositionCursor.X < 0 ? -1 : (int)(tilePositionCursor.X / tileDistanceX) + viewStartX;
                int tileY = tilePositionCursor.Y < 0 ? -1 : (int)(tilePositionCursor.Y / tileDistanceY) + viewStartY;
                bool valid = true;

                if (tileX >= xTiles || tileX < 0)
                    valid = false;
                if (tileY >= yTiles || tileY < 0)
                    valid = false;

                if (valid)
                {
                    cursor.SetPosition(tiles[tileX, tileY], Position);
                }
            }
            if (sourceManagerWindow.Contains(mousePosition))
            {
                if (KeyboardManager.Instance.IsKeyActivity(Keys.W.ToString(),KeyActivity.Pressed))
                {
                    if(viewStartY > 0)
                    {
                        viewStartY--;
                    }
                }
                if (KeyboardManager.Instance.IsKeyActivity(Keys.S.ToString(), KeyActivity.Pressed))
                {
                    if (viewStartY + viewSizeY < yTiles)
                    {
                        viewStartY++;
                    }
                }
                if (KeyboardManager.Instance.IsKeyActivity(Keys.D.ToString(), KeyActivity.Pressed))
                {
                    if (viewStartX + viewSizeX < xTiles )
                    {
                        viewStartX++;
                    }
                }
                if (KeyboardManager.Instance.IsKeyActivity(Keys.A.ToString(), KeyActivity.Pressed))
                {
                    if (viewStartX > 0)
                    {
                        viewStartX--;
                    }
                }
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Draw(emptyTexture, sourceManagerWindow, Color.AliceBlue * .10f);
            SpriteBatchAssist.DrawBox(_spriteBatch, emptyTexture, sourceManagerWindow, Color.DarkSlateBlue,2);


            int showX = Math.Min(viewStartX + viewSizeX, xTiles);
            int showY = Math.Min(viewStartY + viewSizeY, yTiles);
            
            for(int x = viewStartX; x < showX; x++)
            {
                for(int y = viewStartY; y < showY; y++)
                {
                    Tile tile = tiles[x, y];
                    if (tile != null)
                    {

                        int xPosition = (int)(position.X + tile.Destination.X - (viewStartX * tileDistanceX));
                        int yPosition = (int)(position.Y + tile.Destination.Y - (viewStartY * tileDistanceY));

                        Vector2 tilePosition = new Vector2(xPosition,yPosition);

                        _spriteBatch.Draw(texture, tilePosition,tile.Source, Color.White);
                    }
                }
            }
            
            cursor.Draw(_spriteBatch);
        }

        public Texture2D getTexture()
        {
            return texture;
        }
    }
}
