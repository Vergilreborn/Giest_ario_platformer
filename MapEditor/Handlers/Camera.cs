using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MapEditor.Handlers
{
    class Camera
    {

        public Viewport view;
        //public Vector2 savedPosition;
        public Vector2 focusPoint;
        //public float positionShakeAmount;
        //public float maxShakeTime;
        public float zoom;
        //public Matrix transform;
        public Vector2 source; 
        //TimeSpan shaketimer;
        Random random;

        public Camera(Viewport view)
        {
            this.view = view;
            this.zoom = 2;
            random = new Random();
            focusPoint = new Vector2(view.Width / 2, view.Height / 2);

        }

        public void Update(GameTime gametime, Vector2 source)
        {
            //if (shaketimer.TotalSeconds > 0)
            //{
            //    focusPoint = savedPosition;
              
            //    shaketimer = shaketimer.Subtract(gametime.ElapsedGameTime);
            //    if (shaketimer.TotalSeconds > 0)
            //    {
            //        focusPoint += new Vector2((float)((random.NextDouble() * 2) - 1) * positionShakeAmount,
            //            (float)((random.NextDouble() * 2) - 1) * positionShakeAmount);
            //        ;
            //    }
            //}

            this.source = source;
            
        }

        public Matrix GetTransform(Matrix Scale)
        {
            if (source == Vector2.Zero)
                this.source = new Vector2(focusPoint.X/Scale.M11, focusPoint.Y/Scale.M22);
            return (Matrix.CreateTranslation(new Vector3(-source, 0)) *
                     (Matrix.CreateTranslation(new Vector3(focusPoint.X/ Scale.M11, focusPoint.Y/Scale.M22, 0)))) * Scale;
        }
    }
}