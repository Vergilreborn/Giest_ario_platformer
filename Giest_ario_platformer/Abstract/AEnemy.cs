using Giest_ario_platformer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Giest_ario_platformer.Enums;
using Giest_ario_platformer.GameObjects;
using Giest_ario_platformer.Handlers;

namespace Giest_ario_platformer.Abstract
{
    abstract class AEnemy : AGameObject
    {
        
        public bool IsDead
        {
            get
            {
                return isDead;
            }
            set
            {
                isDead = value;
            }
        }

        protected bool isDead;
        protected Texture2D texture;
        protected float fallSpeed;
        protected AnimationSet animations;
        protected Animation current;
        protected Direction direction;

        protected bool isFalling = false;

        public AEnemy()
        {
            isDead = false;
        }

        public void LoadEnemy(Rectangle _destination, Rectangle _source)
        {
            this.Position = new Vector2(_destination.X, _destination.Y);
            this.Width = _source.Width;
            this.Height = _source.Height;
            fallSpeed = 0f;
            animations = new AnimationSet();
            direction = Direction.Right;
        }


        public abstract void Update(GameTime _gameTime, Map _map, Player _player);

     }
}
