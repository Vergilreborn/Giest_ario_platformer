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

namespace Giest_ario_platformer.Abstract
{
    abstract class AEnemy : AGameObject
    {

        public EnemyType Type
        {
            get
            {
                return type;
            }
        }

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

        private bool isDead;
        private EnemyType type;
        
        public AEnemy()
        {
            isDead = false;
        }

        public abstract void Update(GameTime _gameTime, Map _map, Player _player);

     }
}
