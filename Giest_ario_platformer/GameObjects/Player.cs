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

namespace Giest_ario_platformer.GameObjects
{
    class Player : AGameObject
    {

        private Animation currentAnimation;
        private AnimationSet animations;
        private float fallSpeed;
        private bool isJumping;
        private Direction current;
        private Direction moving;
        private String action;
        private float horSpeed;
        private float horSpeedBackup;
        private Texture2D debugtexture;

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

        public override void Load()
        {
            //texture = GameManager.Instance.Content.Load<Texture2D>("Mario");
            //animation = new Animation("Mario", 2, 50f);
            animations = new AnimationSet();
            animations.AddAnimation("Left", "Player/Mario_Still_Left", 1, 100, false);
            animations.AddAnimation("Right", "Player/Mario_Still_Right", 1, 100, false);
            animations.AddAnimation("Jump_Left", "Player/Mario_Jump_Left", 1, 100, false);
            animations.AddAnimation("Jump_Right", "Player/Mario_Jump_Right", 1, 100, false);

            animations.AddAnimation("Turn_Left", "Player/Mario_Turn_Left", 1, 100, false);
            animations.AddAnimation("Turn_Right", "Player/Mario_Turn_Right", 1, 100, false);
            animations.AddAnimation("Walk_Left", "Player/Mario_Walk_Left", 2, 70, true);
            animations.AddAnimation("Walk_Right", "Player/Mario_Walk_Right", 2, 70, true);
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

        internal void Dispose()
        {
            animations.Dispose();
        }

        //handle collision
        internal void Update(GameTime _gameTime, Map _map)
        {

            SavePosition.X = Position.X;
            SavePosition.Y = Position.Y;

            Update(_gameTime);

            float newPosition;
            bool positiveChange = SavePosition.X < Position.X;
            bool collision = CollisionDetection.IsColliding(_map,CollisionBox,positiveChange,true,out newPosition);

            if (collision)
            {
                Position.X = newPosition;
                horSpeed = 0f;
            }

            debugStr = $"Hor:({collision},{horSpeed})";
            Position.Y += fallSpeed;

            positiveChange = SavePosition.Y < Position.Y;
            collision = CollisionDetection.IsColliding(_map, CollisionBox,positiveChange,false, out newPosition);

            if (!collision)
            {
                fallSpeed = Math.Min(fallSpeed + Gravity, 10f);
                if(fallSpeed > 1f)
                    isJumping = true;


            }
            else
            {
                Position.Y = newPosition;
                fallSpeed = Gravity;
                isJumping = false;
            }

            if (isJumping)
            {
                action = "Jump";
                if (Math.Abs(horSpeed) > 8f)
                    action = "RunJump";
            }

            debugStr += Environment.NewLine + $"Ver:({collision},{fallSpeed})";
            debugStr += Environment.NewLine + $"Action:{action}";
            setAnimation(_gameTime);

            //TODO: set death = true and play death animation
            if(Position.Y > _map.MapHeight)
            {
                Position.X = 64;
                Position.Y = 64;
            }

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
                    horSpeed -= .4f;
                else
                    horSpeed -= .2f;
                horSpeed = Math.Max(horSpeed, -10f);
                moving = Direction.Left;
            }
            else if (KeyboardManager.Instance.IsKeyActivity(Keys.Left.ToString(), KeyActivity.Down))
            {
                if (horSpeed > 0f)
                    horSpeed -= .3f;
                else if(horSpeed > -4f)
                    horSpeed -= .2f;
                else if (horSpeed < -4f)
                    horSpeed += .2f;
                moving = Direction.Left;
            }

            if (KeyboardManager.Instance.IsKeyActivity(Keys.Right.ToString(), KeyActivity.Down)
                  && KeyboardManager.Instance.IsKeyActivity(Keys.Z.ToString(), KeyActivity.Hold))
            {
                if (horSpeed < 0f)
                    horSpeed += .4f;
                else
                    horSpeed += .2f;
                horSpeed = Math.Min(horSpeed, 10f);
                moving = Direction.Right;
            }
            else if (KeyboardManager.Instance.IsKeyActivity(Keys.Right.ToString(), KeyActivity.Down)){
                if (horSpeed < 0f)
                    horSpeed += .3f;
                else if (horSpeed < 4f)
                    horSpeed += .2f;
                else if (horSpeed > 4f)
                    horSpeed -= .2f;
                moving = Direction.Right;
            }

            if (KeyboardManager.Instance.IsKeyActivity(Keys.R.ToString(), KeyActivity.Pressed))
            {
                Position.X = 40;
                Position.Y = 20;
                isJumping = true;
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

            if (!isJumping && KeyboardManager.Instance.IsKeyActivity(Keys.Space.ToString(), KeyActivity.Pressed))
            {
                isJumping = true;
                fallSpeed = -10f;
                //action = "Jump";
            }
            else if (isJumping && fallSpeed < 0 && KeyboardManager.Instance.IsKeyActivity(Keys.Space.ToString(), KeyActivity.Hold))
            {
                fallSpeed -= .6f;
                //action = "Jump";
            }
            
        }
        
        private void setAnimation(GameTime _gameTime)
        {

            String mainDirection = current.ToString();

            if (moving == Direction.None && !isJumping)
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

            
            currentAnimation.Draw(_spriteBatch,CollisionBox);

            _spriteBatch.Draw(debugtexture, CollisionBox, Color.White * Constants.DEBUG_OPACITY);
            _spriteBatch.DrawString(GameManager.Instance.DebugFont, debugStr, Position - new Vector2(100, 20), Color.Turquoise);

        }


    }
}
