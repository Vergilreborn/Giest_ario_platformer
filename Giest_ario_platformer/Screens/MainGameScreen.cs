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

namespace Giest_ario_platformer.Screens
{
    class MainGameScreen : AGameScreen
    {

        private Map map;
        public Player Player
        {
            get
            {
                return player;
            }
        }
        private Player player;
       

        public MainGameScreen()
        {
            this.player = new Player(new Vector2(40, 40));
        }

        public override void Init()
        {
            map = new Map();
            map.Init();
            player.Init();
           

        }

        public override void Load()
        {
            map.Load();
            player.Load();
        }

        public override void Update(GameTime _gameTime)
        {
            map.Update(_gameTime);
            player.Update(_gameTime, map);
            GameManager.Instance.Cam.Update(_gameTime, player.Center);
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            map.Draw(_spriteBatch);
            player.Draw(_spriteBatch);
        }

    }
}
