using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giest_ario_platformer.Managers
{
    class SoundManager
    {

        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SoundManager();
                return instance;
            }
        }

        private static SoundManager instance;
            
        public SoundManager()
        {
            
        }

        public void PlaySound(string _sound)
        {
            SoundEffect soundEffect = GameManager.Instance.Content.Load<SoundEffect>($@"Sound\{_sound}");
            soundEffect.Play();
            //soundEffect.Dispose();
        }

    }
}
