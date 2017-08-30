using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Giest_ario_platformer.Abstract;
using Giest_ario_platformer.Managers;
using Giest_ario_platformer.Helpers;
using Giest_ario_platformer.Enums;
using Giest_ario_platformer.Handlers;
using Microsoft.Xna.Framework.Input;

namespace Giest_ario_platformer.GameObjects.EnemyObjects
{
    class BlockEnemy : AEnemy
    {

        private Texture2D texture;
        private float fallSpeed;
        private AnimationSet animations;
        private Animation current;

        public BlockEnemy()
        {
        }
        
        //initialize this enemy
        public override void Init()
        {
            this.Position = new Vector2(0, 0);
            this.Width = 32;
            this.Height = 32;
            fallSpeed = 0f;
            animations = new AnimationSet();
        }

        //loading sprite information for this enemy
        public override void Load()
        {
            texture = GameManager.Instance.Content.Load<Texture2D>("test");
            animations.AddAnimation("Left", "Enemy/BasicBlock_Left", 4, 200, true);
            animations.AddAnimation("Right", "Enemy/BasicBlock_Right", 4, 200, true);
            animations.AddAnimation("Left_Walk", "Enemy/BasicBlock_Left_Walk", 4, 200, true);

            animations.AddAnimation("Right_Walk", "Enemy/BasicBlock_Right_Walk", 4, 200, true);
            current = animations.GetAnimation("Right");
        }

        //normal updates without the player
        public override void Update(GameTime _gameTime)
        {
            if (KeyboardManager.Instance.IsKeyActivity(Keys.U.ToString(), KeyActivity.Pressed))
            {
                current = animations.GetAnimation("Left");
            }
            if (KeyboardManager.Instance.IsKeyActivity(Keys.I.ToString(), KeyActivity.Pressed))
            {

                current = animations.GetAnimation("Right");
            }
            if (KeyboardManager.Instance.IsKeyActivity(Keys.O.ToString(), KeyActivity.Pressed))
            {

                current = animations.GetAnimation("Left_Walk");
            }
            if (KeyboardManager.Instance.IsKeyActivity(Keys.P.ToString(), KeyActivity.Pressed))
            {

                current = animations.GetAnimation("Right_Walk");
            }

            current.Update(_gameTime);
        }

        //Handle interactions with the player
        public override void Update(GameTime _gameTime, Map _map, Player _player)
        {
            SavePosition.X = Position.X;
            SavePosition.Y = Position.Y;
            

            bool positiveChange = SavePosition.X < Position.X;
            float newPosition;
            TileType collisionType;
            bool collision = CollisionDetection.IsColliding(_map, CollisionBox, positiveChange, true, out newPosition, out collisionType);

            if (collision)
            {
                Position.X = newPosition;
            }

            Position.Y += fallSpeed;

            positiveChange = SavePosition.Y < Position.Y;
            TileType collisionTypeVer;
            collision = CollisionDetection.IsColliding(_map, CollisionBox, positiveChange, false, out newPosition, out collisionTypeVer);

            collisionType = (collisionType == TileType.Water ? TileType.Water : collisionTypeVer);

            if (!collision)
            {
                if (collisionType == TileType.Water)
                {
                    fallSpeed = Math.Max(fallSpeed + (Gravity / 3f), -6f);
                    fallSpeed = Math.Min(fallSpeed + (Gravity / 3f), 2f);
                }
                else
                {
                    fallSpeed = Math.Min(fallSpeed + Gravity, 10f);
                }
            }
            else
            {

                Position.Y = newPosition;
                if (collisionType == TileType.Water)
                {
                    fallSpeed = Math.Max(fallSpeed + (Gravity / 3f), -6f);
                    fallSpeed = Math.Min(fallSpeed + (Gravity / 3f), 2f);
                }
                else
                {
                    fallSpeed = Gravity;
           
                 }
           
            }
            Update(_gameTime);            
        }

        //Draw this enemy
        public override void Draw(SpriteBatch _spriteBatch)
        {
            current.Draw(_spriteBatch, CollisionBox);
        }
    }
}
