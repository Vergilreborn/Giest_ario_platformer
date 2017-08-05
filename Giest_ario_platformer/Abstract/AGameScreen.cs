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
    abstract class AGameScreen : IGameObject
    {
        
        public abstract void Draw(SpriteBatch _spriteBatch);
        public abstract void Init();
        public abstract void Load();
        public abstract void Update(GameTime _gameTime);
    }
}
