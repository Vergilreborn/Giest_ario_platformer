using MapEditor.Interfaces;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Enums;

namespace MapEditor.Manager
{
    class MouseManager : IGameObject
    {
        public static MouseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MouseManager();
                }
                return instance;
            }
        }

        public Point Position
        {
            get
            {

                return new Point( (int)(curr.Position.X / MapManager.Instance.Scale.X), (int)(curr.Position.Y / MapManager.Instance.Scale.Y)) ;
            }
        }


        private static MouseManager instance;
        private MouseState curr;
        private MouseState prev;


        public MouseManager()
        {
            curr = Mouse.GetState();
            prev = curr;
        }

        public void Init()
        {
            curr = Mouse.GetState();
            prev = curr;
        }
        
        public void Load() { 
        }

        public Boolean IsKeyActivity(bool _left, KeyActivity _activity)
        {
            switch (_activity)
            {
                case KeyActivity.Down:
                    return _left ? curr.LeftButton == ButtonState.Pressed : curr.RightButton == ButtonState.Pressed;
                case KeyActivity.Up:
                    return _left ? curr.LeftButton == ButtonState.Released: curr.RightButton == ButtonState.Released;
                case KeyActivity.Pressed:
                    return _left ? curr.LeftButton == ButtonState.Released && prev.LeftButton == ButtonState.Pressed: 
                                   curr.RightButton == ButtonState.Released && curr.RightButton == ButtonState.Pressed;
                case KeyActivity.Hold:
                    return _left ? curr.LeftButton == ButtonState.Pressed && prev.LeftButton == ButtonState.Pressed :
                                   curr.RightButton == ButtonState.Pressed && curr.RightButton == ButtonState.Pressed;
                default:
                    return false;
            }
        }

        public void Update(GameTime _gameTime)
        {
            prev = curr;
            curr = Mouse.GetState();
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(MapManager.Instance.DebugFont, $"X: {Position.X}, Y: {Position.Y}",Position.ToVector2(), Color.White);
        }
    }
}
