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

namespace MapEditor.Abstract
{
    //TODO: make abstract
    class AMapButton : IGameObject
    {
        private Rectangle collisionBox;
        private Vector2 position;
        private Vector2 center;
        private String text;
        private Texture2D texture;
        private Vector2 textPosition;
        private SpriteFont font;

        public AMapButton(Vector2 _position, String _text)
        {
            this.position = _position;
            this.text = _text;
        }

        public void Init()
        {
            
        }

        public void Load()
        {
            this.texture = MapManager.Instance.Content.Load<Texture2D>("Button_Up");
            this.center = new Vector2(position.X + texture.Width / 2, position.Y + texture.Height / 2);
            this.collisionBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.font = MapManager.Instance.DebugFont;
            this.textPosition = new Vector2(font.MeasureString(this.text).X/2, font.MeasureString(this.text).Y / 2);
        }

        public void Update(GameTime _gameTime)
        {
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, position, Color.White);
            _spriteBatch.DrawString(font, text, center - textPosition, Color.Black);
        }

        public bool Intersects(Point _position)
        {
            return collisionBox.Contains(_position);
        }

        internal void Draw(SpriteBatch _spriteBatch, bool _setPlayerPosition = false)
        {
            _spriteBatch.Draw(texture, position, _setPlayerPosition ? Color.LightSalmon : Color.White);
            _spriteBatch.DrawString(font, text, center - textPosition, Color.Black);
        }

        internal void setText(String _newText)
        {
            this.text = _newText;
            this.textPosition = new Vector2(font.MeasureString(this.text).X / 2, font.MeasureString(this.text).Y / 2);
        }
    }
}
