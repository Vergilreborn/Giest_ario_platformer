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

       

        public BlockEnemy()
        {
        }
       
        //initialize this enemy
        public override void Init()
        {
            direction = Direction.Right;
            this.Position = new Vector2(100, 0);
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
        
            current.Update(_gameTime);
        }

        //Handle interactions with the player
        public override void Update(GameTime _gameTime, Map _map, Player _player)
        {
            SavePosition.X = Position.X;
            SavePosition.Y = Position.Y;

            if(direction == Direction.Right)
            {
                Position.X += 1;
            }

            if(direction == Direction.Left)
            {
                Position.X -= 1;
            }
            
            bool positiveChange = SavePosition.X < Position.X;
            float newPosition;
            TileType collisionType;
            bool collisionH = CollisionDetection.IsColliding(_map, CollisionBox, positiveChange, true, out newPosition, out collisionType);

            if (collisionH)
            {
                Position.X = newPosition;
            }

            Position.Y += fallSpeed;

            positiveChange = SavePosition.Y < Position.Y;
            TileType collisionTypeVer;
            bool collisionV = CollisionDetection.IsColliding(_map, CollisionBox, positiveChange, false, out newPosition, out collisionTypeVer);

            collisionType = (collisionType == TileType.Water ? TileType.Water : collisionTypeVer);

            if (!collisionV)
            {
                isFalling = true;
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
                    isFalling = false;

                }
           
            }

            float positionXAhead = Position.X + (direction == Direction.Right ? Width/2 : -Width/2);
            TileType type = TileType.None;
            float newValue = 0f;

            if (!isFalling)
            {
                if (collisionH || !CollisionDetection.IsColliding(_map, new Rectangle((int)positionXAhead, (int)Position.Y + 1, CollisionBox.Width, CollisionBox.Height), true, false, out newValue, out type))
                {
                    //if(collisionH)
                    // {
                    direction = direction == Direction.Right ? Direction.Left : Direction.Right;
                    //}
                    current = animations.GetAnimation($"{direction}_Walk");
                }
                current = animations.GetAnimation($"{direction}_Walk");
            }
            else
            {
                current = animations.GetAnimation($"{direction}");
            }



            if (CollisionBox.Intersects(_player.CollisionBox) )
            {
                 if (CollisionDetection.IsAbove(_player.CollisionBox, CollisionBox))
                {
                    IsDead = true;
                    _player.Bounce();
                }
                else {
                    _player.TakeDamage();
                }
            }
            
            Update(_gameTime);            
        }

        //Draw this enemy
        public override void Draw(SpriteBatch _spriteBatch)
        {
            float positionXAhead = Position.X + (direction == Direction.Right ? Width/2 : -Width/2);
            Rectangle rect = new Rectangle((int)positionXAhead, (int)Position.Y + 1, CollisionBox.Width, CollisionBox.Height);

            _spriteBatch.Draw(GameManager.Instance.EmptyTexture, rect, Color.Red * .33f);

            current.Draw(_spriteBatch, CollisionBox);
        }
    }
}
