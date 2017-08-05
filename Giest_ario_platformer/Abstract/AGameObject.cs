using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Giest_ario_platformer.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Giest_ario_platformer.Abstract
{
    abstract class AGameObject : IGameObject
    {

        public Vector2 SavePosition;
        public Vector2 Position;
        //public Rectangle SourceBox;
        //public Rectangle DestinationBox;
       // public Texture2D Texture;
        public float Gravity = 0f;

        private int width;
        private int height;


        #region Setters/Getters
        public int Width
        {
            get
            {
                return width;
            }
            internal set
            {
                this.width = value;
            }
        }

        public int Height
        {
            get
            {
                return height;
            }
            internal set
            {
                this.height = value;
            }
        }


        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, width, height);
            }
        }

        public Vector2 Center
        {
            get
            {
                return new Vector2(Position.X + width/ 2, Position.Y + height/ 2);
            }
        }
        #endregion

        public abstract void Draw(SpriteBatch _spriteBatch);
        public abstract void Init();
        public abstract void Load();
        public abstract void Update(GameTime _gameTime);
    }
}
