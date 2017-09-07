using MapEditor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MapEditor.Helpers;
using MapEditor.Objects;
using MapEditor.Enums;

namespace MapEditor.Manager
{
    class EnemySourceManager : IGameObject
    {

        public bool InScreen
        {
            get
            {
                return inScreen;
            }
        }

        private Texture2D texture;
        private Texture2D emptyTexture;
        private Vector2 position;
        private Rectangle sourceWindow;
        private int viewSizeX;
        private int viewSizeY;
        private int viewStartX;
        private int viewStartY;
        private int tileSizePaddingX;
        private int tileSizePaddingY;

        private int tileSizeX;
        private int tileSizeY;
        private Dictionary<string, List<EnemyObjectInfo>> objectInformation;
        private EnemyObjectInfo hover;
        private EnemyObjectInfo selected;
        private bool inScreen = false;

        public EnemySourceManager()
        {
        }

        public void Init()
        {

            objectInformation = new Dictionary<string, List<EnemyObjectInfo>>();
            this.tileSizeY = 32;
            this.tileSizeX = 32;
            this.tileSizePaddingX = 33;
            this.tileSizePaddingY = 33;
            this.viewStartX = 0;
            this.viewStartY = 0;
            this.viewSizeX = 6;
            this.viewSizeY = 10;
            objectInformation.Add("Default", new List<EnemyObjectInfo>());
            objectInformation["Default"].Add(new EnemyObjectInfo() { Type = EnemyType.Basic, Source = new Rectangle(0, 0, 32, 32), Destination = new Rectangle(0, 0, 32, 32) });

            position = new Vector2(15, 400);
            sourceWindow = new Rectangle((int)position.X, (int)position.Y, tileSizePaddingX * viewSizeX, tileSizePaddingY * viewSizeY);

        }

        public void Load()
        {
            emptyTexture = MapManager.Instance.CreateColorTexture(255, 255, 255, 255);
            texture = MapManager.Instance.Content.Load<Texture2D>("EnemySpriteSheet");
        }

        public void Update(GameTime _gameTime)
        {

            Point mousePosition = MouseManager.Instance.Position;
            Point relativePosition = mousePosition - position.ToPoint();
            bool isClicked = MouseManager.Instance.IsKeyActivity(true, KeyActivity.Pressed);
            bool isRightClicked = MouseManager.Instance.IsKeyActivity(false, KeyActivity.Pressed);
            inScreen = sourceWindow.Contains(mousePosition);
            if (inScreen)
            {
                foreach (EnemyObjectInfo enm in objectInformation["Default"])
                {
                    if (enm.Destination.Contains(relativePosition))
                    {
                        if (isClicked)
                        {
                            selected = enm;
                        }

                        hover = enm;

                    }
                }
                if (isRightClicked)
                {
                    selected = null;
                }
            }
            else
            {
                hover = null;
            }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach(EnemyObjectInfo enm in objectInformation["Default"])
            {
                Rectangle drawPosition = new Rectangle((int)position.X + enm.Source.X, (int)position.Y + enm.Source.Y, enm.Source.Width, enm.Source.Height);
                _spriteBatch.Draw(texture, drawPosition, enm.Source, Color.White);
            }

            _spriteBatch.Draw(emptyTexture, sourceWindow, Color.AliceBlue * .10f);
            SpriteBatchAssist.DrawBox(_spriteBatch, emptyTexture, sourceWindow, Color.Red, 2);

            if (hover != null) {
                Rectangle drawPosition = new Rectangle((int)position.X + hover.Source.X, (int)position.Y + hover.Source.Y, hover.Source.Width, hover.Source.Height);
                SpriteBatchAssist.DrawBox(_spriteBatch, emptyTexture, drawPosition,Color.LightGoldenrodYellow);
            }
            if (selected!= null)
            {
                Rectangle drawPosition = new Rectangle((int)position.X + selected.Source.X, (int)position.Y + selected.Source.Y, selected.Source.Width, selected.Source.Height);
                SpriteBatchAssist.DrawBox(_spriteBatch, emptyTexture, drawPosition, Color.Red);
            }

        }

        internal EnemyObjectInfo GetSelected()
        {
            return selected;
        }
    }
}
