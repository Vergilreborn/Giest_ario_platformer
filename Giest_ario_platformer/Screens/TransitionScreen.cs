using Giest_ario_platformer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Giest_ario_platformer.Managers;

namespace Giest_ario_platformer.Screens
{
    class TransitionScreen : AGameScreen
    {
        public bool isTransition
        {
            get
            {
                return transition;
            }
        }
        
        private Texture2D loadingScreen;
        private Texture2D emptyContent;
        private Rectangle backgroundRect;
        private Vector2 position;
        private bool transition = true;
        private float opacity = 0f;
        private bool increase = true;
        public TransitionScreen()
        {

        }

        public override void Init()
        {
            
        }

        public void StartTransition()
        {
            opacity = 0f;
            transition = true;
            GameManager.Instance.Cam.SetObjectCenter();
        }

        public void StartDecrease()
        {
            increase = false;
        }

        public override void Load()
        {

            loadingScreen = GameManager.Instance.Content.Load<Texture2D>("Transition/LoadingText");
            emptyContent = GameManager.Instance.CreateColorTexture(255, 255, 255, 255);

          
            Vector2 widthHeight = GameManager.Instance.WidthHeight;

            position = new Vector2(widthHeight.X / 2 - loadingScreen.Width / 2, widthHeight.Y / 2 - loadingScreen.Height / 2);
            backgroundRect = new Rectangle(0,0, (int)widthHeight.X, (int)widthHeight.Y);

        }

        public override void UnLoad()
        {
        }

        public override void Update(GameTime _gameTime)
        {
            if (increase && opacity < 1f)
            {
                opacity += .05f; 
            }

            if(opacity >= 1f)
            {
                transition = false;
            }   
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(emptyContent, backgroundRect, Color.Silver * opacity);
            _spriteBatch.Draw(loadingScreen, position, Color.White);
        }
    }
}
