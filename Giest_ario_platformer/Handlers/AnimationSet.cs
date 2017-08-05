using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giest_ario_platformer.Handlers
{
    class AnimationSet
    {
        private Dictionary<String, Animation> animations;

        public AnimationSet()
        {
            animations = new Dictionary<string, Animation>();
        }

        public void AddAnimation(String _animationName,String _filePath, int _maxFrames, float _changeAnimation = 100f, bool isLoop = true)
        {
            animations.Add(_animationName, new Animation(_filePath, _maxFrames, _changeAnimation, isLoop));
        }

        public Animation GetAnimation(String _animationName)
        {
            return animations[_animationName];
        }
    }
}
