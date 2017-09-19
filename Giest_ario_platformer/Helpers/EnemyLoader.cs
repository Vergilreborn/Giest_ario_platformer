using Giest_ario_platformer.Abstract;
using Giest_ario_platformer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giest_ario_platformer.Helpers
{
    class EnemyLoader
    {
        public static AEnemy Create(EnemyType _enemType)
        {
            var type = Type.GetType("Giest_ario_platformer.GameObjects.EnemyObjects." + _enemType.ToString(), throwOnError: false);

            if (type == null)
            {
                throw new InvalidOperationException(_enemType.ToString() + " is not a known dto type");
            }

            if (!typeof(AEnemy).IsAssignableFrom(type))
            {
                throw new InvalidOperationException(type.Name + " does not inherit from AbstractDto");
            }

            return (AEnemy)Activator.CreateInstance(type);
        }
    }

}
