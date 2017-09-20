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
using Giest_ario_platformer.GameObjects.MapObjects;

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

        protected String Attr;
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

        public void LoadEnemy(EnemyObjectInfo _enemyInfo)
        {
            this.Position = new Vector2(_enemyInfo.Destination.X, _enemyInfo.Destination.Y);
            this.Width = _enemyInfo.Source.Width;
            this.Height = _enemyInfo.Source.Height;
            this.Attr = _enemyInfo.Attr;
            if (this.Attr == null)
                Attr = "";
            fallSpeed = 0f;
            animations = new AnimationSet();
            direction = Direction.Right;
        }


        public abstract void Update(GameTime _gameTime, Map _map, Player _player);

     }
}
