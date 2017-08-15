using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Giest_ario_platformer.Managers
{
    class MusicManager
    {

        public static MusicManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MusicManager();
                return instance;
            }
        }

        private static MusicManager instance;

        private Song songPlaying;
        private String currentSong;

        public MusicManager()
        {
            
        }

        public void PlaySong(string _song)
        {
            if (currentSong != _song)
            {
                if (songPlaying != null)
                {
                    MediaPlayer.Stop();
                }

                Song song = GameManager.Instance.Content.Load<Song>($@"Music\{_song}");
                currentSong = _song;
                MediaPlayer.Play(song);
                MediaPlayer.IsRepeating = true;
                songPlaying = song;
            }
        }
        

    }
}
