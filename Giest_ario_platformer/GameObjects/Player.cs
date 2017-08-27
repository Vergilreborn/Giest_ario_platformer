using Giest_ario_platformer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Giest_ario_platformer.Managers;
using Microsoft.Xna.Framework.Input;
using Giest_ario_platformer.Enums;
using Giest_ario_platformer.Handlers;
using Giest_ario_platformer.Helpers;
using Giest_ario_platformer.GameObjects.MapObjects;

namespace Giest_ario_platformer.GameObjects
{
    class Player : AGameObject
    {

        public bool IsDead
        {
            get { return isDead; }
        }

        public String ChangeLevel
        {
            get { return changeLevel; }
        }

        private Animation currentAnimation;
        private AnimationSet animations;
        private float fallSpeed;
        private bool isJumping;
        private bool isSwimming;
        private bool isDead = false;
        private Direction current;
        private Direction moving;
        private String action;
        private float horSpeed;
        private float horSpeedBackup;
        private Texture2D debugtexture;
        private String changeLevel = null;
        private float deadUpSpeed = -12f;
        private float deathSpeedGravity = 0.5f;
        
        private String debugStr;

        public Player(Vector2 _position)
        {
            this.Position = _position;
            current = Direction.Left;
        }

        public override void Init()
        {
            fallSpeed = 0;
            Gravity = 1f;
            horSpeed = 0f;
            SavePosition = new Vector2();
            debugStr = "";
        }

        public void SetPosition(Vector2 _startPosition)
        {
            horSpeed = 0f;
            fallSpeed = 0;
            this.Position.X = _startPosition.X;
            this.Position.Y = _startPosition.Y;
        }

        public override void Load()
        {
            //texture = GameManager.Instance.Content.Load<Texture2D>("Mario");
            //animation = new Animation("Mario", 2, 50f);
            animations = new AnimationSet();
            animations.AddAnimation("Left", "Player/Player_Still_Left", 2, 250, true);
            animations.AddAnimation("Right", "Player/Player_Still_Right", 2, 250, true);
            animations.AddAnimation("Jump_Left", "Player/Mario_Jump_Left", 1, 100, false);
            animations.AddAnimation("Jump_Right", "Player/Mario_Jump_Right", 1, 100, false);
            animations.AddAnimation("Death", "Player/Mario_Death", 1, 100, false);
            animations.AddAnimation("Turn_Left", "Player/Player_Turn_Left", 1, 100, false);
            animations.AddAnimation("Turn_Right", "Player/Player_Turn_Right", 1, 100, false);
            animations.AddAnimation("Walk_Left", "Player/Player_Walk_Left", 4, 175, true);
            animations.AddAnimation("Walk_Right", "Player/Player_Walk_Right", 4, 175, true);
            animations.AddAnimation("Run_Left", "Player/Mario_Run_Left", 2, 35, true);
            animations.AddAnimation("Run_Right", "Player/Mario_Run_Right", 2, 35, true);
            animations.AddAnimation("RunJump_Left", "Player/Mario_RunJump_Left", 1, 100, false);
            animations.AddAnimation("RunJump_Right", "Player/Mario_RunJump_Right", 1, 100, false);
            currentAnimation = animations.GetAnimation("Left");
            action = "";
            debugtexture = GameManager.Instance.CreateColorTexture(20, 255, 20, 1);

            this.Width = 32;
            this.Height = 36;
        }

        //handle collision
        internal void Update(GameTime _gameTime, Map _map)
        {
            debugStr = "";

            if (!isDead)
            {
                changeLevel = null;

                SavePosition.X = Position.X;
                SavePosition.Y = Position.Y;

                Update(_gameTime);

                if (KeyboardManager.Instance.IsKeyActivity(Keys.R.ToString(), KeyActivity.Pressed))
                {
                    Position.X = _map.PlayerPosition.X;
                    Position.Y = _map.PlayerPosition.Y;
                    isJumping = true;
                }

                float newPosition;
                bool positiveChange = SavePosition.X < Position.X;
                TileType collisionType;
                bool collision = CollisionDetection.IsColliding(_map, CollisionBox, positiveChange, true, out newPosition, out collisionType);


                if (collision )
                {
                    Position.X = newPosition;
                    horSpeed = 0f;
                }
                
                debugStr = $"Hor:({collision},{collisionType.ToString()},{horSpeed})";
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
                        fallSpeed = Math.Min(fallSpeed + (Gravity/3f), 2f);
                        isSwimming = true;
                    }
                    else
                    {
                        isSwimming = false;
                        fallSpeed = Math.Min(fallSpeed + Gravity, 10f);
                        if (fallSpeed > 1f)
                            isJumping = true;
                    }
                }
                else
                {
                   
                    Position.Y = newPosition;
                    if (collisionType == TileType.Water)
                    {
                        fallSpeed = Math.Max(fallSpeed + (Gravity / 3f), -6f);
                        fallSpeed = Math.Min(fallSpeed + (Gravity / 3f), 2f);
                        isSwimming = true;
                    }
                    else
                    {
                        fallSpeed = Gravity;
                        isSwimming = false;
                    }
                    isJumping = false;
                   
                }
                
                if (isJumping)
                {
                    action = "Jump";
                    if (Math.Abs(horSpeed) > 8f)
                        action = "RunJump";
                }
                debugStr += Environment.NewLine + $"Ver:({collision},{collisionType.ToString()},{fallSpeed})";

                if (KeyboardManager.Instance.IsKeyActivity(Keys.A.ToString(), KeyActivity.Pressed))
                {
                    setDeath();
                }

                if (collisionType == TileType.Death)
                {
                    setDeath();
                }

                if (Position.Y > _map.MapHeight)
                {
                    setDeath();
                }
            }
            if (Position.Y > _map.MapHeight)
            {
                isDead = false;
                Position.X = _map.PlayerPosition.X;
                Position.Y = _map.PlayerPosition.Y;
                fallSpeed = 0f;
                moving = Direction.None;

            }

            debugStr += Environment.NewLine + $"Action:{action}";
            
            setAnimation(_gameTime);

            MapObject newLevel = CollisionDetection.IsCollidingObjects(_map, CollisionBox);
            if (newLevel != null)
            {
                changeLevel = newLevel.Data;
            }
            
        }

        private void setDeath()
        {
            horSpeed = 0;
            action = "Death";
            isJumping = false;
            moving = Direction.None;
            isDead = true;
            fallSpeed = deadUpSpeed;
            Position.Y += fallSpeed;
        }

        public void PlayDeathUpdate(GameTime _gameTime,Map _map)
        {
            if (Position.Y > _map.MapHeight)
            {
                isDead = false;
                Position.X = _map.PlayerPosition.X;
                Position.Y = _map.PlayerPosition.Y;
                fallSpeed = 0f;
                moving = Direction.None;
                GameManager.Instance.ChangeScreen("StartScreen");
            }
            else
            {
                Position.Y += fallSpeed;
                fallSpeed += deathSpeedGravity;
            }
            setAnimation(_gameTime);

        }


        //handle animation
        public override void Update(GameTime _gameTime)
        {
            moving = Direction.None;
            horSpeedBackup = horSpeed;

            if (KeyboardManager.Instance.IsKeyActivity(Keys.Left.ToString(), KeyActivity.Down)
                    && KeyboardManager.Instance.IsKeyActivity(Keys.Z.ToString(),KeyActivity.Hold))
            {
                if (horSpeed > 0f)
                    horSpeed -= isSwimming ? .2f : .4f;
                else
                    horSpeed -= isSwimming ? .2f : .2f;
                if (isSwimming)
                    horSpeed = Math.Max(horSpeed, -3f);
                horSpeed = Math.Max(horSpeed, -10f);
                moving = Direction.Left;
            }
            else if (KeyboardManager.Instance.IsKeyActivity(Keys.Left.ToString(), KeyActivity.Down))
            {
                if (horSpeed > 0f)
                    horSpeed -= isSwimming ? .2f : .3f;
                else if(horSpeed > -4f)
                    horSpeed -= isSwimming ? .2f : .2f;
                else if (horSpeed < -4f)
                    horSpeed += isSwimming ? .2f : .2f;
                if (isSwimming)
                    horSpeed = Math.Max(horSpeed, -3f);
                moving = Direction.Left;
            }

            if (KeyboardManager.Instance.IsKeyActivity(Keys.Right.ToString(), KeyActivity.Down)
                  && KeyboardManager.Instance.IsKeyActivity(Keys.Z.ToString(), KeyActivity.Hold))
            {
                if (horSpeed < 0f)
                    horSpeed += isSwimming ? .2f : .4f;
                else
                    horSpeed += isSwimming ? .2f : .2f;
                if (isSwimming)
                    horSpeed = Math.Min(horSpeed, 3f);
                horSpeed = Math.Min(horSpeed, 10f);
                moving = Direction.Right;
            }
            else if (KeyboardManager.Instance.IsKeyActivity(Keys.Right.ToString(), KeyActivity.Down))
            {
                if (horSpeed < 0f)
                    horSpeed += isSwimming? .2f : .3f;
                else if (horSpeed < 4f)
                    horSpeed += isSwimming ? .2f : .2f;
                else if (horSpeed > 4f)
                    horSpeed -= isSwimming ? .2f : .2f;
                if (isSwimming)
                    horSpeed = Math.Min(horSpeed, 3f);
                moving = Direction.Right;
            }
          
            Position.X += horSpeed;

            if(Math.Abs(horSpeed) > 8f)
            {
                action = "Run";
            }else if(Math.Abs(horSpeed) > 0f)
            {
                action = "Walk";
            }
            if (moving == Direction.None)
            {
                if (Math.Abs(horSpeed) < .5f)
                    horSpeed = 0f;
                else
                    horSpeed += horSpeed > 0f ? -.5f : horSpeed < 0f ? .5f : 0f;

            }else if((moving == Direction.Left && horSpeed > 0f) || (moving == Direction.Right && horSpeed < 0f))
            {
                action = "Turn";
            }

            if ((!isJumping || isSwimming) && KeyboardManager.Instance.IsKeyActivity(Keys.Space.ToString(), KeyActivity.Pressed))
            {
                if (isSwimming) {
                    fallSpeed = -6f;
                }
                else
                {
                    SoundManager.Instance.PlaySound("Mario_Jump");
                    isJumping = true;
                    fallSpeed = -10f;
                }
            }
            else if ((isJumping || isSwimming) && fallSpeed < 0 && KeyboardManager.Instance.IsKeyActivity(Keys.Space.ToString(), KeyActivity.Hold))
            {
                if (isSwimming)
                    fallSpeed -= .2f;
                else
                    fallSpeed -= .6f;
            }
            
        }
  
        private void setAnimation(GameTime _gameTime)
        {

            String mainDirection = current.ToString();
            if (isDead)
            {
                currentAnimation = animations.GetAnimation(action);
            }
            else if (moving == Direction.None && !isJumping)
            {
                currentAnimation = animations.GetAnimation(mainDirection);
            }
            else
            {
                if(moving != Direction.None)
                    current = moving;
                currentAnimation = animations.GetAnimation($"{action}_{mainDirection}");
            }

            currentAnimation.Update(_gameTime);
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            if (GameManager.Instance.IsDebug)
            {
                _spriteBatch.Draw(debugtexture, CollisionBox, Color.White * Constants.DEBUG_OPACITY);
            }
            currentAnimation.Draw(_spriteBatch, CollisionBox);
            if (GameManager.Instance.IsDebug)
                {

                    _spriteBatch.DrawString(GameManager.Instance.Fonts["Debug"], debugStr, Position - new Vector2(100, 20), Color.Turquoise);
            }
       
        }
    }
}
