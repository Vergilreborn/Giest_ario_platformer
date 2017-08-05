using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giest_ario_platformer.Interfaces
{
    interface IGameObject
    {

        void Init();  //Loading variables for counters, states, position..etc
        void Load();  //Loading graphics for this object
        void Update(GameTime _gameTime); //Managing the update of the object
        void Draw(SpriteBatch _spriteBatch); //Drawing this object
    }
}
