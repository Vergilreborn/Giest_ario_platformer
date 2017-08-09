using MapEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MapEditor.Abstract
{
    abstract class AMapButton : IGameObject
    {

        private Rectangle destination;

        public AMapButton(Rectangle _destination)
        {
            this.destination = _destination;
            
        }

        public void Init()
        {
            
        }

        public void Load()
        {
            throw new NotImplementedException();
        }

        public void Update(GameTime _gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            throw new NotImplementedException();
        }

    }
}
