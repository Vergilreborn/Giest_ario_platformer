using Giest_ario_platformer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Giest_ario_platformer.Managers;
using Giest_ario_platformer.GameObjects;
using Giest_ario_platformer.Handlers;
using Microsoft.Xna.Framework.Input;
using Giest_ario_platformer.Enums;

namespace Giest_ario_platformer.Screens
{
    class StartScreen : AGameScreen
    {

        private List<String> screenOptions;
        private int currentOptionPos;
        private int maxOptionWidth;
        private int maxOptionHeight;
        private SpriteFont font;
        private Vector2 position;        
    
        public StartScreen()
        {
        }

        public override void Init()
        {
            //TODO: Dynamicall load the options and change screen depending on them

            screenOptions = new List<string> { "START", "EXIT" };
            currentOptionPos = 0;
            position = new Vector2(0, 0);
            
        }

        public override void Load()
        {
            //TODO: Use real Font 
            font = GameManager.Instance.Fonts["XLarge"];
            String longestString = screenOptions.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur);
            maxOptionHeight = (int)font.MeasureString(longestString).Y;
            maxOptionWidth = (int)font.MeasureString(longestString).X;

           

        }

        public override void Update(GameTime _gameTime)
        {
            GameManager.Instance.Cam.SetObjectCenter();
          
            //GameManager.Instance.Cam.Update(_gameTime, player.Center);
            //TODO: Convert all Keys.* to a configuration file storing the input of the player
            //      These will all use strings
            if (KeyboardManager.Instance.IsKeyActivity(Keys.Up.ToString(), KeyActivity.Pressed))
            {
                currentOptionPos = (currentOptionPos == 0 ? screenOptions.Count : currentOptionPos)-1;
            }

            if (KeyboardManager.Instance.IsKeyActivity(Keys.Down.ToString(), KeyActivity.Pressed))
            {
                currentOptionPos = (currentOptionPos + 1) % screenOptions.Count;
            }

            if(KeyboardManager.Instance.IsKeyActivity(Keys.Space.ToString(), KeyActivity.Pressed))
            {
                switch (screenOptions[currentOptionPos])
                {
                    case "START" : GameManager.Instance.ChangeScreen("MainGameScreen"); break;
                    case "EXIT": GameManager.Instance.ChangeScreen("Exit"); break;
                }
            }

        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            for(int i = 0; i < screenOptions.Count; i++)
            {
                Vector2 textPosition = position + new Vector2(0, i * (maxOptionHeight + 2));
                //TODO : Make Color configurable and then change font type
                _spriteBatch.DrawString(font, screenOptions[i], textPosition, i == currentOptionPos ? Color.Yellow : Color.White);   
            }
        }

        public override void UnLoad()
        {
            
        }
    }
}
