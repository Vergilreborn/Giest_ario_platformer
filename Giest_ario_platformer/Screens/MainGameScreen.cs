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
using System.Threading;

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
        private TransitionScreen transitionScreen;
        private bool transition;
        private String mapToLoad;

        public MainGameScreen()
        {
            this.player = new Player(new Vector2(40, 40));
        }

        public override void Init()
        {
            map = new Map();
            map.Init();
            player.Init();            
            transitionScreen = new TransitionScreen();
            transition = false;
            isPause = true;

        }
        
        private void loadMap()
        {
            map.LoadMap(mapToLoad);
            player.SetPosition(map.PlayerPosition);
            GameManager.Instance.Cam.SetMapBoundary(map.GetBoundary());
            map.StartMusic();
            transition = false;

        }

        public override void Load()
        {
            
            map.Load();
            player.Load();
            LoadMap("Testing1.gmap");
            font = GameManager.Instance.Fonts["Large"];
            pausePosition= font.MeasureString(pauseString);
            transitionScreen.Load();


        }

        public void LoadMap(String _mapName)
        {
            transition = true;
            mapToLoad = _mapName;
            MusicManager.Instance.Stop();
            transitionScreen.StartTransition();
            
            Thread th = new Thread(loadMap);
            th.Start();
            isPause = false;
          
        }

        public override void Update(GameTime _gameTime)
        {
            if (!transition && !transitionScreen.isTransition)
            {
                if (KeyboardManager.Instance.IsKeyActivity(Keys.OemTilde.ToString(), KeyActivity.Pressed))
                {
                    using (MapSelection se = new MapSelection())
                    {
                        System.Windows.Forms.DialogResult res = se.ShowDialog();
                        if (res == System.Windows.Forms.DialogResult.OK)
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
                else
                {
                    map.Update(_gameTime);
                    player.Update(_gameTime, map);
                    if (player.ChangeLevel != null)
                    {
                        LoadMap(player.ChangeLevel);
                    }
                }
                GameManager.Instance.Cam.Follow(player.CollisionBox);
            }
            else
            {
                GameManager.Instance.Cam.SetObjectCenter();
                transitionScreen.Update(_gameTime);
            }
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {

             if (!transition && !transitionScreen.isTransition)
            {
                map.Draw(_spriteBatch);
                player.Draw(_spriteBatch);

                if (isPause)
                {
                    Vector2 camPosition = GameManager.Instance.Cam.position;
                    Vector2 drawStringPosition = new Vector2(-camPosition.X + (GameManager.Instance.WidthHeight.X / 2), -camPosition.Y + (GameManager.Instance.WidthHeight.Y / 2));
                    _spriteBatch.Draw(GameManager.Instance.EmptyTexture, new Rectangle((int)-camPosition.X, (int)-camPosition.Y, (int)GameManager.Instance.WidthHeight.X, (int)GameManager.Instance.WidthHeight.Y), Color.Black * .75f);
                    _spriteBatch.DrawString(font, pauseString, drawStringPosition, Color.White);

                }
            }
            else
            {
                transitionScreen.Draw(_spriteBatch);
            }
        
        }

        public override void UnLoad()
        {
            
        }
    }
}
