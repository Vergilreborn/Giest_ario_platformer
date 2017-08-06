using MapEditor.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Interfaces;

namespace MapEditor.Manager
{
    class KeyboardManager : IGameObject
    {

        private static KeyboardManager instance;
        public static KeyboardManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new KeyboardManager();
                return instance;
            }
        }
        
        private KeyboardState curr;
        private KeyboardState prev;

        public KeyboardManager()
        {
            curr = Keyboard.GetState();
            prev = curr;
        }

        public void Init()
        {
            curr = Keyboard.GetState();
            prev = curr;
        }

        public void Load()
        {
        }

        public void Update(GameTime _gameTime)
        {
            prev = curr;
            curr = Keyboard.GetState();
        }

        public Boolean IsKeyActivity(String _keyStr,KeyActivity _activity)
        {
            Keys key = (Keys)Enum.Parse(typeof(Keys), _keyStr);
            
            switch (_activity)
            {
                case KeyActivity.Down:
                    return curr.IsKeyDown(key);
                case KeyActivity.Up:
                    return curr.IsKeyUp(key);
                case KeyActivity.Pressed:
                    return curr.IsKeyDown(key) && prev.IsKeyUp(key);
                case KeyActivity.Hold:
                    return curr.IsKeyDown(key) && prev.IsKeyDown(key);
                default: 
                     return false;
            }
           

        } 

        public void Draw(SpriteBatch _spriteBatch)
        {
         
        }
    }
}
