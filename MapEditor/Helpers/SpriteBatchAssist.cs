using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapEditor.Helpers
{
    class SpriteBatchAssist
    {
        public static void DrawBox(SpriteBatch _spriteBatch, Texture2D _t, Rectangle _r, float opacity = 1f)
        {
            _spriteBatch.Draw(_t, new Rectangle(_r.Left, _r.Top, 1, _r.Height), Color.White * opacity); // Left
            _spriteBatch.Draw(_t, new Rectangle(_r.Right- 1, _r.Top, 1, _r.Height), Color.White * opacity); // Right
            _spriteBatch.Draw(_t, new Rectangle(_r.Left, _r.Top, _r.Width, 1), Color.White * opacity); // Top
            _spriteBatch.Draw(_t, new Rectangle(_r.Left, _r.Bottom-1, _r.Width, 1), Color.White * opacity); // Bottom
        }

    }
}
