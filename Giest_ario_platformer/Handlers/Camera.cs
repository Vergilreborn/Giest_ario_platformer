using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Giest_ario_platformer.Managers;

namespace Giest_ario_platformer.Handlers
{
    class Camera
    {

        public Viewport view;
        public Vector2 position;
        public Vector2 savedPosition;
        public Vector2 focusPoint;
        public float positionShakeAmount;
        public float maxShakeTime;
        public float zoom;
        public Rectangle source;
        TimeSpan shaketimer;
        Random random;
        Texture2D texture;
        private Matrix transformation;
        private Matrix scale;
        private bool isFollowing = false;
        private Rectangle mapBoundary;

        private Rectangle boundingBox;

        public Camera(Viewport _view, Vector2 _position)
        {
            this.view = _view;
            this.position = _position;
            this.zoom = 2;
            random = new Random();
            focusPoint = new Vector2(_view.Width / 2, _view.Height / 2);
            boundingBox = new Rectangle(0, 0, _view.Width / 6, _view.Height / 8);

        }

        public void Load()
        {
            texture = GameManager.Instance.CreateColorTexture(255,255,255 , 255);
        }

        public void SetScale(Matrix _Scale)
        {
            scale = _Scale;
        }

        public void SetObjectCenter()
        {
            isFollowing = false;
            source = new Rectangle(0,0,(int)(view.Width/ scale.M22),(int)( view.Height/ scale.M11));
        }
        
        public void SetMapBoundary(Rectangle _mapBoundary)
        {
            this.mapBoundary = _mapBoundary;
        }

        public Matrix GetTransform(bool hasMap = false)
        {
            if (isFollowing)
            {
                if(source.Left < boundingBox.Left)
                {
                    boundingBox.X = source.X;
                }
                if (source.Right > boundingBox.Right)
                {
                    boundingBox.X =source.Right - boundingBox.Width;
                }
                if (source.Top < boundingBox.Top)
                {
                    boundingBox.Y = source.Y;
                }
                if (source.Bottom> boundingBox.Bottom)
                {
                    boundingBox.Y =  source.Bottom - boundingBox.Height;
                }

                transformation = (Matrix.CreateTranslation(new Vector3(-boundingBox.Center.ToVector2(), 0)) *
                    (Matrix.CreateTranslation(new Vector3(focusPoint.X / scale.M11, focusPoint.Y / scale.M22, 0)))) * scale;

                if (mapBoundary != Rectangle.Empty)
                {
                    if (-transformation.M41/scale.M11 < mapBoundary.Left)
                    {
                        transformation.M41 = -(mapBoundary.X/  scale.M11);
                    }
                    if (((-transformation.M41 + view.Width) / scale.M11) > mapBoundary.Right)
                    {
                        transformation.M41 = -((mapBoundary.Right * scale.M22) - view.Width );
                    }
                    if (-transformation.M42 / scale.M22 < mapBoundary.Top)
                    {
                        transformation.M42 = -(mapBoundary.Y) / scale.M22;
                    }
                    if (((-transformation.M42 + view.Height)/scale.M22) > mapBoundary.Bottom)
                    {
                        transformation.M42 = -((mapBoundary.Bottom * scale.M22) - view.Height) ;
                    }
                }


             
            }
            else
            {
                transformation = (Matrix.CreateTranslation(new Vector3(-source.Center.ToVector2(), 0)) *
                       (Matrix.CreateTranslation(new Vector3(focusPoint.X / scale.M11, focusPoint.Y / scale.M22, 0)))) * scale;

            }
            return transformation;
        }

        //public Matrix GetTransform()
        //{
            
        //    transformation =  (Matrix.CreateTranslation(new Vector3(-source.Center.ToVector2(), 0)) *
        //                 (Matrix.CreateTranslation(new Vector3(focusPoint.X / scale.M11, focusPoint.Y / scale.M22, 0)))) * scale;

        //    return transformation;
        //}

        public void Shake(float _shakeTime, float _positionAmount)
        {
            //We only want to perform one shake.  If one is going on currently, we have to
            //wait for that shake to be over before we can do another one.
            if (shaketimer.TotalSeconds <= 0)
            {
                maxShakeTime = _shakeTime;
                shaketimer = TimeSpan.FromSeconds(maxShakeTime);
                positionShakeAmount = _positionAmount;
                savedPosition = focusPoint;

            }
        }
        public void Follow(Rectangle _center)
        {
            isFollowing = true;
            this.source = _center;

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            if(isFollowing)
                _spriteBatch.Draw(texture, boundingBox, Color.White * .20f);

            if (isFollowing)
                _spriteBatch.Draw(texture, new Rectangle((int)(-transformation.M41/scale.M11),(int)(-transformation.M42/scale.M22),(int)(view.Width/scale.M11),(int)(view.Height/scale.M22)), Color.Red* .20f);

        }
    }
}