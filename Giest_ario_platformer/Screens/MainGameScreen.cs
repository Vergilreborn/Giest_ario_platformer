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
using Giest_ario_platformer.Forms;

namespace Giest_ario_platformer.Screens
{
    class MainGameScreen : AGameScreen
    {

        
        public Player Player
        {
            get
            {
                return player;
            }
        }
        private Map map;
        private Player player;
        private bool isPause;
        private String pauseString = "Paused!";
        private Vector2 pausePosition;
        private SpriteFont font;


        public MainGameScreen()
        {
            this.player = new Player(new Vector2(40, 40));
        }

        public override void Init()
        {
            isPause = false;
            map = new Map();
            map.Init();
            player.Init();
            GameManager.Instance.Cam.SetMapBoundary(map.GetBoundary());


        }

        public override void Load()
        {
            map.Load();
            player.Load();
            player.SetPosition(map.PlayerPosition);
            font = GameManager.Instance.Fonts["Large"];
            pausePosition= font.MeasureString(pauseString);
         
        }

        public void LoadMap(String mapName)
        {

            map.LoadTestMap(mapName);
            player.SetPosition(map.PlayerPosition);
        }

        public override void Update(GameTime _gameTime)
        {
            if (KeyboardManager.Instance.IsKeyActivity(Keys.OemTilde.ToString(), KeyActivity.Pressed))
            {
                using (MapSelection se = new MapSelection())
                {
                    System.Windows.Forms.DialogResult res = se.ShowDialog();
                    if(res == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadMap(se.GetCleanText());
                    }
                }
            }

            if (KeyboardManager.Instance.IsKeyActivity(Keys.Enter.ToString(), KeyActivity.Pressed))
            {
                isPause = !isPause;
                MusicManager.Instance.Pause();
            }

            if (isPause)
            {

            }
            else if (player.IsDead)
            {
                player.PlayDeathUpdate(_gameTime, map);
            }
            else { 
                map.Update(_gameTime);
                player.Update(_gameTime, map);
                if (player.ChangeLevel != null){
                    LoadMap(player.ChangeLevel);
                }
                GameManager.Instance.Cam.Follow(player.CollisionBox);
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {

            map.Draw(_spriteBatch);
            player.Draw(_spriteBatch);

            if (isPause)
            {
                Vector2 camPosition = GameManager.Instance.Cam.position;
                Vector2 drawStringPosition= new Vector2(-camPosition.X + (GameManager.Instance.WidthHeight.X / 2), -camPosition.Y + (GameManager.Instance.WidthHeight.Y / 2));
                _spriteBatch.Draw(GameManager.Instance.EmptyTexture, new Rectangle((int)-camPosition.X, (int)-camPosition.Y, (int)GameManager.Instance.WidthHeight.X, (int)GameManager.Instance.WidthHeight.Y), Color.Black * .75f);
                _spriteBatch.DrawString(font, pauseString, drawStringPosition, Color.White);
                
            }

        
        }

        public override void UnLoad()
        {
            map.Dispose();
            player.Dispose();
        }

    }
}
