using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


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
        public Vector2 source;
        TimeSpan shaketimer;
        Random random;

        public Camera(Viewport view, Vector2 position)
        {
            this.view = view;
            this.position = position;
            this.zoom = 2;
            random = new Random();
            focusPoint = new Vector2(view.Width / 2, view.Height / 2);

        }

        public void Update(GameTime gametime, Vector2 source)
        {
            if (shaketimer.TotalSeconds > 0)
            {
                focusPoint = savedPosition;
              
                shaketimer = shaketimer.Subtract(gametime.ElapsedGameTime);
                if (shaketimer.TotalSeconds > 0)
                {
                    focusPoint += new Vector2((float)((random.NextDouble() * 2) - 1) * positionShakeAmount,
                        (float)((random.NextDouble() * 2) - 1) * positionShakeAmount);
                    ;
                }
            }

            this.source = source;

            //transform = Matrix.CreateTranslation(new Vector3(-objectPosition, 0)) *
            //         Matrix.CreateTranslation(new Vector3(focusPoint.X/xScale, focusPoint.Y/yScale, 0));
            

        }

        public Matrix GetTransform(Matrix Scale)
        {
            focusPoint.X = view.Width / Scale.M11;
            focusPoint.Y = view.Height / Scale.M22;

            return (Matrix.CreateTranslation(new Vector3(-source, 0)) *
                     (Matrix.CreateTranslation(new Vector3(focusPoint.X/ Scale.M11, focusPoint.Y/Scale.M22, 0)))) * Scale;
        }

        public void Shake(float shakeTime, float positionAmount)
        {
            //We only want to perform one shake.  If one is going on currently, we have to
            //wait for that shake to be over before we can do another one.
            if (shaketimer.TotalSeconds <= 0)
            {
                maxShakeTime = shakeTime;
                shaketimer = TimeSpan.FromSeconds(maxShakeTime);
                positionShakeAmount = positionAmount;
                savedPosition = focusPoint;

            }
        }
        public void Follow(Vector2 center)
        {
            this.source = center;

        }
    }
}