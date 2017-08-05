using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Interfaces
{
    interface IGameObject
    {
        void Init();
        void Load();
        void Update(GameTime _gameTime);
        void Draw(SpriteBatch _spriteBatch);
    }
}
