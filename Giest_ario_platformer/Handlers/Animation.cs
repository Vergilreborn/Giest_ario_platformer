using Giest_ario_platformer.Interfaces;
using Giest_ario_platformer.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giest_ario_platformer.Handlers
{

    class Animation
    {
        public Rectangle Source
        {
            get
            {
                return new Rectangle(width * currentFrame,0,width,height);
            }
        }
        
        private Texture2D texture;
        private float timer;
        private float changeFrameTimer;
        private int maxFrames;
        private int currentFrame;
        private bool isLooping;
        private int width;
        private int height;


        public Animation(String _animationPath, int _maxFrames, float _changeFrameTimer,bool _isLooping = true)
        {

            this.texture = GameManager.Instance.Content.Load<Texture2D>(_animationPath);
            this.timer = 0f;
            this.changeFrameTimer = _changeFrameTimer;
            this.isLooping = _isLooping;
            this.maxFrames = _maxFrames;
            this.width = this.texture.Width / maxFrames;
            this.height = this.texture.Height;
        }

        internal void Dispose()
        {
            this.texture.Dispose();
        }

        public void Update(GameTime _gameTime)
        {
            timer += _gameTime.ElapsedGameTime.Milliseconds;
            if(timer > changeFrameTimer)
            {
                currentFrame++;
                currentFrame = isLooping? (currentFrame % maxFrames) : Math.Min(currentFrame,maxFrames-1);
                timer = 0f;
            }
            
        }

        internal void Draw(SpriteBatch _spriteBatch, Rectangle _collisionBox)
        {
            _spriteBatch.Draw(texture, _collisionBox, Source, Color.White);
        }

        internal void Draw(SpriteBatch _spriteBatch, Vector2 _animationPosition)
        {
            _spriteBatch.Draw(texture, _animationPosition, Source, Color.White);
        }
    }
}
