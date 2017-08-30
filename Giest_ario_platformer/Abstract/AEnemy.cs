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
        private EnemyType type;
        
        public AEnemy()
        {

        }

        public abstract void Update(GameTime _gameTime, Map _map, Player _player);

     }
}
